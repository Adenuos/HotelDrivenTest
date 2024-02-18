using Moq;
using FluentAssertions;
using Hotel.BLL.Interface;
using Hotel.BLL;

namespace TestHotel;


public class BookingServiceTests
{
    [Fact]
    public void BookRoom_WhenRoomAvailable_ShouldReturnTrue()
    {
        // Arrange
        var room = new Room { Id = 1, Type = RoomType.Single, Price = 100, Bookings = new List<Booking>() };
        var mockRoomRepository = new Mock<IRoomRepository>();
        mockRoomRepository.Setup(repo => repo.GetAvailableRooms(It.IsAny<DateTime>(), It.IsAny<DateTime>(), RoomType.Single))
            .Returns(new List<Room> { room });
        var bookingService = new BookingService(mockRoomRepository.Object);
        var checkInDate = DateTime.Now.AddDays(1);
        var checkOutDate = DateTime.Now.AddDays(2);

        // Act
        var result = bookingService.BookRoom(room, checkInDate, checkOutDate);

        // Assert
        result.Should().BeTrue();
        room.Bookings.Should().NotBeEmpty();
        room.Bookings.Should().ContainSingle(booking =>
            booking.RoomId == room.Id &&
            booking.CheckInDate == checkInDate &&
            booking.CheckOutDate == checkOutDate);
    }

    [Fact]
    public void BookRoom_WhenRoomNotAvailable_ShouldReturnFalse()
    {
        // Arrange
        var room = new Room { Id = 1, Type = RoomType.Single, Price = 100, Bookings = new List<Booking> { new Booking { RoomId = 1, CheckInDate = DateTime.Now.AddDays(1), CheckOutDate = DateTime.Now.AddDays(2) } } };
        var mockRoomRepository = new Mock<IRoomRepository>();
        mockRoomRepository.Setup(repo => repo.GetAvailableRooms(It.IsAny<DateTime>(), It.IsAny<DateTime>(), RoomType.Single))
            .Returns(new List<Room>());
        var bookingService = new BookingService(mockRoomRepository.Object);
        var checkInDate = DateTime.Now.AddDays(1);
        var checkOutDate = DateTime.Now.AddDays(2);

        // Act
        var result = bookingService.BookRoom(room, checkInDate, checkOutDate);

        // Assert
        result.Should().BeFalse();
        room.Bookings.Should().HaveCount(1); 
    }

   
}
