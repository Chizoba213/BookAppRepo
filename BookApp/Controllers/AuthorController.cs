using AutoMapper;
using BookApp.DataLayer;
using BookApp.DTOs.RequestDTOs;
using BookApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    [Route("author")]
    public class AuthorController : ControllerBase
    {
        private readonly AppDatabaseContext _context;
        private readonly IMapper _mapper;
        public AuthorController(AppDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //To add author to the record
        [HttpPost]
        [Route("addAuthor")]
        public IActionResult AddAuthor([FromBody] AuthorRequestDTO authordto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var author = new Author() { AuthorName = authordto.AuthorName };
           // var AddNewAuthor = _context.Authors.Add(author);
            int isSaved = _context.SaveChanges();
            if (isSaved < 1)
            {
                return BadRequest("Record not saved");
            }
            return Ok("addnewAuthor Entity");
        }
       // [HttpPost]
        //[Route("addAuthors")]
        //public IActionResult AddAuthors([FromBody] List<AuthorRequestDTO> authordto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var authors = _mapper.Map<Author>(authordto);
        //    var AddNewAuthors = _context.Authors.Add(authors);
        //    int isSaved = _context.SaveChanges();
        //    if (isSaved < 1)
        //    {
        //        return BadRequest("Record not saved");
        //    }
        //    return Ok("addnewAuthors Entity");
        //}

        //[HttpGet]
        //[Route("get-all-authors")]
        //public async Task<IActionResult> GetAllAuthors()
        //{
        //    var authors = await _context.Authors.Include(a => a.Books).ToListAsync();
        //    return Ok(authors);


        //}
    }
}
