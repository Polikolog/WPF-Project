using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp1
{
    public class Prop : DependencyObject
    {
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty DataProperty;
        public static readonly DependencyProperty TestDataProperty;
        static Prop()
        {
            try
            {
                FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
                metadata.CoerceValueCallback = new CoerceValueCallback(CorrectValue);
                

                DataProperty = DependencyProperty.Register("Data", typeof(int), typeof(Prop), metadata, new ValidateValueCallback(Validate));

                //metadata.CoerceValueCallback = new CoerceValueCallback(CorrecrValueText);
                //metadata.DefaultValue = "default value";
                //metadata.IsNotDataBindable = true;

                FrameworkPropertyMetadata met = new FrameworkPropertyMetadata("default value", FrameworkPropertyMetadataOptions.NotDataBindable);

                TestDataProperty = DependencyProperty.Register("TestData", typeof(string), typeof(Prop), met, new ValidateValueCallback(ValidText));
               // TextProperty = DependencyProperty.Register("TextKotoriNeRabotaet", typeof(string), typeof(Prop), metadata, new ValidateValueCallback(ValidText));
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "; Ты лошара");
            }
        }

        public string TestData
        {
            get { return (string)GetValue(TestDataProperty);}
            set {
                try
                {
                    SetValue(TestDataProperty, value.ToString());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public int Data
        {
            get => (int)GetValue(DataProperty);
            set
            {
                try
                {
                    SetValue(DataProperty, value);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        static bool Validate(object v)
        {
            if (v != null)
            {
                var a = (int)v;
                if (a == 5)
                {
                    MessageBox.Show("Пятерик подъехал (Validate) ");
                }
                return true;
            }
            return true;
        }

        public string Text
        {
            get
            {
                    return (string)GetValue(TextProperty);
            }
            set
            {
                try
                {
                    SetValue(TextProperty, value);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        static object CorrectValue (DependencyObject d, object value) 
        {
            int i = (int)value;
            if (i == 0)
            {
                MessageBox.Show("это correntvalue");
                return 1;
            }
            return i;
        }

        static bool ValidText(object p)
        {
            if (p != null)
            {
                if (p.ToString() == "error")
                    return false;
                Regex reg = new Regex(@"\d");
                var o = p.ToString();
                MatchCollection match = reg.Matches(o);
                if (match.Count > 0)
                    return false;
                return true;
            }
            return true;
        }

        static object CorrecrValueText(DependencyObject d, object p)
        {
            string s = (string)p;
            if (s == null)
                return "eep";
            return p;
        }
    }
}
