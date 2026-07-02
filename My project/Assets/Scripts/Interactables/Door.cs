using UnityEngine;

namespace Carp {
    public class Door : Openable
    {
        public override void Unlock() {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;

            // TODO: do some visual something to show it's unlocked
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
            sr.color = newColor;
            
            isLocked = false;
        }
    }
}
