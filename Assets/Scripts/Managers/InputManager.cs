#define UNITY_REMOTE
using UnityEngine;
using System;
using CarGame.Extensions;
using CarGame.Enums;

namespace CarGame.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public delegate void PressEvent(Direction direction);
        public PressEvent OnPress;

        private Touch touch;
        private Vector2 touchStartPosition, touchEndPosition;

        private void FixedUpdate()
        {
#if UNITY_EDITOR
            HandleKeyboardInput();
#endif

#if UNITY_ANDROID || UNITY_REMOTE
            HandleTouchInput();
#endif

        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        Press(Direction.LEFT);
                    }

                    else if (touch.position.x > Screen.width / 2)
                    {
                        Press(Direction.RIGHT);
                    }
                }
            }
        }

        private void HandleKeyboardInput()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Press(Direction.LEFT);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Press(Direction.RIGHT);
            }
        }

        private void Press(Direction direction)
        {
            if (OnPress == null)
                return;

            Delegate[] calls = OnPress.GetInvocationList();
            foreach (Delegate call in calls)
            {
                ((PressEvent)call).Invoke(direction);
            }
        }
    }
}


