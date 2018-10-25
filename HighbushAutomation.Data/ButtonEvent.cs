namespace HighbushAutomation.Data
{
    public enum ButtonEventType
    {
        Invalid = 0,
        Pressed
    }
    public class ButtonEvent
    {
        public ButtonEventType ButtonEventType { get; set; }
    }
}
