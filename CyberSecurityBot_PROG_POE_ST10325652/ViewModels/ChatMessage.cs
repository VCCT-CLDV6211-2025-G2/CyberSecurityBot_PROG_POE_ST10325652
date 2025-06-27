using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityBot_PROG_POE_ST10325652.ViewModels
{
    public class ChatMessage
    {
        public string Text { get; set; } = "";
        public string Sender { get; set; } = "Bot"; // "User" or "Bot"

        public ChatMessage(string text, string sender)
        {
            Text = text;
            Sender = sender;
        }
    }
}
