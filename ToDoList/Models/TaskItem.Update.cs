using System.ComponentModel.DataAnnotations;
using ToDoList.Helper;

namespace ToDoList.Models
{
    public class TaskItemUpdate
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        public bool IsCompleted { get; set; } = true;
        public int IsDeleted { get; set; }
    }
}
