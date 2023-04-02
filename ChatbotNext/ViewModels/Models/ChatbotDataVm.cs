using NextUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatbotNext.ViewModels.Models
{
    public class ChatbotDataVm : UserInfoDataVm
    {

        private string _chatbotType = "ChatGPT";

        public string ChatbotType
        {
            get { return _chatbotType; }
            set { _chatbotType = value; OnPropertyChanged(); SettingsSetToObject(); }
        }

        private string _settings;

        public string Settings
        {
            get { return _settings; }
            set { _settings = value; OnPropertyChanged(); SettingsSetToObject(); }
        }

        private void SettingsSetToObject()
        {
            if (ChatbotType == "ChatGPT")
            {
                try
                {
                    GPTSettings = JsonSerializer.Deserialize<ChatGPTSettingsVm>(Settings);
                }
                catch
                {
                    GPTSettings = new ChatGPTSettingsVm();
                }
            }
        }

        public void ObjectSetToSettings()
        {
            if (ChatbotType == "ChatGPT")
            {
                try
                {
                    Settings = JsonSerializer.Serialize(GPTSettings);
                }
                catch
                {
                    Settings = "";
                }
            }
        }

        private ChatGPTSettingsVm _gptSettings = new ChatGPTSettingsVm();

        public ChatGPTSettingsVm GPTSettings
        {
            get { return _gptSettings; }
            private set { _gptSettings = value; OnPropertyChanged(); }
        }
    }
}
