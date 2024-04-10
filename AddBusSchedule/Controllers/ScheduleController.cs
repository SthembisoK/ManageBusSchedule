using AddBusSchedule.Dto;
using AddBusSchedule.Interfaces;
using AddBusSchedule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AddBusSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _scheduleRepo;

        public ScheduleController(IScheduleRepository scheduleRepo)
        {
            _scheduleRepo = scheduleRepo;
        }
        [HttpGet]

        public async Task<IActionResult> GetSchedules()
        {
            try
            {
                var schedules = await _scheduleRepo.GetSchedules();
                return Ok(schedules);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{id}", Name = "ScheduleById")]
        public async Task<IActionResult> GetSchedules(int id)
        {
            var schedule = await _scheduleRepo.GetSchedule(id);
            if (schedule == null)
                return NotFound();

            return Ok(schedule);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleForDTO schedule)
        {
            var createdSchedule = await _scheduleRepo.CreateSchedule(schedule);

            // Return the created schedule along with a success message
            return Ok(new { message = "Schedule successfully added", createdSchedule });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleForDTO schedule)
        {
            var dbschedule = await _scheduleRepo.GetSchedule(id);
            if (dbschedule == null)
                return NotFound("Please enter valid ID Baba.");

            await _scheduleRepo.UpdateSchedule(id, schedule);

            var updatedSchedule = await _scheduleRepo.GetSchedule(id);

            return Ok(new { message = "Schedule Updated Succesfully", driver = updatedSchedule });

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var dbschedule = await _scheduleRepo.GetSchedule(id);
            if (dbschedule == null)
                return NotFound("Cannot find the Id, Please insert the Correct Id");

            await _scheduleRepo.DeleteSchedule(id);

            return Ok("Schedule Deleted Successfully");
        }
    }
}
