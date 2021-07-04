using ClipzAPI.Data;
using ClipzAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClipzAPI.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Appointments value)
        {
            _context.Appointments.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAppt(int id,JsonPatchDocument<Appointments> appointmentUpdates)
        {
            var appointmentId = id;
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            appointmentUpdates.ApplyTo(appointment);
            _context.SaveChanges();
            return Ok(appointment);
        }
        [HttpGet("{Id}")]
        public IActionResult GetAllAppointments(string Id)
        {
            var appointments = _context.Appointments.Where(u => u.ServicerId == Id);
            var x = 0;
            foreach (var appointment in appointments)
            {
                x++;
            }
            if (x == 0)
            {
                appointments = _context.Appointments.Where(u => u.CustomerId == Id);
            }
            return Ok(appointments);
        }
    }

}
