using Microsoft.AspNetCore.Mvc;
using ToDoList.Helper;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoListController : BaseController
    {

        #region GetTasks
        public IActionResult Index()
        {
            var taskItemList = FileHelper.ReadCsvFile(c=>c.IsCompleted == true &&  c.IsDeleted == 0);
            return View(taskItemList);
        }

        public IActionResult PendingTask()
        {
            var taskItemList = FileHelper.ReadCsvFile(c => c.IsCompleted == false && c.IsDeleted == 0);
            return View(taskItemList);
        }
        #endregion

        #region CreateTask
        public IActionResult CreateTask()
        {
            var list = new TaskItemPost();
            return View(list);
        }

        [HttpPost]
        public IActionResult CreateTask(TaskItemPost taskItem)
        {
            if (!ModelState.IsValid)
            {
                return View(taskItem);
            }
            var toDoList = FileHelper.ReadCsvFile().ToList();
            toDoList.Add(taskItem);
            var taskItemList = FileHelper.AddTaskToCSVFile(toDoList);
            if (taskItemList)
            {
                ShowSuccessMessage("Task Created Successfully");
            }
            else
            {
                ShowErrorMessage("There is an issue");
            }
            return RedirectToAction(nameof(Index));
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
            if (!ModelState.IsValid)
            {
                return View(taskItemUpadte);
            }
            var taskresponse = FileHelper.UpdateToDoList(taskItemUpadte);
            if (taskresponse)
            {
                ShowSuccessMessage("Task Updated Successfully");
            }
            else
            {
                ShowErrorMessage("There is an issue");
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region DeleteTask
        [HttpPost]
        public IActionResult DeleteItem(TaskItemDelete taskItemDelete)
        {
            var taskItem = FileHelper.ReadSingleRecordCsvFile(c => c.Id == taskItemDelete.Id && c.IsDeleted == 0);
            if (taskItem is null)
            {
                return BadRequest();
            }
            var taskresponse = FileHelper.DelteToDoList(taskItemDelete);
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
