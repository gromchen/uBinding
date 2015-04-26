using System.Collections.Generic;

namespace uBinding.Collections
{
    public interface IObservableCollection<T> : IList<T>, IReadonlyObservableCollection<T>
    {
        void AddRange(IEnumerable<T> items);
    }
}