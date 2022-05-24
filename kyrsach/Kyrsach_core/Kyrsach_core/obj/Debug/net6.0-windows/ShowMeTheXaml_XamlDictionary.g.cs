
using System.Collections.Generic;

namespace ShowMeTheXAML
{
    public static class XamlDictionary
    {
        static XamlDictionary()
        {
            XamlResolver.Set("snackbar_3", @"<smtx:XamlDisplay Width=""200"" Grid.Column=""3"" UniqueKey=""snackbar_3"" VerticalContentAlignment=""Center"" xmlns:smtx=""clr-namespace:ShowMeTheXAML;assembly=ShowMeTheXAML"">
  <materialDesign:Snackbar BorderThickness=""1"" BorderBrush=""Black"" x:Name=""SnackbarThree"" FontFamily=""Geometria"" FontSize=""25"" MessageQueue=""{materialDesign:MessageQueue}"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes"" />
</smtx:XamlDisplay>");
        }
    }
}