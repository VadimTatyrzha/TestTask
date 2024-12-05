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
            return await _context.Authors.Include(b => b.Books)
                            .OrderByDescending(a => a.Books.Max(b => b.Title.Length))
                            .ThenBy(a => a.Id)
                            .FirstOrDefaultAsync();
        }
        public async Task<List<Author>> GetAuthors()
        {
            var minPublishDate = new DateTime(2015);
            return await _context.Authors.Include(a => a.Books)
                            .Where(a => a.Books.Count % 2 == 0 &&
                                        a.Books.Any(b => b.PublishDate >= minPublishDate))
                            .ToListAsync();
        }
    }
}
