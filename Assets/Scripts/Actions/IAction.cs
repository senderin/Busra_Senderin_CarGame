using System;
using UnityEngine;

namespace CarGame.Actions
{
    public interface IAction
    {
        void Execute(GameObject gameObject);
    }
}
