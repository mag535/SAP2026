using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

public class DialogueGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2(150, 200);

    public DialogueGraphView() {
        // ability to zoom in/out
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        // ability to drag
        this.AddManipulator(new ContentDragger());
        // ability to select
        this.AddManipulator(new SelectionDragger());
        // ability to do rectagular selection
        this.AddManipulator(new RectangleSelector());

        // add grid guide on background
        // STYLESHEET REQUIRED OR GRID LINES AND BACKGROUND WILL BE SAME COLOR
        /*
        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
        */

        AddElement(GenerateEntryPointNode());
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
        var compatiblePorts = new List<Port>();

        ports.ForEach(port =>
        {
            if (startPort != port // port is not connecting to itself
                    && startPort.node != port.node // port is not connect to other ports on same node
                    && startPort.direction != port.direction) { // input ports can't connect to input ports, same for output ports
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }

    private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single) {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float)); // arbitrary type
    }

    private DialogueNode GenerateEntryPointNode() {
        var node = new DialogueNode {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "ENTRYPOINT",
            EntryPoint = true
        };
        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void CreateNode(string nodeName) {
        AddElement(CreateDialogueNode(nodeName));
    }

    public DialogueNode CreateDialogueNode(string nodeName) {
        var dialogueNode = new DialogueNode {
            title = nodeName,
            DialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        // Input port
        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

        // Button to create new Choices (output ports)
        var button = new Button(() =>
        {
            AddChoicePort(dialogueNode);
        });
        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);

        // Field to add the actual dialogue
        var textField = new TextField(string.Empty);
        textField.RegisterValueChangedCallback(evt => {
            dialogueNode.DialogueText = evt.newValue;
            dialogueNode.title = evt.newValue;
        });
        textField.SetValueWithoutNotify(dialogueNode.title);
        dialogueNode.mainContainer.Add(textField);

        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return dialogueNode;
    }

    public void AddChoicePort(DialogueNode dialogueNode, string overridenPortName = "") {
        var generatedPort = GeneratePort(dialogueNode, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;

        var choicePortName = string.IsNullOrEmpty(overridenPortName)
                ? $"Choice {outputPortCount + 1}"
                : overridenPortName;

        var textField = new TextField {
            name = string.Empty,
            value = choicePortName
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);
        var deleteButton = new Button(() => RemovePort(dialogueNode, generatedPort)) {
            text = "X"
        };
        generatedPort.contentContainer.Add(deleteButton);
        
        generatedPort.portName = $"Choice {outputPortCount}";
        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
    }

    private void RemovePort(DialogueNode dialogueNode, Port generatedPort) {
        var targetEdge = edges.ToList().Where(x => 
                x.output.portName == generatedPort.portName && 
                x.output.node == generatedPort.node);

        if (targetEdge.Any()) {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }

        dialogueNode.outputContainer.Remove(generatedPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }
}
