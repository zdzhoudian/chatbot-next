using NextUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.ViewModels.Models
{
    public class ChatbotItemVm : UIVm
    {
		private ChatbotDataVm _chatbotData;

		public ChatbotDataVm ChatbotData
		{
			get { return _chatbotData; }
			set { _chatbotData = value; OnPropertyChanged(); }
		}

		public ObservableCollection<ChatbotMessageVm> Messages { get; } = new ObservableCollection<ChatbotMessageVm>();

		private string _typingState;

		public string TypingState
		{
			get { return _typingState; }
			set { _typingState = value; OnPropertyChanged(); }
		}

	}
}
