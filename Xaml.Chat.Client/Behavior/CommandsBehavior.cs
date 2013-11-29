namespace Xaml.Chat.Client.Behavior
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class CommandsBehavior
    {
        private static void ExecuteChangedText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TextBox;
            if (control == null)
            {
                return;
            }
            if ((e.NewValue != null) && (e.OldValue == null))
            {
                control.TextChanged += (snd, args) =>
                {
                    var command = (snd as UIElement).GetValue(CommandsBehavior.SearchChangedProperty) as ICommand;
                    command.Execute(args);
                };
            }
        }
        public static ICommand GetSearchChanged(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(SearchChangedProperty);
        }

        public static void SetSearchChanged(DependencyObject obj, ICommand value)
        {
            obj.SetValue(SearchChangedProperty, value);
        }

        // Using a DependencyProperty as the backing store for SearchChanged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchChangedProperty =
            DependencyProperty.RegisterAttached("SearchChanged", 
            typeof(ICommand), typeof(CommandsBehavior), 
            new PropertyMetadata(ExecuteChangedText));
    }
}
