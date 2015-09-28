using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;

namespace UWPCore.Framework.Behaviors
{
    /// <summary>
    /// Behavior that can be attached to a text box to perform action on ENTER key pressed.
    /// Usage:
    /// <TextBox>
    ///   <Interactivity:Interaction.Behaviors>
    ///     <Behaviors:TextBoxEnterKeyBehavior>
    ///       <Core:CallMethodAction MethodName = "GotoDetailsPage" TargetObject="{Binding}" />
    ///     </Behaviors:TextBoxEnterKeyBehavior>
    ///   </Interactivity:Interaction.Behaviors>
    /// </TextBox>
    /// </summary>
    [ContentProperty(Name = nameof(Actions))]
    [TypeConstraint(typeof(TextBox))]
    public class TextBoxEnterKeyBehavior : DependencyObject, IBehavior
    {
        /// <summary>
        /// Gets the associated text box.
        /// </summary>
        private TextBox AssociatedTextBox { get { return AssociatedObject as TextBox; } }

        /// <summary>
        /// Gets the associated object.
        /// </summary>
        public DependencyObject AssociatedObject { get; private set; }

        /// <summary>
        /// Attaches the object.
        /// </summary>
        /// <param name="associatedObject">The object to attach.</param>
        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
            AssociatedTextBox.KeyUp += AssociatedTextBox_KeyUp;
        }

        /// <summary>
        /// Deteches the object.
        /// </summary>
        public void Detach()
        {
            AssociatedTextBox.KeyUp -= AssociatedTextBox_KeyUp;
        }

        /// <summary>
        /// Fired when a key was pressed.
        /// </summary>
        private void AssociatedTextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Interaction.ExecuteActions(AssociatedObject, this.Actions, null);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Gets the actions.
        /// </summary>
        public ActionCollection Actions
        {
            get
            {
                var actions = (ActionCollection)base.GetValue(ActionsProperty);
                if (actions == null)
                {
                    SetValue(ActionsProperty, actions = new ActionCollection());
                }
                return actions;
            }
        }

        /// <summary>
        /// The actions dependency property.
        /// </summary>
        public static readonly DependencyProperty ActionsProperty =
            DependencyProperty.Register("Actions", typeof(ActionCollection),
                typeof(TextBoxEnterKeyBehavior), new PropertyMetadata(null));
    }
}
