using System.Collections.Generic;

namespace VK.AnimationSystem
{
    [System.Serializable]
    public class AnimationState
    {
        public string stateName;
        public AnimationClip clip;
        public List<AnimationTransition> transitions = new List<AnimationTransition>();
    }
}
