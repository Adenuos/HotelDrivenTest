using System;
namespace Hotel.BLL
{
    public class Room
    {
        public int Id { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public List<Booking>? Bookings { get; set; }
    }
}

