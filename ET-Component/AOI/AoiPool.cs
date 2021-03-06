using System;
using System.Collections.Generic;

namespace ETModel.AOI
{
	//SourceTree修改
	//SourceTree修改123
    public class AoiPool
    {
        public static AoiPool Instance => _instance ?? (_instance = new AoiPool());

        private static AoiPool _instance;
        
        private readonly Dictionary<Type, Queue<object>> _dic = new Dictionary<Type, Queue<object>>();

        public T Fetch<T>() where T : class
        {
            var type = typeof(T);

            if (_dic.TryGetValue(type, out var queue))
            {
                return queue.Count > 0 ? (T) queue.Dequeue() : (T) Activator.CreateInstance(type);
            }
//gittortoise修改
//gittortoise修改123
//gittortoise修改456
//gittortoise修改789
//gittortoise修改111
            queue = new Queue<object>();

            _dic.Add(type, queue);

            return (T) Activator.CreateInstance(type);
        }

        public T Fetch<T>(params object[] args) where T : class
        {
            var type = typeof(T);

            if (_dic.TryGetValue(type, out var queue))
            {
                return queue.Count > 0 ? (T) queue.Dequeue() : (T) Activator.CreateInstance(type, args);
            }

            queue = new Queue<object>();

            _dic.Add(type, queue);

            return (T) Activator.CreateInstance(type, args);
        }

        public void Recycle(object obj)
        {
            var type = obj.GetType();

            if (!_dic.TryGetValue(type, out var queue))
            {
                queue = new Queue<object>();

                _dic.Add(type, queue);
            }

            queue.Enqueue(obj);
        }
    }
}
