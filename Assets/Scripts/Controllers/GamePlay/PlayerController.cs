using System;
using CarGame.Actions;
using CarGame.Enums;
using CarGame.Extensions.Level;
using CarGame.ReplayMechanism;

namespace CarGame.Controllers
{
    public class PlayerController : CarMovementBase
    {
        private ReplayData replayData;

        public override void Start()
        {
            base.Start();
            replayData = new ReplayData(this.gameObject);
        }

        public ReplayData GetReplayData()
        {
            return replayData;
        }

        public override void OnExitPoint()
        {
            playState.AddToDrivenCars(this);
        }

        public override void OnHitObstacle()
        {
            playState.Restart();
        }

        internal EnterExitPair GetEnterExitPair()
        {
            return enterExitPair;
        }

        internal void Reset()
        {
            ClearReplayRecord();
            ReturnEnterPoint();
        }

        internal void Rotate(Direction direction)
        {
            IAction action = null;

            if (drivingDirection == DrivingDirection.DOWN)
                direction = SwitchRotationDirection(direction);

            if (direction == Direction.LEFT)
            {
                action = new RotateLeft(turnSpeed);
            }

            if (direction == Direction.RIGHT)
            {
                action = new RotateRight(turnSpeed);
            }
            replayData.RecordAction(updateCallIndex, action);
            Rotate(action);
        }

        private void ClearReplayRecord()
        {
            replayData.ClearRecord();
        }

        private Direction SwitchRotationDirection(Direction direction)
        {
            if (direction == Direction.LEFT)
                direction = Direction.RIGHT;
            else
                direction = Direction.LEFT;
            return direction;
        }


    }
}


