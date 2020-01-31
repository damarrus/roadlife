using System.Collections.Generic;
using System.Linq;

namespace RaidHealer {
    public class LinkedList<T> {

        private LinkedListNode<T> currentNode;
        private LinkedListNode<T> firstNode;

        public T CurrentValue => currentNode.Value;
        public LinkedListNode<T> CurrentNode => currentNode;


        public LinkedList(IEnumerable<T> collection, bool isLooped) {
            LinkedListNode<T> prevNode = null;

            var length = collection.Count();
            var i = 0;

            foreach (var item in collection) {

                var newNode = new LinkedListNode<T>(item);
                if (i == 0) {
                    currentNode = newNode;
                }

                if (prevNode != null) {
                    newNode.Prev = prevNode;
                    prevNode.Next = newNode;
                }
                prevNode = newNode;
                if (isLooped && i == length - 1) {
                    currentNode.Prev = newNode;
                    newNode.Next = currentNode;
                }
                i++;
            }
            firstNode = currentNode;
        }

        public bool IsFirstNode(LinkedListNode<T> node) {
            return node == firstNode;
        }

        public T Next() {
            currentNode = currentNode.Next;
            return CurrentValue;
        }
    }
}
