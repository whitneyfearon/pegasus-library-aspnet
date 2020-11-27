using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pegasus_library_aspnet.Data;
using pegasus_library_aspnet.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace pegasus_library_aspnet.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;


        public BooksController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            hostingEnvironment = env;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Book.Include(b => b.Author).Include(b => b.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            List<SelectListItem> items = new SelectList((from p in _context.Author.ToList() select new { p.Id, Name = p.FirstName + " " + p.LastName }), "Id", "Name").ToList();
            items.Insert(0, new SelectListItem { Text = "Select an Author", Value = "0" });
            ViewData["AuthorId"] = items;

            List<Genre> genreList = new List<Genre>();
            genreList = (from o in _context.Genre select o).ToList();
            genreList.Insert(0, new Genre { Id = 0, GenreName = "Select a Genre" });
            ViewData["GenreId"] = new SelectList(genreList, "Id", "GenreName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ISBN,Title,AuthorId,GenreId,PublicationDate,Quantity,Price,Description,ImagePath")] Book book, IFormFile imgPath)
        {
            if (ModelState.IsValid)
            {
                if (book.GenreId == 0)
                {
                    book.GenreId = null;
                }
                if (book.AuthorId == 0)
                {
                    book.AuthorId = null;
                }

                var isExists = _context.Book.Any(x => x.ISBN == book.ISBN);
                if (isExists)
                {
                    ModelState.AddModelError("ISBN", "ISBN already exist!");
                    return View(book);
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                // Code to upload image
                if (imgPath == null)
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (imgPath.FileName != null || imgPath.Length != 0)
                    {
                        FileInfo fi = new FileInfo(imgPath.FileName);
                        var newFilename = book.Id + "_" + String.Format("{0:d}", (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                        var webPath = hostingEnvironment.WebRootPath;
                        var path = Path.Combine("", webPath + @"\images\" + newFilename);
                        var pathToSave = newFilename;
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await imgPath.CopyToAsync(stream);
                        }
                        book.ImagePath = pathToSave;
                        _context.Update(book);
                        await _context.SaveChangesAsync();
                    }
                }
                
                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList((from p in _context.Author.ToList() select new { p.Id, Name = p.FirstName + " " + p.LastName }), "Id", "Name", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName", book.GenreId);
            return View(book);
        }


        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            //ViewData["imgPath"] = book.ImagePath;

           
            if (book == null)
            {
                return NotFound();
            }

            List<SelectListItem> items = new SelectList((from p in _context.Author.ToList() select new { p.Id, Name = p.FirstName + " " + p.LastName }), "Id", "Name").ToList();
            items.Insert(0, new SelectListItem { Text = "Select an Author", Value = "0" });
            var selectedTo = items.Where(x => x.Value == book.AuthorId.ToString()).FirstOrDefault();
            if (selectedTo != null)
            {
                selectedTo.Selected = true;
            }
            ViewData["AuthorId"] = items;

            List<Genre> genreList = new List<Genre>();
            genreList = (from o in _context.Genre select o).ToList();
            genreList.Insert(0, new Genre { Id = 0, GenreName = "Select a Genre" });
            ViewData["GenreId"] = new SelectList(genreList, "Id", "GenreName", book.GenreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile imgPath, int id, [Bind("Id,ISBN,Title,AuthorId,GenreId,PublicationDate,Quantity,Price,Description,ImagePath")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

           
            if (ModelState.IsValid)
            {
                try
                {
                    //if (imgPath == null)
                    //{

                    //    _context.Update(book);
                    //    await _context.SaveChangesAsync();
                    //}
                    //else
                    //{
                    //    if (imgPath.FileName != null || imgPath.Length != 0)
                    //    {
                    //        FileInfo fi = new FileInfo(imgPath.FileName);
                    //        var newFilename = book.Id + "_" + String.Format("{0:d}", (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                    //        var webPath = hostingEnvironment.WebRootPath;
                    //        var path = Path.Combine("", webPath + @"\images\" + newFilename);
                    //        var pathToSave = newFilename;
                    //        using (var stream = new FileStream(path, FileMode.Create))
                    //        {
                    //            await imgPath.CopyToAsync(stream);
                    //        }
                    //        book.ImagePath = pathToSave;
                    //        _context.Update(book);
                    //        await _context.SaveChangesAsync();
                    //    }
                    //}
                    if (book.GenreId == 0)
                    {
                        book.GenreId = null;
                    }
                    if (book.AuthorId == 0)
                    {
                        book.AuthorId = null;
                    }
                    _context.Update(book);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> items = new SelectList((from p in _context.Author.ToList() select new { p.Id, Name = p.FirstName + " " + p.LastName }), "Id", "Name").ToList();
            items.Insert(0, new SelectListItem { Text = "Select an Author", Value = "0" });
            var selectedTo = items.Where(x => x.Value == book.AuthorId.ToString()).FirstOrDefault();
            if (selectedTo != null)
            {
                selectedTo.Selected = true;
            }
            ViewData["AuthorId"] = items;

            List<Genre> genreList = new List<Genre>();
            genreList = (from o in _context.Genre select o).ToList();
            genreList.Insert(0, new Genre { Id = 0, GenreName = "Select a Genre" });
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "GenreName", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
