namespace VersionStack
{
    public class VersionStack<T>
    {
        private class Item
        {
            internal T Value;
            internal Item Previous;
        }

        private List<Item> _versions = new();
        private Item Top;

        public void Push(T value)
        {
            if (Top == null) 
                Top = new Item {Value = value, Previous = null};
            else
            {
                var item = new Item { Value = value, Previous = Top };
                Top = item;
            }
            _versions.Add(Top);
        }

        public T Pop()
        {
            if (Top == null) throw new InvalidOperationException();
            var result = Top.Value;
            Top = Top.Previous;
            _versions.Add(Top);
            return result;
        }

        public void RollBack(int version)
        {
            if (version <= 0 || version > _versions.Count)
            {
                throw new IndexOutOfRangeException("Invalid version number");
            }
            Top = _versions[version - 1];
        }

        public void Forget()
        {
            _versions.Clear();
        }
    }

    class Program
    {
        public static void Main()
        {
            VersionStack<int> stack = new VersionStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.RollBack(2);
            stack.Push(6);
            stack.Push(7);
            Console.WriteLine(stack.Pop());

        }
    }
}