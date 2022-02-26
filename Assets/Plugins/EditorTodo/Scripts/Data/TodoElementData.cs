using System;

namespace EditorTodo.Data
{
    [Serializable]
    public class TodoElementData
    {
        public string title;
        public string description;
        public TodoState state;

        public bool IsDone
        {
            get => state == TodoState.Done;
            set => state = value ? TodoState.Done : TodoState.Todo;
        }

        public static TodoElementData Default => new()
        {
            title = "Untitled",
            description = "",
            state = TodoState.Todo
        };
    }

    public enum TodoState
    {
        Todo = 0,
        Done = 1
    }
}