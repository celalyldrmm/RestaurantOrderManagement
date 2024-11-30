﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.BasketDto;
using SignalR.EntityLayer.Entities;
using SignalRApi.Models;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet]
        public IActionResult GetBasketByMenuTableNumber(int id)
        {
            var values = _basketService.TGetBasketByMenuTableNumber(id);
            return Ok(values);
        }

        [HttpGet("BasketListByMenuTableWithProductName")]
        public IActionResult BasketListByMenuTableWithProductName(int id)
        {
            using var context = new SignalRContext();
            var values = context.Baskets.Include(x => x.Product).Where(y => y.MenuTableID == id).Select(z => new ResultBasketListWithProducts
            {
                BasketID=z.BasketID,
                Count=z.Count,
                Price=z.Price,
                TotalPrice=z.TotalPrice,
                ProductID=z.ProductID,
                MenuTableID=z.MenuTableID,
                ProductName=z.Product.ProductName,
            }).ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateBasket(CreateBasketDto createBasketDto)
        {
            using var context=new SignalRContext();
            _basketService.TAdd(new Basket()
            {
                Count=1,
                MenuTableID = createBasketDto.MenuTableID,
                Price=context.Products.Where(x=>x.ProductID==createBasketDto.ProductID).Select(y=>y.Price).FirstOrDefault(),
                TotalPrice=createBasketDto.TotalPrice,
                ProductID=createBasketDto.ProductID
            });
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBasket(int id)
        {   
            var value = _basketService.TGetByID(id);
            _basketService.TDelete(value);
            return Ok("Sepetteki seçilen ürün silindi!");
        }
    }
}
