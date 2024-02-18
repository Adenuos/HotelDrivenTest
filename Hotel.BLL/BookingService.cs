using static Hotel.BLL.BookingService;
using Hotel.BLL.Interface;

namespace Hotel.BLL;

public class BookingService : IBookingService
{
    private readonly IRoomRepository _roomRepository;

    public BookingService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public bool BookRoom(Room room, DateTime checkInDate, DateTime checkOutDate)
    {
        if (IsRoomAvailable(room, checkInDate, checkOutDate))
        {
            room.Bookings!.Add(new Booking { RoomId = room.Id, CheckInDate = checkInDate, CheckOutDate = checkOutDate });
            return true;
        }
        return false;
    }
    public List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, RoomType roomType)
    {
        return _roomRepository.GetAvailableRooms(checkInDate, checkOutDate, roomType);
    }

    private bool IsRoomAvailable(Room room, DateTime checkInDate, DateTime checkOutDate)
    {
        return _roomRepository.GetAvailableRooms(checkInDate, checkOutDate, room.Type).Contains(room);
    }

}


