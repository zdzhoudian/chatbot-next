using ChatbotNext.Models;
using ChatbotNext.ViewModels.Models;
using Microsoft.Win32;
using NextUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatbotNext.ViewModels
{
    public class ChatbotDataEditWindowVm : UIVm
    {
		private ChatbotDataVm _chatbotData;

		public ChatbotDataVm ChatbotData
		{
			get { return _chatbotData; }
			set { _chatbotData = value; OnPropertyChanged(); }
		}

		public ICommand CloseCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand SelectImageCommand { get; }

        public ChatbotDataEditWindowVm()
		{
            CloseCommand = new UICommand<Window>(Close);
            SaveCommand = new UICommand<Window>(Save);
            SelectImageCommand = new UICommand(SelectImage);
        }


		private void Close(Window window)
		{
			window?.Close();
		}

		private void Save(Window window)
		{
			if (ChatbotData == null)
			{
				MessageBox.Show("无效的信息", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (string.IsNullOrWhiteSpace(ChatbotData.Name))
			{
                MessageBox.Show("请输入有效的机器人名称", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ChatbotData.ChatbotType))
            {
                MessageBox.Show("请输入有效的机器人类型", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (ChatbotData.ChatbotType == "ChatGPT" && ChatbotData.GPTSettings != null)
            {
                if (ChatbotData.GPTSettings == null)
                {
                    MessageBox.Show("无效的信息", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (ChatbotData.GPTSettings.ServiceType == ChatGPTServiceType.Custom)
                {
                    if (string.IsNullOrWhiteSpace(ChatbotData.GPTSettings.ApiUrl))
                    {
                        MessageBox.Show("请输入有效的ApiUrl", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(ChatbotData.GPTSettings.ApiKey))
                    {
                        MessageBox.Show("请输入有效的ApiKey", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(ChatbotData.GPTSettings.ChatGPTMode))
                    {
                        MessageBox.Show("请输入有效的AI模型", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
            }

            try
            {
                ChatbotData.ObjectSetToSettings();
                var newData = App.Helper.CopyFrom<ChatbotData>(ChatbotData);
                if (newData.ID == null)
                {
                    newData.ID = Guid.NewGuid().ToString();
                }
                App.LocalData.SetItem(newData);
                window?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void SelectImage(object obj)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "图片|*.jpg;*.jpeg;*.bmp;*.png;";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    this.ChatbotData.Image = App.Helper.GetBase64Image(ofd.FileName, 128, 128);
                }
                catch
                {
                    this.ChatbotData.Image = null;
                }
            }
        }
	}
}
