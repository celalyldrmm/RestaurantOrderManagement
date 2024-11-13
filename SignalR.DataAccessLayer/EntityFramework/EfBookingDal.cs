using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        public EfBookingDal(SignalRContext context) : base(context)
        {
        }

        public void BookingStatusApproved(int id)
        {
            using var context = new SignalRContext();
            var values =context.Bookings.Find(id);
            values.Description = "Rezervasyon Onaylandı";
            context.SaveChanges();
        }

        public List<Booking> BookingStatusApprovedList()
        {
            using var context = new SignalRContext();
            var approvedBookings = context.Bookings
                                           .Where(b => b.Description == "Rezervasyon Onaylandı")
                                           .ToList();
            return approvedBookings;
        }

        public void BookingStatusCancelled(int id)
        {
            using var context = new SignalRContext();
            var values = context.Bookings.Find(id);
            values.Description = "Rezervasyon İptal Edildi";
            context.SaveChanges();
        }

        public List<Booking> BookingStatusCancelledList()
        {
            using var context = new SignalRContext();
            var cancelledBookings = context.Bookings
                                           .Where(b => b.Description == "Rezervasyon İptal Edildi")
                                           .ToList();
            return cancelledBookings;
        }
    }
}
