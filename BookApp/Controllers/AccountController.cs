using AutoMapper;
using BookApp.DataLayer;
using BookApp.DTOs.RequestDTOs;
using BookApp.Entities;
using BookApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        public AccountController(AppDatabaseContext context, IMapper mapper, IConfiguration configuration, IAccountService accountService)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequestDTO registerUserRequestDTO)
        {
            string hashPassword = _accountService.HashPassword(registerUserRequestDTO.Password);
            registerUserRequestDTO.Password = hashPassword;
            var user = _mapper.Map<User>(registerUserRequestDTO);
            var availableRoles = _context.Roles.ToList();
            user.Roles = new List<Role>();
            foreach (var role in registerUserRequestDTO.RoleIds)
            {
                foreach (var avaliableRole in availableRoles)
                {
                    if (role == avaliableRole.RoleId)
                    {
                        user.Roles.Add(avaliableRole);
                    }
                }
            }

            var addNewUser = _context.Users.Add(user);
            int isSaved = _context.SaveChanges();
            if (isSaved < 1)
            {
                return BadRequest("Record not saved");
            }
            return Ok(new
            {
                message = "User registered successfully",
                data = user
            });
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO model)
        {
            //validate user credentials
            if (!IsValidUser(model.Email, model.Password))
            {
                return Unauthorized();
            }
            var user = _context.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == model.Email);
            var roles = user.Roles.Select(r => r.RoleName);
    // Generate JWT token with roles and claims
            var claims = new List<Claim>
            {
             new Claim("Name", model.Email),
    // Add roles as claims
   
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecreteKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        private bool IsValidUser(string email, string password)
        {
            //Write a code to validate the email and password
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
