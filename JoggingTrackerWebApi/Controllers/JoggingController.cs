using System.Security.Claims;
using JoggingTrackerWebApi.Models;
using JoggingTrackerWebApi.Dto;
using JoggingTrackerWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JoggingTrackerWebApi.Controllers
{
    
    [Authorize(Roles = "User,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class JoggingController : ControllerBase
    {
        private readonly IJoggingService _joggingservice;

        public JoggingController(IJoggingService joggingservice)
        {
            _joggingservice = joggingservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var IsAdmin = User.IsInRole("Admin");

            var result= await _joggingservice.GetAllAsync(userId,IsAdmin);

            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var IsAdmin = User.IsInRole("Admin");
            var result = await _joggingservice.GetByIdAsync(id, userId,IsAdmin);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
                
        }

        [HttpPost]
        public async Task<IActionResult> Add(JoggingEntryDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var entry = new JoggingEntry
            {
                Date = dto.Date,
                Distance = dto.Distance,
                Duration = dto.Duration,
            };

             await _joggingservice.AddAsync(entry, userId);
            return Ok(new { message = "Entry added successfully"});

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Ubdate(int id, JoggingEntryDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var entry = new JoggingEntry
            {
                Id = id,
                Date = dto.Date,
                Distance = dto.Distance,
                Duration = dto.Duration,
            };

            try
            {
                await _joggingservice.UpdateAsync(entry, userId);
                return Ok(new { message = "Entry updated successfully" });
            }
            catch
            {
                return NotFound();
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _joggingservice.DeleteAsync(id, userId);
                return Ok(new { message = "Entry deleted successfully" });
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(DateTime startDate, DateTime endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            var result = await _joggingservice.FilterAsync(startDate, endDate, userId, isAdmin);

            return Ok(result);
        }
        [HttpGet("report")]
        public async Task<IActionResult> GetReport()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            var report = await _joggingservice.GetReportAsync(userId, isAdmin);

            return Ok(report);
        }
    }
}
