using CarGame.ReplayMechanism;
using CarGame.StateMachine.States;
using UnityEngine;

namespace CarGame.Controllers
{
    public class ReplayController : CarMovementBase
    {
        private ReplayData replayData;
        private ActionPoint currentActionPoint;

        public void SetReplayData(ReplayData replayData, PlayState playState)
        {
            this.replayData = replayData;
            this.playState = playState;
            currentActionPoint = this.replayData.GetFirstActionPoint();
        }

        public override void FixedUpdate()
        {
            if (!CanMove())
                return;

            Replay();
            base.FixedUpdate();
        }

        public override void ReturnEnterPoint()
        {
            base.ReturnEnterPoint();
            currentActionPoint = replayData.GetFirstActionPoint();
        }

        private void Replay()
        {
            if (replayData.IsRecordOver())
                return;
                
            if (updateCallIndex == currentActionPoint.callIndex)
            {
                for (int i = 0; i < currentActionPoint.times; i++)
                {
                    Rotate(currentActionPoint.action);
                }
                currentActionPoint = replayData.GetNextActionPoint();
            }
        }

        public override void OnExitPoint()
        {
            // do nothing
        }

        public override void OnHitObstacle()
        {
            // nothing to do
        }
    }
}


