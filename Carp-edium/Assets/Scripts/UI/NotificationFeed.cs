using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationFeed : MonoBehaviour
{
    public GameObject notificationPrefab;

    void Awake() {
        EvtSystem.EventDispatcher.AddListener<RequestCreateNotification>(
                CreateNotification);
    }

    void CreateNotification(RequestCreateNotification evt) {
        string notificationText = $"{evt.objectName} has been added to ";
        if (evt.isNoteEntry) {
            notificationText += "Notebook";
        } else {
            notificationText += "Inventory";
        }

        GameObject notification = Instantiate(notificationPrefab,
                gameObject.transform);
        foreach (Transform childTransform in notification.transform) {
            TextMeshProUGUI tmpText = childTransform.GetComponent<TextMeshProUGUI>();
            if (tmpText != null) {
                tmpText.text = notificationText;
            }
        }
    }

    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<RequestCreateNotification>(
                CreateNotification);
    }
}
