using System;

namespace DesignPatterns
{
    public class EagerSingleton
    {
        private static EagerSingleton instance = new EagerSingleton();

        private EagerSingleton()
        {
        }

        public static EagerSingleton GetInstance
        {
            get
             {
                return instance;
             }
        }
    }

    public class LazySingleton
    {
        private static LazySingleton instance = null;

        private LazySingleton()
        {
        }

        public static LazySingleton GetInstance
        {
            get
            {
                if(instance == null)
                {
                    instance = new LazySingleton();
                }
                return instance;
            }
        }
    }

    public class THreadSafeSingleton
    {
        private static THreadSafeSingleton instance = null;
        private static object lockthis = new object();

        private THreadSafeSingleton()
        {
        }

        public static THreadSafeSingleton GetInstance
        {
            get
            {
                lock (lockthis)
                {
                    if (instance == null)
                    {
                        instance = new THreadSafeSingleton();
                    }
                    return instance;
                }
            }
        }
    }
}