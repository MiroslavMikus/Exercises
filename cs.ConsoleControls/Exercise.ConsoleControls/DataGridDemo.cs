using DustInTheWind.ConsoleTools.TabularData;

namespace Exercise.ConsoleControls
{
    public class DataGridDemo : IDemo
    {
        public void Print()
        {
            DataGrid grid = new DataGrid("Here is example grid");
            grid.Columns.Add("Name");
            grid.Columns.Add("Age");

            grid.Rows.Add("Jack", 21);
            grid.Rows.Add("Roman", 99);
            grid.Rows.Add("Vlad", 47);
            grid.Rows.Add("Boris", 62);

            grid.BorderTemplate = BorderTemplate.PlusMinusBorderTemplate;
            grid.Display();

            grid.DisplayColumnHeaders = false;

            grid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;
            grid.Display();

            grid.DisplayTitle = false;
            grid.DisplayColumnHeaders = true;

            grid.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;
            grid.Display();
        }
    }
}
