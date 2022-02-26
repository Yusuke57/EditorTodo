using EditorTodo.Data;
using UnityEditor;

namespace EditorTodo.UI
{
    public static class EditorTodoMenu
    {
        [MenuItem("Tools/EditorTodo/OpenWindow")]
        private static void OpenWindow()
        {
            EditorTodoWindow.Open();
        }
        
        [MenuItem("Tools/EditorTodo/DeleteTodoData")]
        private static void DeleteUserData()
        {
            var isDelete = EditorUtility.DisplayDialog("DeleteTodoData",
                "Delete all Todo List Data.\nIs it OK?", "OK", "Cancel");
            if (!isDelete)
            {
                return;
            }
            
            UserTodoDataHolder.Clear();
            EditorTodoWindow.Open();
        }
    }
}