using System;

namespace AD
{
    class Program
    {
        static void Main(string[] args)
        {
            Scheduler<int> scheduler = new Scheduler<int>();

            scheduler.Enqueue(Priority.High, 1);

            Console.WriteLine(scheduler);

        }
    }
}
