using System;
using Hotel.BLL.Interface;
using System.Diagnostics;

namespace Hotel.BLL
{
    public class RoomRepository : IRoomRepository
    {
        public static readonly List<Room> _rooms = new List<Room>
    {
        new Room { Id = 1, Type = RoomType.Single, Price = 100, Bookings = new List<Booking>() },
        new Room { Id = 2, Type = RoomType.Double, Price = 150, Bookings = new List<Booking>() },
        new Room { Id = 2, Type = RoomType.Double, Price = 200, Bookings = new List<Booking>() },
        new Room { Id = 3, Type = RoomType.Single, Price = 120, Bookings = new List<Booking>() }
    };

        private static bool IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            return _rooms.Any(room =>
         room.Id == roomId &&
         room.Bookings.Any(booking =>
             (checkInDate >= booking.CheckInDate && checkInDate < booking.CheckOutDate) ||
             (checkOutDate > booking.CheckInDate && checkOutDate <= booking.CheckOutDate) ||
             (checkInDate <= booking.CheckInDate && checkOutDate >= booking.CheckOutDate)));
        }

        public List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, RoomType roomType)
        {
            // Här ska du implementera logiken för att hämta tillgängliga rum baserat på inkommande parametrar

            if (checkInDate == default) checkInDate = DateTime.Now;
            if (checkOutDate == default) checkOutDate = DateTime.Now.AddDays(1);


            Debug.WriteLine($"Fetching available rooms for check-in: {checkInDate}, check-out: {checkOutDate}, room type: {roomType}");

            var availableRooms = _rooms.Where(room =>
                room.Type == roomType &&
                !IsRoomBooked(room.Id, checkInDate, checkOutDate)).ToList();

            if (availableRooms.Count == 0)
            {
                Debug.WriteLine("No available rooms meet criteria.");
            }
            else
            {
                foreach (var room in availableRooms)
                {
                    Debug.WriteLine($"Available Room ID: {room.Id}, Type: {room.Type}, Price: {room.Price * (checkOutDate - checkInDate).Days}");
                }
            }

            return availableRooms;
        }

    }
}

