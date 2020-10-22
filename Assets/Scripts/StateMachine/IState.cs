using System;
namespace CarGame.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}
