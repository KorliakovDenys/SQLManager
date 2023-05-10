namespace SQLManager; 

public class MessageHandlerArgs {
    public string Message { get; private set; }
    public DateTime Time { get; private set; }

    public MessageHandlerArgs(string message) {
        Message = message;
        Time = DateTime.Now;
    }
}