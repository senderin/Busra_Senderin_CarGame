using System;
using CarGame.Enums;
using UnityEngine;

namespace CarGame.StateMachine
{
    [System.Serializable]
    public class State
    {
        public StateType stateType;
        public MonoBehaviour stateScript;
    }

    public delegate void Callback();
}