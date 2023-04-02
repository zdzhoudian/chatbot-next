using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NextUI
{
    public partial class NUI
    {
        private static readonly ConcurrentDictionary<string, object> _pageDataCtxCaches = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 读取页面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Page LoadPage(string path, bool cacheDataContext = false)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }
            var page = LoadXaml<Page>(path);
            if (page?.DataContext != null && cacheDataContext)
            {
                var ctx = _pageDataCtxCaches.GetOrAdd(path, k => page.DataContext);
                page.DataContext = ctx;
            }
            return page;
        }

        /// <summary>
        /// 读取Xaml文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T LoadXaml<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return default;

            try
            {
                var uri = new Uri(path, UriKind.RelativeOrAbsolute);
                return (T)Application.LoadComponent(uri);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 读取应用资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T LoadAppResource<T>(string key)
        {
            try
            {
                return (T)Application.Current.Resources[key];
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 读取控件的资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T LoadResource<T>(FrameworkElement element, string key)
        {
            try
            {
                return (T)element.TryFindResource(key);
            }
            catch
            {
                return default;
            }
        }
    }
}
