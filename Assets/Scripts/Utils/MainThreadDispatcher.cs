using System.Collections;
using UnityEngine;

namespace Utils.MainThreadDispatcher {
    public class MainThreadDispatcher : MonoBehaviour {

        private static MainThreadDispatcher instance;

        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static Coroutine StartRoutine(IEnumerator routine) {
            return instance.StartCoroutine(routine);
        }
    }
}
