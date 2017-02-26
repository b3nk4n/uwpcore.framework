using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace UWPCore.Framework.Controls
{
    /// <summary>
    /// Helper class for control bindings.
    /// </summary>
    public static class ControlBindingHelper
    {
        /// <summary>
        /// Forces the currently focused text or password box to update its bindings.
        /// This might be required when an event of the app-bar is fired, which does not change the focus.
        /// </summary>
        /// <see cref="https://www.pedrolamas.com/2013/01/11/how-to-force-a-focused-textbox-binding-to-update-when-i-tap-an-app-bar-item/"/>
        public static void FocusedTextBoxUpdateSource()
        {
            var focusedElement = FocusManager.GetFocusedElement();
            var focusedTextBox = focusedElement as TextBox;

            if (focusedTextBox != null)
            {
                var binding = focusedTextBox.GetBindingExpression(TextBox.TextProperty);

                if (binding != null)
                {
                    binding.UpdateSource();
                }
            }
            else
            {
                var focusedPasswordBox = focusedElement as PasswordBox;

                if (focusedPasswordBox != null)
                {
                    var binding = focusedPasswordBox.GetBindingExpression(PasswordBox.PasswordProperty);

                    if (binding != null)
                    {
                        binding.UpdateSource();
                    }
                }
            }
        }
    }
}
