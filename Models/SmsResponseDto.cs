using System.Text.Json.Serialization;

namespace SmsVideoDemo.Models;

public record SmsResponseDto
{
    [JsonPropertyName("subid")]
    public string SubId { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    public bool IsSuccess => Code == "0";
}
