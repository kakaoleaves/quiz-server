﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI_DotNet8.Data;
using QuizAPI_DotNet8.Entities;
using QuizAPI_DotNet8.Models;

namespace QuizAPI_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var dbUsers = await _context.Users.ToListAsync();

            return Ok(dbUsers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser is null)
            {
                return NotFound("user not found.");
            }

            return Ok(dbUser);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserLoginDto model)
        {
            if (ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
            {
                return BadRequest("username already exists.");
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                IsAdmin = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, UserLoginDto model)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser is null)
            {
                return NotFound("user not found.");
            }

            if (ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Users.AnyAsync(u => u.Username == model.Username && u.UserId != id))
            {
                return BadRequest("username already exists.");
            }

            dbUser.Username = model.Username;
            dbUser.Password = model.Password;

            await _context.SaveChangesAsync();

            return Ok(dbUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser is null)
            {
                return NotFound("user not found.");
            }

            _context.Users.Remove(dbUser);

            await _context.SaveChangesAsync();

            return Ok(dbUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLoginDto model)
        {
            if (ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }

            var username = model.Username;
            var password = model.Password;

            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (dbUser == null || dbUser.Password != password)
            {
                return NotFound("User not found");
            }

            return Ok(dbUser);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserLoginDto model)
        {
            if (ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                IsAdmin = false
            };

            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (dbUser is not null)
            {
                return BadRequest("username already exists.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}