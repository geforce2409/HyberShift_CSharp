namespace HyberShift_CSharp.Model
{
    public class SenderTyping
    {
        public SenderTyping()
        {
        }

        public SenderTyping(string senderName, int index)
        {
            SenderName = senderName;
            Index = index;
        }

        public string SenderName { get; set; }

        public int Index { get; set; }
    }
}