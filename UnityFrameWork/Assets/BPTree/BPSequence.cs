/**
 * 顺序逻辑节点 依次执行子节点 不会跳过执行
 * 
 * */
 namespace TFrame
{
    public class BPSequence: BPNode
    {
        private BPNode _activeNode;
        private int _activeIndex = -1;
        public BPSequence(BPPrecondition precondition = null) : base(precondition) { }
        protected override bool DoEvaluate()
        {
            if(_activeNode != null)
            {
                bool result = _activeNode.Evaluate();
                if(!result)
                {
                    _activeNode.Clear();
                    _activeNode = null;
                    _activeIndex = -1;
                }
                return result;
            }
            else
            {
                return _children[0].Evaluate();
            }
        }

        public override BPResult Tick()
        {
            //默认从第一个子节点开始执行
            if(_activeNode == null)
            {
                _activeNode = _children[0];
                _activeIndex = 0;
            }
            BPResult result = _activeNode.Tick();
            if(result == BPResult.Ended)
            {
                _activeIndex++;
                if(_activeIndex >= _children.Count)
                {
                    //全部执行完毕
                    _activeNode.Clear();
                    _activeNode = null;
                    _activeIndex = -1;

                }
                else
                {
                    _activeNode.Clear();
                    _activeNode = _children[_activeIndex];
                    result = BPResult.Running;
                }
            }
            return result;
        }

        public override void Clear()
        {
            base.Clear();
            _activeNode = null;
            _activeIndex = -1;
        }
    }
}