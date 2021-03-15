using AutoFixture.Xunit2;
using FluentAssertions;
using Optional.Linq;
using Optional.Unsafe;
using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Xunit;

namespace Exercise.Option
{
    public class ReservationOptionTest
    {
        [Theory, AutoData]
        public void RentVehicle_should_return_null_if_user_was_not_found(BusinessLogicOption sut)
        {
            sut.RentVehicke(200, 30).HasValue.Should().BeFalse();
        }

        [Theory, AutoData]
        public void RentVehicle_should_return_null_if_car_was_not_found(BusinessLogicOption sut)
        {
            sut.RentVehicke(30, 300).HasValue.Should().BeFalse();
        }

        [Theory, AutoData]
        public void RentVehicle_should_return_none_if_car_is_not_available(BusinessLogicOption sut)
        {
            sut.RentVehicke(50, 200).HasValue.Should().BeFalse();
        }

        [Theory, AutoData]
        public void RentVehicle_should_return_null_if_user_has_already_car(BusinessLogicOption sut)
        {
            sut.RentVehicke(10, 50).HasValue.Should().BeFalse();
        }

        [Theory, AutoData]
        public void RentVehicle_should_return_valid_reservation(BusinessLogicOption sut)
        {
            var reservation = sut.RentVehicke(50, 51);

            reservation.ValueOrDefault().VehicleId.Should().Be(51);
            reservation.ValueOrDefault().UserId.Should().Be(50);
        }
    }
}
