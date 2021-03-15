using Exercise.NLog;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Nlog
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            //SampleLogLevels();

            //SampleStructuredLogging();

            //LogList();

            LogStructuredList();

            Console.WriteLine("Done");

            Console.ReadLine();
        }

        private static void LogStructuredList()
        {
            var person1 = new Person
            {
                Age = 22,
                Name = "Miroslav Mikus",
                Car = new Car
                {
                    Color = "Black"
                }
            };
            var person2 = new Person
            {
                Age = 33,
                Name = "John",
                Car = new Car
                {
                    Color = "Red"
                }
            };
            var person3 = new Person
            {
                Age = 44,
                Name = "Fritz",
                Car = new Car
                {
                    Color = "White"
                }
            };

            List<Person> people = new List<Person> { person1, person2, person3 };

            logger.Info("here are my people {@ People}", people);
        }

        private static void LogList()
        {
            List<string> Colors = new List<string> { "red", "blue", "black", "white" };

            logger.Info("Here are my colors : {colors}", Colors);
        }

        private static void SampleStructuredLogging()
        {
            var person = new Person
            {
                Age = 22,
                Name = "Miroslav Mikus",
                Car = new Car
                {
                    Color = "Black"
                }
            };

            // log Object
            logger.Info("this is my person : {@ Person}", person);

            // log custom object
            logger.Info("this is my person : {@}", new { Name = person.Name, CarColor = person.Car.Color });
        }

        private static void SampleLogLevels()
        {
            for (int i = 0; i < 1000; i++)
            {
                if (i % 100 == 0)
                    Console.WriteLine(i);

                logger.Trace("Sample trace message");
                logger.Debug("Sample debug message");
                logger.Info("Sample informational message");
                logger.Warn("Sample warning message");
                logger.Error("Sample error message");
                logger.Fatal("Sample fatal error message");

                // alternatively you can call the Log() method
                // and pass log level as the parameter.
                logger.Log(LogLevel.Info, "Sample informational message");
            }
        }
    }
}
