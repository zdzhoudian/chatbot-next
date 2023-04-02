using System.Windows.Input;
using System.Windows;

namespace NextUI
{
    public static class CommonCommands
    {
        static CommonCommands()
        {
            CloseWindowCommand = new UICommand<Window>(CloseWindow);
            MinimizeWindowCommand = new UICommand<Window>(MinimizeWindow);
            MaximizeOrNormalWindowCommand = new UICommand<Window>(MaximizeOrNormalWindow);
        }

        public static ICommand CloseWindowCommand { get; }
        public static ICommand MinimizeWindowCommand { get; }
        public static ICommand MaximizeOrNormalWindowCommand { get; }

        public static void CloseWindow(Window window)
        {
            window.Close();
        }

        public static void MinimizeWindow(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        public static void MaximizeOrNormalWindow(Window window)
        {
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else if (window.WindowState == WindowState.Minimized)
            {
                window.WindowState = WindowState.Normal;
            }
        }
    }
}
