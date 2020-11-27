using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pegasus_library_aspnet.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; } 

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
