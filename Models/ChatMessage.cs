using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.Models
{
    public class ChatMessage
    {
        public string User { get; set; }

        public string Message { get; set; }

        public string Group { get; set; }
    }
}
