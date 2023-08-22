namespace STR.Core
{
    public abstract class Container
    {
    }

    public class EmptyContainer: Container
    {
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
