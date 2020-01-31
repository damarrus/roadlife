using System;

namespace MadaoEcs {

    public class OnEventFire : Attribute {

        public int Order;

        public OnEventFire(int order = 0) {
            Order = order;
        }
    }
}
