using AddBusSchedule.Dto;
using AddBusSchedule.Models;
using Google.Api.Gax;
using static Google.Api.Gax.Grpc.Gcp.AffinityConfig.Types;

namespace AddBusSchedule.Interfaces
{
    public interface IScheduleRepository
    {
       public Task<Schedule> CreateSchedule(ScheduleForDTO schedule);
        public Task<IEnumerable<Schedule>> GetSchedules();
        public Task<Schedule> GetSchedule(int id);
        public Task UpdateSchedule(int id, ScheduleForDTO schedule);
        public Task DeleteSchedule(int id);

    }
}
