using BookApp.DTOs.RequestDTOs;
using BookApp.Entities;
using BookApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp
{
   public interface IOrder_Interface
    {
        ServerResponse OrderBook(int Id, string email, OrderBookRequestDTO orderdto);
    }
}
