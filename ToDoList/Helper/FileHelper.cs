using CsvHelper;
using CsvHelper.TypeConversion;
using System.Formats.Asn1;
using System.Formats.Tar;
using System.Globalization;
using System.Text;
using ToDoList.Models;

namespace ToDoList.Helper
{
    public static class FileHelper
    {
        #region Properties
        private const string FILE_NAME = "TaskItem.csv";
        private const string Folder_NAME = "TaskItemList";
        #endregion

        public static IEnumerable<TaskItem> ReadCsvFile()
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

        public static IList<TaskItem> AddTaskToCSVFile(List<TaskItem> taskItemList)
        {
            using (var writer = new StreamWriter(GetFilePath()))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(taskItemList);
            }
            return taskItemList;
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


        private static List<TaskItem> GetToDoList()
        {
            var taskItemList = new List<TaskItem>()
            {
                 new TaskItem { Id = 1, Title = "Complete project documentation", IsCompleted = true },
                 new TaskItem { Id = 2, Title = "Implement login functionality", IsCompleted = false },
                 new TaskItem { Id = 3, Title = "Fix bugs in the user dashboard", IsCompleted = false },
                 new TaskItem { Id = 4, Title = "Review pull requests from the team", IsCompleted = true },
                 new TaskItem { Id = 5, Title = "Plan next sprint tasks", IsCompleted = false }
            };
            return taskItemList;
        }

        private static string GetFilePath()
        {
            var filePath = Path.Combine(Folder_NAME, FILE_NAME);
            return filePath;

        }
    }
}
