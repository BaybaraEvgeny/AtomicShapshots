using System;
using System.Threading.Tasks;

namespace AtomicShapshots
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            const int n = 2;
            
            Random random = new Random();
            RegisterManager Manager = new RegisterManager(n);
            Task[] tasks = new Task[n];

            for (int i = 0; i < 20; i++)
            {
                int id = i % 2;
                int value = random.Next(100);
                
                tasks[id] = Task.Run(() =>
                {
                    Console.WriteLine("write value = {0} in #{1} register", value, id);
                    Manager.Update(id, value);
                });

                if (i % 3 == 0)
                {
                    var count = i;
                    Task.Run(() =>
                    {
                        Console.WriteLine("read from {0} thread on {1} interation: ({2})", id, count, string.Join(", ", Manager.Scan(id)));
                    });
                }

                if (i % 2 == 1)
                {
                    Task.WaitAll(tasks);
                }

            }

        }
    }
}