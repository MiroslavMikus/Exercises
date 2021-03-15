using DustInTheWind.ConsoleTools.InputControls;
using System;

namespace Exercise.ConsoleControls
{
    public class YesNoDemo : IDemo
    {
        public void Print()
        {
            YesNoQuestion yesNoQuestion = new YesNoQuestion("Do you want to continue?");
            yesNoQuestion.AcceptCancel = false;

            YesNoAnswer answer = yesNoQuestion.ReadAnswer();

            Console.WriteLine("Your answer is " + answer);
        }
    }
}
