
using System.Collections.Generic;
/**
* 行为节点基类 行为树的叶子节点 主要用来执行相关的游戏逻辑
* 
* */
using UnityEngine;
namespace TFrame
{
    public abstract class BPNode
    {
        public string _name;
        protected List<BPNode> _children;
        public List<BPNode> Children
        {
            get
            {
                return this._children;
            }
        }

        //节点的准入条件
        public BPPrecondition precondition;
        //黑板
        public BPBlackBoard blackBoard;
        //冷却功能
        public float interval = 0;
        private float _lastTimeEvaluated = 0;
        public bool activated;

        public BPNode():this(null)
        { }
        public BPNode(BPPrecondition precondition)
        {
            this.precondition = precondition;
        }
        //节点初始化
        public virtual void Activate(BPBlackBoard blackBoard)
        {
            if (activated)
                return;
            this.blackBoard = blackBoard;
            if (precondition != null)
                precondition.Activate(blackBoard);
            if(_children != null)
            {
                foreach(BPNode node in _children)
                {
                    node.Activate(blackBoard);
                }
            }
            activated = true;
        }

        //检测是否可执行
        public bool Evaluate()
        {
            bool coolDownOK = CheckTimer();
            return activated && coolDownOK && (precondition == null || precondition.Check()) && DoEvaluate();
        }
        private bool CheckTimer()
        {
            if(Time.time - _lastTimeEvaluated > interval)
            {
                _lastTimeEvaluated = Time.time;
                return true;
            }
            return false;
        }
        protected virtual bool DoEvaluate()
        {
            return true;
        }

        public virtual BPResult Tick()
        {
            return BPResult.Ended;
        }
        public virtual void Clear()
        {
            foreach (BPNode child in _children)
                child.Clear();
        }

        public virtual void AddChild(BPNode node)
        {
            if (_children == null)
                _children = new List<BPNode>();
            if (node != null)
                _children.Add(node);
        }

        public virtual void RemoveChild(BPNode node)
        {
            if (_children == null || node == null)
                return;
            _children.Remove(node);
        }
    }
}
