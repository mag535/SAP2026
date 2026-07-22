using UnityEngine;

namespace Carp {
    public class PlayerAnimator : MonoBehaviour
    {
        // 0 == W
        // 1 == D
        // 2 == S
        // 3 == A
        public Sprite[] spriteArray;

        private SpriteRenderer sr;

        void Awake() {
            EvtSystem.EventDispatcher.AddListener<ChangePlayerSprite>(HandlePlayerSprite);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = spriteArray[2];
        }

        void HandlePlayerSprite(ChangePlayerSprite evt) {
            Vector2 up = new Vector2(0,1);
            Vector2 right = new Vector2(1,0);
            Vector2 down = new Vector2(0,-1);
            Vector2 left = new Vector2(-1,0);

            if (evt.direction == up) {
                sr.sprite = spriteArray[0];
            } else if (evt.direction == right) {
                sr.sprite = spriteArray[1];
            } else if (evt.direction == down) {
                sr.sprite = spriteArray[2];
            } else if (evt.direction == left) {
                sr.sprite = spriteArray[3];
            }
        }

        void OnDestroy() {
            EvtSystem.EventDispatcher.RemoveListener<ChangePlayerSprite>(HandlePlayerSprite);
        }
    }
}
