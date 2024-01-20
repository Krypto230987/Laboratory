using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homm28;
namespace Homm28
{
    public class MyLinkedList<T>
    {
        private MyNode<T> head;
        private MyNode<T> tail;
        private int count;

        public MyNode<T> Add(T element)
        {
            MyNode<T> newNode = new MyNode<T>(element);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }

            count++;
            return newNode;
        }

        public int Count
        {
            get { return count; }
        }

        public MyNode<T> First
        {
            get { return head; }
        }

        public MyNode<T> Last
        {
            get { return tail; }
        }
    }
}
