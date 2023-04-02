using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NextUI
{
    public partial class NUI
    {
        private static ResourceDictionary _lastAddLangDic;
        private static readonly ConditionalWeakTable<object, ResourceDictionary> _lastElementAddLangDics = new ConditionalWeakTable<object, ResourceDictionary>();

        public static string GetLang(string key)
        {
            return LoadAppResource<string>(key);
        }

        public static string GetLang(FrameworkElement element, string key)
        {
            if (element == null)
            {
                return string.Empty;
            }
            return LoadResource<string>(element, key);
        }

        public static void ChangeLang(string path)
        {
            var newDic = LoadXaml<ResourceDictionary>(path);
            if (newDic == null)
            {
                return;
            }
            if (_lastAddLangDic != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(_lastAddLangDic);
            }
            Application.Current.Resources.MergedDictionaries.Add(newDic);
            _lastAddLangDic = newDic;
        }

        public static void ChangeLang(FrameworkElement element, string path)
        {
            if (element == null)
            {
                return;
            }
            var newDic = LoadXaml<ResourceDictionary>(path);
            if (newDic == null)
            {
                return;
            }
            if (_lastElementAddLangDics.TryGetValue(element, out var lastAddDic))
            {
                element.Resources.MergedDictionaries.Remove(lastAddDic);
                _lastElementAddLangDics.Remove(lastAddDic);
            }
            element.Resources.MergedDictionaries.Add(newDic);
            _lastElementAddLangDics.Add(element, newDic);
        }
    }
}
