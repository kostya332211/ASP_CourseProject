
namespace Chess.Web.Models.ChatModels
{
    public class MessageModel
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public bool IsCallerMessage { get; set; }
    }
}