using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace uBinding.Collections
{
    public class ObservableCollection<T> : IObservableCollection<T>
    {
        private readonly List<T> _items;

        public ObservableCollection()
        {
            _items = new List<T>();
        }

        public void AddRange(IEnumerable<T> items)
        {
            var collection = items as T[] ?? items.ToArray();
            _items.AddRange(collection);
            OnChanged(new CollectionChange<T>(collection));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var isRemoved = _items.Remove(item);

            if (isRemoved)
            {
                OnChanged(new CollectionChange<T>(Enumerable.Empty<T>(), item));
            }

            return isRemoved;
        }

        public void Clear()
        {
            var array = new T[Count];
            _items.CopyTo(array);
            _items.Clear();
            OnChanged(new CollectionChange<T>(Enumerable.Empty<T>(), array));
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void Add(T item)
        {
            _items.Add(item);
            OnChanged(new CollectionChange<T>(new[] {item}));
        }

        public int IndexOf(T item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _items.Insert(index, item);
            OnChanged(new CollectionChange<T>(new[] {item}));
        }

        public void RemoveAt(int index)
        {
            var item = _items[index];
            _items.RemoveAt(index);
            OnChanged(new CollectionChange<T>(Enumerable.Empty<T>(), item));
        }

        public T this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<T>) _items).IsReadOnly; }
        }

        public event Action<CollectionChange<T>> Changed;

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void OnChanged(CollectionChange<T> change)
        {
            var handler = Changed;
            if (handler != null) handler(change);
        }
    }
}