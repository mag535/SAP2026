using UnityEngine;

namespace Carp {
    public class PlayerAnimator : MonoBehaviour
    {
        public Animator anim;
        public bool isMoving;

        private PlayerMovement pm;

        void Awake() {
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            pm = GetComponent<PlayerMovement>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            Animate();
        }

        private void Animate()
        {
            Vector2 input = pm.GetInputVector();
            input.Normalize();
            isMoving=pm.GetIsMoving();
            if (isMoving)
            {
                anim.SetFloat("x", input.x);
                anim.SetFloat("y", input.y);
            }

            anim.SetBool("Moving", isMoving);
        }
    }
}
