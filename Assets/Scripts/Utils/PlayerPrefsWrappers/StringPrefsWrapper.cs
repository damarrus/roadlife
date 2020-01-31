using UnityEngine;

namespace Utils.PlayerPrefsWrappers {
    public class StringPrefsWrapper : AbstractPrefsWrapper<string> {

        public StringPrefsWrapper(string key) : base(key) {
        }

        public StringPrefsWrapper(string key, string value) : base(key, value) {
        }

        public override string GetFromPrefs() {
            return PlayerPrefs.GetString(Key);
        }

        public override void SetToPrefs(string value) {
            PlayerPrefs.SetString(Key, value);
        }
    }
}
