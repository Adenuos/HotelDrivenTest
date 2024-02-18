using Hotel.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace HotelD.Pages
{
    public class ConfirmationModel : PageModel
    {

        public bool IsRoomBooked { get; set; }
        public Room? BookedRoom { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public RoomType SelectedRoomType { get; set; }
        public int SelectedRoomId { get; set; }

        public void OnGet()
        {
            SelectedRoomType = Enum.Parse<RoomType>(HttpContext.Session.GetString("SelectedRoomType"));
            SelectedRoomId = HttpContext.Session.GetInt32("SelectedRoomId") ?? 0;
            CheckInDate = DateTime.Parse(HttpContext.Session.GetString("CheckInDate"));
            CheckOutDate = DateTime.Parse(HttpContext.Session.GetString("CheckOutDate"));

            IsRoomBooked = !string.IsNullOrEmpty(SelectedRoomType.ToString()) && SelectedRoomId != 0;
        }

    }

}