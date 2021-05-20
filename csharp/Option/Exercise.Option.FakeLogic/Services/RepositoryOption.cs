using Optional;
using Optional.Collections;

namespace Exercise.Option
{
    public static class RepositoryOption
    {
        public static Option<Person> GetUser(int id)
        {
            return Repository._users.FirstOrNone(a => a.Id == id);
        }

        public static Option<Reservation> GetReservations(int id)
        {
            return Repository._reservations.FirstOrNone(a => a.Id == id);
        }

        public static Option<Reservation> GetReservationsByUser(int userId)
        {
            return Repository._reservations.FirstOrNone(a => a.UserId == userId);
        }

        public static Option<Reservation> GetReservationsByVehicle(int vehicleId)
        {
            return Repository._reservations.FirstOrNone(a => a.VehicleId == vehicleId);
        }

        public static Option<Vehicle> GetCar(int id)
        {
            return Repository._cars.FirstOrNone(a => a.Id == id);
        }
    }
}
