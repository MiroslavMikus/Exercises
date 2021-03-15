using DustInTheWind.ConsoleTools;
using System;

namespace Exercise.ConsoleControls
{
    public class TextDemo : IDemo
    {
        public void Print()
        {
            TextBlock textBlock = new TextBlock
            {
                Text = "This is a demo for the TextBox control.",
                MarginTop = 1,
                MarginBottom = 1,
                ForegroundColor = ConsoleColor.Cyan
            };
            textBlock.Display();
        }
    }
}
