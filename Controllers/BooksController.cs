using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.DataAccess;
using Library.Models;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string? sort)
        {
    
            // Start Query
            var query = _context.Books.AsQueryable();

            // Sort
            switch (sort)
            {
                case "title_desc":
                    query = query.OrderByDescending(b => b.Title);
                    break;
                case "author_asc":
                    query = query.OrderBy(b => b.Author);
                    break;
                case "author_desc":
                    query = query.OrderByDescending(b => b.Author);
                    break;
                case "date_asc":
                    query = query.OrderBy(b => b.PublicationDate);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(b => b.PublicationDate);
                    break;
                default:
                    query = query.OrderBy(b => b.Title);
                    break;
            }

            if (sort is null)
            {
                sort = "title_asc";
            }
            
            ViewBag.CurrentSort = sort;

            ViewBag.TitleSort = sort == "title_asc" ? "title_desc" : "title_asc";
            ViewBag.AuthorSort = sort == "author_asc" ? "author_desc" : "author_asc";
            ViewBag.DateSort = sort == "date_asc" ? "date_desc" : "date_asc";

            return View(await query.Include(b => b.BookType).ToListAsync());
        }
    }
}
