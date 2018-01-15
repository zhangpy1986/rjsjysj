using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Contacts
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }

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
