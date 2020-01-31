using System;
using UnityEngine;

namespace Utils.Timer {
    public class CurveTimer : MonoBehaviour {

        [SerializeField] private AnimationCurve curve;

        private float duration;
        private Action<float, bool> onTick;
        private bool isStarted;
        private float startTime;

        public void Set(float duration, Action<float, bool> onTick) {
            this.duration = duration;
            this.onTick = onTick;
        }

        public void StartTimer() {
            startTime = Time.time;
            isStarted = true;
        }

        public void Pause() {
            isStarted = false;
        }

        public void Continue() {
            isStarted = true;
        }

        void Update() {
            if (!isStarted) return;

            var timePassed = Time.time - startTime;
            var value = Mathf.Clamp01(timePassed / duration);
            var curveValue = curve.Evaluate(value);
            if (float.IsNaN(curveValue)) return;

            var isFinished = value == 1;

            onTick.Invoke(curveValue, isFinished);
            if (isFinished) {
                isStarted = false;
            }
        }
    }
}
