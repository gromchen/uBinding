using System;

namespace uBinding.Changables
{
    public interface IChangable
    {
        event Action ValueChanged;
    }
}