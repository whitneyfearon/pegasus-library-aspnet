using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pegasus_library_aspnet.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name = "Genre")]
        public string GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
