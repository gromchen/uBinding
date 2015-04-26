using System;

namespace uBinding.Changables
{
    public class EmptyChangable : IChangable
    {
        public event Action ValueChanged;
    }
}