using System;
using UnityEngine;

namespace Play
{
    public class PlayerController : MonoBehaviour 
    {
        private float _y;
        private float _gravity;     // 중력 느낌용

        private enum Direction
        {
            Stop,    // 0: 정지 상태
            Jump,    // 1: 점프 중
            Down     // 2: 다운 중
        };        

        private Direction _direction;
    
        // 설정값
        private const float JumpSpeed = 0.102f;      // 점프 속도
        private const float JumpAccell = 0.01f;    // 점프 가속
        private float _yBase;                      // 캐릭터가 서있는 기준점

        private void Start()
        {
            _yBase = GetComponent<Transform>().position.y;
            _y = _yBase;
        }

        private void Update()
        {
            JumpProcess();
        
            if (Input.GetKeyDown(KeyCode.Space))
                DoJump();
        
            // y값을 gameObject에 적용
            var pos = transform.position;
        
            pos.y = _y;
            transform.position = pos;
        }

        public void DoJump() // 점프 키 누를때 1회만 호출
        {
            _direction = Direction.Jump;
            _gravity = JumpSpeed;
        }

        private void JumpProcess()
        {
            switch (_direction)
            {
                case Direction.Stop:
                {
                    if (_y > _yBase)
                    {
                        if (_y >= JumpAccell)
                        {
                            //y -= jump_accell;
                            _y -= _gravity;
                        }
                        else
                        {
                            _y = _yBase;
                        }
                    }

                    break;
                }
                case Direction.Jump:
                {
                    _y += _gravity;
                
                    if (_gravity <= 0.0f)
                        _direction = Direction.Down;
                    else
                        _gravity -= JumpAccell;

                    break;
                }
                case Direction.Down:
                {
                    _y -= _gravity;
                
                    if (_y > _yBase)
                        _gravity += JumpAccell;
                    else
                    {
                        _direction = 0;
                        _y = _yBase;
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    
        /*public void OnJumpBtnClick()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            "islocal", true,
            "y", 0,
            "time", 1.0f,
            "easetype", iTween.EaseType.linear,
            /*"oncomplete", "ViewResult",#1#
            "oncompletetarget", gameObject
        ));
    }*/
    }
}