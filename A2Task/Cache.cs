using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using A2_test_task;

namespace A2Task
{
    public class Cache
    {
        public HashSet<int> Set { get; set; }

        public Cache()
        {
            Set = new HashSet<int>();
        }

        public bool Contains(Model obj)
        {
            return Set.Contains(obj.GetHashCode());
        }

        public void Add(Model obj)
        {
            Set.Add(obj.GetHashCode());
        }
        
        public void AddRange(IEnumerable<Model> objs)
        {
            foreach (var model in objs)
            {
                Set.Add(model.GetHashCode());
            }
        }

        public void Update()
        {
            Set.Clear();
            AddRange( DBManager.GetInstance().GetRows(100));
            Console.WriteLine("Cache updated");
        }
    }
}