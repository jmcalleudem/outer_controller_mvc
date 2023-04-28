using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using outer_controller_mvc.Models;

namespace outer_controller_mvc.Data
{
    public class outer_controller_mvcContext : DbContext
    {
        public outer_controller_mvcContext (DbContextOptions<outer_controller_mvcContext> options)
            : base(options)
        {
        }

        public DbSet<outer_controller_mvc.Models.BookData> BookData { get; set; } = default!;
        public DbSet<outer_controller_mvc.Models.Book> Book { get; set; } = default!;
        public DbSet<outer_controller_mvc.Models.Author> Author { get; set; } = default!;
    }
}
