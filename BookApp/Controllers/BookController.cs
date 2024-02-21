using AutoMapper;
using BookApp.DataLayer;
using BookApp.DTOs.RequestDTOs;
using BookApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BookApp.Controllers
{

    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;
        public BookController(AppDatabaseContext context, IMapper mapper, ILogger<BookController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("addBookWithOneAuthor")]
        [Authorize]
        public IActionResult AddBookWithOneAuthor([FromBody] BookWithOneAuthorRequestDTO bookdto)
        {
            var book = _mapper.Map<Book>(bookdto);
            var userIdentity = HttpContext.User;
            string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value.ToString();
            if (email == "" || email == null)
            {
                return NotFound("Email not found");
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User not found");
            }
            book.Users = new List<User> { user };
            _context.Books.Add(book);
            _context.SaveChanges();
            return Ok("Book with one author added succesfully");
        }


        [HttpPost]
        [Route("addBookWithManyAuthorsCorrection")]
        [Authorize(Roles = "Author")]
        public IActionResult AddBookWithManyAuthors([FromBody] BookWithManyAuthorsRequestDTO bookdto)
        {
            try
            {
                var userIdentity = HttpContext.User;
                string email = userIdentity.Claims.FirstOrDefault(c => c.Type == "Name")?.Value.ToString();
                if (email == null)
                {
                    return BadRequest();
                }
                var user = _context.Users.FirstOrDefault(u => u.Email == email);
                var otherAuthors = new List<User>();
                foreach (var Id in bookdto.AuthorIds)
                {
                    var u = _context.Users.Where(a => a.UserId == Id).Include(p => p.Roles).FirstOrDefault();
                    if (u != null)
                    {
                        otherAuthors.Add(u);
                    }
                }
                // return Ok( "Testing");
                var AllAuthors = new List<User>();
                //Validate Author Roles
                foreach (var ot in otherAuthors)
                {
                    foreach (var r in ot.Roles)
                        if (r.RoleName == "Author")
                        {
                            AllAuthors.Add(ot);
                            break;
                        }
                }
                AllAuthors.Add(user);
                var book = _mapper.Map<Book>(bookdto);
                book.Users = AllAuthors;
                _context.Books.Add(book);
                _context.SaveChanges();
                return Ok("Book With Many Authors Added Successfully.");

            }
            catch (Exception ex)
                
            {
                _logger.LogError(ex.Message);

                return StatusCode(500);
            } 

        }

        //[HttpGet]
        //[Route("books/authors")]
        //[Authorize]

        //public IActionResult GetAuthorsSale()
        //{
        //    var books = _context.Books.Where(b => b.Author == CustomerId).ToList();
        //    var 
        //}







        //To add book to the record 
        [HttpPost]
        [Route("addBook")]
        public IActionResult AddBook([FromBody] BookRequestDTO bookdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var book = new Book()
            //{
            //    AuthorId = bookdto.AuthorId,
            //    Title = bookdto.Title,
            //    Quantity = bookdto.Quantity,
            //    Price = bookdto.Price
            //};
            var book = _mapper.Map<Book>(bookdto);
            var AddNewBook = _context.Books.Add(book);
            int isSaved = _context.SaveChanges();
            if (isSaved < 1)
            {
                return BadRequest("Record not saved");
            }
            return Ok(new
            {
                message = "Book added successfully",
                data = book
            }) ;
             
        }
        [HttpPost]
        [Route("addBookWithAuthors")]
        public IActionResult AddBookWithAuthors([FromBody] BookWithAuthorsRequestDTO bookdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var book = new Book()
            //{
            //    AuthorId = bookdto.AuthorId,
            //    Title = bookdto.Title,
            //    Quantity = bookdto.Quantity,
            //    Price = bookdto.Price
            //};
            var book = _mapper.Map<Book>(bookdto);
            var AddNewBook = _context.Books.Add(book);
            int isSaved = _context.SaveChanges();
            if (isSaved < 1)
            {
                return BadRequest("Record not saved");
            }
            return Ok(new
            {
                message = "Book added successfully",
                data = book
            });

        }

        //To get all book in the record
        [HttpGet]
        [Route("getAllBooks")]
        public IActionResult GetBooks()
        {
            var books = _context.Books.ToList();
            if (books.Count()  > 0)
            {
                return Ok(books);
            }
            return NotFound("No record found");
        }


        //To Get book by Id
        [HttpGet]
        [Route ("getBook/{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _context.Books.Where(x => x.BookId == id).FirstOrDefault();
            if (book == null)
            {
                return NotFound($"Book of id of {id} does not exist");
            }
            return Ok(book);
        }

        //To delete from the record
        [HttpDelete]
        [Route("deleteBook/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Where(x => x.BookId == id).FirstOrDefault();
            if (book == null)
            {
                return BadRequest();
            }
           _context.Books.Remove(book);
            _context.SaveChanges();
            return NoContent();
        }

        //To update book
        [HttpPut]
        [Route("updateBook/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var existingBook = _context.Books.FirstOrDefault(x => x.BookId == id);

            if (existingBook == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            existingBook.BookId = updatedBook.BookId;
            existingBook.Title = updatedBook.Title;
            //existingBook.Author = updatedBook.Author;
            existingBook.Price = updatedBook.Price;
            existingBook.Quantity = updatedBook.Quantity;
            

            _context.SaveChanges(); 

            return Ok(existingBook);
        }


    }
}
