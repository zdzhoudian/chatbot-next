using ChatbotNext.ViewModels.Models;
using NextUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChatbotNext.ViewModels
{
    public class MainWindowVm : UIVm
    {
        /// <summary>
        /// 左侧页面菜单
        /// </summary>
        public AppMenuItemCollection PageMenuItems { get; } = new AppMenuItemCollection();

        /// <summary>
        /// 底部菜单（设置等）
        /// </summary>
        public AppMenuItemCollection BottomMenuItems { get; } = new AppMenuItemCollection();

        private Page _currentPage;
        /// <summary>
        /// 当前页面
        /// </summary>
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }



        public MainWindowVm()
        {
            PageMenuItems.SelectedItemChanged += PageMenuItems_SelectedItemChanged;
            PageMenuItems.Add(new AppMenuItemVm()
            {
                Name = "Menu_Chatbot_Tip",
                Icon = NUI.LoadAppResource<Geometry>("NUI.Icon.Wx"),
                PagePath = "/ChatbotNext;component/Pages/RebotPage.xaml"
            });
            //PageMenuItems.Add(new AppMenuItemVm()
            //{
            //    Name = "Menu_Document_Tip",
            //    Icon = NUI.LoadAppResource<Geometry>("NUI.Icon.Document"),
            //    PagePath = "/ChatbotNext;component/Pages/DocumentPage.xaml"
            //});
            //PageMenuItems.Add(new AppMenuItemVm()
            //{
            //    Name = "Menu_Tools_Tip",
            //    Icon = NUI.LoadAppResource<Geometry>("NUI.Icon.Tools"),
            //    PagePath = "/ChatbotNext;component/Pages/ToolsPage.xaml"
            //});
            PageMenuItems.Add(new AppMenuItemVm()
            {
                Name = "Menu_About_Tip",
                Icon = NUI.LoadAppResource<Geometry>("NUI.Icon.About"),
                PagePath = "/ChatbotNext;component/Pages/AboutPage.xaml"
            });

            PageMenuItems.SelectedItem = PageMenuItems.FirstOrDefault();

            BottomMenuItems.Add(new AppMenuItemVm()
            {
                Name = "Menu_Settings_Tip",
                Icon = NUI.LoadAppResource<Geometry>("NUI.Icon.Settings"),
                PagePath = ""
            });
        }

        private void PageMenuItems_SelectedItemChanged(object sender, EventArgs e)
        {
            CurrentPage = NUI.LoadPage(PageMenuItems.SelectedItem.PagePath, true);
        }
    }
}
