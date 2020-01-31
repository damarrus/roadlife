using System;
using Utils.PlayerPrefsWrappers;

namespace Settings {

    public static class SettingsController {

        public const float MAX_VOLUME = 100f;

        private const string SETTINGS = "Settings";

        private static SettingsValue<float> MUSIC_VOLUME_VALUE = new SettingsValue<float>(50f, new FloatPrefsWrapper($"{SETTINGS}_MusicVolume"));
        private static SettingsValue<float> SOUNDS_VOLUME_VALUE = new SettingsValue<float>(50f, new FloatPrefsWrapper($"{SETTINGS}_SoundsVolume"));
        private static SettingsValue<float> MOUSE_SENS_VALUE = new SettingsValue<float>(1f, new FloatPrefsWrapper($"{SETTINGS}_MouseSens"));
        private static SettingsValue<string> LANGUAGE_VALUE = new SettingsValue<string>(string.Empty, new StringPrefsWrapper($"{SETTINGS}_Language"));

        public static event Action OnMusicVolumeChanged = delegate { };
        public static event Action OnSoundsVolumeChanged = delegate { };
        public static event Action<string> OnLanguageChanged = delegate { };

        public static float MUSIC_VOLUME {
            get {
                return MUSIC_VOLUME_VALUE.Value;
            }
            set {
                MUSIC_VOLUME_VALUE.Value = value;
                OnMusicVolumeChanged.Invoke();
            }
        }

        public static float SOUNDS_VOLUME {
            get {
                return SOUNDS_VOLUME_VALUE.Value;
            }
            set {
                SOUNDS_VOLUME_VALUE.Value = value;
                OnSoundsVolumeChanged.Invoke();
            }
        }

        public static float MOUSE_SENS {
            get {
                return MOUSE_SENS_VALUE.Value;
            }
            set {
                MOUSE_SENS_VALUE.Value = value;
            }
        }

        public static string LANGUAGE {
            get {
                return LANGUAGE_VALUE.Value;
            }
            set {
                LANGUAGE_VALUE.Value = value;
                OnLanguageChanged.Invoke(LANGUAGE_VALUE.Value);
            }
        }
    }
}
