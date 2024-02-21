using AutoMapper;
using BookApp.DataLayer;
using BookApp.DTOs.RequestDTOs;
using BookApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly AppDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IOrder_Interface _orderservice;
        public OrderController(AppDatabaseContext context, IMapper mapper, IOrder_Interface orderservice)
        {
            _context = context;
            _mapper = mapper;
            _orderservice = orderservice;
        }

        [HttpPost]
        [Route("Orders/{Id:int}")]
        [Authorize]
        public IActionResult CreateOrders(int Id, [FromBody] OrderBookRequestDTO orderdto)
        {
            var userIdentity = HttpContext.User;
            string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();
            if (email == null)
            {
                return BadRequest();
            }
            var orderBookResponse = _orderservice.OrderBook(Id, email, orderdto);
            return Ok(orderBookResponse);
        }
    }
}
