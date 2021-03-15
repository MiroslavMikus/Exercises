using System;
using System.Threading;
using Terminal.Gui;

namespace Exercise.GuiCs
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        public static void Start() => Application.Run<PickerApp>();
    }
}
