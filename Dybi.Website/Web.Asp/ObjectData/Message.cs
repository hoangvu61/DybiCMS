namespace Web.Asp.ObjectData
{
    public class Message
    {
        public string MessageString { get; set; }
        public string MessageType { get; set; }
        public bool IsEmpty { get { return string.IsNullOrEmpty(MessageString); } }

        public Message() { }
        public Message(string type, string message) {
            MessageType = type;
            MessageString = message;
        }

        public void Info(string message)
        {
            MessageType = "INFO";
            MessageString = message;
        }

        public void Warning(string message)
        {
            MessageType = "WARNING";
            MessageString = message;
        }

        public void Error(string message)
        {
            MessageType = "ERROR";
            MessageString = message;
        }
    }
}