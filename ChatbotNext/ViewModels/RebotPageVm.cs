using ChatbotNext.Models;
using ChatbotNext.ViewModels.Models;
using ChatbotNext.Windows;
using NextUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace ChatbotNext.ViewModels
{
    public class RebotPageVm : UIVm
    {
        private readonly List<ChatbotItemVm> _chatbotCollection = new List<ChatbotItemVm>();

        /// <summary>
        /// 机器人列表(搜索结果)
        /// </summary>
        public ObservableCollection<ChatbotItemVm> ChatbotCollection { get; } = new ObservableCollection<ChatbotItemVm>();


        private ChatbotItemVm _selectedChatbot;
        /// <summary>
        /// 当前选择的机器人
        /// </summary>
        public ChatbotItemVm SelectedChatbot
        {
            get { return _selectedChatbot; }
            set { _selectedChatbot = value; OnPropertyChanged(); }
        }

        private string _searchText;
        /// <summary>
        /// 搜索的内容
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SetChatbotCollection(_searchText);
            }
        }

        private string _sendText;

        public string SendText
        {
            get { return _sendText; }
            set { _sendText = value; OnPropertyChanged(); }
        }


        public ICommand AddNewBotCommand { get; }
        public ICommand EditBotCommand { get; }
        public ICommand DeleteBotCommand { get; }
        public ICommand SendCommand { get; }
        public ICommand ClearMessagesCommand { get; }

        public RebotPageVm()
        {
            AddNewBotCommand = new UICommand(AddNewBot);
            EditBotCommand = new UICommand(EditBot);
            DeleteBotCommand = new UICommand(DeleteBot);
            SendCommand = new UICommand(Send);
            ClearMessagesCommand = new UICommand(ClearMessages);
            if (!IsInDesignMode)
            {
                App.LocalData.CollectionChanged += LocalData_CollectionChanged;
            }

            LoadAllRebot();
        }

        private void LoadAllRebot()
        {
            try
            {
                _chatbotCollection.Clear();
                var chatbots = App.LocalData.GetCollection<ChatbotData>();
                foreach (var chatbot in chatbots)
                {
                    var item = new ChatbotItemVm()
                    {
                        ChatbotData = App.Helper.CopyFrom<ChatbotDataVm>(chatbot)
                    };
                    var msgs = App.LocalData.GetCollection<ChatbotMessage>(chatbot.ID);
                    foreach (var msg in msgs)
                    {
                        item.Messages.Add(App.Helper.CopyFrom<ChatbotMessageVm>(msg));
                    }
                    _chatbotCollection.Add(item);
                }
                SetChatbotCollection(SearchText);
            }
            catch
            {
                //nothing
            }
        }

        private void LocalData_CollectionChanged(object sender, Core.DataContextCollectionChangedEventArgs e)
        {
            try
            {
                if (e.DataType == typeof(ChatbotData))
                {
                    if (e.Action == Core.DataContextCollectionChangedAction.Add)
                    {
                        foreach (var item in e.Items)
                        {
                            _chatbotCollection.Add(new ChatbotItemVm()
                            {
                                ChatbotData = App.Helper.CopyFrom<ChatbotDataVm>(item)
                            });
                        }
                    }
                    else if (e.Action == Core.DataContextCollectionChangedAction.Update)
                    {
                        foreach (ChatbotData item in e.Items)
                        {
                            var dataVm = _chatbotCollection.FirstOrDefault(p => p.ChatbotData.ID == item.ID);
                            if (dataVm == null)
                            {
                                _chatbotCollection.Add(new ChatbotItemVm()
                                {
                                    ChatbotData = App.Helper.CopyFrom<ChatbotDataVm>(item)
                                });
                            }
                            else
                            {
                                dataVm.ChatbotData = App.Helper.CopyFrom<ChatbotDataVm>(item);
                            }
                        }
                    }
                    else if (e.Action == Core.DataContextCollectionChangedAction.Delete)
                    {
                        foreach (ChatbotData item in e.Items)
                        {
                            var dataVm = _chatbotCollection.FirstOrDefault(p => p.ChatbotData.ID == item.ID);
                            if (dataVm != null)
                            {
                                _chatbotCollection.Remove(dataVm);
                            }
                        }

                    }
                    SetChatbotCollection(SearchText);
                }
                //else if (e.DataType == typeof(ChatbotMessage))
                //{
                //    foreach (ChatbotMessage msg in e.Items)
                //    {
                //        var id = msg.FromUserID == App.Vm.CurrentUser.ID ? msg.ToUserID : msg.FromUserID;
                //        var chatbot = ChatbotCollection.FirstOrDefault(p => p.ChatbotData.ID == id);
                //        if (chatbot == null)
                //        {
                //            continue;
                //        }
                //        if (e.Action == Core.DataContextCollectionChangedAction.Add)
                //        {
                //            chatbot.Messages.Add(App.Helper.CopyFrom<ChatbotMessageVm>(msg));
                //        }
                //        else if (e.Action == Core.DataContextCollectionChangedAction.Update)
                //        {

                //        }
                //    }
                //}
            }
            catch
            {
                //nothing
            }
        }

        private void SetChatbotCollection(string searchText)
        {
            try
            {
                var orgSelectedChatbot = SelectedChatbot;
                var chatbotCollection = _chatbotCollection;
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    if (ChatbotCollection.Count != chatbotCollection.Count)
                    {
                        ChatbotCollection.Clear();
                        foreach (var item in chatbotCollection)
                        {
                            ChatbotCollection.Add(item);
                        }
                    }
                }
                else
                {
                    var resultChatbots = chatbotCollection.Where(p =>
                    {
                        return p.ChatbotData.Name?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true ||
                            p.ChatbotData.Description?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true;
                    }).ToArray();
                    if (!resultChatbots.SequenceEqual(ChatbotCollection))
                    {
                        ChatbotCollection.Clear();
                        foreach (var item in resultChatbots)
                        {
                            ChatbotCollection.Add(item);
                        }
                    }
                }
                if (orgSelectedChatbot != null && ChatbotCollection.Contains(orgSelectedChatbot))
                {
                    SelectedChatbot = orgSelectedChatbot;
                }
            }
            catch
            {
                //nothing...
            }
        }

        private void ShowEditWindow(ChatbotDataVm data)
        {
            var win = new ChatbotDataEditWindow();
            win.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            win.Owner = App.Current.MainWindow;
            win.DataContext = new ChatbotDataEditWindowVm()
            {
                ChatbotData = data ?? new ChatbotDataVm()
            };
            win.ShowDialog();
        }

        private void AddNewBot(object obj)
        {
            ShowEditWindow(new ChatbotDataVm());
        }

        private void DeleteBot(object obj)
        {
            if (SelectedChatbot == null)
            {
                return;
            }
            if (MessageBox.Show($"确定删除[{SelectedChatbot.ChatbotData.Name}]吗？","提示", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                return;
            }
            try
            {
                App.LocalData.DeleteItem<ChatbotData>(new ChatbotData() { ID = SelectedChatbot.ChatbotData.ID });
            }
            catch
            {
                //nothing
            }
            _chatbotCollection.Remove(SelectedChatbot);
            SetChatbotCollection(SearchText);
        }

        private void EditBot(object obj)
        {
            if (SelectedChatbot == null)
            {
                return;
            }
            ShowEditWindow(App.Helper.CopyFrom<ChatbotDataVm>(SelectedChatbot.ChatbotData));
        }

        private async void Send(object obj)
        {
            if (this.SelectedChatbot == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(SendText))
            {
                return;
            }
            if (Keyboard.IsKeyDown(Key.Enter) && 
                (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)))
            {
                return;
            }
            var curChatbot = this.SelectedChatbot;
            if (!string.IsNullOrWhiteSpace(curChatbot.TypingState))
            {
                MessageBox.Show($"等待回复，别慌......", "提示", MessageBoxButton.OK);
                return;
            }
            try
            {
                curChatbot.TypingState = "（正在输入...）";
                var msg = BuildUserMessage(curChatbot.ChatbotData.ID, SendText);
                var msgVm = App.Helper.CopyFrom<ChatbotMessageVm>(msg);
                SendText = "";
                App.LocalData.SetItem(msg, curChatbot.ChatbotData.ID);
                curChatbot.Messages.Add(msgVm);

                var replyContent = await App.ChatbotService.GetReply(curChatbot);
                if (string.IsNullOrWhiteSpace(replyContent))
                {
                    var replyMsg = BuildChatbotMessage(curChatbot.ChatbotData.ID, "风太大了，我看不见消息了...");
                    curChatbot.Messages.Add(App.Helper.CopyFrom<ChatbotMessageVm>(replyMsg));
                }
                else
                {
                    var replyMsg = BuildChatbotMessage(curChatbot.ChatbotData.ID, replyContent);
                    var replyMsgVm = App.Helper.CopyFrom<ChatbotMessageVm>(replyMsg);
                    msg.Success = true;
                    msgVm.Success = true;
                    replyMsg.Success = true;
                    replyMsgVm.Success = true;
                    curChatbot.Messages.Add(replyMsgVm);
                    App.LocalData.SetItem(msg, curChatbot.ChatbotData.ID);
                    App.LocalData.SetItem(replyMsg, curChatbot.ChatbotData.ID);
                }
            }
            catch
            {
                var replyMsg = BuildChatbotMessage(curChatbot.ChatbotData.ID, "糟糕，出现了一些异常问题...");
                curChatbot.Messages.Add(App.Helper.CopyFrom<ChatbotMessageVm>(replyMsg));
            }
            curChatbot.TypingState = "";
        }

        private void ClearMessages(object obj)
        {
            if (this.SelectedChatbot == null)
            {
                return;
            }
            var curChatbot = this.SelectedChatbot;
            try
            {
                App.LocalData.DeleteItems(curChatbot.Messages.Select(p => new ChatbotMessage() { ID = p.ID }), curChatbot.ChatbotData.ID);
                curChatbot.Messages.Clear();
            }
            catch
            {
                //nothing
            }
        }

        private ChatbotMessage BuildUserMessage(string chatbotID, string content)
        {
            return new ChatbotMessage()
            {
                ID = Guid.NewGuid().ToString(),
                FromUserID = App.Vm.CurrentUser.ID,
                ToUserID = chatbotID,
                CreateTime = DateTime.Now,
                Content = content
            };
        }

        private ChatbotMessage BuildChatbotMessage(string chatbotID, string content)
        {
            return new ChatbotMessage()
            {
                ID = Guid.NewGuid().ToString(),
                FromUserID = chatbotID,
                ToUserID = App.Vm.CurrentUser.ID,
                CreateTime = DateTime.Now,
                Content = content
            };
        }
    }
}
