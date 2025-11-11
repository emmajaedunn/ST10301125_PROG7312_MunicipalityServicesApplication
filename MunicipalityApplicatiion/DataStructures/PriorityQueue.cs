using System;
using System.Collections.Generic;
using System.Threading;

namespace MunicipalityApplicatiion.DataStructures
{
    /// <summary>
    /// A priority queue that stores items along with a priority value.
    /// Higher priority values are dequeued before lower ones.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the queue.</typeparam>
    public class PriorityQueue<T>
    {
        // List to store items with their priority and insertion sequence
        private readonly List<(T item, int priority, long seq)> _data = new();
        private static long _seqGen = 0;

        /// <summary>
        /// Returns the number of items in the queue.
        /// </summary>
        public int Count => _data.Count;

        /// <summary>
        /// Inserts an item with a given priority.
        /// </summary>
        public void Insert(T item, int priority)
        {
            var seq = Interlocked.Increment(ref _seqGen);
            _data.Add((item, priority, seq));
            HeapifyUp(_data.Count - 1);
        }

        /// <summary>
        /// Removes and returns the highest-priority item.
        /// </summary>
        public T ExtractMax()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("Priority queue is empty.");

            var ret = _data[0].item;
            _data[0] = _data[^1];
            _data.RemoveAt(_data.Count - 1);
            if (_data.Count > 0)
                HeapifyDown(0);

            return ret;
        }

        /// <summary>
        /// Returns the highest-priority item without removing it.
        /// </summary>
        public T Peek()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("Priority queue is empty.");
            return _data[0].item;
        }

        /// <summary>
        /// Removes all items from the queue.
        /// </summary>
        public void Clear() => _data.Clear();

        // Internal helpers

        private static bool Greater((T item, int priority, long seq) a,
                                    (T item, int priority, long seq) b)
        {
            // Higher priority first; if equal, earlier insertion wins
            if (a.priority != b.priority)
                return a.priority > b.priority;
            return a.seq < b.seq;
        }

        private void HeapifyUp(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (!Greater(_data[i], _data[p])) break;
                (_data[i], _data[p]) = (_data[p], _data[i]);
                i = p;
            }
        }

        private void HeapifyDown(int i)
        {
            while (true)
            {
                int l = 2 * i + 1, r = l + 1, best = i;
                if (l < _data.Count && Greater(_data[l], _data[best])) best = l;
                if (r < _data.Count && Greater(_data[r], _data[best])) best = r;
                if (best == i) break;
                (_data[i], _data[best]) = (_data[best], _data[i]);
                i = best;
            }
        }

        /// <summary>
        /// Creates a shallow clone of the priority queue (internal items are not cloned).
        /// Useful to perform destructive operations (ExtractMax) on a clone.
        /// </summary>
        public PriorityQueue<T> Clone()
        {
            var q = new PriorityQueue<T>();
            // copy internal array (shallow copy of tuples)
            q._data.AddRange(_data);
            return q;
        }

        /// <summary>
        /// Returns an ordered list (non-destructive) of items from highest to lowest priority.
        /// </summary>
        public List<T> ToOrderedList()
        {
            var clone = Clone();
            var list = new List<T>();
            while (clone.Count > 0) list.Add(clone.ExtractMax());
            return list;
        }
    }
}