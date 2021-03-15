using System;

namespace Exercise.Stateless.Car
{
    class Program
    {
        static void Main(string[] args)
        {
            var car = new Car();

            car.MoveToDestination("Mexico");

            Console.WriteLine(car.Graph);

            Console.ReadLine();
        }
    }
}
