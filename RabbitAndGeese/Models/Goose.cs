using System.Collections.Generic;

namespace RabbitAndGeese.Models
{
    public class Goose
    {
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public Size Size { get; set; }
        public bool Social { get; set; }
        public Saddle Saddle { get; set; }
        public string EmotionalState { get; set; }
    }
}