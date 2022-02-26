using EditorTodo.Data;
using EditorTodo.Helper;
using EditorTodo.InputEvent;
using UnityEditor;
using UnityEngine;

namespace EditorTodo.UI
{
    /// <summary>
    /// 詳細表示ウィンドウ
    /// </summary>
    public static class TodoDetailView
    {
        public static void Show(TodoElementData elementData)
        {
            if (elementData == null)
            {
                return;
            }

            var windowStyle = GUIStyleProvider.Get(StyleKey.DetailWindow);
            using (new GUILayout.VerticalScope(windowStyle))
            {
                ShowHeader(elementData);
                ShowTitle(elementData);
                GUILayout.Space(8);
                ShowDescription(elementData);
            }
        }

        /// <summary>
        /// ヘッダー
        /// </summary>
        private static void ShowHeader(TodoElementData elementData)
        {
            var closeStyle = GUIStyleProvider.Get(StyleKey.CloseButton);
            var headerStyle = new GUIStyle
            {
                stretchWidth = true,
                fixedHeight = closeStyle.fixedHeight
            };
            
            using (new EditorGUILayout.HorizontalScope(headerStyle))
            {
                GUILayout.FlexibleSpace();
                
                // 閉じるボタン
                var isClickCloseButton = GUILayout.Button("", closeStyle);
                if (isClickCloseButton)
                {
                    DetailInputEvent.OnClickCloseButton();
                }
            }
        }

        /// <summary>
        /// Todoのタイトル
        /// </summary>
        private static void ShowTitle(TodoElementData elementData)
        {
            var labelStyle = GUIStyleProvider.Get(StyleKey.DetailLabel);
            
            GUILayout.Label("Title", labelStyle);
            var titleStyle = new GUIStyle(GUI.skin.textField)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold
            };
            elementData.title = EditorGUILayout.TextField(elementData.title, titleStyle);
        }
        
        /// <summary>
        /// Todoの詳細説明
        /// </summary>
        private static void ShowDescription(TodoElementData elementData)
        {
            var labelStyle = GUIStyleProvider.Get(StyleKey.DetailLabel);
            
            GUILayout.Label("Description", labelStyle);
            var descriptionStyle = new GUIStyle(GUI.skin.textArea)
            {
                fontSize = 14,
                stretchHeight = true
            };
            elementData.description = EditorGUILayout.TextArea(elementData.description, descriptionStyle);
        }
    }
}