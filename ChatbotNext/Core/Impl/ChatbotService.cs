using ChatbotNext.API.ChatGPT;
using ChatbotNext.Models;
using ChatbotNext.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.Core
{

    public class ChatbotService : IChatbotService
    {
        private ChatGPTClient _client = new ChatGPTClient();

        public async Task<string> GetReply(ChatbotItemVm chatbotItem)
        {
            var chatbotSettings = App.Helper.CopyFrom<ChatGPTSettingsVm>(chatbotItem.ChatbotData.GPTSettings);
            var chatbotMessages = chatbotItem.Messages.Select(p => p).ToList();
            if (chatbotMessages.Count == 0)
            {
                return "";
            }

            var req = new ChatGPTRequest();
            if (chatbotSettings.ServiceType == ChatGPTServiceType.Free)
            {
                _client.Config.ApiUrl = FreeServiceConfig.ApiUrl;
                _client.Config.ApiKey = FreeServiceConfig.ApiKey;
                req.Model = FreeServiceConfig.Mode;
            }
            else
            {
                _client.Config.ApiUrl = chatbotSettings.ApiUrl;
                _client.Config.ApiKey = chatbotSettings.ApiKey;
                req.Model = chatbotSettings.ChatGPTMode;
            }

            var tokenCount = 0;
            if (!string.IsNullOrWhiteSpace(chatbotSettings.BehaviorDesc))
            {
                req.Messages.AddSystemMessage(chatbotSettings.BehaviorDesc);
                tokenCount += chatbotSettings.BehaviorDesc.Length;
            }
            if (chatbotSettings.ContextEnabled)
            {
                var maxCount = 2000;
                if (tokenCount >= maxCount)
                {
                    throw new Exception("BehaviorDesc is too long");
                }
                var msgs = new List<ChatGPTMessage>();
                var maxIndex = chatbotMessages.Count - 1;
                for (int i = maxIndex; i >= 0; i--)
                {
                    var msgVm = chatbotMessages[i];
                    if (i != maxIndex && !msgVm.Success)
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(msgVm.Content))
                    {
                        continue;
                    }
                    tokenCount += msgVm.Content.Length;
                    if (tokenCount > maxCount)
                    {
                        break;
                    }
                    if (msgVm.IsMe)
                    {
                        msgs.Add(ChatGPTMessage.CreateUserMessage(msgVm.Content));
                    }
                    else
                    {
                        msgs.Add(ChatGPTMessage.CreateAssistantMessage(msgVm.Content));
                    }
                }
                msgs.Reverse();
                foreach (var item in msgs)
                {
                    req.Messages.Add(item);
                }
            }
            else
            {
                req.Messages.AddUserMessage(chatbotMessages.Last().Content);
            }

            var res = await _client.SendAsync(req);
            return res.Choices?.FirstOrDefault()?.Message?.Content;
        }
    }
}
