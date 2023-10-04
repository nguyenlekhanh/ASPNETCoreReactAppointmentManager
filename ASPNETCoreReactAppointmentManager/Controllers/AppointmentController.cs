using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreReactAppointmentManager.Data;
using ASPNETCoreReactAppointmentManager.Data.Models;
using Microsoft.Data.SqlClient;

namespace ASPNETCoreReactAppointmentManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
          if (_context.Appointments == null)
          {
              return NotFound("No Data Found");
          }
            return await _context.Appointments.Where(a => !a.Deleted && !a.Done).ToListAsync();
        }

        // GET: api/Appointment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
          if (_context.Appointments == null)
          {
              return NotFound("No Data Found");
          }
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound("No Data Found");
            }

            return appointment;
        }

        [HttpPost("filters")]
        public async Task<ActionResult<IEnumerable<Appointment>>> FilteredAppointments(Filter filter)
        {
            if(_context.Appointments == null)
            {
                return NotFound("No Data Found");
            }

            List<Appointment> allData = await _context.Appointments.ToListAsync();

            if(filter.All)
            {
                return allData;
            }

            if(filter.LevelOfImportance != null)
            {
                allData = allData.Where(e => e.LevelOfImportance == filter.LevelOfImportance).ToList();
            }

            if (filter.SpecifiedDate != null)
            {
                allData = allData.Where(e => e.Date == filter.SpecifiedDate).ToList();
            }

            if (filter.StartDate != null)
            {
                allData = allData.Where(e => e.Date >= filter.StartDate && e.Date <= filter.EndDate).ToList();
            }

            if (filter.SpecifiedTime != null)
            {
                allData = allData.Where(e => e.Time == filter.SpecifiedTime).ToList();
            }

            allData = allData.Where(e => e.Done == filter.Done).ToList();
            allData = allData.Where(e => e.Deleted == filter.Deleted).ToList();

            return allData;
        }

        // PUT: api/Appointment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("You are trying to modify the wrong appointment");
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                Appointment entry = await _context.Appointments.FindAsync(appointment.Id);

                if(entry != null) { 
                    if(entry.Title != appointment.Title)
                    {
                        entry.Title = appointment.Title;
                    }
                    if (entry.Description != appointment.Description)
                    {
                        entry.Description = appointment.Description;
                    }
                    if (entry.Address != appointment.Address)
                    {
                        entry.Address = appointment.Address;
                    }
                    if (entry.LevelOfImportance != appointment.LevelOfImportance)
                    {
                        entry.LevelOfImportance = appointment.LevelOfImportance;
                    }
                    if (entry.Done != appointment.Done)
                    {
                        entry.Done = appointment.Done;
                    }
                    if (entry.Deleted != appointment.Deleted)
                    {
                        entry.Deleted = appointment.Deleted;
                    }
                    if (entry.Date != appointment.Date)
                    {
                        entry.Date = appointment.Date;
                    }
                    if (entry.Time != appointment.Time)
                    { 
                        entry.Time = appointment.Time;
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound($"The appointment with Id {appointment.Id} does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Appointment updated successfully");
        }

        // POST: api/Appointment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'Appointments'  is null.");
            }
            
            try { 
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                return BadRequest("Could not create the new Appointment: " + ex.Message);
            }

            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
        }

        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (_context.Appointments == null)
            {
                return NotFound("No Data Found");
            }
            Appointment appointment = await _context.Appointments.FirstAsync(e => e.Id == id);
            if (appointment == null)
            {
                return NotFound("No appointment with the ID " + id);
            }

            //_context.Appointments.Remove(appointment);

            appointment.Deleted = true;
            await _context.SaveChangesAsync();

            return Ok("Appointment deleted successfully.");
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
