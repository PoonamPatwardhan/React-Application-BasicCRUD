using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlanningPokerWebAPI.Models;

namespace PlanningPokerWebAPI.ApplicationLayer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage message)
        {
            //await Clients.All.SendAsync("ReceiveMessage", message);
            await Clients.Group("PLC").SendAsync("ReceiveMessage" + message);
            // Clients.Group(message.Group).addMessage("Group Message " + message.Msg);
            //Clients.Client(user).ReceiveMessage(message)
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            //await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        //public async Task RemoveFromGroup(string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        //}
    }
}
