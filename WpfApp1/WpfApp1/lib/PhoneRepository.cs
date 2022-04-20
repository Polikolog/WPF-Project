using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfApp1.model;

namespace WpfApp1.lib
{
    public class PhoneRepository
    {
        private ObservableCollection<PhoneProduct> phones = new ObservableCollection<PhoneProduct>();
        private ObservableCollection<PhoneProduct> localPhones = new ObservableCollection<PhoneProduct>();

        public PhoneRepository()
        {
            localPhones = new ObservableCollection<PhoneProduct>();
            Commit();
        }
        public PhoneRepository(IList<PhoneProduct> phonesInit)
        {
            localPhones = new ObservableCollection<PhoneProduct>(phonesInit);
            Commit();
        }

        public ObservableCollection<PhoneProduct> GetPhones()
        {
            return localPhones;
        }
        public void AddPhone(PhoneProduct phone)
        {
            localPhones.Add(phone);
        }
        public PhoneProduct? GetPhone(uint id)
        {
            return localPhones.Where(phone => phone.Id == id).First();
        }
        public bool DeletePhone(PhoneProduct phone)
        {
            uint id = phone.Id;
            if (GetPhone(id) == null) return false;
            localPhones.Remove(phone);
            return true;
        }
        public bool EditPhone(PhoneProduct newPhone)
        {
            int editPhoneIndex = -1;
            for (int i = 0; i < localPhones.Count; i++)
            {
                if (localPhones[i].Id == newPhone.Id)
                    editPhoneIndex = i;
            }
            if (editPhoneIndex == -1) return false;
            localPhones[editPhoneIndex] = newPhone;
            return true;
        }
        public void Commit()
        {
            PhoneProduct[] tmpl = new PhoneProduct[localPhones.Count()];
            localPhones.CopyTo(tmpl, 0);
            phones.Clear();
            for (int i = 0; i < tmpl.Length; i++)
            {
                phones.Add(tmpl[i]);
            }
        }
        public void Backup()
        {
            PhoneProduct[] tmpl = new PhoneProduct[phones.Count()];
            phones.CopyTo(tmpl, 0);
            localPhones.Clear();
            for (int i = 0; i < tmpl.Length; i++)
            {
                localPhones.Add(tmpl[i]);
            }
        }
        public string Log()
        {
            string result = "";
            foreach (var phone in localPhones)
                result += phone.Id + "; ";
            return result;
        }
    }
}