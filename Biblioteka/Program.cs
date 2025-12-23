using Biblioteka.Data;
using Biblioteka.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// REST controllers
builder.Services.AddControllers();

// EF Core - SQLite
builder.Services.AddDbContext<LibraryDbContext>(opt =>
    opt.UseSqlite("Data Source=library.db"));

// Services (jeœli masz AuthorsService i BooksService w folderze Services)
builder.Services.AddScoped<AuthorsService>();
builder.Services.AddScoped<BooksService>();

var app = builder.Build();

// Automatyczne migracje przy starcie (tworzy bazê / aktualizuje schemat)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    db.Database.Migrate();
}

// Serwowanie testów z wwwroot (index.html + main.js)
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();

// czasem przydaje siê do testów integracyjnych
public partial class Program { }

