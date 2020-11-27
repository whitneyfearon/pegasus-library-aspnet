using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pegasus_library_aspnet.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(13)]
        [Required(ErrorMessage = "ISBN Required")]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }


        [Required(ErrorMessage = "Title Required")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [ForeignKey("Author")]
        [Display(Name = "Author")]
        public int? AuthorId { get; set; }

        [ForeignKey("Genre")]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }


        //[Display(Name = "Published Date")]
        //public DateTime PublishDate { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Publication Date")]
        public DateTime? PublicationDate { get; set; }

        [Required(ErrorMessage = "Quantity Required")]
        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public virtual Author Author { get; set; }
        public virtual Genre Genre { get; set; }


    }
}
