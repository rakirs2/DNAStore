namespace Base.Utils;


    public class ListEqualityComparer<T> : IEqualityComparer<IList<T>>
    {
        public static readonly ListEqualityComparer<T> Default = new ListEqualityComparer<T>();

        public bool Equals(IList<T> x, IList<T> y)
        {
            if (x == y) return true;
            if (x == null || y == null || x.Count != y.Count) return false;
            for (int i = 0; i < x.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(x[i], y[i])) return false;
            }
            return true;
        }

        public int GetHashCode(IList<T> obj)
        {
            int hash = 17;
            foreach (T item in obj)
            {
                hash = hash * 23 + (item == null ? 0 : item.GetHashCode());
            }
            return hash;
        }
    }
