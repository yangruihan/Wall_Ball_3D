using UnityEngine;

namespace Ruihanyang.Game
{
    public class Tile : MonoBehaviour
    {
        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void OnCollisionExit(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                animator.SetBool("isDisapear", true);
            }
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}