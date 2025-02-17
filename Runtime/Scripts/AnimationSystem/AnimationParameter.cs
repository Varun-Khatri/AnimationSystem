using UnityEngine;

namespace VK.AnimationSystem
{
    public abstract class AnimationParameter : ScriptableObject { }

    [CreateAssetMenu(menuName = "Animation System/Parameters/Bool")]
    public class BoolParameter : AnimationParameter
    {
        public bool value;
    }

    [CreateAssetMenu(menuName = "Animation System/Parameters/Trigger")]
    public class TriggerParameter : AnimationParameter
    {
        [System.NonSerialized] public bool value;
    }
}
