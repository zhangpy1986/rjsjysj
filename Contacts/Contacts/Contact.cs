using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Contacts
{
    public partial class Contact 
    {
        //[Key]
        //public int Id { get; set; }
        [NotMapped]
        public string DisplayName { get { return string.IsNullOrWhiteSpace(_Name) ? "未命名" : _Name; } }
        //public string Name { get { return name; } set { name = value; RaisePropertyChanged("Name"); RaisePropertyChanged("DisplayName"); } }
        //public string Phone { get { return phone; } set { phone = value; RaisePropertyChanged("Phone"); } }
        //public string Email { get { return email; } set { email = value; RaisePropertyChanged("Email"); } }
        //public string Photo { get { return photo; } set { photo = value; RaisePropertyChanged("Photo"); } }

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void RaisePropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private string name;
        //private string phone;
        //private string email;
        //private string photo;

        //public bool CanExecuteCallback(object param)
        //{
        //    return true;
        //}

        //public void ExecuteCallback(object param)
        //{
        //    param.ToString();
        //}


    }
}
