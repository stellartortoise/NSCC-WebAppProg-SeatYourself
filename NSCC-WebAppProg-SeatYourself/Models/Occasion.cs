namespace NSCC_WebAppProg_SeatYourself.Models
{
    public class Occasion
    {
        //Primary key
        public int OccasionId { get; set; }


        //Attributes
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Owner { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }


        //Foreign keys
        public int VenueId { get; set; }
        public int CategoryId { get; set; }
        

        //Navigation properties
        public Venue? Venue { get; set; }
        public Category? Category { get; set; }
    }
}
