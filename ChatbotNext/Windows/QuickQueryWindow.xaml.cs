using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatbotNext.Windows
{
    /// <summary>
    /// QuickQueryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QuickQueryWindow : Window
    {
        public QuickQueryWindow()
        {
            InitializeComponent();
        }

        private void win_Loaded(object sender, RoutedEventArgs e)
        {
            var v = 128;
            this.Top = v;
            this.Left = SystemParameters.WorkArea.Width - v - this.Width;
        }

        private Point _lastMousePoint;
        private bool _startMoveWindow;
        private bool _hasMoveWindow;

        private void win_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _hasMoveWindow = false;
            if (e.ChangedButton == MouseButton.Left)
            {
                this.CaptureMouse();
                _startMoveWindow = true;
                _lastMousePoint = e.GetPosition(this);
            }
        }

        private void win_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _startMoveWindow)
            {
                var p = e.GetPosition(this);
                var x = this.Left + p.X - _lastMousePoint.X;
                var y = this.Top + p.Y - _lastMousePoint.Y;
                this.Left = x;
                this.Top = y;
                //_lastMousePoint = p;
                _hasMoveWindow = true;
            }
        }

        private void win_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            _startMoveWindow = false;
            if (e.ChangedButton == MouseButton.Left && !_hasMoveWindow)
            {
                this.quickQueryPopup.IsOpen = !this.quickQueryPopup.IsOpen;
            }
        }
    }
}
