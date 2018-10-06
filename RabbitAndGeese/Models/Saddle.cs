namespace RabbitAndGeese.Models
{
    public class Saddle
    {
        public string Material { get; set; }
        public Size Size { get; set; }
        public double Price { get; set; }
        public SaddleType Type { get; set; }
        public bool InUse { get; set; }
        public bool Thing { get; set; }
        public bool Ugh { get; set; }
        public bool MakenBacon { get; set; }
    }

    public enum SaddleType
    {
        War,
        Speed,
        Yoga,
        Karate,
        Luxury,
        Party
    }
}