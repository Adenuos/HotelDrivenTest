using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hotel.BLL;
using Hotel.BLL.Interface;
using Microsoft.AspNetCore.Http;

namespace HotelD.Pages
{
    public class BookingRoomModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomRepository _roomRepository;

        public BookingRoomModel(IBookingService bookingService, IRoomRepository roomRepository)
        {
            _bookingService = bookingService;
            _roomRepository = roomRepository;
        }

        public Room? BookedRoom { get; set; }

        [BindProperty]
        public DateTime CheckInDate { get; set; } = DateTime.Now;

        [BindProperty]
        public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(1);

        [BindProperty]
        public RoomType SelectedRoomType { get; set; }

        [BindProperty]
        public List<Room> AvailableRooms { get; set; }

        [BindProperty]
        public int TotalDays { get; set; }

        [BindProperty]
        public int SelectedRoomId { get; set; }

        public void OnGet()
        {
            TotalDays = (CheckOutDate - CheckInDate).Days;
            AvailableRooms = _bookingService.GetAvailableRooms(CheckInDate, CheckOutDate, SelectedRoomType);
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            TotalDays = (CheckOutDate - CheckInDate).Days;
            try
            {
                if (CheckInDate == default || CheckOutDate == default)
                {
                    // Om inga datum anges, använd statiska rum direkt
                    AvailableRooms = _roomRepository.GetAvailableRooms(DateTime.Now, DateTime.Now.AddDays(1), SelectedRoomType);
                }
                else
                {
                    // Annars hämta tillgängliga rum baserat på angivna datum och rumstyp
                    AvailableRooms = _bookingService.GetAvailableRooms(CheckInDate, CheckOutDate, SelectedRoomType);
                }

                
                TempData["SelectedRoomType"] = SelectedRoomType;
                TempData["SelectedRoomId"] = SelectedRoomId;
                return Page();

            }
            catch (Exception ex)
            {
                // Om något går fel, logga felet och visa ett generiskt felmeddelande på sidan
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
            
        }
        [HttpPost]
        public IActionResult OnPostBook()
        {
            AvailableRooms = _bookingService.GetAvailableRooms(CheckInDate, CheckOutDate, SelectedRoomType);
            var selectedRoom = AvailableRooms.FirstOrDefault(room => room.Id == SelectedRoomId);
            if (selectedRoom != null)
            {
                var booked = _bookingService.BookRoom(selectedRoom, CheckInDate, CheckOutDate);
                if (booked)
                {
                    HttpContext.Session.SetString("SelectedRoomType", SelectedRoomType.ToString());
                    HttpContext.Session.SetInt32("SelectedRoomId", SelectedRoomId);
                    HttpContext.Session.SetString("CheckInDate", CheckInDate.ToString("yyyy-MM-dd"));
                    HttpContext.Session.SetString("CheckOutDate", CheckOutDate.ToString("yyyy-MM-dd"));
                    return RedirectToPage("/Confirmation");
                    
                }
                else
                {
                    return StatusCode(400, "Room already booked for selected dates. Please choose another date.");
                }
            }
            else
            {
                return NotFound("Selected Room not found");
            }
        }

    }
}
