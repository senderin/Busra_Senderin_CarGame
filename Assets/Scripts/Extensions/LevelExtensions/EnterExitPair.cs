using System;
using CarGame.Enums;
using UnityEngine;

namespace CarGame.Extensions.Level
{
    [Serializable]
    public class EnterExitPair
    {
        public Transform enterPoint;
        public Transform exitPoint;

        private DrivingDirection drivingDirection = DrivingDirection.NONE;

        public void Enable()
        {
            SetEnable(true);
        }

        public void Disable()
        {
            SetEnable(false);
        }

        internal void DisableTexts()
        {
            enterPoint.GetChild(0).gameObject.SetActive(false);
            exitPoint.GetChild(0).gameObject.SetActive(false);
        }

        internal DrivingDirection GetDrivingDirection()
        {
            if (drivingDirection == DrivingDirection.NONE)
            {
                drivingDirection = enterPoint.GetComponent<EnterPointData>().drivingDirection;
            }

            return drivingDirection;
        }

        private void SetEnable(bool isEnable)
        {
            enterPoint.gameObject.SetActive(isEnable);
            exitPoint.gameObject.SetActive(isEnable);
        }
    }

}
