using System;
using DustInTheWind.ConsoleTools.Menues;

namespace HalloConfig.Menu
{
    public class SimpleCommand : ICommand
    {
        private readonly Action<SimpleCommand> _execute;
        public string Title { get; }

        public SimpleCommand(string title, Action<SimpleCommand> execute)
        {
            _execute = execute;
            Title = title;
        }

        public bool IsActive => true;

        public void Execute()
        {
            _execute(this);
        }
    }
}