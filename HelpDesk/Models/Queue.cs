using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Queue
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
