﻿
namespace ToDoList.Models
{
    public class TaskItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public int IsDeleted { get; set; }

    }
}