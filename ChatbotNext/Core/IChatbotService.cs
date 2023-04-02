using ChatbotNext.API.ChatGPT;
using ChatbotNext.Models;
using ChatbotNext.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace ChatbotNext.Core
{
    public interface IChatbotService
    {
        Task<string> GetReply(ChatbotItemVm chatbotItem);
    }
}
