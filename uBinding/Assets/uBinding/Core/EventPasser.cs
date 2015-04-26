using System;

namespace uBinding.Core
{
    public class EventPasser : IRaisable, IInvokable
    {
        public EventPasser(Action<IInvokable> action)
        {
            action(this);
        }

        public void Invoke()
        {
            var handler = Raised;
            if (handler != null) handler();
        }

        public event Action Raised;
    }
}