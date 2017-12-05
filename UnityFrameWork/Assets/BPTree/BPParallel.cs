/**
 * 逻辑节点 同时执行 各个子节点 只要有子节点前置条件失败 不会执行
 * 
 * */
using UnityEngine;
using System.Collections.Generic;
namespace TFrame
{
    public class BPParallel: BPNode
    {
        public enum ParallelFunction
        {
            And = 0, //所有子节点全部运行完成 才算完成
            Or,      // 只要有一个节点完成 即退出
        }
        protected List<BPResult> _results;
        protected ParallelFunction _func;

        public BPParallel(ParallelFunction func):this(func, null)
        { }
        public BPParallel(ParallelFunction func, BPPrecondition precondition)
            :base(precondition)
        {
            _results = new List<BPResult>();
            this._func = func;
        }

        protected override bool DoEvaluate()
        {
            foreach(BPNode node in _children)
            {
                if(!node.Evaluate())
                {
                    return false;
                }
            }
            return true;
        }

        public override BPResult Tick()
        {
            int endingResultCount = 0;
            for(int i = 0; i < _children.Count; i++)
            {
                if (_results[i] == BPResult.Running)
                {
                    _results[i] = _children[i].Tick();
                }
                if (_results[i] != BPResult.Running)
                {
                    if(_func == ParallelFunction.And)
                        endingResultCount++;
                    else
                    {
                        ResetResults();
                        return BPResult.Ended;
                    }
                }
                if(endingResultCount == _children.Count)
                {
                    ResetResults();
                    return BPResult.Ended;
                }
               
            }
            return BPResult.Running;
        }

        public override void Clear()
        {
            base.Clear();
            ResetResults();
        }

        public override void AddChild(BPNode node)
        {
            base.AddChild(node);
            _results.Add(BPResult.Running);
        }

        public override void RemoveChild(BPNode node)
        {
            int index = _children.IndexOf(node);
            _results.RemoveAt(index);
            base.RemoveChild(node);
            
        }

        private void ResetResults()
        {
            for(int i = 0; i < _results.Count; i++)
            {
                _results[i] = BPResult.Running;
            }
        }


    }
}