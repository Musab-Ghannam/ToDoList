using System.ComponentModel.DataAnnotations;
using ToDoList.Controllers;
using ToDoList.Helper;

namespace ToDoList.Models
{
    public class TaskItemPost
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        public bool IsCompleted { get; set; } = true;

        public static implicit operator TaskItem(TaskItemPost taskItemPost)
        {
            return new TaskItem
            {
                Title = taskItemPost.Title,
                IsCompleted = taskItemPost.IsCompleted,
                Id = FileHelper.ReadCsvFile().Count() + 1
            };
        }
    

    }
}
