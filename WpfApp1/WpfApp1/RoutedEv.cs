using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    internal class RoutedEv : Button
    {
        public static RoutedEvent MyButtonClickEvent;

        static RoutedEv()
        {
            MyButtonClickEvent = EventManager.RegisterRoutedEvent("MyButtonClick", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(RoutedEv));
        }

        public event RoutedEventHandler MyButtonClick
        {
            add { AddHandler(MyButtonClickEvent, value); }
            remove { RemoveHandler(MyButtonClickEvent, value); }
        }

        protected override void OnClick()
        {
            base.OnClick();
            RoutedEventArgs args = new RoutedEventArgs(RoutedEv.MyButtonClickEvent, this);
            RaiseEvent(args);
        }
    }
}
