using EditorTodo.Data;
using UnityEngine;

namespace EditorTodo.InputEvent
{
    /// <summary>
    /// 詳細表示ウィンドウの操作処理
    /// </summary>
    public static class DetailInputEvent
    {
        /// <summary>
        /// 閉じるボタンをクリックした時
        /// </summary>
        public static void OnClickCloseButton()
        {
            GUI.FocusControl(null);
            UserTodoDataHolder.DisplayElement = null;
        }
    }
}