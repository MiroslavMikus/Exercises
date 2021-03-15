using Terminal.Gui;

namespace Exercise.GuiCs
{
    public interface ITopHost
    {
        Toplevel Top();
        string Name { get; }
    }
}
