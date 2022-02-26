using System;
using System.Collections.Generic;

namespace EditorTodo.Data
{
    [Serializable]
    public class TodoListData
    {
        public string title;
        public List<TodoElementData> elementDataList;

        public static TodoListData Default => new()
        {
            title = "Todo List",
            elementDataList = new List<TodoElementData> { TodoElementData.Default }
        };
    }
}