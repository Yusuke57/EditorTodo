using System;
using UnityEditor;
using UnityEngine;

namespace EditorTodo.Data
{
    public class AnimData
    {
        private readonly TodoElementData elementData;
        private readonly float from;
        private readonly float to;
        private readonly float duration;
        private readonly Action onComplete;

        private double preTime;
        private float value01;
        private float EasedValue01 => 1 - Mathf.Clamp01(Mathf.Pow(1 - value01, 2));
        public float Value => Mathf.Lerp(from, to, EasedValue01);
        public AnimType Anim { get; }
        public InputType Input { get; }

        public bool IsMatchElementData(TodoElementData target)
        {
            return elementData == target;
        }

        public AnimData(AnimType animType, InputType inputType, TodoElementData elementData,
            float from, float to, float duration, float delay, Action onComplete)
        {
            Anim = animType;
            Input = inputType;
            this.elementData = elementData;
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.onComplete = onComplete;
            preTime = EditorApplication.timeSinceStartup;
            value01 = -delay / duration;
        }

        public void Update()
        {
            var dt = (float) (EditorApplication.timeSinceStartup - preTime);

            value01 += dt / duration;
            if (value01 > 1)
            {
                value01 = 1;
            }

            preTime = EditorApplication.timeSinceStartup;
        }

        public bool IsCompleted()
        {
            return value01 >= 1;
        }

        public void Complete()
        {
            onComplete?.Invoke();
        }

        public enum AnimType
        {
            Idle,
            SlideIn,
            SlideOut,
        }

        public enum InputType
        {
            None,
            ToggleDone,
            ToggleTodo,
            Move,
            Delete,
            Add,
        }
    }
}