using System;

namespace Exercise_Akka
{
    public static class ColorConsole
    {
        static object locker = new object();

        public static void WriteMagenta(string message, params object[] args) =>
           WriteWithColor(ConsoleColor.Magenta, message, args);

        public static void WriteWhite(string message, params object[] args) =>
            WriteWithColor(ConsoleColor.White, message, args);

        public static void WriteCyan(string message, params object[] args) =>
            WriteWithColor(ConsoleColor.Cyan, message, args);

        public static void WriteGreen(string message, params object[] args) =>
            WriteWithColor(ConsoleColor.Green, message, args);

        public static void WriteYellow(string message, params object[] args) =>
            WriteWithColor(ConsoleColor.Yellow, message, args);

        public static void WriteRed(string message, params object[] args) =>
            WriteWithColor(ConsoleColor.Red, message, args);

        public static void WriteGray(string message, params object[] args) =>
            WriteWithColor(ConsoleColor.Gray, message, args);

        public static void WriteWithColor(ConsoleColor color, string message, params object[] args)
        {
            lock (locker)
            {
                var before = Console.ForegroundColor;

                Console.ForegroundColor = color;

                Console.WriteLine(string.Format(message, args));

                Console.ForegroundColor = before;
            }
        }
    }
}
