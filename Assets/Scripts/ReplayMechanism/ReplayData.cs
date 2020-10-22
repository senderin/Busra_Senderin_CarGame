using System;
using System.Collections.Generic;
using CarGame.Actions;
using UnityEngine;

namespace CarGame.ReplayMechanism
{   
    public class ReplayData
    {
        private List<ActionPoint> actionList;
        private ActionPoint recordingPoint;
        private int lastPlayedActionIndex;

        public ReplayData(GameObject gameObject)
        {
            actionList = new List<ActionPoint>();
            lastPlayedActionIndex = 0;
        }

        internal void RecordAction(int callIndex, IAction action)
        {
            // in same update call duration, actions can call multiple times 
            if (recordingPoint != null && recordingPoint.callIndex == callIndex)
                recordingPoint.times++;

            else
            {
                recordingPoint = new ActionPoint
                {
                    callIndex = callIndex,
                    action = action
                };
                actionList.Add(recordingPoint);
            }
        }

        internal ActionPoint GetFirstActionPoint()
        {
            lastPlayedActionIndex = 0;
            return actionList[lastPlayedActionIndex];
        }

        internal ActionPoint GetNextActionPoint()
        {
            lastPlayedActionIndex++;
            return actionList[lastPlayedActionIndex];
        }

        internal bool IsRecordOver()
        {
            if (lastPlayedActionIndex == (actionList.Count - 1))
                return true;
            return false;
        }

        internal void ClearRecord()
        {
            actionList.Clear();
        }
    }
}
