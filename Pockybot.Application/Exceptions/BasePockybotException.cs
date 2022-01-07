using Pockybot.Core.Interfaces;
using System.Text;

namespace Pockybot.Application.Exceptions
{
    public abstract class BasePockybotException : Exception, IPockybotException
    {
        public BasePockybotException() { }
        public BasePockybotException(string reason) : base(reason) { }

        public abstract IDictionary<string, object?> DebugObjects { get; set; }

        public string FormatDebugObjects()
        {
            StringBuilder debugLog = new();
            foreach (var set in DebugObjects)
                debugLog.AppendLine($"{set.Key}: {set.Value?.ToString() ?? "null"}");

            debugLog.AppendLine($"Reason: {Message}");
            return debugLog.ToString();
        }
    }
}
