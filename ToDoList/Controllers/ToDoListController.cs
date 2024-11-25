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
            var taskItem = FileHelper.ReadSingleRecordCsvFile(c => c.Id == id);
            var taskresponse = (TaskItemResponse)taskItem;
            return View(taskresponse);
        }
        #endregion

        #region UpdateTask
        [HttpPatch]
        public IActionResult UpdateItem(TaskItemUpdate taskItemUpadte)
        {
            var taskItem = FileHelper.ReadSingleRecordCsvFile(c => c.Id == taskItemUpadte.Id);
            var taskresponse = (TaskItemResponse)taskItem;
            return View(taskresponse);
        }
        #endregion

    }
}
