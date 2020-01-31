using UnityEngine;

namespace Utils.PlayerPrefsWrappers {
    public class BoolPrefsWrapper : AbstractPrefsWrapper<bool> {

        public BoolPrefsWrapper(string key) : base(key) {
        }

        public BoolPrefsWrapper(string key, bool value) : base(key, value) {
        }

        public override bool GetFromPrefs() {
            return PlayerPrefs.GetInt(Key) > 0;
        }

        public override void SetToPrefs(bool value) {
            PlayerPrefs.SetInt(Key, value ? 1 : 0);
        }
    }
}
