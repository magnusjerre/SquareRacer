using System.Collections.Generic;

namespace Jerre {
    public class Observable : IObservable
    {

        private List<IObserver> listeners;

        public Observable() {
            listeners = new List<IObserver>();
        }

        public void AddObserver(IObserver listener)
        {
            listeners.Add(listener);
        }

        public void RemoveObserver(IObserver listener)
        {
            listeners.Remove(listener);
        }

        public void NotifyListeners(object obj) {
            foreach (var listener in listeners) {
                listener.MJNotify(obj);
            }
        }
    }
}