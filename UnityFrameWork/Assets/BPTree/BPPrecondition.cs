/**
 *  条件节点 既可以作为前置条件 本身也可以作为节点运行
 **/
namespace TFrame
{
    public abstract class BPPrecondition: BPNode
    {
        public BPPrecondition():base(null)
        { }
        public abstract bool Check();
        public override BPResult Tick()
        {
            bool success = Check();
            if (success)
                return BPResult.Ended;
            else
                return BPResult.Running;
        }
    }
}
