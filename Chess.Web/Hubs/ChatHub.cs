using System.Security.Claims;
using Chess.Web.Models.ChatModels;
using Microsoft.AspNetCore.SignalR;

namespace Chess.Web.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            var userName = "Anon";
            foreach (var claim in Context.GetHttpContext().User.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    userName = claim.Value;
                }
            }

            Clients.Caller.SendAsync("Send",
                new MessageModel() {Name = userName, Message = message, IsCallerMessage = true});
            Clients.Others.SendAsync("Send",
                new MessageModel() {Name = userName, Message = message, IsCallerMessage = false});
        }

    }
}


