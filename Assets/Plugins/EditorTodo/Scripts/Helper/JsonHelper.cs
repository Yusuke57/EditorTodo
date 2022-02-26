using System.IO;
using EditorTodo.Data;
using UnityEngine;

namespace EditorTodo.Helper
{
    public static class JsonHelper
    {
        private static string JsonDirPath => $"{Application.dataPath}/{EDITOR_TODO_ROOT_DIR_PATH_UNDER_ASSETS}";
        private static string JsonPath => $"{JsonDirPath}/{JSON_FILE_NAME}";
        private static string JsonMetaPath => JsonPath + ".meta";
        
        /// <summary>
        /// データを保存するJsonファイルを格納するディレクトリパス（Assets/ 以下）
        /// 必要に応じて変更する
        /// </summary>
        private const string EDITOR_TODO_ROOT_DIR_PATH_UNDER_ASSETS = "Plugins/EditorTodo";
        
        private const string JSON_FILE_NAME = "UserTodoData.json";

        /// <summary>
        /// Jsonからデータを読み込む
        /// </summary>
        public static UserTodoData Load()
        {
            if (!Directory.Exists(JsonDirPath))
            {
                Debug.LogError($"[EditorTodo]データ保存用のディレクトリが見つかりません: {JsonDirPath}\n" +
                               $"作成するか、JsonHelper.csの[EDITOR_TODO_ROOT_DIR_PATH_UNDER_ASSETS]を書き換えてください");
            }
            
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
            if (!Directory.Exists(JsonDirPath))
            {
                Debug.LogError($"[EditorTodo]データ保存用のディレクトリが見つかりません: {JsonDirPath}\n" +
                               $"作成するか、JsonHelper.csの[EDITOR_TODO_ROOT_DIR_PATH_UNDER_ASSETS]を書き換えてください");
                return;
            }
            
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