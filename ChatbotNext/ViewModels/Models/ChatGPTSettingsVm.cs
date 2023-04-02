using ChatbotNext.Models;
using NextUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.ViewModels.Models
{
    public class ChatGPTSettingsVm : UIVm
    {
        private string _behaviorDesc;
        /// <summary>
        /// 行为描述
        /// </summary>
        public string BehaviorDesc
        {
            get { return _behaviorDesc; }
            set { _behaviorDesc = value; OnPropertyChanged(); }
        }

        private string _apiUrl = "https://api.openai.com/v1/chat/completions";
        /// <summary>
        /// 接口地址
        /// </summary>
        public string ApiUrl
        {
            get { return _apiUrl; }
            set { _apiUrl = value; OnPropertyChanged(); }
        }

        private string _chatGPTMode = "gpt-3.5-turbo";
        /// <summary>
        /// ChatGPT模型
        /// </summary>
        public string ChatGPTMode
        {
            get { return _chatGPTMode; }
            set { _chatGPTMode = value; OnPropertyChanged(); }
        }

        private string _apiKey;
        /// <summary>
        /// 接口密钥
        /// </summary>
        public string ApiKey
        {
            get { return _apiKey; }
            set { _apiKey = value; OnPropertyChanged(); }
        }

        private bool _contextEnabled;
        /// <summary>
        /// 是否启用上下文
        /// </summary>
        public bool ContextEnabled
        {
            get { return _contextEnabled; }
            set { _contextEnabled = value; OnPropertyChanged(); }
        }

        private ChatGPTServiceType _serviceType;

        public ChatGPTServiceType ServiceType
        {
            get { return _serviceType; }
            set { _serviceType = value; OnPropertyChanged(); }
        }
    }

    public enum ChatGPTServiceType
    {
        Free = 0,
        Custom = 1
    }
}
