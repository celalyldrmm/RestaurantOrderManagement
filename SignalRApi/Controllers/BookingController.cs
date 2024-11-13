using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.BookingDto;
using SignalR.EntityLayer.Entities;
using System.Net.Mail;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet]
        public IActionResult BookingList()
        {
            var value=_bookingService.TGetListAll();
            return Ok(value);
        }
        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDto createBookingDto) 
        {
            Booking booking = new Booking()
            {
                Mail = createBookingDto.Mail,
                Date = createBookingDto.Date,
                Name = createBookingDto.Name,
                PersonCount = createBookingDto.PersonCount,
                Phone = createBookingDto.Phone,
                Description=createBookingDto.Description
            };
            _bookingService.TAdd(booking);
            return Ok("Rezervasyon başarıyla yapıldı!");
        }
        [HttpPut]
        public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto) 
        {
            Booking booking = new Booking()
            {
                BookingID = updateBookingDto.BookingID,
                Mail = updateBookingDto.Mail,
                Date = updateBookingDto.Date,
                Name = updateBookingDto.Name,
                PersonCount = updateBookingDto.PersonCount,
                Phone = updateBookingDto.Phone,
                Description=updateBookingDto.Description
            };
            _bookingService.TUpdate(booking);
            return Ok("Rezervasyon başarıyla güncellendi!");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            _bookingService.TDelete(value);
            return Ok("Rezervasyonunuz iptal edilmiştir!");
        }
        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            return Ok(value);
        }
        [HttpGet("BookingStatusApproved/{id}")]
        public IActionResult BookingStatusApproved(int id)
        {
            _bookingService.TBookingStatusApproved(id);
            return Ok("Rezervasyon Açıklaması Değiştirildi");
        }
        [HttpGet("BookingStatusCancelled/{id}")]
        public IActionResult BookingStatusCancelled(int id)
        {
             _bookingService.TBookingStatusCancelled(id);
            return Ok("Rezervasyon Açıklaması Değiştirildi");
        }
        [HttpGet("BookingStatusCancelledList")]
        public IActionResult BookingStatusCancelledList()
        {
            var cancelledBookings = _bookingService.TBookingStatusCancelledList();
            return Ok(cancelledBookings);
        }
        [HttpGet("BookingStatusApprovedList")]
        public IActionResult BookingStatusApprovedList()
        {
            var approvedBookings= _bookingService.TBookingStatusApprovedList();
            return Ok(approvedBookings);
        }
    }
}
