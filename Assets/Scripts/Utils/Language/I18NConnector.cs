using Settings;
using UnityEngine;

namespace MultiLanguage {
    public class I18NConnector : MonoBehaviour {

        [SerializeField] private I18NController I18NController;

        private static I18NConnector instance;

        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start() {
            SettingsController.OnLanguageChanged += I18NController.SwitchClientLanguage;
            //OnServerLanguageLoad += I18NController.UpdateUI
        }
    }
}
