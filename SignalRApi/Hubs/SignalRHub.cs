using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;

namespace SignalRApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IMenuTableService _menuTableService;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;

        public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService orderService, IMoneyCaseService moneyCaseService, IMenuTableService menuTableService, IBookingService bookingService, INotificationService notificationService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _moneyCaseService = moneyCaseService;
            _menuTableService = menuTableService;
            _bookingService = bookingService;
            _notificationService = notificationService;
        }

        public async Task SendStatistic()
        {
            var value = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount", value);

            var value2 = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", value2);

            var value3 = _categoryService.TActiveCategoryCount();
            await Clients.All.SendAsync("ReceiveActiveCategoryCount", value3);

            var value4 = _categoryService.TPassiveCategoryCount();
            await Clients.All.SendAsync("ReceivePassiveCategoryCount", value4);

            var value5 = _productService.TProductCountByCategoryHamburger();
            await Clients.All.SendAsync("ReceiveTProductCountByCategoryHamburger", value5);

            var value6 = _productService.TProductCountByCategoryDrink();
            await Clients.All.SendAsync("ReceiveTProductCountByCategoryDrink", value6);

            var value7 = _productService.TProductPriceAvg();
            await Clients.All.SendAsync("ReceiveTProductPriceAvg", value7.ToString("0.00") + "₺");

            var value8 = _productService.TProductNameByMaxPrice();
            await Clients.All.SendAsync("ReceiveTProductNameByMaxPrice", value8);

            var value9 = _productService.TProductNameByMinPrice();
            await Clients.All.SendAsync("ReceiveTProductNameByMinPrice", value9);

            var value10 = _productService.TProductPriceByHamburger();
            await Clients.All.SendAsync("ReceiveTProductPriceByHamburger", value10.ToString("0.00") + "₺");

            var value11 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTTotalOrderCount", value11);

            var value12 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveTActiveOrderCount", value12);

            var value13 = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveTLastOrderPrice", value13.ToString("0.00") + "₺");

            var value14 = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTTotalMoneyCaseAmount", value14.ToString("0.00") + "₺");

            var value15 = _orderService.TTodayTotalPrice();
            await Clients.All.SendAsync("ReceiveTTodayTotalPrice", value15.ToString("0.00") + "₺");

            var value16 = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveTMenuTableCount", value16);

        }

        public async Task SendProgress()
        {
            var value = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReciveTTotalMoneyCaseAmount", value.ToString("0.00") + "₺");

            var value2 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", value2);

            var value3 = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", value3);
        }

        public async Task GetBookingList()
        {
            var values = _bookingService.TGetListAll();
            await Clients.All.SendAsync("ReceiveBookingList", values);
        }
        public async Task SendNotification()
        {
            var value = _notificationService.TNotificationCountbyStatusFalse();
            await Clients.All.SendAsync("ReciveNotificationCountbyStatusFalse", value);

            var value1 = _notificationService.TGetAllNotificationByFalse();
            await Clients.All.SendAsync("ReciveGetAllNotificationByFalse", value1);
        }
        public async Task GetMenuTableStatus()
        {
            var value=_menuTableService.TGetListAll();
            await Clients.All.SendAsync("ReciveGetMenuTableStatus",value);
        }
        public async Task SendMessage(string user string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user , message);
        }

    }
}
