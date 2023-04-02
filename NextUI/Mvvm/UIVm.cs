using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace NextUI
{
    public abstract class UIVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnPropertiesChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 当前是否处于设计器模式
        /// </summary>
        public static bool IsInDesignMode
        {
            get
            {
                return NUI.IsInDesignMode;
            }
        }
    }
}
