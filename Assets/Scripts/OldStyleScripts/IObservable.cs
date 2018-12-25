namespace Jerre 
{
    public interface IObservable
    {
        void AddObserver(IObserver listener);
        void RemoveObserver(IObserver listener);
    }

}