using Biblioteka.Data;
using Biblioteka.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Services;

public class AuthorsService
{
    private readonly LibraryDbContext _db;
    public AuthorsService(LibraryDbContext db) => _db = db;

    public Task<List<Author>> GetAllAsync() =>
        _db.Authors.AsNoTracking().OrderBy(a => a.Id).ToListAsync();

    public Task<Author?> GetByIdAsync(int id) =>
        _db.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Author> CreateAsync(string first, string last)
    {
        var a = new Author { FirstName = first.Trim(), LastName = last.Trim() };
        _db.Authors.Add(a);
        await _db.SaveChangesAsync();
        return a;
    }

    public async Task<bool> UpdateAsync(int id, string first, string last)
    {
        var a = await _db.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (a is null) return false;

        a.FirstName = first.Trim();
        a.LastName = last.Trim();
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var a = await _db.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (a is null) return false;

        _db.Authors.Remove(a);
        await _db.SaveChangesAsync();
        return true;
    }
}

