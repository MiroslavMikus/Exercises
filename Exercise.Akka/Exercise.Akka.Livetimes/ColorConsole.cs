using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_Akka
{
    public static class ColorConsole
    {
        public static void WriteGreen(string message) =>
            WriteWithColor(message, ConsoleColor.Green);

        public static void WriteYellow(string message) =>
            WriteWithColor(message, ConsoleColor.Yellow);

        public static void WriteRed(string message) =>
            WriteWithColor(message, ConsoleColor.Red);

        public static void WriteGray(string message) =>
            WriteWithColor(message, ConsoleColor.Gray);

        public static void WriteWithColor(string message, ConsoleColor color)
        {
            var before = Console.ForegroundColor;

            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ForegroundColor = before;
        }
    }
}
