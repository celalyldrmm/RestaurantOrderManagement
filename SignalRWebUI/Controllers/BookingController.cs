using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalR.DataAccessLayer.Concrete;
using SignalR.EntityLayer.Entities;
using SignalRWebUI.Dtos.BookingDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5195/api/Booking");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateBooking()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {
            createBookingDto.Description = "Rezervasyon Alındı";
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5195/api/Booking", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5195/api/Booking/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> UpdateBooking(int id)
        {
            var clint = _httpClientFactory.CreateClient();
            var responseMessage = await clint.GetAsync($"http://localhost:5195/api/Booking/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBookingDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBookingDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5195/api/Booking", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }
        public async Task<IActionResult> BookingStatusApproved(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5195/api/Booking/BookingStatusApproved/{id}");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> BookingStatusCancelled(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5195/api/Booking/BookingStatusCancelled/{id}");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> BookingStatusApprovedList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5195/api/Booking/BookingStatusApprovedList");
            using var context = new SignalRContext();
            var x = context.Bookings.Where(x => x.Description == "Rezervasyon Onaylandı").Count();

            if (responseMessage.IsSuccessStatusCode)
            {
                if (x != 0)
                {
                    var approvedBookings = await responseMessage.Content.ReadFromJsonAsync<List<Booking>>();
                    return View(approvedBookings);
                }
                else
                {
                    return Content(
                                        "<div style='display: flex; align-items: center; justify-content: center; height: 100vh; text-align: center;'>" +
                                            "<div>" +
                                            "<h2>Kimsenin Rezervasyonu Onaylanmamış</h2>" +
                                            "<meta http-equiv='refresh' content='20;url=/Booking/Index' />" +
                                            "</div>" +
                                        "</div>",
                                     "text/html"
    );
                }

            }

            return View(new List<Booking>());
        }

        public async Task<IActionResult> BookingStatusCancelledList()
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5195/api/Booking/BookingStatusCancelledList");
            using var context = new SignalRContext();
            var x = context.Bookings.Where(x => x.Description == "Rezervasyon İptal Edildi").Count();
            if (responseMessage.IsSuccessStatusCode)
            {
                if (x != 0)
                {
                    var cancelledBookings = await responseMessage.Content.ReadFromJsonAsync<List<Booking>>();
                    return View(cancelledBookings);
                }
                else
                {
                    return Content(
                                      "<div style='display: flex; align-items: center; justify-content: center; height: 100vh; text-align: center;'>" +
                                        "<div>" +
                                        "<h2>Kimsenin Rezervasyonu Iptal Edilmemis</h2>" +
                                        "<meta http-equiv='refresh' content='20;url=/Booking/Index' />" +
                                        "</div>" +
                                      "</div>",
                                      "text/html"
                                    );
                }

            }

            return View(new List<Booking>());
        }
    }
}
