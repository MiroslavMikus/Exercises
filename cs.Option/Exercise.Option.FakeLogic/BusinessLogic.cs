namespace Exercise.Option
{
    public class BusinessLogic
    {
        public Reservation RentVehicke(int userId, int vehicleId)
        {
            var user = Repository.GetUser(userId);

            if (user == null)
            {
                return null;
            }

            if (Repository.GetReservationsByUser(userId) != null)
            {
                return null;
            }

            var car = Repository.GetCar(vehicleId);

            if (car == null)
            {
                return null;
            }

            if (Repository.GetReservationsByVehicle(vehicleId) != null)
            {
                return null;
            }

            return new Reservation()
            {
                Id = 1,
                UserId = userId,
                VehicleId = vehicleId
            };
        }
    }
}
