using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VK.AnimationSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private AnimationState[] states;
        [SerializeField] private AnimationState defaultState;

        private SpriteRenderer spriteRenderer;
        private Coroutine currentAnimation;
        private Dictionary<string, AnimationParameter> parameters = new Dictionary<string, AnimationParameter>();
        private AnimationState currentState;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            InitializeParameters();
            PlayDefaultState();
        }

        private void InitializeParameters()
        {
            foreach (var state in states)
            {
                foreach (var transition in state.transitions)
                {
                    AnimationParameter param = transition.parameter;
                    if (param != null && !parameters.ContainsKey(param.name))
                        parameters.Add(param.name, param);
                }
            }
        }

        private void PlayDefaultState()
        {
            if (defaultState != null)
                PlayState(defaultState);
        }

        public void SetParameter<T>(string name, T value) where T : struct
        {
            if (parameters.TryGetValue(name, out AnimationParameter param))
            {
                switch (param)
                {
                    case BoolParameter boolParam:
                        boolParam.value = (bool)(object)value;
                        CheckTransitions();
                        break;
                    case TriggerParameter triggerParam:
                        triggerParam.value = (bool)(object)value;
                        CheckTransitions();
                        triggerParam.value = false;
                        break;
                }
            }
        }

        private void CheckTransitions()
        {
            foreach (var transition in currentState.transitions)
            {
                if (transition.parameter == null) continue;

                bool conditionMet = false;
                switch (transition.parameter)
                {
                    case BoolParameter boolParam:
                        conditionMet = boolParam.value == transition.boolCondition;
                        break;
                    case TriggerParameter triggerParam:
                        conditionMet = triggerParam.value;
                        break;
                }

                if (conditionMet)
                {
                    PlayState(transition.targetState);
                    break;
                }
            }
        }

        public void PlayState(AnimationState state)
        {
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);

            currentState = state;
            currentAnimation = StartCoroutine(PlayAnimationRoutine(state.clip));
        }

        private IEnumerator PlayAnimationRoutine(AnimationClip clip)
        {
            int currentFrame = 0;
            float frameDuration = 1f / clip.frameRate;

            while (currentFrame < clip.frames.Length)
            {
                spriteRenderer.sprite = clip.frames[currentFrame];
                CheckEvents(clip, currentFrame);

                currentFrame++;

                if (currentFrame >= clip.frames.Length && clip.loop)
                    currentFrame = 0;

                yield return new WaitForSeconds(frameDuration);
            }
        }

        private void CheckEvents(AnimationClip clip, int currentFrame)
        {
            foreach (var e in clip.events)
            {
                if (e.frame == currentFrame)
                    e.onFrameReached?.Invoke();
            }
        }
    }
}
