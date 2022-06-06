using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_Doubly_Linked_List
{
    public class DoublyLinkedList<T>
    {
        public int Count { get; private set; }
        public Node Head { get; private set; }
        public Node Tail { get; private set; }

        public bool Empty => this.Count == 0;

        public void AddFirst(T value)
        {
            if (this.Empty)
            {
                this.Head = this.Tail = new Node(value);
            }
            else
            {
                var newHead = new Node(value);
                newHead.NextNode = this.Head;
                this.Head.PreviousNode = newHead;
                this.Head = newHead;
            }

            this.Count++;
        }

        public void AddLast(T value)
        {
            if (this.Empty)
            {
                this.Head = this.Tail = new Node(value);
            }
            else
            {
                var newTail = new Node(value);
                newTail.PreviousNode = this.Tail;
                this.Tail.NextNode = newTail;
                this.Tail = newTail;
            }

            this.Count++;
        }

        public T RemoveFirst()
        {
            if (this.Empty)
            {
                throw new InvalidOperationException("The list is empty");
            }

            T nodeValue = this.Head.Value;
            this.Head = this.Head.NextNode;

            if (this.Head != null)
            {
                this.Head.PreviousNode = null;
            }
            else
            {
                this.Tail = null;
            }

            this.Count--;

            return nodeValue;
        }

        public T RemoveLast()
        {
            T nodeValue;

            if (this.Empty)
            {
                throw new InvalidOperationException("The list is empty");
            }

            nodeValue = this.Tail.Value;
            this.Tail = this.Tail.PreviousNode;

            if (this.Tail != null)
            {
                this.Tail.NextNode = null;
            }
            else
            {
                this.Head = null;
            }

            this.Count--;

            return nodeValue;
        }

        public void ForEach(Action<T> action)
        {
            var currentNode = this.Head;
            while (currentNode != null)
            {
                action(currentNode.Value);
                currentNode = currentNode.NextNode;
            }
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];

            int counter = 0;

            var currentNode = this.Head;
            while (currentNode != null)
            {
                array[counter++] = currentNode.Value;
                currentNode = currentNode.NextNode;
            }

            return array;
        }

        public List<T> ToList()
        {
            var list = new List<T>();

            var currentNode = this.Head;
            while (currentNode != null)
            {
                list.Add(currentNode.Value);
                currentNode = currentNode.NextNode;
            }

            return list;
        }

        public class Node
        {
            public T Value { get; set; }
            public Node PreviousNode { get; set; }
            public Node NextNode { get; set; }

            public Node(T value)
            {
                this.Value = value;
            }
        }
    }
}
