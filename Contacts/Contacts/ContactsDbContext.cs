using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Contacts
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext()
            : base("Contacts.Properties.Settings.ContactsConnectionString")
        { }
        
        public DbSet<Contact> ContactSet { get; set; }
    }
}
