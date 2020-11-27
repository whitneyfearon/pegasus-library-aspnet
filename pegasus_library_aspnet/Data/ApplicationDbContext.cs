using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace pegasus_library_aspnet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

          public DbSet<pegasus_library_aspnet.Models.Book> Book { get; set; }
        public DbSet<pegasus_library_aspnet.Models.Author> Author { get; set; }
        public DbSet<pegasus_library_aspnet.Models.Genre> Genre { get; set; }
    }
}
