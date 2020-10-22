using System;
using System.Collections.Generic;
using CarGame.Actions;
using UnityEngine;

namespace CarGame.ReplayMechanism
{
    public class ActionPoint
    {
        public int callIndex;
        public IAction action;
        public int times = 1;
    }
}
