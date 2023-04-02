using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace NextUI
{
    public class ResourceBinding : MarkupExtension
    {
        #region class
        private class ResourceKeyConverter : IMultiValueConverter
        {
            public static readonly ResourceKeyConverter Default = new ResourceKeyConverter();

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                return new Tuple<object, DependencyProperty>(values[0], (DependencyProperty)parameter);
            }


            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                return new object[] { Binding.DoNothing };
            }
        }
        #endregion

        #region ResourceKey
        public static object GetResourceKey(DependencyObject obj)
        {
            return (object)obj.GetValue(ResourceKeyProperty);
        }

        public static void SetResourceKey(DependencyObject obj, object value)
        {
            obj.SetValue(ResourceKeyProperty, value);
        }

        // Using a DependencyProperty as the backing store for ResourceKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResourceKeyProperty =
            DependencyProperty.RegisterAttached("ResourceKey", typeof(object), typeof(ResourceBinding), new PropertyMetadata(null, ResourceKeyChanged));

        private static void ResourceKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as FrameworkElement;
            var newVal = e.NewValue as Tuple<object, DependencyProperty>;
            if (target == null || newVal == null)
            {
                return;
            }

            var dp = newVal.Item2;

            if (newVal.Item1 == null)
            {
                target.SetValue(dp, dp.GetMetadata(target).DefaultValue);
                return;
            }

            target.SetResourceReference(dp, newVal.Item1);
        }
        #endregion

        #region ctor

        public ResourceBinding()
        {

        }

        public ResourceBinding(string path)
        {
            Path = new PropertyPath(path);
        }

        #endregion

        #region 属性

        public PropertyPath Path { get; set; }

        public object Source { get; set; }

        [DefaultValue(null)]
        public string XPath { get; set; }

        [DefaultValue(BindingMode.Default)]
        public BindingMode Mode { get; set; }


        [DefaultValue(UpdateSourceTrigger.Default)]
        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        [DefaultValue(null)]
        public IValueConverter Converter { get; set; }

        [DefaultValue(null)]
        public object ConverterParameter { get; set; }

        [DefaultValue(null)]
        [TypeConverter(typeof(System.Windows.CultureInfoIetfLanguageTagConverter))]
        public CultureInfo ConverterCulture { get; set; }

        [DefaultValue(null)]
        public RelativeSource RelativeSource { get; set; }

        [DefaultValue(null)]
        public string ElementName { get; set; }

        public string FallbackValue { get; set; }
        #endregion


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (provideValueTarget == null)
            {
                return null;
            }

            if (provideValueTarget.TargetObject != null &&
                provideValueTarget.TargetObject.GetType().FullName == "System.Windows.SharedDp")
            {
                return this;
            }

            if (provideValueTarget.TargetObject is not FrameworkElement targetObject ||
                provideValueTarget.TargetProperty is not DependencyProperty targetProperty)
            {
                return null;
            }

            var binding = new Binding()
            {
                Path = Path,
                XPath = XPath,
                Mode = Mode,
                UpdateSourceTrigger = UpdateSourceTrigger,
                Converter = Converter,
                ConverterParameter = ConverterParameter,
                ConverterCulture = ConverterCulture,
                FallbackValue = FallbackValue,
            };

            if (RelativeSource != null)
            {
                binding.RelativeSource = RelativeSource;
            }
            if (ElementName != null)
            {
                binding.ElementName = ElementName;
            }
            if (Source != null)
            {
                binding.Source = Source;
            }

            var multiBinding = new MultiBinding()
            {
                Converter = ResourceKeyConverter.Default,
                ConverterParameter = targetProperty,
            };
            multiBinding.Bindings.Add(binding);
            multiBinding.NotifyOnSourceUpdated = true;
            targetObject.SetBinding(ResourceKeyProperty, multiBinding);
            
            return null;
        }
    }
}
