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

        // Add button to create new node
        // TODO: add dropdown of types of new nodes to create
        var nodeCreateButton = new Button(() => 
        {
            _graphView.CreateNode("Dialogue Node");
        });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);

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

    private void OnDisable() {
        rootVisualElement.Remove(_graphView);
    }
}
