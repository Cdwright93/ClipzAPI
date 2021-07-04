using ClipzAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClipzAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ClipzAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        // <baseurl>/api/examples/user
        [HttpGet("user"), Authorize]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            var x = 0;
            var f = 0;
            var overallRating = _context.Ratings.Where(u => u.UserId == userId);
            foreach (var rating in overallRating)
            {
                x++;
                f += rating.Rating;
            }
            var update = new double();
            if (x != 0)
            {
               update = f / x;
               user.Overall_rating = update;
               _context.Update(user);
               _context.SaveChanges();
            }
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPatch("update"), Authorize]
        public async Task<ActionResult> UpdateUser(JsonPatchDocument<AspNetUsers> userUpdates)
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            userUpdates.ApplyTo(user);
            _context.SaveChanges();
            return Ok(user);
        }
        [HttpGet("all")]
        public IActionResult GetAllservicers()
        {
            var users = _context.Users.Where(r => r.Is_servicer == true);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
    }
}
