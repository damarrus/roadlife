using MultiLanguage;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UsefullButton : Button
    {

        [SerializeField] protected TextState buttonText;

        public TextState ButtonText
        {
            get
            {
                if (buttonText == null)
                {
                    buttonText = GetComponentInChildren<TextState>(true);
                }
                return buttonText;
            }
            set { buttonText = value; }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (buttonText == null)
            {
                buttonText = GetComponentInChildren<TextState>(true);
            }
        }
#endif
    }
}