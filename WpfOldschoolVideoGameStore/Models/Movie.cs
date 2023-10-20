namespace WpfOldschoolVideoGameStore.Models
{
    public class Movie : IMedia
    {
        public string TypeOfMedia { get; set; } = "Movie";
        public string Name { get; set; }
        public int Rating { get; set; }
        public int NumRatings { get; set; }
        public string Genre { get; set; }
        public Customer Renter { get; set; }
        public bool IsRentedOut { get; set; }
        public bool IsRRated { get; set; }

        public string IsAvailable()
        {
            if (!IsRentedOut)
            {
                return "✔";
            }
            return "";
        }

        public string IsRRatedString()
        {
            if (IsRRated)
            {
                return "✔";
            }
            return "";
        }
    }
}
