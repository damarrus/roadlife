using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Editor.Tools.ButtonToUsefull {
    public class ButtonConverter : MonoBehaviour {

        [MenuItem("CONTEXT/Button/To usefull button")]
        private static void ConvertButtonToUsefull(MenuCommand command) {
            ConvertButton<UsefullButton>(command);
        }

        [MenuItem("CONTEXT/Button/To button")]
        private static void ConvertButtonToButton(MenuCommand command) {
            ConvertButton<Button>(command);
        }

        private static void ConvertButton<To>(MenuCommand command) where To : Button {

            var gameObject = GetGameObject(command);
            var oldButton = gameObject.GetComponent<Button>();
            if (Equals(oldButton.GetType(), typeof(To))) {
                Debug.Log($"Button type already equal to {typeof(To).ToString()}");
                return;
            }

            var oldButtonInfo = new ButtonInfo(oldButton);
            DestroyImmediate(oldButton);

            var newButton = gameObject.AddComponent<To>();
            oldButtonInfo.ApplyToButton(newButton);
        }

        private static GameObject GetGameObject(MenuCommand command) {
            return ((Button)command.context).gameObject;
        }

        private struct ButtonInfo {
            private bool interactable;
            private Selectable.Transition transition;
            private SpriteState spriteState;
            private ColorBlock colorBlock;
            private Button.ButtonClickedEvent onClick;
            private AnimationTriggers triggers;

            public ButtonInfo(Button button) {
                interactable = button.interactable;
                transition = button.transition;
                spriteState = button.spriteState;
                colorBlock = button.colors;
                onClick = button.onClick;
                triggers = button.animationTriggers;
            }

            public void ApplyToButton(Button button) {
                button.interactable = interactable;
                button.transition = transition;
                button.spriteState = spriteState;
                button.colors = colorBlock;
                button.onClick = onClick;
                button.animationTriggers = triggers;
            }
        }
    }
}
