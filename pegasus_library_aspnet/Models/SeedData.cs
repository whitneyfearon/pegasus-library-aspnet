//using pegasus_library_aspnet.Data;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace pegasus_library_aspnet.Models
//{
//    public class SeedData
//    {
//        public static void Initialize(IServiceProvider serviceProvider)
//        {
//            using (var context = new ApplicationDbContext(
//                serviceProvider.GetRequiredService<
//                    DbContextOptions<ApplicationDbContext>>()))
//            {
//                // Look for any book
//                if (context.Book.Any())
//                {
//                    return;   // DB has been seeded
//                }
//                if (context.Author.Any())
//                {
//                    return;   // DB has been seeded
//                }
//                if (context.Genre.Any())
//                {
//                    return;   // DB has been seeded
//                }


//                var authors = new List<Author>
//                {
//                    new Author
//                    {
//                        FirstName = "Jennifer",
//                        LastName = "Niven"
//                    },

//                    new Author
//                    {
//                         FirstName = "Roald",
//                         LastName = "Dahl"
//                    }
//                };

//                foreach (Author a in authors)
//                {

//                    if (!context.Author.Any(o => o.FirstName == a.FirstName))
//                    {
//                        context.Author.Add(a);
//                    }
//                    else
//                    {
//                        var authorData = context.Author.SingleOrDefault(m => m.FirstName == a.FirstName);
//                        context.Author.Update(authorData);
//                    }
//                }
//                context.SaveChanges();

//                var genres = new List<Genre>
//                {

//                     new Genre
//                     {
//                         GenreName = "Teen & Young Adult"
//                     },

//                     new Genre
//                     {
//                          GenreName = "Children's"
//                     },
//                     new Genre
//                     {
//                          GenreName = "Mystery & Suspense"
//                     },
//                     new Genre
//                     {
//                          GenreName = "Literary Fiction"
//                     },
//                     new Genre
//                     {
//                          GenreName = "Romance"
//                     },
//                     new Genre
//                     {
//                          GenreName = "Educational"
//                     },
//                     new Genre
//                     {
//                          GenreName = "Action & Adventure"
//                     }
//                };

//                foreach (Genre g in genres)
//                {

//                    if (!context.Genre.Any(o => o.GenreName == g.GenreName))
//                    {
//                        context.Genre.Add(g);
//                    }
//                    else
//                    {
//                        var genreData = context.Genre.SingleOrDefault(m => m.GenreName == g.GenreName);
//                        context.Genre.Update(genreData);
//                    }
//                }

//                context.SaveChanges();

//                var books = new List<Book>
//                {
//                     new Book
//                     {
//                         ISBN = "9780593126",
//                         Title = "All The Bright Places",
//                         PublicationDate = DateTime.Parse("2019-07-18"),
//                         Quantity = 15,
//                         Price = 2000,
//                         Description = "The New York Times bestselling love story about two teens who find each other while standing on the edge.",
//                         ImagePath = "atbp.jpg",
//                         AuthorId = authors.Single(m => m.Id == 1).Id,
//                         GenreId = genres.Single(m => m.Id == 1).Id
//                     },

//                    new Book
//                    {
//                         ISBN = "9781984836",
//                         Title = "Matilda",
//                         PublicationDate = DateTime.Parse("2010-02-05"),
//                         Quantity = 10,
//                         Price = 500,
//                         Description = "From the bestselling author of Charlie and the Chocolate Factory and The BFG comes the story of a girl with extraordinary abilities.",
//                         ImagePath = "matilda.jpg",
//                         AuthorId = authors.Single(m => m.Id == 2).Id,
//                         GenreId = genres.Single(m => m.Id == 2).Id
//                     },

//                     new Book
//                     {
//                         ISBN = "9780385755",
//                         Title = "Hold Up The Universe",
//                         PublicationDate = DateTime.Parse("2016-04-04"),
//                         Quantity = 5,
//                         Price = 1500,
//                         Description = "a heart-wrenching story about what it means to see someone—and love someone—for who they truly are.",
//                         ImagePath = "hutu.jpg",
//                         AuthorId = authors.Single(m => m.Id == 1).Id,
//                         GenreId = genres.Single(m => m.Id == 1).Id
//                     }


//                };

//                foreach (Author e in authors)
//                {

//                    if (!context.Author.Any(o => o.FirstName == e.FirstName))
//                    {
//                        context.Author.Add(e);
//                    }
//                    else
//                    {
//                        var authorData = context.Author.SingleOrDefault(m => m.FirstName == e.FirstName);
//                        context.Author.Update(authorData);
//                    }
//                }
//                context.SaveChanges();


//            }
//        }
//    }
//}
