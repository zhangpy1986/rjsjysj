using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Contact = Contacts.contact;

namespace Contacts
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext()
                  //: base("Contacts.Properties.Settings.contactsConnectionString1")
                  : base("local")
        { }

        public DbSet<Contact> ContactSet { get; set; }
    }
}
