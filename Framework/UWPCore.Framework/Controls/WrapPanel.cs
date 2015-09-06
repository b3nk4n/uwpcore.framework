using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// A wrap panel class that can be used as an <see cref="ItemsControl.ItemsPanel"/> for auto wrapping its content.
    /// Can also be used within a <see cref="ListView"/> or a <see cref="ListBox"/>
    /// </summary>
    /// <see cref="http://www.visuallylocated.com/post/2015/02/20/Creating-a-WrapPanel-for-your-Windows-Runtime-apps.aspx"/>
    public class WrapPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            // just take up all of the width
            Size finalSize = new Size { Width = availableSize.Width };
            double x = 0;
            double rowHeight = 0d;
            foreach (var child in Children)
            {
                // tell the child control to determine the size needed
                child.Measure(availableSize);

                x += child.DesiredSize.Width;
                if (x > availableSize.Width)
                {
                    // this item will start the next row
                    x = child.DesiredSize.Width;

                    // adjust the height of the panel
                    finalSize.Height += rowHeight;
                    rowHeight = child.DesiredSize.Height;
                }
                else
                {
                    // get the tallest item
                    rowHeight = Math.Max(child.DesiredSize.Height, rowHeight);
                }
            }

            // add the final height
            finalSize.Height += rowHeight;
            return finalSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect finalRect = new Rect(0, 0, finalSize.Width, finalSize.Height);

            double rowHeight = 0;
            foreach (var child in Children)
            {
                if ((child.DesiredSize.Width + finalRect.X) > finalSize.Width)
                {
                    // next row!
                    finalRect.X = 0;
                    finalRect.Y += rowHeight;
                    rowHeight = 0;
                }
                // place the item
                child.Arrange(new Rect(finalRect.X, finalRect.Y, child.DesiredSize.Width, child.DesiredSize.Height));

                // adjust the location for the next items
                finalRect.X += child.DesiredSize.Width;
                rowHeight = Math.Max(child.DesiredSize.Height, rowHeight);
            }
            return finalSize;
        }
    }
}
