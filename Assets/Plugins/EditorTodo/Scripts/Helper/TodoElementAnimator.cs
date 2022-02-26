using System;
using System.Collections.Generic;
using System.Linq;
using EditorTodo.Data;

namespace EditorTodo.Helper
{
    public static class TodoElementAnimator
    {
        /// <summary>
        /// 実行中アニメーションリスト
        /// </summary>
        private static readonly List<AnimData> AnimDataList = new();

        /// <summary>
        /// スライドアニメーションの秒数
        /// </summary>
        private const float SLIDE_DURATION = 0.16f;

        /// <summary>
        /// アニメーション中か
        /// </summary>
        public static bool IsPlayingAnim(TodoElementData elementData)
        {
            return AnimDataList.Exists(animData => animData.IsMatchElementData(elementData));
        }

        /// <summary>
        /// 実行中アニメーションのAnimTypeを返す
        /// </summary>
        public static AnimData.AnimType GetPlayingAnimType(TodoElementData elementData)
        {
            var animData = AnimDataList.FirstOrDefault(animData => animData.IsMatchElementData(elementData));
            return animData?.Anim ?? AnimData.AnimType.Idle;
        }
        
        /// <summary>
        /// 実行中アニメーションのInputTypeを返す
        /// </summary>
        public static AnimData.InputType GetPlayingInputType(TodoElementData elementData)
        {
            var animData = AnimDataList.FirstOrDefault(animData => animData.IsMatchElementData(elementData));
            return animData?.Input ?? AnimData.InputType.None;
        }

        /// <summary>
        /// アニメーションの進捗(0-1)を返す
        /// </summary>
        public static float GetAnimValue(TodoElementData elementData)
        {
            var animData = AnimDataList.FirstOrDefault(data => data.IsMatchElementData(elementData));
            if (animData == null)
            {
                return 0;
            }

            animData.Update();
            if (animData.IsCompleted())
            {
                AnimDataList.Remove(animData);
                animData.Complete();
            }

            return animData.Value;
        }

        /// <summary>
        /// スライドインアニメーション
        /// </summary>
        public static void SlideIn(AnimData.InputType inputType, TodoElementData elementData,
            float delay = 0, Action onComplete = null)
        {
            AnimDataList.Add(new AnimData(AnimData.AnimType.SlideIn, inputType, elementData,
                GlobalVariable.WindowRect.width, 0f, SLIDE_DURATION, delay, onComplete));
        }

        /// <summary>
        /// スライドアウトアニメーション
        /// </summary>
        public static void SlideOut(AnimData.InputType inputType, TodoElementData elementData,
            float delay = 0, Action onComplete = null)
        {
            AnimDataList.Add(new AnimData(AnimData.AnimType.SlideOut, inputType, elementData,
                0, GlobalVariable.WindowRect.width * 4f, SLIDE_DURATION, delay, onComplete));
        }

        /// <summary>
        /// スライドアウトした後にスライドイン
        /// </summary>
        public static void SlideOutIn(AnimData.InputType inputType, TodoElementData elementData,
            float delay = 0, Action onOutComplete = null, Action onComplete = null)
        {
            SlideOut(inputType, elementData, delay, () =>
            {
                onOutComplete?.Invoke();
                SlideIn(inputType, elementData, 0, onComplete);
            });
        }
    }
}