namespace TFrame
{
    using System;
    using System.Reflection;
    public class SingletonCreator
    {
        public static T CreateSingleton<T>() where T:class, ISingleton
        {
            T retInstance = default(T);
            //通过反射调用无参的构造函数
            ConstructorInfo[] ctors = typeof(T).GetConstructors(System.Reflection.BindingFlags.Instance
                | BindingFlags.NonPublic );
            ConstructorInfo ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
            if (ctor == null)
                throw new System.Exception("Non-public ctor() not found !");
            retInstance = ctor.Invoke(null) as T;
            retInstance.OnSingletonInit();
            return retInstance;
        }
    }
}