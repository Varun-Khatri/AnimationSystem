using UnityEngine;

namespace VK.AnimationSystem
{
    [CreateAssetMenu(menuName = "Animation System/Animation Clip")]
    public class AnimationClip : ScriptableObject
    {
        public Sprite[] frames;
        public float frameRate = 12;
        public bool loop = true;
        public AnimationEvent[] events;
    }
}
