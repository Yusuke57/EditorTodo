using System;
using EditorTodo.Data;
using EditorTodo.Helper;
using UnityEngine;

namespace EditorTodo.InputEvent
{
    /// <summary>
    /// Todoリストの各Elementについてのユーザー操作処理
    /// </summary>
    public static class ElementInputEvent
    {
        /// <summary>
        /// トグルをクリックした時
        /// </summary>
        public static void OnClickToggle(TodoElementData elementData)
        {
            GUI.FocusControl(null);
            if (TodoElementAnimator.IsPlayingAnim(elementData))
            {
                return;
            }

            if (elementData.IsDone)
            {
                elementData.IsDone = false;
                Move(AnimData.InputType.ToggleTodo, elementData, 0, 0.2f);
                return;
            }

            var insertIdx = UserTodoDataHolder.GetLastTodoElementIdx();
            if (insertIdx == -1)
            {
                insertIdx = UserTodoDataHolder.ElementCount;
            }

            elementData.IsDone = true;
            Move(AnimData.InputType.ToggleDone, elementData, insertIdx, 0.3f);
        }

        /// <summary>
        /// Element本体（テキスト部分）をクリックした時
        /// </summary>
        public static void OnClickBody(TodoElementData elementData)
        {
            GUI.FocusControl(null);
            UserTodoDataHolder.DisplayElement = elementData;
        }

        /// <summary>
        /// 移動ボタンをクリックした時
        /// </summary>
        public static void OnClickMoveButton(TodoElementData elementData, bool isUp)
        {
            GUI.FocusControl(null);
            if (TodoElementAnimator.IsPlayingAnim(elementData))
            {
                return;
            }

            var preIdx = UserTodoDataHolder.GetIdx(elementData);
            var insertIdx = isUp ? preIdx - 1 : preIdx + 1;

            if (elementData.IsDone != UserTodoDataHolder.GetElementData(insertIdx).IsDone)
            {
                return;
            }

            Move(AnimData.InputType.Move, elementData, insertIdx);
        }

        /// <summary>
        /// 削除ボタンをクリックした時
        /// </summary>
        public static void OnClickDeleteButton(TodoElementData elementData)
        {
            GUI.FocusControl(null);
            if (TodoElementAnimator.IsPlayingAnim(elementData))
            {
                return;
            }

            TodoElementAnimator.SlideOut(AnimData.InputType.Delete, elementData, 0.1f, () =>
            {
                if (elementData == null)
                {
                    return;
                }

                UserTodoDataHolder.Remove(elementData);
                if (UserTodoDataHolder.DisplayElement == elementData)
                {
                    UserTodoDataHolder.DisplayElement = null;
                }
            });
        }

        /// <summary>
        /// Elementを移動させるアニメーションを呼び出す
        /// </summary>
        private static void Move(AnimData.InputType inputType, TodoElementData elementData,
            int movedIdx, float delay = 0, Action onOutComplete = null)
        {
            TodoElementAnimator.SlideOutIn(inputType, elementData, delay, () =>
            {
                if (elementData == null)
                {
                    return;
                }

                onOutComplete?.Invoke();
                UserTodoDataHolder.Remove(elementData);
                UserTodoDataHolder.Insert(movedIdx, elementData);
            });
        }
    }
}