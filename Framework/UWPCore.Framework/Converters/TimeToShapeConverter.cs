using System;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UWPCore.Framework.Converters
{
    /// <summary>
    /// Value converter that creates a clock image from a time value.
    /// Reference the <see cref="System.Windows.Media"/> namespace.
    /// You can bind the result to a Path element.
    /// <remarks>
    /// This snipped is taken from the Nokia Developer Community.
    /// </remarks>
    /// </summary>
    public sealed class TimeToShapeConverter : IValueConverter
    {
        /// <summary>
        /// Converts a datetime to a visual shape.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language information.</param>
        /// <returns>The time shape.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime dt = (DateTime)value;
            GeometryGroup coll = new GeometryGroup();
            EllipseGeometry ell = new EllipseGeometry();
            ell.Center = new Point(55, 55);
            ell.RadiusX = ell.RadiusY = 60;
            coll.Children.Add(ell);
            LineGeometry hour = new LineGeometry();
            double deg = (dt.Hour % 12) * Math.PI / 6;
            hour.StartPoint = ell.Center;
            hour.EndPoint = new Point(55 + Math.Sin(deg) * 35, 55 - Math.Cos(deg) * 35);
            coll.Children.Add(hour);
            LineGeometry minute = new LineGeometry();
            minute.StartPoint = ell.Center;
            deg = dt.Minute * Math.PI / 30;
            minute.EndPoint = new Point(55 + Math.Sin(deg) * 50, 55 - Math.Cos(deg) * 50);
            coll.Children.Add(minute);
            return coll;
        }

        /// <summary>
        /// Backward convesion is not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
