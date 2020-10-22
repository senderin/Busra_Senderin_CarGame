using System;
using UnityEngine;

namespace CarGame.Actions
{
    public class RotateLeft : IAction
    {
        private float turnSpeed;

        public RotateLeft(float turnSpeed)
        {
            this.turnSpeed = turnSpeed;
        }

        public void Execute(GameObject gameObject)
        {
            Vector3 angle = gameObject.transform.forward * turnSpeed;
            gameObject.transform.Rotate(angle);
            // Debug.Log("Action left");
        }
    }
}
