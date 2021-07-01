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
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Services value)
        {
            _context.Services.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        [HttpGet("{Id}")]
        public IActionResult GetAllServices(string Id)
        {
            var ratings = _context.Services.Where(s => s.UserId == Id);

            return Ok(ratings);
        }

    }
}