using Settings;
using System.Linq;
using UnityEngine;

namespace MultiLanguage {
    public class I18NController : MonoBehaviour {

        private static I18NController instance;

        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguage();
        }

        private void LoadLanguage() {
            if (string.IsNullOrEmpty(SettingsController.LANGUAGE)) {
                SettingsController.LANGUAGE = I18N.GetSystemLanguage();
            }

            I18N.SwitchLanguage(SettingsController.LANGUAGE);
        }

        public void SwitchClientLanguage(string language) {
            I18N.SwitchLanguage(language);
        }

        public void SwitchUILanguage() {
            foreach (var item in Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)).OfType<ITranslatable>()) {
                item.UpdateTranslation();
            }
        }
    }
}
