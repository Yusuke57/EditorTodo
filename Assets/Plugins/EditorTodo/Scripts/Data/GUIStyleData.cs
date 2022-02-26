using UnityEngine;

namespace EditorTodo.Data
{
    //[CreateAssetMenu(fileName = "GUIStyleData", menuName = "EditorTodoList/GUIStyleData", order = 0)]
    public class GUIStyleData : ScriptableObject
    {
        public StyleKey key;
        public GUIStyle style;
    }
    
    public enum StyleKey
    {
        // Header
        Header = 0,
        ProgressGauge = 5,
        HeaderAddButton = 7,
        HeaderDeleteButton = 8,
        Title = 10,
        PrevListButton = 12,
        NextListButton = 13,
        
        // List
        AddButton = 15,
        
        // Element
        ElementBody = 20,
        Toggle = 30,
        MoveUpButton = 50,
        MoveDownButton = 51,
        DeleteButton = 80,
        
        // Detail
        DetailWindow = 100,
        DetailLabel = 110,
        CloseButton = 120,
    }
}