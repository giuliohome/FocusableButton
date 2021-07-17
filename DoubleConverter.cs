using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Text2Double
{
    public class CultureAwareMultiBinding : MultiBinding
    {
        public CultureAwareMultiBinding() : base()
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }
    }
    public class CultureAwareBinding : Binding
    {
        public CultureAwareBinding() : base()
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }
        public CultureAwareBinding(string path) : base(path)
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }
    }

    public class DoubleMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null)
            {
                return values[1];
            }
            double source = (double)values[0];
            string text = (string)values[1];
            
            if (double.TryParse(text, NumberStyles.Any, culture, out double target) && target == source)
            {
                return Binding.DoNothing;
            }
            else
            {
                return source.ToString(culture);
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object ret = null;

            string text = (string)value;

            if (string.IsNullOrWhiteSpace(text))
                ret = null;

            else if (double.TryParse(text, NumberStyles.Any, culture, out double target))
                ret = target;

            //if (ret == null)
            //    return null;

            return new object[] { ret };
        }

    }

    public class DoubleSingleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                Console.WriteLine("Convert back double " + (double)value);
                if (parameter is string)
                {
                    if (double.TryParse((string)parameter, NumberStyles.Float, culture, out double result))
                    {
                        if ((double)result == (double)value)
                        {
                            return Binding.DoNothing;
                        } 
                    }
                }
                return value.ToString();
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string)
            {

                if (double.TryParse((string)value, NumberStyles.Float,  culture, out double result))
                {
                    return result;
                } else
                {
                    return Binding.DoNothing;
                }
            }
            return Binding.DoNothing;
        }
    }
}
