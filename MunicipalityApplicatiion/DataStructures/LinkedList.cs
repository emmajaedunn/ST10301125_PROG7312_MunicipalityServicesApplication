using System;

namespace MunicipalityApplicatiion.DataStructures
{
    /// <summary>
    /// Minimal singly-linked list with Add, GetAt, RemoveAt, Count, ForEach.
    /// Avoids built-in generic collections and arrays for primary storage.
    /// </summary>
    public class LinkedList<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;
        private int _count;

        public int Count => _count;

        public void Add(T item)
        {
            var node = new Node<T>(item);
            if (_head == null)
            {
                _head = _tail = node;
            }
            else
            {
                _tail!.Next = node;
                _tail = node;
            }
            _count++;
        }

        public T GetAt(int index)
        {
            if (index < 0 || index >= _count) throw new ArgumentOutOfRangeException(nameof(index));
            var current = _head; int i = 0;
            while (current != null)
            {
                if (i == index) return current.Value;
                current = current.Next; i++;
            }
            throw new InvalidOperationException("Index traversal failed.");
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= _count) return false;
            if (index == 0 && _head != null)
            {
                _head = _head.Next;
                if (_head == null) _tail = null;
                _count--; return true;
            }
            var prev = _head; int i = 0;
            while (prev != null && i < index - 1)
            {
                prev = prev.Next; i++;
            }
            if (prev?.Next == null) return false;
            if (prev.Next == _tail) _tail = prev;
            prev.Next = prev.Next.Next;
            _count--; return true;
        }

        public void ForEach(Action<T> action)
        {
            var current = _head;
            while (current != null)
            {
                action(current.Value);
                current = current.Next;
            }
        }
    }
}