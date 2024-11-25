
namespace ToDoList.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public int IsDeleted { get; set; }

        public static implicit operator TaskItemResponse(TaskItem taskItem)
        {
            return new TaskItemResponse
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                IsCompleted = taskItem.IsCompleted
            };

        }
    }
}
