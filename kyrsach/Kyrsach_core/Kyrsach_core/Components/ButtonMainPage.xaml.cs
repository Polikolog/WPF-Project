using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Kyrsach_core.ViewModel;

namespace Kyrsach_core.Components
{
    /// <summary>
    /// Логика взаимодействия для ButtonMainPage.xaml
    /// </summary>
    public partial class ButtonMainPage : UserControl
    {
        

        public ButtonMainPage()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ButtonMainPage), new PropertyMetadata("Гостиная"));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set 
            { 
                SetValue(TextProperty, value);
                //TextBlock = Text;
            }
        }
        public static readonly DependencyProperty SourceImageProperty = DependencyProperty.Register("SourceImage", typeof(ImageSource), typeof(ButtonMainPage), new PropertyMetadata(null));
        public ImageSource SourceImage
        {
            get => (ImageSource)GetValue(SourceImageProperty);
            set
            {
                SetValue(SourceImageProperty, value);           
                //ImageSource = SourceImage;
                
            }
        }

    }
}
