using BookApp.DTOs.RequestDTOs;
using BookApp.Entities;
using BookApp.Interfaces.IRepository;
using BookApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Services
{
    public class OrderService : IOrder_Interface
    { 
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IBaseRepository<OrderBook> _orderBookRepository;
        public OrderService(IBaseRepository<User> userRepository, IBaseRepository<Book> bookRepository, IBaseRepository<OrderBook> orderBookRepository)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _orderBookRepository = orderBookRepository;
            
        }
        public ServerResponse OrderBook(int Id, string email, OrderBookRequestDTO orderdto)
        {
            Func<User, bool> findByEmailPredicate = (User user) => user.Email == email;
            var user = _userRepository.GetByPredicate(findByEmailPredicate); 
            //var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var customer = new List<User>();

            Func<Book, bool> findBookByIdPredicate = (Book book) => book.BookId == Id; 
            var book = _bookRepository.GetByPredicate(findBookByIdPredicate);
            //var book = _context.Books.Where(x => x.BookId == Id).FirstOrDefault();

            if (book == null)
            {
                return new ServerResponse
                {
                    code = ServerResponseCode.InvalidBookIdCode,
                    message = ServerResponseCode.InvalidBookIdMessage
                };
            }
            if (book.Quantity < orderdto.Quantity)
            {
                return new ServerResponse
                {
                    code = ServerResponseCode.InsufficientBookQunatityAvailableCode,
                    message = ServerResponseCode.InsufficientBookQunatityAvailableMessage
                };
            }
            book.Quantity -= orderdto.Quantity;
            book.SoldCount += orderdto.Quantity;
            var OrderBook = new OrderBook
            {
                BookID = book.BookId,
                Quantity = orderdto.Quantity,
                CustomerName = orderdto.CustomerName,
                CustomerEmail = orderdto.CustomerEmail,
                Price = book.Price * orderdto.Quantity,
            };
            _orderBookRepository.Add(OrderBook);
            _orderBookRepository.SaveChanges();
            return new ServerResponse
            {
                code = ServerResponseCode.BookOrderedSuccessfullyCode,
                message = ServerResponseCode.BookOrderedSuccessfullyMessage
            };
        }
    }
}
