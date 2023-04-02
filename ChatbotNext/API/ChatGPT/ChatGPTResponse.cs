using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatbotNext.API.ChatGPT
{
    public class ChatGPTResponse
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("usage")]
        public ChatGPTUsage Usage { get; set; }

        [JsonPropertyName("choices")]
        public ChatGPTChoiceCollection Choices { get; set; }
    }
}
