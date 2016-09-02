using UnityEngine;
using System.Collections;

namespace Ruihanyang.Game
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private KeyCode changeDirectionKey = KeyCode.Space;

        #region 身上组件

        private PlayerMotor motor;

        #endregion

        #region 回调函数

        void Awake()
        {
            motor = GetComponent<PlayerMotor>();
        }

        void Update()
        {
            if (Input.GetKeyDown(changeDirectionKey))
            {
                motor.ChangeDirection(motor.GetDirection());
            }
        }

        #endregion

        #region 自定义公共函数

        public void Init(Vector3 _dir)
        {
            motor.InitDirection(_dir);
        }

        public void AddSpeed(float _value)
        {
            motor.AddSpeed(_value);
        }

        public float GetSpeed()
        {
            return motor.GetSpeed();
        }

        public float GetTraveledDistance()
        {
            return motor.traveledDistance;
        }

        #endregion
    }
}
