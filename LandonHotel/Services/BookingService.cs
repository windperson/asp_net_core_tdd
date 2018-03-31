﻿using LandonHotel.Data;
using LandonHotel.Repositories;

namespace LandonHotel.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly IRoomsRepository _roomsRepository;

        public BookingService(IBookingsRepository bookingsRepository, IRoomsRepository roomsRepository)
        {
            _bookingsRepository = bookingsRepository;
            _roomsRepository = roomsRepository;
        }

        public int CalculateBookingCost(int roomId, Booking booking)
        {
            return 0;
        }

        public bool IsBookingValid(int roomId, Booking booking)
        {
            var guestIsSmoking = booking.IsSmoking;
            var guestIsBringingPets = booking.HasPets;
            var numberOfGuests = booking.NumberOfGuests;

            var room = _roomsRepository.GetRoom(roomId);
            if (booking.HasPets && !room.ArePetsAllowed)
            {
                return false;
            }

            return !guestIsSmoking;
        }
    }
}
