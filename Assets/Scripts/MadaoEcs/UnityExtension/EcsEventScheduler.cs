using System;
using System.Collections;
using UnityEngine;
using Utils.MainThreadDispatcher;

namespace MadaoEcs {
    public class EcsEventScheduler {

        public static void SendEvent(IEntity entity, Event eventInstance, float delay) {
            MainThreadDispatcher.StartRoutine(SendEventWithDelay(entity, eventInstance, delay));
        }

        public static void SendEvent(Node node, Event eventInstance, float delay) {
            MainThreadDispatcher.StartRoutine(SendEventWithDelay(node.Entity, eventInstance, delay));
        }

        public static void SendEventWithGameDelay(Node node, Event eventInstance, float delay) {
            MainThreadDispatcher.StartRoutine(SendEventWithGameDelay(node.Entity, eventInstance, delay));
        }

        public static void DoActionAfterDelay(float delay, Action action) {
            MainThreadDispatcher.StartRoutine(DoActionAfterDelayRoutine(delay, action));
        }

        public static void DoActionAfterGameDelay(float delay, Action action) {
            MainThreadDispatcher.StartRoutine(DoActionAfterGameTimeDelayRoutine(delay, action));
        }

        private static IEnumerator SendEventWithDelay(IEntity entity, Event eventInstance, float delay) {
            yield return new WaitForSecondsRealtime(delay);
            entity.SendEvent(eventInstance);
        }

        private static IEnumerator SendEventWithGameDelay(IEntity entity, Event eventInstance, float delay) {
            yield return new WaitForSeconds(delay);
            entity.SendEvent(eventInstance);
        }

        private static IEnumerator DoActionAfterDelayRoutine(float delay, Action action) {
            yield return new WaitForSecondsRealtime(delay);
            action.Invoke();
        }
        private static IEnumerator DoActionAfterGameTimeDelayRoutine(float delay, Action action) {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }
    }
}
