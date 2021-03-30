using System;
using DustInTheWind.ConsoleTools.InputControls;

namespace Exercise.ConsoleControls
{
    public class YesNoDemo : IDemo
    {
        public void Print()
        {
            var yesNoQuestion = new YesNoQuestion("Do you want to continue?");
            yesNoQuestion.AcceptCancel = false;

            var answer = yesNoQuestion.ReadAnswer();

            Console.WriteLine("Your answer is " + answer);
        }
    }
}