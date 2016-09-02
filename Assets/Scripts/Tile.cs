using UnityEngine;

namespace Ruihanyang.Game
{
    public class Tile : MonoBehaviour
    {
        private Animator animator;

        #region 回调函数

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        #endregion

        #region 自定义公共函数

        public void Disapear()
        {
            PlayDisapearAnimation();
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        #endregion

        #region 自定义私有函数

        private void PlayDisapearAnimation()
        {
            animator.SetBool("isDisapear", true);
        }

        #endregion
    }
}