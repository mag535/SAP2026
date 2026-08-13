using UnityEngine;

namespace Carp {
    public class CheckForUnlockedGates : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (GameManager.Instance.GetAreGatesUnlocked()) {
                foreach (Transform childtransform in gameObject.transform) {
                    childtransform.gameObject.SetActive(true);
                }
            }
        }
    }
}
