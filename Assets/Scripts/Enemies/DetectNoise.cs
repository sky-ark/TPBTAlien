namespace Enemies
{
    public class DetectNoise : NodeLeaf
    {
        private Blackboard _blackboard;
        
        public DetectNoise(Blackboard blackboard)
        {
            _blackboard = blackboard;
        }
        public override NodeState Execute()
        {
            return _blackboard.HasHeardNoise ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }
}