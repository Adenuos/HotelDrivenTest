using System;
using FluentAssertions;
using Hotel.BLL;
using Hotel.BLL.Interface;
using Moq;

namespace TestHotel
{
    public class RoomRepositoryTests
    {
        [Fact]
        public void GetAvailableRooms_ReturnsAvailableRooms()
        {
            // Arrange
            var roomRepository = new RoomRepository();
            var checkInDate = DateTime.Now;
            var checkOutDate = DateTime.Now.AddDays(1);
            var roomType = RoomType.Single;

            // Act
            var availableRooms = roomRepository.GetAvailableRooms(checkInDate, checkOutDate, roomType);

            // Assert
            availableRooms.Should().NotBeNull();
            availableRooms.Should().NotBeEmpty();
        }

        [Fact]
        public void GetAvailableRooms_NoAvailableRooms_ReturnsEmptyList()
        {
            // Arrange
            var roomRepository = new RoomRepository();
            var checkInDate = DateTime.Now;
            var checkOutDate = DateTime.Now.AddDays(1);
            var roomType = RoomType.Double; 

            // Act
            var availableRooms = roomRepository.GetAvailableRooms(checkInDate, checkOutDate, roomType);

            // Assert
            availableRooms.Should().NotBeNull();
            availableRooms.Should().BeEmpty();
        }

        
    }
}

