using System;
using System.Collections.Generic;

namespace Game.Scripts.Root
{
    public static class G
    {
        private static Dictionary<Type, object> _objects = new Dictionary<Type, object>();
        private static List<object> _objectsList = new List<object>();
        
        public static void Register<T>(T instance)
        {
            if (_objects.ContainsKey(typeof(T)))
            {
                return;
            }
            
            _objectsList.Add(instance);
            _objects.Add(typeof(T), instance);
        }

        public static T Get<T>()
        {
            if (_objects.ContainsKey(typeof(T)))
            {
                return (T)_objects[typeof(T)];
            }
            
            throw new KeyNotFoundException();
        }

        public static void InitializeAll()
        {
            foreach (var obj in _objectsList)
            {
                if (obj is IInitializable initializable)
                {
                    initializable.Initialize();
                }
            }
        }
    }
}