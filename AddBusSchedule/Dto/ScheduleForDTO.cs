namespace AddBusSchedule.Dto
{
    public class ScheduleForDTO
    {
        public int RouteNo { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public DateTime StartTime { get; set; }

        public string Day { get; set; }
    }
}
