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
            : base("ContactsDbContext")
        { }

        public DbSet<Contact> ContactSet { get; set; }
    }
}
