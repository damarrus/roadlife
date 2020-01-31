using System;
using UnityEngine;

namespace Utils.PlayerPrefsWrappers {
    public class EnumPrefsWrapper<T> : AbstractPrefsWrapper<T> where T : struct, IConvertible {

        public EnumPrefsWrapper(string key) : base(key) {
        }

        public EnumPrefsWrapper(string key, T value) : base(key, value) {
        }

        public override T GetFromPrefs() {
            return (T)(object)PlayerPrefs.GetInt(Key);
        }

        public override void SetToPrefs(T value) {
            PlayerPrefs.SetInt(Key, (int)(object)value);
        }
    }
}
