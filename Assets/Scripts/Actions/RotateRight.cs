using System;
using UnityEngine;

namespace CarGame.Actions
{
    public class RotateRight : IAction
    {
        private float turnSpeed;

        public RotateRight(float turnSpeed)
        {
            this.turnSpeed = turnSpeed;
        }

        public void Execute(GameObject gameObject)
        {
            Vector3 angle = -gameObject.transform.forward * turnSpeed;
            gameObject.transform.Rotate(angle);
            // Debug.Log("Action right");
        }
    }
}
