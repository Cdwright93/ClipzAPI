using ClipzAPI.Data;
using ClipzAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClipzAPI.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ratings value)
        {
            _context.Ratings.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        [HttpGet("{Id}")]
        public IActionResult GetAllRatings(string Id)
        {
            var ratings = _context.Ratings.Where(r => r.UserId == Id);


            return Ok(ratings);
        }

    }
}