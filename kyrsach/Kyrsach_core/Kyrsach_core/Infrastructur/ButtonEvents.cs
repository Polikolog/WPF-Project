using System.Windows;
using System.Windows.Controls;

namespace Kyrsach_core.Infrastructur
{
    public class ButtonEvents :Button
    {
        public static RoutedEvent ButtonClickEvent;
        static ButtonEvents()
        {
            ButtonClickEvent = EventManager.RegisterRoutedEvent("Command", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ButtonEvents));
        }
        public event RoutedEventHandler Command
        {
            add => AddHandler(ButtonClickEvent, value);
            remove => RemoveHandler(ButtonClickEvent, value);
        }
    }
}
