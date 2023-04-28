using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using outer_controller_mvc.Data;
using outer_controller_mvc.Models;

namespace outer_controller_mvc.Controllers
{
    public class BookDatasController : Controller
    {
        private readonly outer_controller_mvcContext _context;

        public BookDatasController(outer_controller_mvcContext context)
        {
            _context = context;
        }

        // GET: BookDatas
        public async Task<IActionResult> Index()
        {
              return _context.BookData != null ? 
                          View(await _context.BookData.ToListAsync()) :
                          Problem("Entity set 'outer_controller_mvcContext.BookData'  is null.");
        }


        // GET: BookDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,AuthorName")] BookData bookData)
        {

            //controlando autores repetidos
            Author author_aux = _context.Author.FirstOrDefault(a => a.Name == bookData.AuthorName);
            var rawsql = _context.Author
                .FromSqlRaw($"select * from Author where Name = '{bookData.AuthorName}'")
                .ToList();
            var linqquery = _context.Author
                         .FromSqlRaw($"select * from Author")
                         .Where(a=>a.Name == bookData.AuthorName)
                         .ToList();
            int id;
            if(author_aux == null)
            {
                
                //creando Author si no existe uno con el mismo nombre
                Author author = new Author();
                author.Name = bookData.AuthorName;
                _context.Author.Add(author);
                await _context.SaveChangesAsync();
                id = author.Id;
            }
            else
            {
                id = author_aux.Id;
            }

            //creando Book
            Book book = new Book();
            book.Title = bookData.Title;
            //Añadiendo foránea
            book.AuthorId = id;
            _context.Book.Add(book);

            _context.BookData.Add(bookData);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }

        private bool BookDataExists(int id)
        {
          return (_context.BookData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
