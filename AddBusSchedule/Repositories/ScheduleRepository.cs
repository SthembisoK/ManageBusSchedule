using AddBusSchedule.Interfaces;
using AddBusSchedule.Models;
using Google.Cloud.Firestore;
using Dapper;
using AddBusSchedule.Context;
using static Google.Api.Gax.Grpc.Gcp.AffinityConfig.Types;
using System.Data;
using AddBusSchedule.Dto;
using Google.Api.Gax;
using Google.Api;

namespace AddBusSchedule.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DBContext _dbContext;

        public ScheduleRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Schedule>> GetSchedules()
        {
            var query = "Select * FROM Schedules";
            using (var connection = _dbContext.CreateConnection())
            {
                var schedules = await connection.QueryAsync<Schedule>(query);
                return schedules.ToList();

            }
        }

        public async Task<Schedule> GetSchedule(int id)
        {
            var query = "SELECT * FROM Schedules WHERE Id=@Id";

            using (var connection = _dbContext.CreateConnection())
            {
                var schedule = await connection.QuerySingleOrDefaultAsync<Schedule>(query, new { id });

                return schedule;
            }
        }

        public async Task<Schedule> CreateSchedule(ScheduleForDTO schedule)
        {
            var query = "Insert into Schedules (RouteNo, Source,Destination,StartTime,Day )" +
              " VALUES (@RouteNo, @Source,@Destination,@StartTime,@Day);SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var parameters = new DynamicParameters();
            parameters.Add("RouteNo", schedule.RouteNo, DbType.String);
            parameters.Add("Source", schedule.Source, DbType.String);
            parameters.Add("Destination", schedule.Destination, DbType.String);
            parameters.Add("StartTime", schedule.StartTime, DbType.DateTime2);
            parameters.Add("Day", schedule.Day, DbType.String);


            using (var connection = _dbContext.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdSchedule = new Schedule
                {
                    Id = id,
                    RouteNo = schedule.RouteNo,
                    Source = schedule.Source,
                    Destination = schedule.Destination,
                    StartTime = schedule.StartTime,
                    Day = schedule.Day
                };
                return createdSchedule;
            }
        }

        public async Task UpdateSchedule(int id, ScheduleForDTO schedule)
        {
            var query = "UPDATE Schedules SET RouteNo=@RouteNo, Source=@Source, Destination=@Destination, StartTime=@StartTime, Day=@Day WHERE Id=@Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("RouteNo", schedule.RouteNo, DbType.String);
            parameters.Add("Source", schedule.Source, DbType.String);
            parameters.Add("Destination", schedule.Destination, DbType.String);
            parameters.Add("StartTime", schedule.StartTime, DbType.DateTime2);
            parameters.Add("Day", schedule.Day, DbType.String);

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }
        }

        public async Task DeleteSchedule(int id)
        {
            var query = "DELETE FROM Schedules WHERE Id = @Id";

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
