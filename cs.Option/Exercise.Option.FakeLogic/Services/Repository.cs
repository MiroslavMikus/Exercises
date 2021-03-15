using AutoFixture;
using System.Collections.Generic;
using System.Linq;

namespace Exercise.Option
{
    public static class Repository
    {
        internal static readonly List<Person> _users = new List<Person>();
        internal static readonly List<Vehicle> _cars = new List<Vehicle>();
        internal static readonly List<Reservation> _reservations = new List<Reservation>();

        static Repository()
        {
            var fixture = new Fixture();

            for (int i = 0; i < 100; i++)
            {
                _users.Add(fixture.Build<Person>()
                    .With(a => a.Id, i)
                    .Create());

                _cars.Add(fixture.Build<Vehicle>()
                    .With(a => a.Id, i)
                    .Create());
            }

            for (int i = 0; i < 20; i++)
            {
                _reservations.Add(new Reservation()
                {
                    Id = i,
                    UserId = i,
                    VehicleId = i
                });
            }
        }

        public static Person GetUser(int id)
        {
            return _users.FirstOrDefault(a => a.Id == id);
        }

        public static Reservation GetReservations(int id)
        {
            return _reservations.FirstOrDefault(a => a.Id == id);
        }

        public static Reservation GetReservationsByUser(int userId)
        {
            return _reservations.FirstOrDefault(a => a.UserId == userId);
        }

        public static Reservation GetReservationsByVehicle(int vehicleId)
        {
            return _reservations.FirstOrDefault(a => a.VehicleId == vehicleId);
        }

        public static Vehicle GetCar(int id)
        {
            return _cars.FirstOrDefault(a => a.Id == id);
        }
    }
}
