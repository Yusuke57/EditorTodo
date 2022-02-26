using EditorTodo.Data;

namespace EditorTodo.UI
{
    public static class WindowView
    {
        public static void Show()
        {
            var todoListData = UserTodoDataHolder.CurrentTodoListData;
            HeaderView.Show(todoListData);
            TodoListView.Show(todoListData);

            var displayElement = UserTodoDataHolder.DisplayElement;
            TodoDetailView.Show(displayElement);
        }
    }
}