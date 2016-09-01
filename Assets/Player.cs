using UnityEngine;

namespace Ruihanyang.Game
{
    public class Player : MonoBehaviour
    {
        [HideInInspector]
        public PlayerController controller;

        // 玩家得分
        public int score = 0;

        #region 回调函数

        void Awake()
        {
            controller = GetComponent<PlayerController>();
        }

        #endregion

        #region 自定义公共函数

        public void Init(Vector3 _dir)
        {
            controller.Init(_dir);
        }

        public void AddScore(int _score)
        {
            score += _score;
        }

        #endregion

    }
}
