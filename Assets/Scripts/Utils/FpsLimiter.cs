using UnityEngine;

namespace Utils {
    public class FpsLimiter : MonoBehaviour {

        void Awake() {
            Application.targetFrameRate = 60;
        }
    }
}
