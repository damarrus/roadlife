using UnityEngine;

namespace Utils {
    public static class TimeController {

        public static float DefaultFixedDelta;

        static TimeController() {
            DefaultFixedDelta = Time.fixedDeltaTime;
        }

        public static void SetTimeScale(float scale) {
            Time.timeScale = scale;
            Time.fixedDeltaTime = DefaultFixedDelta * scale;
        }
    }
}
