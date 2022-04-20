using System;
using System.ComponentModel;

namespace WpfApp1.model
{
    [Serializable]
    public class PhoneProduct : INotifyPropertyChanged
    {
        private string title;
        private string company;
        private string image;
        private string description;
        private bool inStock;
        private decimal price;
        private float rating;
        public uint Id { get; set; }
        public string Title {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Company {
            get { return company; }
            set
            {
                company = value;
                OnPropertyChanged("Company");
            }
        }
        public string Image {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        public string Description {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public float Rating {
            get { return rating; }
            set
            {
                rating = value;
                OnPropertyChanged("Rating");
            }
        }
        public decimal Price {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public bool InStock {
            get { return inStock; }
            set
            {
                inStock = value;
                OnPropertyChanged("InStock");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
