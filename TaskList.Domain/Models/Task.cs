using System;
using System.ComponentModel.DataAnnotations;

namespace TaskList.Domain.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required(ErrorMessage = "You must enter a task description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must enter a due date for the task")]
        //[DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public int TimeLeft { get; set; }
        public bool Complete { get; set; }
    }
}
