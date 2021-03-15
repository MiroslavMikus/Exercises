using Optional;
using Optional.Linq;

namespace Exercise.Option
{
    public class BusinessLogicOption
    {
        public Option<Reservation> RentVehicke(int userId, int vehicleId)
        {
            var user = RepositoryOption.GetUser(userId);

            var car = RepositoryOption.GetCar(vehicleId);

            var oldReservation = RepositoryOption.GetReservationsByUser(userId);

            if (user.HasValue && car.HasValue && !oldReservation.HasValue)
            {
                var reservation = new Reservation()
                {
                    Id = 1,
                    UserId = userId,
                    VehicleId = vehicleId
                };

                Repository._reservations.Add(reservation);

                return reservation.Some();
            }

            return Optional.Option.None<Reservation>();
        }
    }
}
