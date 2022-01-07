namespace Pockybot.Core.Interfaces
{
    public interface IPockybotException
    {
        IDictionary<string, object?> DebugObjects { get; set; }

        string FormatDebugObjects();
    }
}
