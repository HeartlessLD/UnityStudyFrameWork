namespace TFrame
{
    using System;
    using UnityEngine;
    public abstract class TSingleton<T> : MonoBehaviour, ISingleton where T : MonoBehaviour, new()
    {
        protected static T _instance;
        static object _lock = new object();
        protected TSingleton()
        {
            OnSingletonInit();
        }

        public static T Instance
        {
            get
            {
                lock(_lock)
                {
                    if(_instance == null)
                    {
                        //_instance = SingletonCreator.CreateSingleton<T>();
                        _instance = new T();
                    }
                }
                return _instance;
            }
        }

        public virtual void Dispose()
        {
            _instance = default(T);
        }
        public void OnSingletonInit()
        {
           
        }
    }
}
