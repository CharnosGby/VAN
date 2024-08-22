using Microsoft.AspNetCore.SignalR;

namespace VAN.WebCore.Init
{
    public class MessageChatHubInit : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // 广播消息到所有连接的客户端
            Console.WriteLine($@"{user}: {message}");
            await Clients.All.SendAsync("APoint", user, message);
        }
    }
}
