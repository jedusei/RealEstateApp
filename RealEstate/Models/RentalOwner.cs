namespace RealEstate.Models
{
    public class RentalOwner: User
    {
        public float Rating { get; set; }
        public int ReviewCount { get; set; }
        public string[] Languages { get; set; }
    }
}
