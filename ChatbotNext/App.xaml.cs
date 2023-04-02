using ChatbotNext.Core;
using ChatbotNext.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NextUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChatbotNext
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _host;

        public static GlobalVm Vm
        {
            get
            {
                if (NUI.IsInDesignMode)
                {
                    return new GlobalVm();
                }
                else
                {
                    return _host.Services.GetService<GlobalVm>();
                }
            }
        }

        public static MainWindowVm MainWindowVm
        {
            get
            {
                if (NUI.IsInDesignMode)
                {
                    return new MainWindowVm();
                }
                else
                {
                    return _host.Services.GetService<MainWindowVm>();
                }
            }
        }

        public static IHost Host => _host;

        public static IDataContext LocalData => _host.Services.GetRequiredService<IDataContext>();
        public static IHelper Helper => _host.Services.GetRequiredService<IHelper>();
        public static IChatbotService ChatbotService => _host.Services.GetRequiredService<IChatbotService>();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _host = new SimpleHost();
            _host.Init();
        }
    }
}
