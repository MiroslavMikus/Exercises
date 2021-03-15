using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleExec;

namespace Exercise.Simple_Exec
{
    class Program
    {
        //static async Task Main(string[] args)
        //{
        //     await Command.RunAsync("notepad");
        //}

        static async Task Main(string[] args)
        {
            Console.WriteLine(await GitStatus());
            Console.ReadLine();
        }

        static Task<string> GitStatus()
        {
            return Command.ReadAsync("git", "status", Environment.CurrentDirectory);
        }
    }
}
