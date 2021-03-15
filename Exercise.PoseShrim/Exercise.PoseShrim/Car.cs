using System.IO;

namespace Exercise.PoseShrim
{
    public class Car
    {
        public string Color { get; }

        public Car()
        {
            Color = File.ReadAllText("color.txt");
        }
    }
}
