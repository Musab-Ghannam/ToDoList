using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToDoList.Helper;
using ToDoList.Option;

namespace ToDoList.Worker
{
    public class CreateFileWorker(IOptions<FileOption> options, ILogger<CreateFileWorker> logger) : BackgroundService
    {
        private readonly ILogger<CreateFileWorker> _logger = logger;
        private readonly IOptions<FileOption> _option = options;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                if (options.Value.EnabledWorker)
                {
                    if (!stoppingToken.IsCancellationRequested)
                    {
                        FileHelper.CreateAndAddListToCSVFile();
                        await Task.CompletedTask;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

        }
    }
}
