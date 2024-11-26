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
        public int IsDeleted { get; set; } = 0;

        public static implicit operator TaskItem(TaskItemPost taskItemPost)
        {
            return new TaskItem
            {
                Title = taskItemPost.Title,
                IsCompleted = taskItemPost.IsCompleted,
                IsDeleted = taskItemPost.IsDeleted,
                Id = FileHelper.ReadAllCsvFile().Count() + 1
            };
        }
    

    }
}
