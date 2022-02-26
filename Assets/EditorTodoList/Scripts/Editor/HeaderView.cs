using System.Linq;
using EditorTodo.Data;
using EditorTodo.Helper;
using EditorTodo.InputEvent;
using UnityEngine;

namespace EditorTodo.UI
{
    /// <summary>
    /// ヘッダー
    /// </summary>
    public static class HeaderView
    {
        public static void Show(TodoListData listData)
        {
            ShowProgressGauge(listData);

            var headerStyle = GUIStyleProvider.Get(StyleKey.Header);
            using (new GUILayout.HorizontalScope(headerStyle))
            {
                ShowPrevListButton();
                ShowTodoTitle(listData);
                ShowNextListButton();
                ShowAddListButton();
                ShowDeleteListButton();
            }
        }

        /// <summary>
        /// 進捗ゲージ
        /// </summary>
        private static void ShowProgressGauge(TodoListData listData)
        {
            var doneCount = listData.elementDataList.Count(elementData => elementData.IsDone);
            var allCount = listData.elementDataList.Count;
            var progress = doneCount / (float) allCount;
            
            var gaugeStyle = GUIStyleProvider.Get(StyleKey.ProgressGauge);
            gaugeStyle.fixedWidth = progress * GlobalVariable.WindowRect.width;
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Box("", gaugeStyle);
                GUILayout.FlexibleSpace();
            }
        }

        /// <summary>
        /// 前のリスト表示ボタン
        /// </summary>
        private static void ShowPrevListButton()
        {
            if (UserTodoDataHolder.ListCount <= 1)
            {
                GUILayout.Space(0);
                return;
            }
            
            var style = GUIStyleProvider.Get(StyleKey.PrevListButton);
            var isClickPrevListButton = GUILayout.Button("", style);
            if (!isClickPrevListButton)
            {
                return;
            }
            
            HeaderInputEvent.OnClickChangeListButton(false);
        }
        
        /// <summary>
        /// 次のリスト表示ボタン
        /// </summary>
        private static void ShowNextListButton()
        {
            if (UserTodoDataHolder.ListCount <= 1)
            {
                GUILayout.Space(0);
                return;
            }
            
            var style = GUIStyleProvider.Get(StyleKey.NextListButton);
            var isClickNextListButton = GUILayout.Button("", style);
            if (!isClickNextListButton)
            {
                return;
            }
            
            HeaderInputEvent.OnClickChangeListButton(true);
        }
        
        /// <summary>
        /// Todoリストのタイトル
        /// </summary>
        private static void ShowTodoTitle(TodoListData listData)
        {
            var width = GlobalVariable.WindowRect.width
                        - GUIStyleProvider.GetWidth(StyleKey.PrevListButton)
                        - GUIStyleProvider.GetWidth(StyleKey.NextListButton)
                        - GUIStyleProvider.GetWidth(StyleKey.HeaderAddButton)
                        - GUIStyleProvider.GetWidth(StyleKey.HeaderDeleteButton);
            
            var style = GUIStyleProvider.Get(StyleKey.Title);
            style = TextWrapper.BestFitStyle(listData.title, style, width);
            listData.title = GUILayout.TextField(listData.title, style);
        }
        
        /// <summary>
        /// Todoリスト追加ボタン
        /// </summary>
        private static void ShowAddListButton()
        {
            var style = GUIStyleProvider.Get(StyleKey.HeaderAddButton);
            
            var isClickAddListButton = GUILayout.Button("", style);
            if (!isClickAddListButton)
            {
                return;
            }

            GUI.FocusControl(null);
            HeaderInputEvent.OnClickAddListButton();
        }
        
        /// <summary>
        /// Todoリスト削除ボタン
        /// </summary>
        private static void ShowDeleteListButton()
        {
            if (UserTodoDataHolder.ListCount <= 1)
            {
                GUILayout.Space(0);
                return;
            }
            
            var style = GUIStyleProvider.Get(StyleKey.HeaderDeleteButton);
            
            var isClickDeleteListButton = GUILayout.Button("", style);
            if (!isClickDeleteListButton)
            {
                return;
            }

            GUI.FocusControl(null);
            HeaderInputEvent.OnClickDeleteListButton();
        }
    }
}