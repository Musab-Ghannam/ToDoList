using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;
using ToDoList.Models;

namespace ToDoList.Helper
{
    public static class FileHelper
    {
        #region Properties
        private const string FILE_NAME = "TaskItem.csv";
        private const string Folder_NAME = "TaskItemList";
        #endregion

        #region ReadFile
        public static IEnumerable<TaskItem> ReadCsvFile()
        {
            try
            {
                using (var reader = new StreamReader(GetFilePath()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<TaskItem>().Where(c => c.IsDeleted == 0);
                    return records.ToList();
                }
            }
            catch (HeaderValidationException ex)
            {
                throw new ApplicationException("CSV file header is invalid.", ex);
            }
            catch (TypeConverterException ex)
            {
                throw new ApplicationException("CSV file contains invalid data format.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error reading CSV file", ex);
            }
        }

        public static IEnumerable<TaskItem> ReadAllCsvFile()
        {
            try
            {
                using (var reader = new StreamReader(GetFilePath()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<TaskItem>();
                    return records.ToList();
                }
            }
            catch (HeaderValidationException ex)
            {
                throw new ApplicationException("CSV file header is invalid.", ex);
            }
            catch (TypeConverterException ex)
            {
                throw new ApplicationException("CSV file contains invalid data format.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error reading CSV file", ex);
            }
        }
        public static TaskItem ReadSingleRecordCsvFile(Func<TaskItem, bool> func)
        {
            try
            {
                using (var reader = new StreamReader(GetFilePath()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<TaskItem>().FirstOrDefault(func);
                    return records;
                }
            }
            catch (HeaderValidationException ex)
            {
                throw new ApplicationException("CSV file header is invalid.", ex);
            }
            catch (TypeConverterException ex)
            {
                throw new ApplicationException("CSV file contains invalid data format.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error reading CSV file", ex);
            }
        }
        #endregion

        #region AddToDoList
        public static bool AddTaskToCSVFile(List<TaskItem> taskItemList)
        {
            try
            {
                if (taskItemList is null)
                {
                    return false;
                }
                using (var writer = new StreamWriter(GetFilePath()))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(taskItemList);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error Update CSV file", ex);
            }

        }

        public static void CreateAndAddListToCSVFile()
        {
            string taskFile = Path.Combine(Directory.GetCurrentDirectory(), Folder_NAME, FILE_NAME);

            if (!Directory.Exists(taskFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(taskFile));
            }

            if (!File.Exists(taskFile))
            {
                using (var writer = new StreamWriter(taskFile))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(GetToDoList());
                    }
                }
            }
        }

        #endregion

        #region UpdateToDoList
        public static bool UpdateToDoList(TaskItemUpdate taskItemUpadte)
        {
            try
            {
                if (taskItemUpadte is null)
                {
                    return false;
                }
                var existToDoList = ReadAllCsvFile();
                var existTask = existToDoList.FirstOrDefault(c => c.Id == taskItemUpadte.Id);
                if (existTask is null)
                {
                    throw new KeyNotFoundException($"Task with ID {taskItemUpadte.Id} not found.");
                }
                existTask.Title = taskItemUpadte.Title;
                existTask.IsCompleted = taskItemUpadte.IsCompleted;
                existTask.IsDeleted = taskItemUpadte.IsDeleted;
                using (var writer = new StreamWriter(GetFilePath()))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(existToDoList);
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error Update CSV file", ex);
            }


        }
        #endregion

        #region Delete
        public static List<TaskItem> DelteToDoList(TaskItemDelete taskItemDelete)
        {
            try
            {
                var existToDoList = ReadAllCsvFile();
                var existTask = existToDoList.FirstOrDefault(c => c.Id == taskItemDelete.Id);
                if (existTask is null)
                {
                    throw new KeyNotFoundException($"Task with ID {taskItemDelete.Id} not found.");
                }
                existTask.IsDeleted = 1;
                using (var writer = new StreamWriter(GetFilePath()))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(existToDoList);
                }
                return existToDoList.ToList();

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error Update CSV file", ex);
            }


        }
        #endregion

        #region NoAction
        private static List<TaskItem> GetToDoList()
        {
            var taskItemList = new List<TaskItem>()
            {
                 new TaskItem { Id = 1, Title = "Complete project documentation", IsCompleted = true, IsDeleted = 0 },
                 new TaskItem { Id = 2, Title = "Implement login functionality", IsCompleted = false, IsDeleted = 0  },
                 new TaskItem { Id = 3, Title = "Fix bugs in the user dashboard", IsCompleted = false, IsDeleted = 0  },
                 new TaskItem { Id = 4, Title = "Review pull requests from the team", IsCompleted = true, IsDeleted = 0  },
                 new TaskItem { Id = 5, Title = "Plan next sprint tasks", IsCompleted = false, IsDeleted = 0  }
            };
            return taskItemList;
        }

        private static string GetFilePath()
        {
            var filePath = Path.Combine(Folder_NAME, FILE_NAME);
            return filePath;

        }
        #endregion
    }
}
