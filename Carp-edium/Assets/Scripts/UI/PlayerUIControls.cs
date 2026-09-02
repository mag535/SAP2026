using UnityEngine;
using UnityEngine.UI;

namespace Carp {
    public class PlayerUIControls : MonoBehaviour
    {
        [SerializeField]
        private GameObject inventoryButton;
        [SerializeField]
        private Sprite inventoryDefaultIcon;
        [SerializeField]
        private Sprite inventoryOpenIcon;


        [SerializeField]
        private GameObject notebookButton;
        [SerializeField]
        private Sprite notebookDefaultIcon;
        [SerializeField]
        private Sprite notebookOpenIcon;

        private void Awake() {
            EvtSystem.EventDispatcher.AddListener<RequestCloseInventory>(HandleCloseInventory);
            EvtSystem.EventDispatcher.AddListener<RequestCloseNotebook>(HandleCloseNotebook);
        }

        public void SendOpenInventoryEvent() {
            EvtSystem.EventDispatcher.Raise<RequestOpenInventory>(new
                    RequestOpenInventory {});

            Image im = inventoryButton.GetComponent<Image>();
            im.sprite = inventoryOpenIcon;
        }

        public void SendOpenNotebookEvent() {
            EvtSystem.EventDispatcher.Raise<RequestOpenNotebookPre>(new
                    RequestOpenNotebookPre {});

            Image im = notebookButton.GetComponent<Image>();
            im.sprite = notebookOpenIcon;
        }

        private void HandleCloseInventory(RequestCloseInventory _) {
            Image im = inventoryButton.GetComponent<Image>();
            im.sprite = inventoryDefaultIcon;
        }

        private void HandleCloseNotebook(RequestCloseNotebook _) {
            Image im = notebookButton.GetComponent<Image>();
            im.sprite = notebookDefaultIcon;
        }

        private void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<RequestCloseInventory>(HandleCloseInventory);
            EvtSystem.EventDispatcher.RemoveListener<RequestCloseNotebook>(HandleCloseNotebook);
        }
    }
}
