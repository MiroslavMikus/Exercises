using NStack;
using System;
using Terminal.Gui;

namespace Exercise.GuiCs
{
    public class LoginApp : ITopHost
    {
        public string Name => "Modal login";

        public Toplevel Top()
        {
            var top = new Toplevel()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            bool okpressed = false;

            var ok = new Button(3, 14, "Ok")
            {
                Clicked = () => { Application.RequestStop(); okpressed = true; }
            };

            var cancel = new Button(10, 14, "Cancel")
            {
                Clicked = Application.RequestStop
            };

            var dialog = new Dialog("Login", 60, 18, ok, cancel);

            var name = new Label(1, 1, "Name:");

            var entry = new TextField("Some Text field")
            {
                X = 1,
                Y = 2,
                Width = Dim.Fill(),
                Height = 1
            };

            dialog.Add(entry, name);


            Application.Run(dialog);

            if (okpressed)
            {
                PrintResult(entry.Text);
                Program.Start();
            }

            return top;
        }

        public void PrintResult(ustring result)
        {
            var dialog = new Dialog("Wellcome", 60, 18)
            {
                new Button(3, 14, "Ok"){Clicked = Application.RequestStop},
                new Label(1, 1, $"We are happy to see you again {result}")
            };

            Application.Run(dialog);
        }
    }
}
