using System.Collections;
using UnityEngine;

namespace UI {
    public class FpsDisplay : MonoBehaviour {

        [SerializeField] private TMPro.TextMeshProUGUI text;

        float count;

        private IEnumerator Start() {
            GUI.depth = 2;
            while (true) {
                yield return new WaitForSeconds(0.1f);
                count = (1 / Time.deltaTime);
                text.text = "FPS :" + (Mathf.Round(count));
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}