using UnityEngine;

namespace Utils.PlayerPrefsWrappers {
    public class FloatPrefsWrapper : AbstractPrefsWrapper<float> {

        public FloatPrefsWrapper(string key) : base(key) {
        }

        public FloatPrefsWrapper(string key, float value) : base(key, value) {
        }

        public override float GetFromPrefs() {
            return PlayerPrefs.GetFloat(Key);
        }

        public override void SetToPrefs(float value) {
            PlayerPrefs.SetFloat(Key, value);
        }
    }
}
