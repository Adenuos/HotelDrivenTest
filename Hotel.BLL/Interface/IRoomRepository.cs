using System;
namespace Hotel.BLL.Interface
{
    public interface IRoomRepository
    {
        List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, RoomType roomType);
    }
}

