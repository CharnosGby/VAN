namespace Utils
{
    public class ThreadLocalUtil
    {
        private static readonly ThreadLocal<object> THREAD_LOCAL = new ThreadLocal<object>();

        public static T Get<T>()
        {
            if (THREAD_LOCAL.Value == null) 
            {
                throw new InvalidOperationException("Value not set in ThreadLocal.");
            }
            
            return (T)THREAD_LOCAL.Value;
        }

        public static void Set(object value)
        {
            THREAD_LOCAL.Value = value;
        }

        public static void Remove()
        {
            THREAD_LOCAL.Dispose(); // 清理 ThreadLocal 对象
        }
    }
}
