using System.Collections.ObjectModel;

namespace HyberShift_CSharp.Model.List
{
    public class ListMessageModel : BaseList<MessageModel>
    {
        private static ListMessageModel instance;

        // constructor

        public ObservableCollection<object> ListMessage => GetCollectionOfField("Message");
        public ObservableCollection<object> ListSender => GetCollectionOfField("Sender");

        // singleton
        public static ListMessageModel GetInstance()
        {
            if (instance == null)
                instance = new ListMessageModel();
            return instance;
        }

        public MessageModel GetMessageFromId(string id)
        {
            return GetFirstObjectByValue("ID", id);
        }

        public MessageModel GetMessageFromSender(string sender)
        {
            return GetFirstObjectByValue("Sender", sender);
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}