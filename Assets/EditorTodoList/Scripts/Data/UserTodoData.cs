using System;
using System.Collections.Generic;

namespace EditorTodo.Data
{
    [Serializable]
    public class UserTodoData
    {
        public List<TodoListData> todoListDataList;

        public static UserTodoData Default => new()
        {
            todoListDataList = new List<TodoListData> { TodoListData.Default }
        };
    }
}