namespace VK.AnimationSystem
{
    [System.Serializable]
    public class AnimationTransition
    {
        public AnimationParameter parameter;
        public AnimationState targetState;
        public bool boolCondition;
    }
}