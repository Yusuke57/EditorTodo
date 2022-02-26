using System.Collections.Generic;
using UnityEngine;

namespace EditorTodo.Helper
{
    public static class TextWrapper
    {
        /// <summary>
        /// 文字列が領域外にはみ出る分は「...」にする
        /// </summary>
        public static string Ellipsis(string originText, GUIStyle style, float maxWidth, int maxLine)
        {
            var lines = new List<string>();
            var line = "";
            var idx = 0;
            while (idx < originText.Length)
            {
                var lineWidth = style.CalcSize(new GUIContent(line + originText[idx])).x;
                if (lineWidth < maxWidth)
                {
                    line += originText[idx];
                    idx++;
                    continue;
                }
                
                lines.Add(line);
                line = "";

                if (lines.Count >= maxLine)
                {
                    break;
                }
            }

            if (!string.IsNullOrEmpty(line))
            {
                lines.Add(line);
            }

            var wrappedText = string.Join("\n", lines);

            if (idx < originText.Length)
            {
                wrappedText = wrappedText[..^1];
                wrappedText += "...";
            }

            return wrappedText;
        }

        /// <summary>
        /// 領域内に入るフォントサイズにしたGUIStyleを返す
        /// </summary>
        public static GUIStyle BestFitStyle(string originText, GUIStyle style, float maxWidth)
        {
            var dumpedStyle = new GUIStyle(style);
            while (dumpedStyle.fontSize > 0)
            {
                var lineWidth = dumpedStyle.CalcSize(new GUIContent(originText)).x;
                if (lineWidth < maxWidth)
                {
                    return dumpedStyle;
                }

                dumpedStyle.fontSize--;
            }

            return dumpedStyle;
        }
    }
}