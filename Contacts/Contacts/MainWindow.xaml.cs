using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Contacts
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            contactsViewModel = new ContactsViewModel();
            this.DataContext = contactsViewModel;
            //contactList.ItemsSource = contactsViewModel.Contacts;
        }

        private void ContactList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contactsViewModel.SelectedIndex = (sender as ListView).SelectedIndex;
        }

        private ContactsViewModel contactsViewModel;

        private void AddContact_OnClick(object sender, RoutedEventArgs e)
        {
            contactsViewModel.Contacts.Add(new Contact() { });
            contactsViewModel.SelectedIndex = contactsViewModel.Contacts.Count - 1;
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            errorDispay.Content = string.Empty;
            string error;
            if (!contactsViewModel.Save(out error))
            {
                errorDispay.Content = error;
            }
        }

        private void RemoveContact_OnClick(object sender, RoutedEventArgs e)
        {
            int removedIndex = contactList.SelectedIndex;
            contactsViewModel.Remove();
            if (removedIndex > 0)
            {
                contactsViewModel.SelectedIndex = removedIndex - 1;
            }
            else if (contactList.Items.Count > 0)
            {
                contactsViewModel.SelectedIndex = 0;
            }
        }
    }
}
