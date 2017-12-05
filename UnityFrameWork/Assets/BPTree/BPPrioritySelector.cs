/**
 * 条件逻辑节点 只执行满足条件的第一个子节点
 * 
 * */
using UnityEngine;
namespace TFrame
{
    public class BPPrioritySelector: BPNode
    {
        private BPNode _activeChild;
        public BPPrioritySelector(BPPrecondition precondition) : base(precondition) { }
        protected override bool DoEvaluate()
        {
            foreach(BPNode child in _children)
            {
                if(child.Evaluate())
                {
                    if(_activeChild != null && _activeChild != child)
                    {
                        _activeChild.Clear();
                        
                    }
                    _activeChild = child;
                    return true;
                }
            }
            if(_activeChild != null)
            {
                _activeChild.Clear();
                _activeChild = null;
            }
            return false;
        }
        public override void Clear()
        {
            base.Clear();
            if(_activeChild != null)
            {
                _activeChild.Clear();
                _activeChild = null;
            }
        }

        public override BPResult Tick()
        {
            if (_activeChild == null)
                return BPResult.Ended;
            BPResult result = _activeChild.Tick();
            if(result != BPResult.Running)
            {
                _activeChild.Clear();
                _activeChild = null;
            }
            return result;
        }
    }
}