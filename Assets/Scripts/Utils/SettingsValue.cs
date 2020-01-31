using Utils.PlayerPrefsWrappers;

namespace Settings {
    public class SettingsValue<T> {

        private AbstractPrefsWrapper<T> storage;

        public T DefaultValue { get; private set; }

        public T Value {
            get {
                if (!storage.HasValue()) {
                    storage.SetToPrefs(DefaultValue);
                }
                return storage.GetFromPrefs();
            }
            set {
                storage.SetToPrefs(value);
            }
        }

        public SettingsValue(T defaultValue, AbstractPrefsWrapper<T> storage) {
            DefaultValue = defaultValue;
            this.storage = storage;
        }
    }
}
