using Biblioteka.Domain.Entities;
using Biblioteka.Dtos;

namespace Biblioteka.Mappings;

public static class MappingExtensions
{
    public static AuthorResponseDto ToDto(this Author a) => new()
    {
        Id = a.Id,
        FirstName = a.FirstName,
        LastName = a.LastName
    };

    public static BookResponseDto ToDto(this Book b) => new()
    {
        Id = b.Id,
        Title = b.Title,
        Year = b.Year,
        Author = b.Author!.ToDto()
    };
}
