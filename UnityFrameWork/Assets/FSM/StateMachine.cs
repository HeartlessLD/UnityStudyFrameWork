

namespace TFrame
{
    using System.Collections.Generic;
    using UnityEngine;
    public class StateMachine
    {

        private Dictionary<uint, IState> _stateDic = null;

        private IState _currentState = null;

        public IState CurrentState
        {
            get
            {
                return _currentState;
            }
        }
        public delegate void SwitchStateCallback(IState from, IState to, Object param1, Object param2);
        public SwitchStateCallback switchStateCallback = null;
        public StateMachine()
        {
            _stateDic = new Dictionary<uint, IState>();
            _currentState = null;
        }

        public bool RegisterState(IState state)
        {
            if(state == null)
            {
                Debug.LogError("StateMachine.RegisterState state cann't be null");
                return false;
            }
            if(_stateDic.ContainsKey(state.GetStateID()))
            {
                Debug.LogError("StateMachine.RegisterState state has contain state" + state.GetStateID());
                return false;
            }
            _stateDic.Add(state.GetStateID(), state);
            return true;
        }
        public bool RemoveState(uint stateID)
        {
            if(!_stateDic.ContainsKey(stateID))
            {
                return false;
            }
            if(_currentState.GetStateID() == stateID)
            {
                Debug.LogError("cann't removeState because currentstate is " + stateID);
                return false;
            }
            _stateDic.Remove(stateID);
            return true;
        }
        public void StopState(Object param1, Object param2)
        {
            if(_currentState != null)
            {
                _currentState.OnLeave(null, param1, param2);
            }
            _currentState = null;
        }
        
        public bool SwitchState(uint newStateID, Object param1, Object param2)
        {
            //切换到当前的状态
            if(_currentState != null && _currentState.GetStateID() == newStateID)
            {
                return false;
            }
            IState newState = null;
            _stateDic.TryGetValue(newStateID, out newState);
            if (newState == null)
            {
                return false;
            }
            if (_currentState != null)
                _currentState.OnLeave(newState, param1, param2);
            IState oldState = _currentState;
            _currentState = newState;
            if (switchStateCallback != null)
                switchStateCallback(oldState, _currentState, param1, param2);
            newState.OnEnter(this, oldState, param1, param2);
            return true;
        }

        public void OnUpdate()
        {
            if (_currentState != null)
                _currentState.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            if (_currentState != null)
                _currentState.OnFixedUpdate();
        }

        public void OnLateUpdate()
        {
            if (_currentState != null)
                _currentState.OnLateUpdate();
        }
    }

}