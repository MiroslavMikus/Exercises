using System;
using DustInTheWind.ConsoleTools.Menues;

namespace HalloConsoleControls
{
    public class TextMenuDemo : IDemo
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
            TextMenu scrollMenu = new TextMenu()
            {
                Margin = 1,
                EraseAfterClose = true,
            };

            scrollMenu.AddItems(new TextMenuItem[]
            {
                new TextMenuItem
                {
                    Id = "1",
                    Text = "New Game",
                    Command = new SomeCommad("new game command")
                },
                new TextMenuItem
                {
                    Id = "2",
                    Text = "Save Game",
                    Command = new SomeCommad("save game command")
                },
                new TextMenuItem
                {
                    Id = "3",
                    Text = "Load Game",
                    Command = new SomeCommad("new load command")
                },
            });

            scrollMenu.Display();
        }
    }
}
