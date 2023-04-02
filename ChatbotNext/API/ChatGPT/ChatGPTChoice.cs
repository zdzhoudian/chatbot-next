using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatbotNext.API.ChatGPT
{
    public class ChatGPTChoice
    {
        [JsonPropertyName("message")]
        public ChatGPTMessage Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }


        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}
