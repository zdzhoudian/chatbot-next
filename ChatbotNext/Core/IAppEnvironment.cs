
namespace ChatbotNext.Core
{
    /// <summary>
    /// 应用程序环境变量
    /// </summary>
    public interface IAppEnvironment
    {
        /// <summary>
        /// 当前环境名称
        /// </summary>
        string EnvironmentName { get; }

        /// <summary>
        /// 获取 是否是开发环境
        /// </summary>
        bool IsDevelopment { get; }

        /// <summary>
        /// 获取 是否是演示环境
        /// </summary>
        bool IsStaging { get; }

        /// <summary>
        /// 获取 是否是生产环境
        /// </summary>
        bool IsProduction { get; }

        /// <summary>
        /// 判断是否是指定的环境
        /// </summary>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        bool IsEnvironment(string environmentName);

        /// <summary>
        /// 获取环境变量的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);
    }
}
