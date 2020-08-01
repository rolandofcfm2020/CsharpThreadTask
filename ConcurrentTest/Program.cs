using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentTest
{
    class Program
    {
        public static async Task<string> MakeSomethingAsync()
        {
            Thread.Sleep(1000);
            var myString = "Hola";
            return await Task.FromResult<string>(myString);
        }

        public static void MakeSomethingAsync(string name)
        {
            Thread.Sleep(1000);
            var myString = "Hola " + name;
            Console.WriteLine(myString);

        }


        static void Main(string[] args)
        {
            string value = "";
            Console.WriteLine("Voy a mandar a llamar las tareas");
            var taskCompleteds = 0;
            var stopCicle = false;
            Task.Run(async () =>
            {
                value = await MakeSomethingAsync();

            }).ContinueWith(cont =>
                {
                    Console.WriteLine("Terminé de ejecutar esta tarea: void MakeSomethingAsync()");
                    taskCompleteds++;
                });
            //.ContinueWith(cont => taskCompleteds++);

            Task.Run(() =>
            {
                MakeSomethingAsync("Rolando");
                //Console.WriteLine("Terminé de ejecutar esta tarea: void MakeSomethingAsync(Parameter)");
            }).ContinueWith(cont =>
            {
                Console.WriteLine("Terminé de ejecutar esta tarea: void MakeSomethingAsync(Parameter)");
                taskCompleteds++;
            });

            Console.WriteLine("Esta es la tarea principal");
            do
            {
                for (int i = 0; ; i++)
                {
                    if (taskCompleteds == 2)
                    {
                        Console.WriteLine("Ambas tareas fueron finalizadas");
                        stopCicle = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(i);
                    }
                }

            }
            while (!stopCicle);
            Console.ReadLine();
        }
    }
}
