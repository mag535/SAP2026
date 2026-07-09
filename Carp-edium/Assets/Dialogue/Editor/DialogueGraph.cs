using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView _graphView;
    private string _fileName = "New Conversation";

    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphViewWindow() {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void OnEnable() {
        ConstructGraphView();
        GenerateToolbar();
    }

    private void ConstructGraphView() {
        _graphView = new DialogueGraphView {
            name = "Dialogue Graph"
        };
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar() {
        var toolbar = new Toolbar();

        // Adding ability to see and change filename of Conversation graph
        var fileNameTextField = new TextField("File Name");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        // Adds buttons to save and load data
        toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });
        toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });

        // Add button to clear graph
        toolbar.Add(new Button(() => ClearGraph()) { text = "Clear Graph" });

        // Add dropdown menu for different dialogue node types
        var toolbarMenu = new ToolbarMenu() { text = "Create Dialogue" };
        toolbarMenu.menu.AppendAction("Simple", (a) => 
                { _graphView.CreateNode(DialogueType.SIMPLE, "Simple Dialogue Node"); });
        toolbarMenu.menu.AppendAction("Branch", (a) => 
                { _graphView.CreateNode(DialogueType.BRANCH, "Branch Dialogue Node"); });
        toolbarMenu.menu.AppendAction("Give-Item", (a) => 
                { _graphView.CreateNode(DialogueType.GIVEITEM, "Give-Item Dialogue Node"); });
        toolbarMenu.menu.AppendAction("Set-Flag", (a) => 
                { _graphView.CreateNode(DialogueType.SETFLAG, "Set-Flag Dialogue Node"); });
        toolbarMenu.menu.AppendAction("END_OF_CONVERSATION", (a) => 
                { _graphView.CreateNode(DialogueType.ENDOFCONVERSATION, 
                        "END OF CONVERSATION"); });
        toolbar.Add(toolbarMenu);

        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save) {
        if (string.IsNullOrEmpty(_fileName)) {
            EditorUtility.DisplayDialog(
                    "Invalid file name!", 
                    "Please enter a valid file name", 
                    "Okay");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(_graphView);
        if (save) {
            saveUtility.SaveGraph(_fileName);
        } else {
            saveUtility.LoadGraph(_fileName);
        }
    }

    private void ClearGraph() {
        Debug.Log("Not Implemented");
        /*
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGuid;
        foreach (var node in Nodes) {
            if (node.EntryPoint) continue;
            // Remove edges that connected to this node
            Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => _targetGraphView.RemoveElement(edge));

            // Remove the node
            _targetGraphView.RemoveElement(node);
        }
        */
    }

    private void OnDisable() {
        rootVisualElement.Remove(_graphView);
    }
}
