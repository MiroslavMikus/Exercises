using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Menues;
using DustInTheWind.ConsoleTools.Menues.MenuItems;

namespace HalloConfig.Menu
{
    public class ScrollMenuPrinter
    {
        private readonly IEnumerable<SimpleCommand> _staticCommands;

        public ScrollMenuPrinter()
        {
            _staticCommands = typeof(Program).Assembly.GetTypes()
                .Where(a => a.IsAbstract && a.IsSealed)
                .SelectMany(a => a.GetMethods(BindingFlags.Public | BindingFlags.Static))
                .Where(a => a.Name.EndsWith("Command"))
                .Select(a =>
                {
                    return new SimpleCommand(a.Name.Replace("Command", ""), b =>
                    {
                        Console.Clear();
                        var textBlock = new TextBlock
                        {
                            Text = $"Executing {b.Title}",
                            Margin = 1,
                            ForegroundColor = ConsoleColor.Cyan
                        };
            
                        textBlock.Display();
                        a.Invoke(null, null);
                        
                        Console.WriteLine("return to menu");
                        Console.ReadLine();
                        Console.Clear();
                        Print();
                    });
                });
        }

        public void Print()
        {
            var textBlock = new TextBlock
            {
                Text = $"Menu",
                Margin = 1,
                ForegroundColor = ConsoleColor.Green
            };
            
            textBlock.Display();
            
            ScrollMenu scrollMenu = new ScrollMenu()
            {
                Margin = 1,
                EraseAfterClose = true,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var menuItems = _staticCommands.Select(a => new LabelMenuItem()
            {
                Text = a.Title, Command = a,
                HorizontalAlignment = HorizontalAlignment.Left
            } as IMenuItem).Concat(new IMenuItem[]
            {
                new LabelMenuItem()
                {
                    Text= "Exit",
                    Command = new SimpleCommand("", a => Environment.Exit(0)),
                    HorizontalAlignment = HorizontalAlignment.Left
                }
            }).ToArray();

            scrollMenu.AddItems(menuItems);

            scrollMenu.Display();
        }
    }
}