using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Contact = Contacts.contact;

namespace Contacts
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public ContactsViewModel()
        {
            ReloadContacts();
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public Contact Selected { get { return selectedIndex >= Contacts.Count || selectedIndex < 0 ? null : Contacts[selectedIndex]; } }

        public int SelectedIndex { get { return selectedIndex; } set { selectedIndex = value; RaisePropertyChanged("Selected"); } }

        public void Reload()
        {
            ReloadContacts();
        }

        public bool Save(out string errorMsg)
        {
            if (!Validate(Selected, out errorMsg))
            {
                return false;
            }
            errorMsg = null;
            using (ContactsDbContext context = new ContactsDbContext())
            {
                string cstr = ((IObjectContextAdapter)context).ObjectContext.Connection.ConnectionString;
                if (Selected.Id == 0)
                {
                    context.ContactSet.Add(Selected);
                }
                else
                {
                    Contact existed = (from c in context.ContactSet where c.Id == Selected.Id select c).FirstOrDefault();
                    if (existed != null)
                    {
                        existed.Name = Selected.Name;
                        existed.Email = Selected.Email;
                        existed.Phone = Selected.Phone;
                        existed.Photo = Selected.Photo;
                        context.Entry(existed).State = EntityState.Deleted;
                    }
                    else
                    {
                        return true; // 设置断点来debug
                    }
                }

                bool hasChange = context.ChangeTracker.HasChanges();
                if (hasChange)
                {
                    try
                    {
                        int resultAffected = context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        string error = ex.Message;
                        throw;
                    }
                }

                // 确认
                //List<Contact> news = context.ContactSet.SqlQuery("select * from contact").ToList();
                //int count = news.Count;
                //bool hasAdded = count == Contacts.Count;
            }
            return true;
        }

        public void Search(string text)
        {
            using (ContactsDbContext context = new ContactsDbContext())
            {
                Contacts = new ObservableCollection<Contact>(from c in context.ContactSet where c.Name.Contains(text) || c.Phone.Contains(text) select c);
            }
            RaisePropertyChanged("Contacts");
        }

        private bool Validate(Contact contact, out string errorMsg)
        {
            errorMsg = null;

            if (string.IsNullOrWhiteSpace(contact.Name))
            {
                errorMsg = "姓名不可为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(contact.Phone))
            {
                errorMsg = "电话号码不可为空";
                return false;
            }
            if (!phoneRegex.IsMatch(contact.Phone))
            {
                errorMsg = "电话号码格式错误";
                return false;
            }
            if (!string.IsNullOrWhiteSpace(contact.Email) && !emailRegex.IsMatch(contact.Email))
            {
                errorMsg = "电子邮件格式错误";
                return false;
            }
            return true;
        }

        public void Remove()
        {
            Contact temp = Selected;
            this.Contacts.Remove(temp);
            using (ContactsDbContext context = new ContactsDbContext())
            {
                DbEntityEntry<Contact> entry = context.Entry(temp);
                entry.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public bool CanExecuteSelectCallback(object param)
        {
            return !string.IsNullOrWhiteSpace(Selected.Name);
        }

        public void ExecuteSelectCallback(object param)
        {
            param.ToString();
        }

        public ICommand Select
        {
            //return new RoutedCommand();
            get
            {
                //return new RoutedUICommand();
                return new RelayCommand(ExecuteSelectCallback, CanExecuteSelectCallback);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ReloadContacts()
        {
            using (ContactsDbContext context = new ContactsDbContext())
            {
                Contacts = new ObservableCollection<Contact>(from c in context.ContactSet select c);
            }
            RaisePropertyChanged("Contacts");
        }

        private int selectedIndex;
        private Regex phoneRegex = new Regex("^(?:\\+86)?0?(13|15|18)\\d{9}$");
        //private Regex emailRegex = new Regex("^[a-zA-Z0-9_\\.]+(?!\\.)@[a-zA-Z0-9]+$");
        private Regex emailRegex = new Regex("^[a-zA-Z0-9_\\.]+(?!\\.)@[a-zA-Z0-9\\.]+$");
        private ContactsDbContext context = new ContactsDbContext();
    }
}
