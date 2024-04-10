using Google.Cloud.Firestore;

namespace AddBusSchedule.Models
{

    public class Schedule
    {
        public int Id { get; set; }
       
        public int RouteNo { get; set; }
        
        public string Source { get; set; }
    
        public string Destination { get; set; }
       
        public DateTime StartTime { get; set; }
      
        public string Day { get; set; }
        
    }
}
