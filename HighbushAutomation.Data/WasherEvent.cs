namespace HighbushAutomation.Data
{
    public enum WasherEventType
    {
        Invalid = 0,
        WashFinished
    }
    public class WasherEvent
    {
        public WasherEventType WasherEventType { get; set; }
    }
}