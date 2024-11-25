using Microsoft.AspNetCore.Mvc;
using ToDoList.Helper;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {

        public IActionResult Index()
        {
            var taskItemList = FileHelper.ReadCsvFile();
            return View(taskItemList);
        }

        #region CreateTask
        public IActionResult CreateTask()
        {
            var list = new TaskItemPost();
            return View(list);
        }

        [HttpPost]
        public IActionResult CreateTask(TaskItemPost taskItem)
        {
            var toDoList = FileHelper.ReadCsvFile().ToList();
            toDoList.Add(taskItem);
            var taskItemList = FileHelper.AddTaskToCSVFile(toDoList);
            return View(nameof(Index), taskItemList);
        }

        #endregion

        #region GetTaskById
        [HttpGet]
        public IActionResult GetTaskById(int id)
        {
            var taskItem = FileHelper.ReadSingleRecordCsvFile(c => c.Id == id && c.IsDeleted == 0);
            var taskresponse = (TaskItemResponse)taskItem;
            return View(taskresponse);
        }
        #endregion

        #region UpdateTask
        [HttpPost]
        public IActionResult UpdateItem(TaskItemUpdate taskItemUpadte)
        {
            var taskresponse = FileHelper.UpdateToDoList(taskItemUpadte);
            return View(nameof(Index), taskresponse);
        }
        #endregion

        #region DeleteTask
        [HttpPost]
        public IActionResult DeleteItem(int id)
        {
            var taskItem = FileHelper.ReadSingleRecordCsvFile(c => c.Id == id && c.IsDeleted == 0);
            var taskUpdate = new TaskItemUpdate
            {
                Id = id,
                IsDeleted = 1,
                IsCompleted = taskItem.IsCompleted,
                Title = taskItem.Title,
            };
            var taskresponse = FileHelper.UpdateToDoList(taskUpdate);
            return View(nameof(Index), taskresponse);
        }
        #endregion

    }
}
