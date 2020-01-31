using UnityEngine;

namespace MultiLanguage {
    public class TextState : MonoBehaviour, ITranslatable {

        [SerializeField] private TMPro.TextMeshProUGUI textMesh;
        [SerializeField] private string translationKey;
        [SerializeField] private bool isAutofillableOnStart;

        private object[] translationParams = new object[0];

        public TMPro.TextMeshProUGUI TextMesh => textMesh;

        void Start() {
            if (isAutofillableOnStart) {
                UpdateTranslation();
            }
        }

        public void SetTextKey(string translationKey, params object[] translationParams) {
            this.translationKey = translationKey;
            this.translationParams = translationParams;
            UpdateTranslation();
        }

        public void SetTextKey(TextStateData textData) {
            translationKey = textData.TranslationKey;
            translationParams = textData.Params;
            UpdateTranslation();
        }

        public void SetText(string text) {
            this.translationKey = string.Empty;
            this.translationParams = new object[0];
            textMesh.text = text;
        }

        public void UpdateTranslation() {
            if (!string.IsNullOrEmpty(translationKey)) {
                var text = I18N.GetString(translationKey);
                textMesh.text = string.Format(text, translationParams);
            }
        }

#if UNITY_EDITOR
        void OnValidate() {
            if (textMesh == null) {
                textMesh = GetComponent<TMPro.TextMeshProUGUI>();
            }
        }
#endif 
    }
}
