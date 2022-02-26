using EditorTodo.Data;
using EditorTodo.Helper;
using EditorTodo.InputEvent;
using UnityEditor;
using UnityEngine;

namespace EditorTodo.UI
{
    /// <summary>
    /// Todoリストの各Element
    /// </summary>
    public static class TodoElementView
    {
        public static void Show(TodoElementData elementData)
        {
            // アニメーションの現在の状態を取得
            var animValue = TodoElementAnimator.GetAnimValue(elementData);
            
            using (new EditorGUILayout.HorizontalScope())
            {
                ShowToggle(elementData, animValue);
                ShowBody(elementData, animValue);
                ShowMoveButton(elementData, animValue);
                ShowDeleteButton(elementData, animValue);
            }
        }

        /// <summary>
        /// トグル
        /// </summary>
        private static void ShowToggle(TodoElementData elementData, float animValue)
        {
            var style = GUIStyleProvider.Get(StyleKey.Toggle);
            ApplyAnimValue(style, animValue);
            
            var isDone = GUILayout.Toggle(elementData.IsDone, "", style);
            if (isDone == elementData.IsDone)
            {
                return;
            }

            ElementInputEvent.OnClickToggle(elementData);
        }
        
        /// <summary>
        /// 本体（テキスト部分）
        /// </summary>
        private static void ShowBody(TodoElementData elementData, float animValue)
        {
            var style = GUIStyleProvider.Get(StyleKey.ElementBody);
            ApplyAnimValue(style, animValue);

            var width = GlobalVariable.WindowRect.width
                        - GUIStyleProvider.GetWidth(StyleKey.Toggle)
                        - GUIStyleProvider.GetWidth(StyleKey.MoveUpButton)
                        - GUIStyleProvider.GetWidth(StyleKey.DeleteButton);
            
            var text = TextWrapper.Ellipsis(elementData.title, style, width, 2);

            var isDisplay = elementData == UserTodoDataHolder.DisplayElement;
            if (isDisplay)
            {
                var activeColor = style.active.textColor;
                style.normal.textColor = activeColor;
                style.hover.textColor = activeColor;
                style.onNormal.textColor = activeColor;
                style.onHover.textColor = activeColor;
            }
            
            // TodoとDoneのStyleを一元管理するために、状態変更操作ができないToggleで制御する
            var isActive = GUILayout.Toggle(elementData.IsDone, text, style);
            if (isActive == elementData.IsDone)
            {
                return;
            }
            
            ElementInputEvent.OnClickBody(elementData);
        }

        /// <summary>
        /// 移動ボタン
        /// </summary>
        private static void ShowMoveButton(TodoElementData elementData, float animValue)
        {
            var upStyle = GUIStyleProvider.Get(StyleKey.MoveUpButton);
            var downStyle = GUIStyleProvider.Get(StyleKey.MoveDownButton);
            var bodyStyle = GUIStyleProvider.Get(StyleKey.ElementBody);
            var verticalStyle = new GUIStyle(downStyle)
            {
                margin = new RectOffset(0, 0, bodyStyle.margin.top, bodyStyle.margin.bottom)
            };

            ApplyAnimValue(upStyle, animValue);
            ApplyAnimValue(downStyle, animValue);
            ApplyAnimValue(verticalStyle, animValue);

            // 移動できる方向のボタンのみ表示する
            var idx = UserTodoDataHolder.GetIdx(elementData);
            if (idx == 0 || elementData.IsDone != UserTodoDataHolder.GetElementData(idx - 1)?.IsDone)
            {
                upStyle.normal.background = upStyle.focused.background;
                upStyle.hover.background = upStyle.focused.background;
                upStyle.onNormal.background = upStyle.onFocused.background;
                upStyle.onHover.background = upStyle.onFocused.background;
            }
            if (idx == UserTodoDataHolder.ElementCount - 1 || elementData.IsDone != UserTodoDataHolder.GetElementData(idx + 1)?.IsDone)
            {
                downStyle.normal.background = downStyle.focused.background;
                downStyle.hover.background = downStyle.focused.background;
                downStyle.onNormal.background = downStyle.onFocused.background;
                downStyle.onHover.background = downStyle.onFocused.background;
            }

            var isClickUpButton = false;
            var isClickDownButton = false;
            using (new EditorGUILayout.VerticalScope(verticalStyle))
            {
                // TodoとDoneのStyleを一元管理するために、状態変更操作ができないToggleで制御する
                var isUpActive = GUILayout.Toggle(elementData.IsDone, "", upStyle);
                isClickUpButton = isUpActive != elementData.IsDone;
                var isDownActive = GUILayout.Toggle(elementData.IsDone, "", downStyle);
                isClickDownButton = isDownActive != elementData.IsDone;
            }

            if (!isClickUpButton && !isClickDownButton)
            {
                return;
            }

            ElementInputEvent.OnClickMoveButton(elementData, isClickUpButton);
        }
        
        /// <summary>
        /// 削除ボタン
        /// </summary>
        private static void ShowDeleteButton(TodoElementData elementData, float animValue)
        {
            var style = GUIStyleProvider.Get(StyleKey.DeleteButton);
            ApplyAnimValue(style, animValue);
            
            // TodoとDoneのStyleを一元管理するために、状態変更操作ができないToggleで制御する
            var isActive = GUILayout.Toggle(elementData.IsDone, "", style);
            if (isActive == elementData.IsDone)
            {
                return;
            }
            
            ElementInputEvent.OnClickDeleteButton(elementData);
        }

        private static void ApplyAnimValue(GUIStyle style, float animValue)
        {
            style.overflow = new RectOffset(-(int)animValue, (int)animValue, 0, 0);
            style.contentOffset = Vector2.right * animValue;
        }
    }
}