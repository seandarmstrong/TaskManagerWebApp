using System;

namespace TaskList.Domain.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int TimeLeft { get; set; }
        public bool Complete { get; set; }
    }
}
