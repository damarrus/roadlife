

namespace RaidHealer {
    public class LinkedListNode<T> {

        public T Value;
        public LinkedListNode<T> Next;
        public LinkedListNode<T> Prev;

        public LinkedListNode(T value) {
            Value = value;
        }
    }
}
