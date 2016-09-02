using UnityEngine;

namespace Ruihanyang.Game
{
    public class Player : MonoBehaviour
    {
        [HideInInspector]
        public PlayerController controller;

        // 玩家得分
        public int score = 0;

        // 玩家走过的路程
        public float traveledTime
        {
            get
            {
                return controller.GetTraveledTime();
            }
        }

        // 玩家走过的方块数
        public int traveledTileCount = 0;

        // 玩家速度增长
        public float playerSpeedIncValue = 0.005f;

        #region 回调函数

        void Awake()
        {
            controller = GetComponent<PlayerController>();

            CameraSmoothFollow.target = this.transform;
        }

        #endregion

        #region 自定义公共函数

        public void Init(Vector3 _dir)
        {
            controller.Init(_dir);

            Init();
        }

        public void AddScore(int _score)
        {
            score += _score;
        }

        #endregion

        #region 自定义私有函数

        void Init()
        {
            score = 0;
            traveledTileCount = 0;
        }

        #endregion
    }
}
