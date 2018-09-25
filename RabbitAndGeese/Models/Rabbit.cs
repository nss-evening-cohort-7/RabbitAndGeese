using System.Collections.Generic;

namespace RabbitAndGeese.Models
{
    public class Rabbit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int MaxFeetPerSecond { get; set; }
        public Size Size { get; set; }
        public Sex Sex { get; set; }
        public List<Goose> OwnedGeese { get; set; } = new List<Goose>();
        public List<Saddle> OwnedSaddles { get; set; } = new List<Saddle>();

        public void SaddleThatGoose(Goose goose, Saddle saddle)
        {
            if (goose.Saddle != null)
            {
                goose.EmotionalState = "Exhausted";
                return;
            }

            if (goose.Size == saddle.Size)
            {
                goose.Saddle = saddle;
                saddle.InUse = true;
            }
            else if (goose.Size > saddle.Size)
            {
                goose.EmotionalState = "Distraught by your fat shaming";
            }
        }
    }
}