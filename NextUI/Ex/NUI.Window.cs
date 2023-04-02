using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NextUI
{
    public partial class NUI
    {
        #region Window CaptionHeight
        public static double GetCaptionHeight(Window obj)
        {
            return (double)obj.GetValue(CaptionHeightProperty);
        }

        public static void SetCaptionHeight(Window obj, double value)
        {
            obj.SetValue(CaptionHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for CaptionHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.RegisterAttached("CaptionHeight", typeof(double), typeof(NUI), new PropertyMetadata(32d));
        #endregion
    }
}
