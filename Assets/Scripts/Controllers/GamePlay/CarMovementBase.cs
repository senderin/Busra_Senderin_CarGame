using UnityEngine;
using CarGame.Actions;
using CarGame.Enums;
using CarGame.Extensions.Level;
using CarGame.StateMachine.States;

namespace CarGame.Controllers
{
    public abstract class CarMovementBase : MonoBehaviour
    {
        public float forwardSpeed = 1f;
        public float turnSpeed = 1f;

        protected PlayState playState;
        protected int updateCallIndex;
        protected bool isFinished = false;
        protected EnterExitPair enterExitPair;
        protected DrivingDirection drivingDirection;

        private Vector3 startPosition;
        private Quaternion startRotation;

        public abstract void OnExitPoint();
        public abstract void OnHitObstacle();

        public virtual void Start()
        {
            playState = FindObjectOfType<PlayState>();
            startPosition = transform.position;

            drivingDirection = enterExitPair.GetDrivingDirection();
            // start rotation is set according to the driving direction
            SetStartRotation();

            updateCallIndex = 0;
            isFinished = false;
        }

        public virtual void FixedUpdate()
        {
            if (!CanMove())
                return;

            updateCallIndex++;

            MoveForward();
        }

        public bool CanMove()
        {
            if (!playState.IsGameStart() || isFinished)
                return false;
            return true;
        }

        private void MoveForward()
        {
            // Debug.Log("MoveForward() call");
            transform.position += transform.up * Time.deltaTime * forwardSpeed;
        }

        public void Rotate(IAction action)
        {
            if (!CanMove())
                return;

            // Debug.Log("Rotate() call");
            action.Execute(this.gameObject);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!CanMove())
                return;

            if (collision.CompareTag("Exit") && OnOwnExitPoint(collision.transform.position))
            {
                isFinished = true;
                OnExitPoint();
            }
        }

        private bool OnOwnExitPoint(Vector3 collidedExitPosition)
        {
            if (Vector3.Distance(collidedExitPosition, enterExitPair.exitPoint.position) <= Mathf.Epsilon)
                return true;
            return false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!CanMove())
                return;

            if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Car"))
            {
                OnHitObstacle();
            }
        }

        public void SetEnterExitPair(EnterExitPair enterExitPair)
        {
            this.enterExitPair = enterExitPair;
        }

        public virtual void ReturnEnterPoint()
        {
            isFinished = false;
            updateCallIndex = 0;
            transform.position = enterExitPair.enterPoint.position;
            transform.rotation = startRotation;
        }

        private void SetStartRotation()
        {
            if(drivingDirection == DrivingDirection.DOWN)
            {
                Quaternion rotation = Quaternion.Euler(180, 0, 0);
                transform.rotation = rotation;
                startRotation = rotation;
            }
        }
    }
}


