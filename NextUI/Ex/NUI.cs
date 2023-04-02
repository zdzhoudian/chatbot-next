using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shell;

namespace NextUI
{
    public partial class NUI
    {
        #region Some values 值

        public static bool True { get; } = true;

        public static bool False { get; } = false;

        #endregion

        #region IsInDesignMode 设计器模式状态
        public static bool IsInDesignMode
        {
            get
            {
                return (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
            }
        }
        #endregion

        #region IsHitTestVisibleInCaption 在标题栏可以和控件交互


        public static bool GetIsHitTestVisibleInCaption(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsHitTestVisibleInCaptionProperty);
        }

        public static void SetIsHitTestVisibleInCaption(DependencyObject obj, bool value)
        {
            obj.SetValue(IsHitTestVisibleInCaptionProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsHitTestVisibleInCaption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHitTestVisibleInCaptionProperty =
            DependencyProperty.RegisterAttached("IsHitTestVisibleInCaption", typeof(bool), typeof(NUI), new PropertyMetadata(false, (d, e) =>
            {
                d.SetValue(WindowChrome.IsHitTestVisibleInChromeProperty, e.NewValue);
            }));


        #endregion

        #region CornerRadius 圆角
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(NUI), new PropertyMetadata(new CornerRadius(4d)));
        #endregion

        #region Effect 阴影
        public static Effect GetEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(EffectProperty);
        }

        public static void SetEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(EffectProperty, value);
        }

        // Using a DependencyProperty as the backing store for Effect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EffectProperty =
            DependencyProperty.RegisterAttached("Effect", typeof(Effect), typeof(NUI), new PropertyMetadata(null));
        #endregion

        #region Icon 图标
        public static object GetCheckedIcon(DependencyObject obj)
        {
            return (object)obj.GetValue(CheckedIconProperty);
        }

        public static void SetCheckedIcon(DependencyObject obj, object value)
        {
            obj.SetValue(CheckedIconProperty, value);
        }

        // Using a DependencyProperty as the backing store for CheckedIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedIconProperty =
            DependencyProperty.RegisterAttached("CheckedIcon", typeof(object), typeof(NUI), new PropertyMetadata(null));



        public static object GetUncheckedIcon(DependencyObject obj)
        {
            return (object)obj.GetValue(UncheckedIconProperty);
        }

        public static void SetUncheckedIcon(DependencyObject obj, object value)
        {
            obj.SetValue(UncheckedIconProperty, value);
        }

        // Using a DependencyProperty as the backing store for UncheckedIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UncheckedIconProperty =
            DependencyProperty.RegisterAttached("UncheckedIcon", typeof(object), typeof(NUI), new PropertyMetadata(null));
        #endregion

        #region PrefixContent 前缀内容
        public static object GetPrefixContent(DependencyObject obj)
        {
            return (object)obj.GetValue(PrefixContentProperty);
        }

        public static void SetPrefixContent(DependencyObject obj, object value)
        {
            obj.SetValue(PrefixContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for PrefixContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrefixContentProperty =
            DependencyProperty.RegisterAttached("PrefixContent", typeof(object), typeof(NUI), new PropertyMetadata(null));
        #endregion

        #region SuffixContent 后缀内容

        public static object GetSuffixContent(DependencyObject obj)
        {
            return (object)obj.GetValue(SuffixContentProperty);
        }

        public static void SetSuffixContent(DependencyObject obj, object value)
        {
            obj.SetValue(SuffixContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for SuffixContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SuffixContentProperty =
            DependencyProperty.RegisterAttached("SuffixContent", typeof(object), typeof(NUI), new PropertyMetadata(null));

        #endregion

        #region WaterMark 水印
        public static object GetWaterMark(DependencyObject obj)
        {
            return (object)obj.GetValue(WaterMarkProperty);
        }

        public static void SetWaterMark(DependencyObject obj, object value)
        {
            obj.SetValue(WaterMarkProperty, value);
        }

        // Using a DependencyProperty as the backing store for WaterMark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.RegisterAttached("WaterMark", typeof(object), typeof(NUI), new PropertyMetadata(null));
        #endregion

        #region WaterMarkBrush 水印前景颜色
        public static Brush GetWaterMarkBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(WaterMarkBrushProperty);
        }

        public static void SetWaterMarkBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(WaterMarkBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for WaterMarkBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterMarkBrushProperty =
            DependencyProperty.RegisterAttached("WaterMarkBrush", typeof(Brush), typeof(NUI), new PropertyMetadata(null));
        #endregion


    }
}
