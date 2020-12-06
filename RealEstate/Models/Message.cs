using System;

namespace RealEstate.Models
{
    public class Message
    {
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsFromMe { get; set; }
    }
}
