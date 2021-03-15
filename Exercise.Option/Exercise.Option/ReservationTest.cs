using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Exercise.Option
{
    public class ReservationTest
    {
        [Theory, AutoData]
        public void RentVehicle_should_return_null_if_user_was_not_found(BusinessLogic sut)
        {
            sut.RentVehicke(200, 30).Should().BeNull();
        }

        [Theory, AutoData]
        public void RentVehicle_should_return_null_if_car_was_not_found(BusinessLogic sut)
        {
            sut.RentVehicke(30, 300).Should().BeNull();
        }

        [Theory, AutoData]
        public void RentVehicle_should_return_null_if_car_is_not_available(BusinessLogic sut)
        {
            sut.RentVehicke(50, 10).Should().BeNull();
        }

        [Theory, AutoData]
        public void RentVehicle_should_return_null_if_user_has_already_car(BusinessLogic sut)
        {
            sut.RentVehicke(10, 50).Should().BeNull();
        }

        [Theory, AutoData]
        public void RentVehicle_should_return_valid_reservation(BusinessLogic sut)
        {
            var reservation = sut.RentVehicke(50, 51);

            reservation.VehicleId.Should().Be(51);
            reservation.UserId.Should().Be(50);
        }
    }
}
