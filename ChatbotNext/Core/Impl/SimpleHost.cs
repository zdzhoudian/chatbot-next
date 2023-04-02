using ChatbotNext.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace ChatbotNext.Core
{
    /// <summary>
    /// 一个简单的主机
    /// </summary>
    public class SimpleHost : IHost
    {
        private IServiceProvider _services;

        private IConfiguration _config;

        private IAppEnvironment _env;

        public IServiceProvider Services => _services;

        public IConfiguration Config => _config;

        public IAppEnvironment Env => _env;

        public ILoggerProvider LoggerProvider => _services.GetRequiredService<ILoggerProvider>();

        public void Init()
        {
            _env = new AppEnvironment();

            _services = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddDebug();
                })
                .AddSingleton<GlobalVm>()
                .AddSingleton<MainWindowVm>()
                .AddSingleton<IDataContext>(new LocalJsonDataContext()
                {
                    ConnectionString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data")
                })
                .AddSingleton<IHelper, Helper>()
                .AddSingleton<IChatbotService, ChatbotService>()
                .BuildServiceProvider();


            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", true)
                .Build();
        }
    }
}
