using System.ComponentModel.DataAnnotations;
using ToDoList.Helper;

namespace ToDoList.Models
{
    public class TaskItemDelete
    {
        public int Id { get; set; }
        public int IsDeleted { get; set; } = 1;
    }
}
