using ChatbotNext.Models;
using NextUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.ViewModels.Models
{
    public class ChatbotMessageVm : UIVm
    {
        public string ID { get; set; }

		public bool Success { get; set; }

        private string _content;

		public string Content
		{
			get { return _content; }
			set { _content = value; OnPropertyChanged(); }
		}

		private string _toUserID;

		public string ToUserID
		{
			get { return _toUserID; }
			set { _toUserID = value; OnPropertyChanged(); }
		}

		private string _fromUserID;

		public string FromUserID
		{
			get { return _fromUserID; }
			set { _fromUserID = value; OnPropertiesChanged(nameof(FromUserID), nameof(IsMe), nameof(Image)); }
		}

		private DateTime _createTime;

		public DateTime CreateTime
		{
			get { return _createTime; }
			set { _createTime = value; OnPropertyChanged(); }
		}

		public string Image
		{
			get
			{
				try
				{
					if (IsMe)
					{
						return App.Vm.CurrentUser.Image;
					}
					else
                    {
                        var collection = App.LocalData.GetCollection<ChatbotData>();
                        var chatbot = collection.FirstOrDefault(p => p.ID == FromUserID);
                        return chatbot?.Image;
                    }
                }
				catch
				{
					return "";
				}
			}
		}

		public bool IsMe
		{
			get
			{
				return FromUserID == App.Vm.CurrentUser.ID;
			}
		}
	}
}
