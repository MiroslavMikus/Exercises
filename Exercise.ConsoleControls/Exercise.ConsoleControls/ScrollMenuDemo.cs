using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Menues;
using DustInTheWind.ConsoleTools.Menues.MenuItems;
using System;

namespace Exercise.ConsoleControls
{
    public class ScrollMenuDemo : IDemo
    {
        public class SomeCommad : ICommand
        {
            private readonly string _title;

            public SomeCommad(string title)
            {
                _title = title;
            }

            public bool IsActive => true;

            public void Execute()
            {
                Console.WriteLine(_title);
            }
        }

        public void Print()
        {
            ScrollMenu scrollMenu = new ScrollMenu()
            {
                MarginTop = 1,
                MarginBottom = 1,
                EraseAfterClose = true,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            scrollMenu.AddItems(new IMenuItem[]
            {
                new LabelMenuItem
                {
                    Text = "New Game",
                    Command = new SomeCommad("new game command")
                },
                new YesNoMenuItem
                {
                    Text = "Save Game",
                    Command = new SomeCommad("save game command")
                },
                new LabelMenuItem
                {
                    Text = "Load Game",
                    Command = new SomeCommad("new load command")
                },
            });

            scrollMenu.Display();
        }
    }
}
