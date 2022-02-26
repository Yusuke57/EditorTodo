using EditorTodo.Data;
using UnityEngine;

namespace EditorTodo.InputEvent
{
    /// <summary>
    /// ヘッダーについてのユーザー操作処理
    /// </summary>
    public static class HeaderInputEvent
    {
        /// <summary>
        /// 前/次のTodoリスト表示ボタン
        /// </summary>
        public static void OnClickChangeListButton(bool isNext)
        {
            GUI.FocusControl(null);
            UserTodoDataHolder.ChangeTodoList(isNext ? 1 : -1);
        }

        /// <summary>
        /// Todoリスト追加ボタン
        /// </summary>
        public static void OnClickAddListButton()
        {
            GUI.FocusControl(null);
            UserTodoDataHolder.AddTodoList();
        }
        
        /// <summary>
        /// Todoリスト削除ボタン
        /// </summary>
        public static void OnClickDeleteListButton()
        {
            GUI.FocusControl(null);
            UserTodoDataHolder.DeleteTodoList();
        }
    }
}