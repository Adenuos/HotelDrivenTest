using System;
namespace Hotel.BLL.Interface
{
    public interface IBookingService
    {
        bool BookRoom(Room room, DateTime checkInDate, DateTime checkOutDate);
        List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, RoomType roomType);
    }
}

