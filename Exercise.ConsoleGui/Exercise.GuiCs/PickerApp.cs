using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Terminal.Gui;

namespace Exercise.GuiCs
{
    public class PickerApp : Toplevel
    {
        private readonly Dictionary<int, ITopHost> _apps = new Dictionary<int, ITopHost>();

        public PickerApp()
        {
            int i = 0;

            foreach (var host in GetHosts())
            {
                _apps[i++] = host;
            }

            Application.Init();

            var top = Application.Top;

            var win = new Window("Picker")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            top.Add(win);

            var options = new RadioGroup(1, 1, _apps.Select(a => a.Value.Name).ToArray());

            var confirm = new Button("OK")
            {
                Y = Pos.Bottom(options) + 1,
                X = 1,
                Clicked = () =>
                {
                    Application.RequestStop();
                    Application.Run(_apps[options.Selected].Top());
                }
            };

            var quit = new Button("Quit")
            {
                Y = Pos.Bottom(options) + 1,
                X = Pos.Right(confirm) + 1,
                Clicked = () =>
                {
                    Application.RequestStop();
                    Environment.Exit(0);
                }
            };

            win.Add(options, confirm, quit);

            Application.Run();
        }

        private IEnumerable<ITopHost> GetHosts()
        {
            var type = typeof(ITopHost);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => !p.IsAbstract);

            foreach (var t in types)
            {
                yield return (ITopHost)Activator.CreateInstance(t);
            }
        }
    }
}
