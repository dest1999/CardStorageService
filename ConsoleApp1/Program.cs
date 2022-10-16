using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.Title = Properties.Settings.Default.ApplicationNameDebug;
#else
            Console.Title = Properties.Settings.Default.ApplicationName;
#endif

            if (string.IsNullOrEmpty(Properties.Settings.Default.Name) || Properties.Settings.Default.Age <= 0)
            {
                Console.Write("Name: ");
                Properties.Settings.Default.Name = Console.ReadLine();

                Console.Write("Age: ");
                if (int.TryParse(Console.ReadLine(), out int age))
                {
                    Properties.Settings.Default.Age = age;
                }
                else
                {
                    Properties.Settings.Default.Age = 0;
                }
                Properties.Settings.Default.Save();
            }

            Console.WriteLine($"Name is: {Properties.Settings.Default.Name}");
            Console.WriteLine($"Age is: {Properties.Settings.Default.Age}");
            Console.ReadKey();
        }
    }
}
