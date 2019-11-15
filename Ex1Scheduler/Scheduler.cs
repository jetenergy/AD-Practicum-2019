using System.Collections.Generic;
using System.Text;

namespace AD
{
    public class Scheduler<T> : IScheduler<T>
    {
        // Insert data members here
        MyQueue<T>[] scheduler;

        public Scheduler()
        {
            scheduler = new MyQueue<T>[3] { new MyQueue<T>(), new MyQueue<T>(), new MyQueue<T>() };
        }

        public void Enqueue(Priority priority, T Data)
        {
            scheduler[(int)priority].Enqueue(Data);
        }

        public T Dequeue()
        {
            T returnVal = default(T);

            if (!scheduler[(int)Priority.High].IsEmpty())
            {
                returnVal = scheduler[(int)Priority.High].Dequeue();
            }
            else if (!scheduler[(int)Priority.Medium].IsEmpty())
            {
                returnVal = scheduler[(int)Priority.Medium].Dequeue();
            }
            else if (!scheduler[(int)Priority.Low].IsEmpty())
            {
                returnVal = scheduler[(int)Priority.Low].Dequeue();
            }

            if (!scheduler[(int)Priority.Medium].IsEmpty())
            {
                scheduler[(int)Priority.High].Enqueue(scheduler[(int)Priority.Medium].Dequeue());
            }
            if (!scheduler[(int)Priority.Low].IsEmpty())
            {
                scheduler[(int)Priority.Medium].Enqueue(scheduler[(int)Priority.Low].Dequeue());
            }

            return returnVal;
        }
        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{High:[");
            sb.Append(ElementsOfList(Priority.High));
            sb.Append("],Medium:[");
            sb.Append(ElementsOfList(Priority.Medium));
            sb.Append("],Low:[");
            sb.Append(ElementsOfList(Priority.Low));
            sb.Append("]}");

            return sb.ToString();
        }

        private string ElementsOfList(Priority priority)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < scheduler[(int)priority].list.Count; i++)
            {
                sb.Append(((i == 0) ? "" : ",") + scheduler[(int)priority].list[i]);
            }

            return sb.ToString();
        }
    }

    public class MyQueue<T>
    {
        public List<T> list = new List<T>();

        public bool IsEmpty()
        {
            return list.Count == 0;
        }

        public void Enqueue(T data)
        {
            list.Add(data);
        }

        public T GetFront()
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            return list[0];
        }

        public T Dequeue()
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            var item = list[0];

            list.RemoveRange(0, 1);

            return item;
        }

        public void Clear()
        {
            list.Clear();
        }

    }
}
