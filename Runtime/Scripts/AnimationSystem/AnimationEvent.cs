using UnityEngine.Events;

namespace VK.AnimationSystem
{
    [System.Serializable]
    public class AnimationEvent
    {
        public int frame;
        public UnityEvent onFrameReached;
    }
}
