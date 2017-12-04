namespace TFrame
{
    using UnityEngine;
    public interface IState
    {
        uint GetStateID();
        void OnEnter(StateMachine machine, IState prevState, Object param1, Object param2);
        void OnLeave(IState nextState, Object param1, Object param2);
        void OnUpdate();
        void OnFixedUpdate();
        void OnLateUpdate();
    }
}