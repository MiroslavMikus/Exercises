using System;
using DustInTheWind.ConsoleTools;

namespace HalloConsoleControls
{
    public class TextDemo : IDemo
    {
        public void Print()
        {
            var textBlock = new TextBlock
            {
                Text = "This is a demo for the TextBox control.",
                Margin = 1,
                ForegroundColor = ConsoleColor.Cyan
            };
            textBlock.Display();
        }
    }
}