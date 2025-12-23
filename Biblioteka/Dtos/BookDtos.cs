using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Biblioteka.Dtos;

public class BookResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("author")]
    public AuthorResponseDto Author { get; set; } = new();
}

public class BookCreateDto
{
    [Required, MinLength(1)]
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [Range(0, int.MaxValue)]
    [JsonPropertyName("year")]
    public int Year { get; set; }

    [Range(1, int.MaxValue)]
    [JsonPropertyName("authorId")]
    public int AuthorId { get; set; }
}

public class BookUpdateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required, MinLength(1)]
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [Range(0, int.MaxValue)]
    [JsonPropertyName("year")]
    public int Year { get; set; }

    [Range(1, int.MaxValue)]
    [JsonPropertyName("authorId")]
    public int AuthorId { get; set; }
}
