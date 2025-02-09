namespace Base
{
    public interface ITree <T>
    {
        int Height { get; }

        int Count { get; }

        void Insert(T type);

        void Remove(T type);

        bool Contains(T type);
    }
}
