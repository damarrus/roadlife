namespace MultiLanguage {
    public struct TextStateData {
        public string TranslationKey;
        public object[] Params;

        public TextStateData(string translationKey, params object[] translationParams) {
            TranslationKey = translationKey;
            Params = translationParams;
        }
    }
}
