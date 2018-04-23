namespace HyberShift_CSharp.Model
{
    public class Message
    {
        public Message()
        {
        }

        public Message(string id, string message, string sender, string imgstring, int timestamp)
        {
            ID = id;
            MessageContent = message;
            Sender = sender;
            TimeStamp = timestamp;
            ImgString = imgstring;
        }

        public string ID { get; set; }

        public string MessageContent { get; set; }

        public string Sender { get; set; }

        public int TimeStamp { get; set; }

        public string ImgString { get; set; }
    }
}