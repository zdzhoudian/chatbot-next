using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatbotNext.API.ChatGPT
{
    public class ChatGPTRequest
    {
        public static ChatGPTRequest Create(params ChatGPTMessage[] messages)
        {
            if (messages == null || messages.Length == 0)
            {
                throw new ArgumentException(nameof(messages));
            }
            var req = new ChatGPTRequest()
            {
                Model = ChatGPTParameters.GPT35TurboModel
            };
            foreach (var msg in messages)
            {
                req.Messages.Add(msg);
            }
            return req;
        }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("messages")]
        public ChatGPTMessageCollection Messages { get; } = new ChatGPTMessageCollection();
    }
}
