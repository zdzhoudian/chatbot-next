using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatbotNext.API.ChatGPT
{
    public class ChatGPTMessage
    {
        public static ChatGPTMessage CreateUserMessage(string content)
        {
            return new ChatGPTMessage()
            {
                Role = ChatGPTParameters.UserRole,
                Content = content,
            };
        }

        public static ChatGPTMessage CreateSystemMessage(string content)
        {
            return new ChatGPTMessage()
            {
                Role = ChatGPTParameters.SystemRole,
                Content = content,
            };
        }

        public static ChatGPTMessage CreateAssistantMessage(string content)
        {
            return new ChatGPTMessage()
            {
                Role = ChatGPTParameters.AssistantRole,
                Content = content,
            };
        }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
