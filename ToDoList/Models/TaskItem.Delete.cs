using System.ComponentModel.DataAnnotations;
using ToDoList.Helper;

namespace ToDoList.Models
{
    public class TaskItemDelete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; } = true;
        public int IsDeleted { get; set; } = 1;

        public static implicit operator TaskItemUpdate(TaskItemDelete taskItemDelete)
        {
            return new TaskItemUpdate
            {
                Title = taskItemDelete.Title,
                IsCompleted = taskItemDelete.IsCompleted,
                IsDeleted = 1,
                Id = taskItemDelete.Id,
            };
        }
    }
}
