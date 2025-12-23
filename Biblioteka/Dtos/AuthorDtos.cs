using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Biblioteka.Dtos;

public class AuthorResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = "";

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = "";
}

public class AuthorCreateDto
{
    [Required, MinLength(1)]
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = "";

    [Required, MinLength(1)]
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = "";
}

public class AuthorUpdateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required, MinLength(1)]
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = "";

    [Required, MinLength(1)]
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = "";
}
