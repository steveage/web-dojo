namespace HighbushAutomation.Data
{
    public enum DoorEventType
    {
        Invalid = 0,
        Opened,
        Closed
    }
    public class DoorEvent
    {
        public DoorEventType DoorEventType { get; set; }
    }
}
