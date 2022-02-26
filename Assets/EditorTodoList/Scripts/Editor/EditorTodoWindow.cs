using EditorTodo.Data;
using EditorTodo.Helper;
using UnityEditor;
using UnityEngine;

namespace EditorTodo.UI
{
    public class EditorTodoWindow : EditorWindow
    {
        private const string TITLE_NAME = "Todo";
        private const string FAVICON_FIND_PARAM = "favicon t:Texture2D";

        public static void Open()
        {
            Init();
            
            var window = (EditorTodoWindow) GetWindow(typeof(EditorTodoWindow));
            
            // タイトルとfavicon設定
            var faviconGuids = AssetDatabase.FindAssets(FAVICON_FIND_PARAM);
            if (faviconGuids.Length > 0)
            {
                var faviconPath = AssetDatabase.GUIDToAssetPath(faviconGuids[0]);
                var favicon = AssetDatabase.LoadAssetAtPath(faviconPath, typeof(Texture2D)) as Texture2D;
                window.titleContent = new GUIContent(TITLE_NAME, favicon);
            }
            else
            {
                window.titleContent = new GUIContent(TITLE_NAME);
            }

            window.minSize = new Vector2(240, 480);
            window.Show();
        }

        private static void Init()
        {
            GUIStyleProvider.Init();
        }

        private void OnDisable()
        {
            UserTodoDataHolder.Save();
        }

        private void OnGUI()
        {
            GlobalVariable.WindowRect = position;
            WindowView.Show();
        }

        private void Update()
        {
            if (focusedWindow != this)
            {
                GUI.FocusControl(null);
            }
            
            if (mouseOverWindow != this)
            {
                return;
            }

            Repaint();
        }
    }
}