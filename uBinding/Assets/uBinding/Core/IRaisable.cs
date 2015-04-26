using System;

namespace uBinding.Core
{
    public interface IRaisable
    {
        event Action Raised;
    }
}