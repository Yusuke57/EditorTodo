using EditorTodo.Helper;
using UnityEngine;

namespace EditorTodo.Data
{
    /// <summary>
    /// ユーザーのデータを管理
    /// </summary>
    public static class UserTodoDataHolder
    {
        private static UserTodoData userTodoData;
        private static UserTodoData UserTodoData => userTodoData ??= JsonHelper.Load();

        private static int currentTodoListIdx;
        private static int CurrentTodoListIdx
        {
            get => currentTodoListIdx;
            set => currentTodoListIdx = (value + ListCount) % ListCount;
        }
        
        public static TodoListData CurrentTodoListData => UserTodoData.todoListDataList[CurrentTodoListIdx];

        public static TodoElementData DisplayElement { get; set; }
        
        public static int ListCount => UserTodoData.todoListDataList.Count;
        public static int ElementCount => CurrentTodoListData.elementDataList.Count;

        public static void Save()
        {
            JsonHelper.Save(UserTodoData);
        }

        public static void Insert(int idx, TodoElementData elementData)
        {
            idx = Mathf.Clamp(idx, 0, ElementCount);
            CurrentTodoListData.elementDataList.Insert(idx, elementData);
        }

        public static void Remove(TodoElementData elementData)
        {
            if (CurrentTodoListData.elementDataList.Contains(elementData))
            {
                CurrentTodoListData.elementDataList.Remove(elementData);
            }
        }

        public static int GetIdx(TodoElementData elementData)
        {
            return CurrentTodoListData.elementDataList.IndexOf(elementData);
        }

        public static TodoElementData GetElementData(int idx)
        {
            if (ElementCount == 0)
            {
                return null;
            }
            
            idx = Mathf.Clamp(idx, 0, ElementCount);
            return CurrentTodoListData.elementDataList[idx];
        }

        public static int GetLastTodoElementIdx()
        {
            var lastTodoElementDataIdx = -1;
            for (var i = 0; i < CurrentTodoListData.elementDataList.Count; i++)
            {
                var elementData = CurrentTodoListData.elementDataList[i];
                var animType = TodoElementAnimator.GetPlayingAnimType(elementData);
                var inputType = TodoElementAnimator.GetPlayingInputType(elementData);
                var isTodoAreaElement = !elementData.IsDone && !(animType == AnimData.AnimType.SlideOut
                                                                 && inputType == AnimData.InputType.ToggleTodo)
                                        || elementData.IsDone && (animType == AnimData.AnimType.SlideOut
                                                                  && inputType == AnimData.InputType.ToggleDone);
                if (isTodoAreaElement)
                {
                    lastTodoElementDataIdx = i;
                }
            }

            return lastTodoElementDataIdx;
        }

        public static void Clear()
        {
            currentTodoListIdx = 0;
            userTodoData = null;
            DisplayElement = null;
            JsonHelper.Clear();
        }
        
        public static void ChangeTodoList(int addIdx)
        {
            CurrentTodoListIdx += addIdx;
        }

        public static void AddTodoList()
        {
            UserTodoData.todoListDataList.Add(TodoListData.Default);
            ChangeTodoList(1);
        }

        public static void DeleteTodoList()
        {
            UserTodoData.todoListDataList.RemoveAt(CurrentTodoListIdx);
            ChangeTodoList(-1);
        }
    }
}