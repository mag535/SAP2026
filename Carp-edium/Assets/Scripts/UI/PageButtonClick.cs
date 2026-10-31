using UnityEngine;
using UnityEngine.UI;

namespace Carp {
    public class PageButtonClick : MonoBehaviour
    {
        void Start() {
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(PlayPageFlip);
        }

        void PlayPageFlip() {
            EvtSystem.EventDispatcher.Raise<PlayPredeterminedSound>( new
                    PlayPredeterminedSound { preSound = PredeterminedSounds.PAGEFLIP });
        }
    }
}
