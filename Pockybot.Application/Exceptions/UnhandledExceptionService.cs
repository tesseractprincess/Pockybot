using Microsoft.Extensions.Logging;
using System.Text;

namespace Pockybot.Application.Exceptions
{
    public sealed class UnhandledExceptionService
    {
        private readonly ILogger<UnhandledExceptionService> _logger;

        public UnhandledExceptionService(ILogger<UnhandledExceptionService> logger)
        {
            _logger = logger;
        }

        public void HandleException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception? ex = args.ExceptionObject as Exception;
            StringBuilder sb = new();
            sb.AppendLine($"Unhandled exception from [{sender.GetType().FullName}]");
            sb.AppendLine($"\tException: {ex?.ToString()}");

            _logger.LogCritical(sb.ToString());
        }
    }
}
