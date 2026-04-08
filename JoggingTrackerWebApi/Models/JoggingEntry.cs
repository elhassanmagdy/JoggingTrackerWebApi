namespace JoggingTrackerWebApi.Models
{
    public class JoggingEntry
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public double Distance { get; set; } // km
        public int Duration { get; set; } // minutes

        // relation with user
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
