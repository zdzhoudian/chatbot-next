using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.API.ChatGPT
{
    public class ChatGPTConfig
    {
        public string ApiUrl { get; set; } = "https://api.openai.com/v1/chat/completions";

        public string ApiKey { get; set; } = "";
    }
}
