using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        public BookService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Book> GetBook()
        {
            //var costOfPublishedEdition = 
            return await _context.Books.OrderBy(b => b.QuantityPublished * b.Price).ElementAtOrDefaultAsync(1);
        }
        public async Task<List<Book>> GetBooks()
        {
            // Carolus Rex release date — 2012 may 25.
            var carolusRexReleaseDate = new DateTime(2012, 05, 25);
            var keyWord = "red";
            return await _context.Books.Where(b => b.PublishDate > carolusRexReleaseDate && b.Title.ToLower().Contains(keyWord)).ToListAsync();
        }
    }
}
