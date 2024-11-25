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

        public static implicit operator TaskItem(TaskItemUpdate taskItemPut)
        {
            return new TaskItem
            {
                Title = taskItemPut.Title,
                IsCompleted = taskItemPut.IsCompleted,
                Id = taskItemPut.Id
            };
        }
    }
}
