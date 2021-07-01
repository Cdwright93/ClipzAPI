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
    }
}
