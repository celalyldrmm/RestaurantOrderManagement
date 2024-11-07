using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.EntityFramework;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal _notificationDal;
        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public List<Notification> TGetAllNotificationByFalse()
        {
            return _notificationDal.GetAllNotificationByFalse();
        }

        public int TNotificationCountbyStatusFalse()
        {
            return _notificationDal.NotificationCountbyStatusFalse();
        }

        void IGenericService<Notification>.TAdd(Notification entity)
        {
            _notificationDal.Add(entity);
        }

        void IGenericService<Notification>.TDelete(Notification entity)
        {
            _notificationDal.Delete(entity);
        }

        Notification IGenericService<Notification>.TGetByID(int id)
        {
            return _notificationDal.GetByID(id);
        }

        List<Notification> IGenericService<Notification>.TGetListAll()
        {
            return _notificationDal.GetListAll();
        }

        void IGenericService<Notification>.TUpdate(Notification entity)
        {
            _notificationDal.Update(entity);
        }
    }
}
