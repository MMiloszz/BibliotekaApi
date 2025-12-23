namespace Biblioteka.Domain.Entities;

public class Copy
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book? Book { get; set; }
}
