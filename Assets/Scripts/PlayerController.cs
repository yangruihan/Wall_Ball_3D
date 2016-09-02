using UnityEngine;
using System.Collections;

namespace Ruihanyang.Game
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private KeyCode changeDirectionKey = KeyCode.J;

        [SerializeField]
        private KeyCode JumpKey = KeyCode.K;

        #region 身上组件

        private PlayerMotor motor;

        #endregion

        private bool isJump = false;

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

            if (Input.GetKeyDown(JumpKey) && !isJump)
            {
                isJump = true;
                motor.Jump();
            }
        }

        void OnCollisionEnter(Collision other)
        {
            isJump = false;
        }

        #endregion

        #region 自定义公共函数

        public void Init(Vector3 _dir)
        {
            motor.Init(_dir);

            isJump = false;
        }

        public void AddSpeed(float _value)
        {
            motor.AddSpeed(_value);
        }

        public float GetSpeed()
        {
            return motor.GetSpeed();
        }

        public float GetTraveledTime()
        {
            return motor.traveledTime;
        }

        #endregion
    }
}
