using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

public class GraphSaveUtility
{
    private string savedGraphsFolder = "Conversations";

    private string _loadedGraphPath = "";
    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView) {
        return new GraphSaveUtility {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName) {
        if (!Edges.Any()) return; // if no connections, don't save

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (var i = 0; i < connectedPorts.Length; i++) {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;
            dialogueContainer.NodeLinks.Add(new NodeLinkData {
                    BaseNodeGuid = outputNode.GUID,
                    PortName = connectedPorts[i].output.portName,
                    TargetNodeGuid = inputNode.GUID
            });
        }

        foreach (var dialogueNode in Nodes.Where(node => !node.EntryPoint)) {
            dialogueContainer.DialogueNodeData.Add(new DialogueNodeData {
                    type = dialogueNode.type,
                    Guid = dialogueNode.GUID,
                    speaker = dialogueNode.speaker,
                    DialogueText = dialogueNode.DialogueText,
                    // TODO: cost
                    cost = dialogueNode.cost,
                    trade = dialogueNode.trade,
                    flag = dialogueNode.flag,

                    Position = dialogueNode.GetPosition().position,
            });
        }

        // TODO: use cached loadedGraphPath to save graph in same location
        //
        // Is "Resources" folder doesn't exist, create it
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");
        // If "Conversations" folder doesn't exist inside "Assets/Resources", create it
        if (!AssetDatabase.IsValidFolder("Assets/Resources/Conversations"))
            AssetDatabase.CreateFolder("Assets/Resources", "Conversations");

        // If filename already exists, save in same path
        string[] fileNames = Directory.GetFiles("Assets/Resources/Conversations", 
                "*", SearchOption.AllDirectories);
        string existingFilePath = "";
        foreach (string s in fileNames) {
            if (s.Contains(fileName)) { 
                existingFilePath = s;
                break;
            }
        }
        if (!string.IsNullOrEmpty(existingFilePath)) {
            AssetDatabase.CreateAsset(dialogueContainer, existingFilePath);
        // Otherwise, save in top directory `Resources/Conversations/`
        } else {
            AssetDatabase.CreateAsset(dialogueContainer, 
                    $"Assets/Resources/Conversations/{fileName}.asset");
        }
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName) {
        // Use `Assets/Resources/Conversations/"
        string resourcesPath = Application.dataPath + "/Resources";
        string[] subDirectories = Directory.GetDirectories(resourcesPath + "/" +
                savedGraphsFolder, "*", SearchOption.AllDirectories);

        // Search top directory: `Resources/Conversations/`
        _containerCache = Resources.Load<DialogueContainer>(savedGraphsFolder + 
                "/" + fileName);
        // Search sub-directories if top directory search fails
        if (_containerCache == null) {
            foreach (string directory in subDirectories) {
                string subDir = directory.Substring(resourcesPath.Length+1);
                _containerCache = Resources.Load<DialogueContainer>(
                        subDir + "/" + fileName);
                if (_containerCache != null) {
                    _loadedGraphPath = subDir;
                    break;
                }
            }
        }

        if (_containerCache == null) {
            EditorUtility.DisplayDialog(
                    "File Not Found",
                    "Target dialogue graph file does not exist.",
                    "Okay");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ConnectNodes() {
        for (int i = 0; i < Nodes.Count; i++) {
            var connections = _containerCache.NodeLinks.Where(x => 
                    x.BaseNodeGuid == Nodes[i].GUID).ToList();
            for (var j = 0; j < connections.Count; j++) {
                var targetNodeGuid = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                LinkNodes((Port) Nodes[i].outputContainer[j], (Port) targetNode.inputContainer[0]);
                targetNode.SetPosition(new Rect(
                            _containerCache.DialogueNodeData.First(x => x.Guid == targetNodeGuid).Position,
                            _targetGraphView.defaultNodeSize));
            }
        }
    }

    private void LinkNodes(Port output, Port input) {
        var tempEdge = new Edge {
            output = output,
            input = input
        };

        tempEdge?.input.Connect(tempEdge);
        tempEdge?.output.Connect(tempEdge);

        _targetGraphView.Add(tempEdge);
    }

    private void CreateNodes() {
        foreach (var nodeData in _containerCache.DialogueNodeData) {
            DialogueNode tempNode = null;

            switch (nodeData.type) {
            case DialogueType.SIMPLE:
                tempNode = _targetGraphView.CreateSimpleDialogueNode(
                        nodeData.DialogueText, nodeData.speaker);
                break;
            case DialogueType.BRANCH:
                tempNode = _targetGraphView.CreateBranchDialogueNode(
                        nodeData.DialogueText, nodeData.speaker);
                var nodePorts = _containerCache.NodeLinks.Where(x => 
                        x.BaseNodeGuid == nodeData.Guid).ToList();
                nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, 
                            x.PortName));
                break;
            case DialogueType.GIVEITEM:
                tempNode = _targetGraphView.CreateGiveItemDialogueNode(
                        nodeData.DialogueText,
                        nodeData.speaker,
                        (Object) nodeData.cost,
                        (Object) nodeData.trade);
                tempNode.cost = (Object) nodeData.cost;
                tempNode.trade = (Object) nodeData.trade;
                break;
            case DialogueType.SETFLAG:
                tempNode = _targetGraphView.CreateSetFlagDialogueNode(
                        nodeData.DialogueText,
                        nodeData.speaker,
                        (string) nodeData.flag);
                tempNode.flag = (string) nodeData.flag;
                break;
            case DialogueType.ENDOFCONVERSATION:
                tempNode = _targetGraphView.CreateEndOfConversationDialogueNode(
                        nodeData.DialogueText);
                break;
            }

            tempNode.speaker = nodeData.speaker;
            tempNode.GUID = nodeData.Guid;
            _targetGraphView.AddElement(tempNode);

            tempNode.RefreshExpandedState();
            tempNode.RefreshPorts();
        }
    }

    private void ClearGraph() {
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGuid;
        foreach (var node in Nodes) {
            if (node.EntryPoint) continue;
            // Remove edges that connected to this node
            Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => _targetGraphView.RemoveElement(edge));

            // Remove the node
            _targetGraphView.RemoveElement(node);
        }
    }
}
