using Microsoft.Extensions.Configuration;
using System;

namespace ChatbotNext.Core
{
    /// <summary>
    /// 应用程序环境变量
    /// </summary>
    public class AppEnvironment : IAppEnvironment
    {
        /// <summary>
        /// 开发环境
        /// </summary>
        public const string DevelopmentEnvironmentName = "Development";

        /// <summary>
        /// 演示环境
        /// </summary>
        public const string StagingEnvironmentName = "Staging";

        /// <summary>
        /// 生产环境
        /// </summary>
        public const string ProductionEnvironmentName = "Production";

        /// <summary>
        /// 环境变量配置
        /// </summary>
        private IConfiguration _env;

        /// <summary>
        /// 初始化，可以指定前缀
        /// </summary>
        /// <param name="prefix"></param>
        public AppEnvironment(string prefix = "APP_")
        {
            _env = new ConfigurationBuilder()
               .AddEnvironmentVariables(prefix)
               .Build();
        }

        /// <summary>
        /// 当前环境名称
        /// </summary>
        public string EnvironmentName
        {
            get
            {
                return _env["ENVIRONMENT"];
            }
        }

        /// <summary>
        /// 获取 是否是开发环境
        /// </summary>
        public bool IsDevelopment => IsEnvironment(DevelopmentEnvironmentName);

        /// <summary>
        /// 获取 是否是演示环境
        /// </summary>
        public bool IsStaging => IsEnvironment(StagingEnvironmentName);

        /// <summary>
        /// 获取 是否是生产环境
        /// </summary>
        public bool IsProduction => IsEnvironment(ProductionEnvironmentName);

        /// <summary>
        /// 判断是否是指定的环境
        /// </summary>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public bool IsEnvironment(string environmentName)
        {
            return EnvironmentName?.Equals(environmentName, StringComparison.OrdinalIgnoreCase) == true;
        }

        /// <summary>
        /// 获取环境变量的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(nameof(key));
            }
            return _env[key];
        }
    }
}
