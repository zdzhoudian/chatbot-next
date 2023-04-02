using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.API.ChatGPT
{
    public class ChatGPTMessageCollection : Collection<ChatGPTMessage>
    {
        /// <summary>
        /// 用户消息
        /// </summary>
        /// <param name="content"></param>
        public void AddUserMessage(string content)
        {
            Add(ChatGPTMessage.CreateUserMessage(content));
        }

        /// <summary>
        /// 系统消息，设置ChatGPT的行为和特征
        /// </summary>
        /// <param name="content"></param>
        public void AddSystemMessage(string content)
        {
            Add(ChatGPTMessage.CreateSystemMessage(content));
        }

        /// <summary>
        /// ChatGPT回复的消息
        /// </summary>
        /// <param name="content"></param>
        public void AddAssistantMessage(string content)
        {
            Add(ChatGPTMessage.CreateAssistantMessage(content));
        }
    }
}
