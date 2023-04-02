using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotNext.Core
{
    /// <summary>
    /// 主机接口
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// 获取 当前注册的服务集合
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// 获取 配置
        /// </summary>
        IConfiguration Config { get; }

        /// <summary>
        /// 获取 环境变量
        /// </summary>
        IAppEnvironment Env { get; }

        /// <summary>
        /// 日志
        /// </summary>
        ILoggerProvider LoggerProvider { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        void Init();

    }

}
