using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Carp {
    public class PlayerMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject parentOfInventoryFocus;
        [SerializeField]
        private GameObject parentOfNotebookFocus;

        private PlayerInput playerInput;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        public void TogglePauseMenu(InputAction.CallbackContext context) {
            if (context.canceled) {
                EvtSystem.EventDispatcher.Raise<TogglePauseMenu>( new TogglePauseMenu {});
            }
        }

        public void OpenInventory(InputAction.CallbackContext context) {
            if (context.canceled) {
                EvtSystem.EventDispatcher.Raise<RequestOpenInventory>( new
                    RequestOpenInventory { });
                if (playerInput != null) {
                    playerInput.SwitchCurrentActionMap("UI");
                }
            }
        }
        public void CloseInventory(InputAction.CallbackContext context) {
            if (context.canceled) {
                EvtSystem.EventDispatcher.Raise<RequestCloseInventory>( new
                    RequestCloseInventory { });
                if (playerInput != null) {
                    playerInput.SwitchCurrentActionMap("Game");
                }
            }
        }

        public void OpenNotebook(InputAction.CallbackContext context) {
            if (context.canceled) {
                EvtSystem.EventDispatcher.Raise<RequestOpenNotebookPre>( new
                    RequestOpenNotebookPre { });
                if (playerInput != null) {
                    playerInput.SwitchCurrentActionMap("UI");
                }
            }
        }
        public void CloseNotebook(InputAction.CallbackContext context) {
            if (context.canceled) {
                EventSystem.current.SetSelectedGameObject(null);
                EvtSystem.EventDispatcher.Raise<RequestCloseNotebook>( new
                    RequestCloseNotebook { });
                if (playerInput != null) {
                    playerInput.SwitchCurrentActionMap("Game");
                }
            }
        }

        public void PageBackwards(InputAction.CallbackContext context) {
            if (context.canceled) {
                EvtSystem.EventDispatcher.Raise<RequestPreviousPage>( new
                        RequestPreviousPage {});
            }
        }
        public void PageForward(InputAction.CallbackContext context) {
            if (context.canceled) {
                EvtSystem.EventDispatcher.Raise<RequestNextPage>( new
                        RequestNextPage {});
            }
        }

        public void SwitchToInventory(InputAction.CallbackContext context) {
            if (context.canceled) {
                // Set focused game object
                foreach (Transform childTransform in parentOfInventoryFocus.transform) {
                    EventSystem.current.SetSelectedGameObject(childTransform.gameObject);
                    break;
                }
            }
        }
        public void SwitchToNotebook(InputAction.CallbackContext context) {
            if (context.canceled) {
                // Set focused game object
                foreach (Transform childTransform in parentOfNotebookFocus.transform) {
                    EventSystem.current.SetSelectedGameObject(childTransform.gameObject);
                    break;
                }
            }
        }
    }
}
