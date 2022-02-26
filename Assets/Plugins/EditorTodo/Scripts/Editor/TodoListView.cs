using EditorTodo.Data;
using EditorTodo.Helper;
using UnityEditor;
using UnityEngine;

namespace EditorTodo.UI
{
    /// <summary>
    /// Todoリストのリスト部分
    /// </summary>
    public static class TodoListView
    {
        private static Vector2 scrollPosition;

        public static void Show(TodoListData listData)
        {
            var scrollStyle = new GUIStyle(GUI.skin.box)
            {
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 12, 12)
            };
            using var scroll = new EditorGUILayout.ScrollViewScope(scrollPosition, scrollStyle);
            {
                scrollPosition = scroll.scrollPosition;

                ShowAddElementButton(0);

                var lastTodoElementIdx = UserTodoDataHolder.GetLastTodoElementIdx();

                // 要素がなくなると以下のエラーが出るため暫定対応
                // ArgumentException: Getting control n's position in a group with only 4 controls when doing repaint
                if (lastTodoElementIdx == -1)
                {
                    GUILayout.Space(0);
                }
                
                for (var i = 0; i < listData.elementDataList.Count; i++)
                {
                    TodoElementView.Show(listData.elementDataList[i]);
                    if (i == lastTodoElementIdx)
                    {
                        ShowAddElementButton(lastTodoElementIdx + 1);
                    }
                }
                
                GUILayout.FlexibleSpace();
            }
        }

        /// <summary>
        /// Element追加ボタン
        /// </summary>
        private static void ShowAddElementButton(int index)
        {
            var style = GUIStyleProvider.Get(StyleKey.AddButton);
            var isClickAddElementButton = false;
            
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                isClickAddElementButton = GUILayout.Button("", style);
                GUILayout.FlexibleSpace();
            }
            
            if (!isClickAddElementButton)
            {
                return;
            }

            GUI.FocusControl(null);
            var defaultElementData = TodoElementData.Default;
            UserTodoDataHolder.Insert(index, defaultElementData);

            if (index > 0)
            {
                scrollPosition.y += GUIStyleProvider.GetHeight(StyleKey.ElementBody);
            }

            UserTodoDataHolder.DisplayElement = defaultElementData;
            
            TodoElementAnimator.SlideIn(AnimData.InputType.Add, defaultElementData);
        }
    }
}