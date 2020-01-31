using UnityEngine;

namespace Utils.PlayerPrefsWrappers {
    public class IntPrefsWrapper : AbstractPrefsWrapper<int> {

        public IntPrefsWrapper(string key) : base(key) {
        }

        public IntPrefsWrapper(string key, int value) : base(key, value) {
        }

        public override int GetFromPrefs() {
            return PlayerPrefs.GetInt(Key);
        }

        public override void SetToPrefs(int value) {
            PlayerPrefs.SetInt(Key, value);
        }
    }
}
