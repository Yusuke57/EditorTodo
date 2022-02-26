using System.Collections.Generic;
using EditorTodo.Data;
using UnityEditor;
using UnityEngine;

namespace EditorTodo.Helper
{
    /// <summary>
    /// GUIStyleをAssetDatabaseから探してきて提供する
    /// </summary>
    public static class GUIStyleProvider
    {
        private static readonly Dictionary<StyleKey, GUIStyleData> StyleDataDictionary = new();

        private static bool isLoaded = false;

        public static void Init()
        {
            StyleDataDictionary.Clear();
            isLoaded = false;
        }
        
        public static GUIStyle Get(StyleKey key)
        {
            if (!isLoaded)
            {
                LoadStyles();
                isLoaded = true;
            }

            var style = StyleDataDictionary.ContainsKey(key) ? StyleDataDictionary[key].style : GUIStyle.none;
            return new GUIStyle(style);  // 変更されても良いようにディープコピーして返す
        }

        public static float GetWidth(StyleKey key)
        {
            if (!isLoaded)
            {
                LoadStyles();
                isLoaded = true;
            }

            var style = StyleDataDictionary.ContainsKey(key) ? StyleDataDictionary[key].style : GUIStyle.none;
            var width = style.fixedWidth + style.margin.horizontal + style.padding.horizontal;
            return width;
        }
        
        public static float GetHeight(StyleKey key)
        {
            if (!isLoaded)
            {
                LoadStyles();
                isLoaded = true;
            }

            var style = StyleDataDictionary.ContainsKey(key) ? StyleDataDictionary[key].style : GUIStyle.none;
            var height = style.fixedHeight + style.margin.vertical + style.padding.vertical;
            return height;
        }

        private static void LoadStyles()
        {
            var guids = AssetDatabase.FindAssets("t:" + nameof(GUIStyleData));
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath (guid);
                var styleData = AssetDatabase.LoadAssetAtPath<GUIStyleData>(path);
                if (StyleDataDictionary.ContainsKey(styleData.key))
                {
                    continue;
                }
                
                StyleDataDictionary.Add(styleData.key, styleData);
            }
        }
    }
}