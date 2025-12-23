using Biblioteka.Data;
using Biblioteka.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Services;

public class BooksService
{
    private readonly LibraryDbContext _db;
    public BooksService(LibraryDbContext db) => _db = db;

    public async Task<List<Book>> GetAllAsync(int? authorId)
    {
        var q = _db.Books.AsNoTracking().Include(b => b.Author).AsQueryable();
        if (authorId is not null)
            q = q.Where(b => b.AuthorId == authorId.Value);

        return await q.OrderBy(b => b.Id).ToListAsync();
    }

    public Task<Book?> GetByIdAsync(int id) =>
        _db.Books.AsNoTracking()
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<(bool ok, Book? book)> CreateAsync(string title, int year, int authorId)
    {
        var author = await _db.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
        if (author is null) return (false, null);

        var b = new Book { Title = title.Trim(), Year = year, AuthorId = authorId };
        _db.Books.Add(b);
        await _db.SaveChangesAsync();

        await _db.Entry(b).Reference(x => x.Author).LoadAsync();
        return (true, b);
    }

    public async Task<(bool found, bool authorOk)> UpdateAsync(int id, string title, int year, int authorId)
    {
        var b = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (b is null) return (false, true);

        var authorExists = await _db.Authors.AnyAsync(a => a.Id == authorId);
        if (!authorExists) return (true, false);

        b.Title = title.Trim();
        b.Year = year;
        b.AuthorId = authorId;
        await _db.SaveChangesAsync();
        return (true, true);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var b = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (b is null) return false;

        _db.Books.Remove(b);
        await _db.SaveChangesAsync();
        return true;
    }
}
