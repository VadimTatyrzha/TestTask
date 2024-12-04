using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;
        public AuthorService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Author> GetAuthor()
        {
            return await _context.Authors
                            .OrderByDescending(a => a.Books.Max(b => b.Title.Length))
                            //.OrderByDescending(a => a.Id)
                            .FirstOrDefaultAsync();
        }
        public async Task<List<Author>> GetAuthors()
        {
            var minPublishDate = new DateTime(2016, 01, 01);
            return await _context.Authors
                            .Where(a => a.Books.Count % 2 != 0 && 
                                        a.Books.All(b => b.PublishDate >= minPublishDate) &&
                                        a.Books != null)
                            .ToListAsync();
        }
    }
}
