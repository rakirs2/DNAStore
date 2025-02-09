namespace Base
{
    public abstract class BTree<T> : ITree<T> //where T : class
    {
        
        public int Height => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool Contains(T type)
        {
            throw new NotImplementedException();
        }

        public void Insert(T type)
        {
            throw new NotImplementedException();
        }

        public void Remove(T type)
        {
            throw new NotImplementedException();
        }
    }
}
