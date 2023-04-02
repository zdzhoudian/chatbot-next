using System;
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

        public static bool GetAutoScrollToEnd(ScrollViewer obj)
        {
            return (bool)obj.GetValue(AutoScrollToEndProperty);
        }

        public static void SetAutoScrollToEnd(ScrollViewer obj, bool value)
        {
            obj.SetValue(AutoScrollToEndProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoScrollToEnd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoScrollToEndProperty =
            DependencyProperty.RegisterAttached("AutoScrollToEnd", typeof(bool), typeof(NUI), new PropertyMetadata(false, AutoScrollToEndChanged));


        private static void AutoScrollToEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = d as ScrollViewer;
            if (e.NewValue is bool b && b)
            {
                viewer.ScrollToEnd();
                viewer.ScrollChanged += Viewer_ScrollChanged;
            }
            else
            {
                viewer.ScrollChanged -= Viewer_ScrollChanged;
            }
        }

        private static bool _autoScroll;

        private static void Viewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var viewer = sender as ScrollViewer;
            if (viewer == null)
            {
                return;
            }


            if (e.ExtentHeightChange == 0)
            {
                _autoScroll = viewer.VerticalOffset == viewer.ScrollableHeight;
            }
            if (_autoScroll && e.ExtentHeightChange != 0)
            {
                viewer.ScrollToVerticalOffset(viewer.ExtentHeight);
            }
        }
    }
}
