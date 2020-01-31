using UnityEngine;

namespace Utils.PlayerPrefsWrappers {
    public abstract class AbstractPrefsWrapper<T> {

        public string Key { get; protected set; }

        public AbstractPrefsWrapper(string key) {
            Key = key;
        }

        public AbstractPrefsWrapper(string key, T value) : this(key) {
            SetToPrefs(value);
        }

        public abstract T GetFromPrefs();
        public abstract void SetToPrefs(T value);

        public bool HasValue() {
            return PlayerPrefs.HasKey(Key);
        }
    }
}
