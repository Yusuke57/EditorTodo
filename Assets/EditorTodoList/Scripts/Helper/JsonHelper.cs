using System.IO;
using EditorTodo.Data;
using UnityEngine;

namespace EditorTodo.Helper
{
    public static class JsonHelper
    {
        private static string JsonPath => Application.dataPath + JSON_PATH_UNDER_ASSETS;
        private static string JsonMetaPath => JsonPath + ".meta";
        
        private const string JSON_PATH_UNDER_ASSETS = "/EditorTodoList/UserTodoData.json";

        /// <summary>
        /// Jsonからデータを読み込む
        /// </summary>
        public static UserTodoData Load()
        {
            if (!File.Exists(JsonPath))
            {
                return UserTodoData.Default;
            }
            
            using var reader = new StreamReader(JsonPath);
            var json = reader.ReadToEnd();
            if (string.IsNullOrWhiteSpace(json))
            {
                return UserTodoData.Default;
            }
            
            var userTodoData = JsonUtility.FromJson<UserTodoData>(json);
            return userTodoData;
        }

        /// <summary>
        /// Jsonにデータを保存する
        /// </summary>
        public static void Save(UserTodoData userTodoData)
        {
            userTodoData ??= UserTodoData.Default;

            StreamWriter writer;
            if (!File.Exists(JsonPath))
            {
                var fileStream = File.Create(JsonPath);
                writer = new StreamWriter(fileStream);
            }
            else
            {
                writer = new StreamWriter(JsonPath, false);
            }

            using (writer)
            {
                var json = JsonUtility.ToJson(userTodoData);
                writer.WriteLine(json);
                writer.Flush();
            }
        }

        public static void Clear()
        {
            if (File.Exists(JsonPath))
            {
                File.Delete(JsonPath);
            }
            if (File.Exists(JsonMetaPath))
            {
                File.Delete(JsonMetaPath);
            }
        }
    }
}