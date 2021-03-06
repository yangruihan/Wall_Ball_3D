﻿using UnityEngine;
using System.Collections;

namespace Ruihanyang.Game
{
    public class PlayerMotor : MonoBehaviour
    {
        static public Vector3 DIRECTION_LEFT = new Vector3(1f, 0f, 0f);
        static public Vector3 DIRECTION_RIGHT = new Vector3(0f, 0f, 1f);

        #region 身上组件

        private Player player;
        private Rigidbody rigid;

        #endregion

        public float traveledTime = 0f;

        [SerializeField]
        private float initSpeed = 2f;

        private float currentSpeed = 0f;

        private Vector3 direction = Vector3.zero;

        #region 回调函数

        void Awake()
        {
            player = GetComponent<Player>();
            rigid = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            currentSpeed += Time.fixedDeltaTime * player.playerSpeedIncValue;

            rigid.MovePosition(transform.position + direction * currentSpeed * Time.fixedDeltaTime);

            traveledTime += Time.fixedDeltaTime;
        }

        #endregion

        #region 自定义公共函数

        public void Init(Vector3 _dir)
        {
            direction = _dir;

            currentSpeed = initSpeed;

            traveledTime = 0f;
        }

        public void ChangeDirection(Vector3 _dir)
        {
            if (_dir == DIRECTION_LEFT)
            {
                direction = DIRECTION_RIGHT;
            }
            else if (_dir == DIRECTION_RIGHT)
            {
                direction = DIRECTION_LEFT;
            }
        }

        public void Jump()
        {
            rigid.AddForce(new Vector3(0f, 1400f, 0f));
        }

        public void AddSpeed(float _value)
        {
            currentSpeed += _value;
        }

        public Vector3 GetDirection()
        {
            return direction;
        }

        public float GetSpeed()
        {
            return currentSpeed;
        }

        #endregion
    }
}
