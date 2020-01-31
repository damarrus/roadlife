using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using Settings;

namespace MultiLanguage {
    public static class I18N {

        private static string language;
        private static Dictionary<string, string> data;

        private static readonly string languageFolder = $@"{Application.streamingAssetsPath}\LanguageResources";
        private const string fileName = "translation.txt";
        private const char separator = '=';

        public static string GetSystemLanguage() {
            switch (Application.systemLanguage) {
                case SystemLanguage.Russian:
                case SystemLanguage.Ukrainian:
                    return "ru";
                default:
                    return "en";
            }
        }

        public static void SwitchLanguage(string language) {
            I18N.language = language;
            LoadDataFromClientFile();
        }

        public static void AddTranslations(Dictionary<string, string> translations) {
            foreach (var item in translations) {
                data.Add(item.Key, item.Value);
            }
        }

        public static string GetString(string key) {
            if (data.ContainsKey(key)) {
                return data[key];
            }
            return string.Empty;
        }

        private static void LoadDataFromClientFile() {
            var path = $@"{languageFolder}\{language}-{language}.{fileName}";
            var lines = GetLinesFromFile(path);
            data = new Dictionary<string, string>();

            foreach (var line in lines) {
                if (IsLineTranslation(line)) {
                    var separatorIndex = line.IndexOf(separator);
                    var key = line.Substring(0, separatorIndex);
                    var value = ParseEscapeCharacters(line.Substring(separatorIndex + 1));
                    data.Add(key, value);
                }
            }
        }

        private static string[] GetLinesFromFile(string path) {
            var www = UnityEngine.Networking.UnityWebRequest.Get(path);
            www.SendWebRequest();
            while (!www.isDone) {
            }
            var fileContentString = www.downloadHandler.text;
            return fileContentString.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
        }

        private static string ParseEscapeCharacters(string line) {
            return line.Replace("\\n", "\n");
        }

        private static bool IsLineTranslation(string line) {
            return line.Contains(separator);
        }
    }
}
