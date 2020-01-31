using System;

namespace MadaoEcs {
    public class EventNotRegisteredException : Exception {

        public Type EventType;

        public EventNotRegisteredException(Type eventType) {
            EventType = eventType;
        }
    }
}
