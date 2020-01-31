using System;
using System.Collections.Generic;
using UnityEngine;

namespace RaidHealer.UI {
    public class AnimatorEventor : MonoBehaviour {

        [SerializeField] private Animator animator;

        public Animator Animator => animator;

        public Dictionary<string, Action> EventHandlers = new Dictionary<string, Action>();

        public void AnimationEvent(AnimationEvent e) {

            var key = e.stringParameter;
            if (string.IsNullOrEmpty(key)) return;

            if (EventHandlers.TryGetValue(key, out var action)) {
                action.Invoke();
            }
        }
    }
}
