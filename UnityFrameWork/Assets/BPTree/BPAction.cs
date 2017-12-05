/**
 * 行为节点
 * 
 * */
using UnityEngine;
 namespace TFrame
{

    public class BPAction: BPNode
    {
        private enum BPActionStatus
        {
            Ready = 0,
            Running,
        }
        private BPActionStatus _status = BPActionStatus.Ready;
        public BPAction(BPPrecondition precondition = null):base(precondition)
        { }
        protected virtual void Enter()
        {
            if(BPConfiguration.ENABLE_BPACTION_LOG)
            {
                Debug.Log("Enter" + this._name + "[" + this.GetType().ToString() + "]");
            }
        }
        protected virtual void Exit()
        {
            if(BPConfiguration.ENABLE_BPACTION_LOG)
            {
                Debug.Log("Exit" + this._name + "[" + this.GetType().ToString() + "]");
            }
        }

        protected virtual BPResult Execute()
        {
            return BPResult.Running;
        }

        public override void Clear()
        {
            base.Clear();
            if(_status != BPActionStatus.Ready)
            {
                Exit();
                _status = BPActionStatus.Ready;
            }
        }

        public override BPResult Tick()
        {
            BPResult result = BPResult.Ended;
            if(_status == BPActionStatus.Ready)
            {
                Enter();
                _status = BPActionStatus.Running;
            }
            if(_status == BPActionStatus.Running)
            {
                result = Execute();
                if(result != BPResult.Running)
                {
                    Exit();
                    _status = BPActionStatus.Ready;
                }
            }
            return result;
        }

        public override void AddChild(BPNode node)
        {
            Debug.LogError("BTAction cann't add child node");
        }
        public override void RemoveChild(BPNode node)
        {
            Debug.LogError("BTAction cann't remove child node");
        }
    }
}