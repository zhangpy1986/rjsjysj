using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Contacts
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public ContactsViewModel()
        {
            Initialize();
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public Contact Selected { get { return Contacts[selectedIndex]; } }

        public int SelectedIndex { get { return selectedIndex; } set { selectedIndex = value; RaisePropertyChanged("Selected"); } }



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

        private int selectedIndex;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Initialize()
        {
            ContactsDbContext dbContext = new ContactsDbContext();
            DbSqlQuery<Contact> query = dbContext.ContactSet.SqlQuery("select * from Contact");
            Contacts = new ObservableCollection<Contact>(query);

        }
    }
}
