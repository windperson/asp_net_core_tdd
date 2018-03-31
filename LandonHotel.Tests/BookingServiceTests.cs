using System;
using LandonHotel.Data;
using LandonHotel.Repositories;
using LandonHotel.Services;
using Moq;
using Xunit;

namespace LandonHotel.Tests
{
    public class BookingServiceTests
    {
        private Mock<IRoomsRepository> _roomRepo;
        private Mock<IBookingsRepository> _bookingRepo;

        public BookingServiceTests()
        {
            _bookingRepo = new Mock<IBookingsRepository>();
            _roomRepo = new Mock<IRoomsRepository>();
        }

        private BookingService Subject()
        {
            return new BookingService(_bookingRepo.Object, _roomRepo.Object);
        }

        [Fact]
        public void IsBookingValid_NonSmoker_Valid()
        {
            var service = Subject();
            var isValid = service.IsBookingValid(1, new Booking() { IsSmoking = false });

            Assert.True(isValid);
        }

        [Fact]
        public void IsBookingValid_Smoker_Invalid()
        {
            var service = Subject();
            var isValid = service.IsBookingValid(1, new Booking() { IsSmoking = true });

            Assert.False(isValid);
        }

        [Fact]
        public void IsBookingValid_PetsAllowed_IsValid()
        {
            var service = Subject();
            _roomRepo.Setup(r => r.GetRoom(1)).Returns(new Room {ArePetsAllowed = true});

            bool isValid = service.IsBookingValid(1, new Booking {HasPets = true});

            Assert.True(isValid);
        }

        [Fact]
        public void IsBookingValid_PetsAllowd_IsValid()
        {
            var service = Subject();
            _roomRepo.Setup(r => r.GetRoom(1)).Returns(new Room { ArePetsAllowed = true });

            bool isValid = service.IsBookingValid(1, new Booking { HasPets = false });

            Assert.True(isValid);
        }

        [Fact]
        public void IsBookingValid_NoPetsAllowd_IsValid()
        {
            var service = Subject();
            _roomRepo.Setup(r => r.GetRoom(1)).Returns(new Room { ArePetsAllowed = true });

            bool isValid = service.IsBookingValid(1, new Booking { HasPets = false });

            Assert.True(isValid);
        }

        [Fact]
        public void IsBookingValid_PetsNotAllowed_Invalid()
        {
            //Arrange
            var service = Subject();
            _roomRepo.Setup(r => r.GetRoom(1)).Returns(new Room {ArePetsAllowed = false});

            //Act
            var isValid = service.IsBookingValid(1, new Booking {HasPets = true});

            //Assert
            Assert.False(isValid);
        }
        
    }
}
