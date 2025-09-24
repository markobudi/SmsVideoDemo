using Microsoft.Extensions.Options;
using SmsVideoDemo.Configurations;
using SmsVideoDemo.Interfaces;
using SmsVideoDemo.Models;
using System.Text;
using System.Text.Json;

namespace SmsVideoDemo.Services;

public class SmsService : ISmsService
{
    private readonly LabsMobileOptions _options;
    private readonly HttpClient _httpClient;

    public SmsService(
        IOptions<LabsMobileOptions> options,
        IHttpClientFactory httpClientFactory)
    {
        _options = options.Value;
        _httpClient = httpClientFactory.CreateClient("LabsMobile");
    }

    public async Task<SmsResponseDto> SendSmsMessageAsync(string phoneNumber, string message)
    {
        try
        {
            // Basic Auth
            var credentials = $"{_options.User}:{_options.ApiKey}";
            var credentialsEncoding = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentialsEncoding);

            // Payload
            var payload = new
            {
                message,
                recipient = new[] { new { msisdn = phoneNumber } }
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Request
            var response = await _httpClient.PostAsync(_options.BasePath, content);
            var body = await response.Content.ReadAsStringAsync();

            SmsResponseDto smsResponse;

            if (!string.IsNullOrWhiteSpace(body))
            {
                smsResponse = JsonSerializer.Deserialize<SmsResponseDto>(body);
            }
            else
            {
                smsResponse = new SmsResponseDto
                {
                    Code = "-1",
                    Message = "Empty response from LabsMobile"
                };
            }

            return smsResponse;
        }
        catch (Exception ex)
        {
            return new SmsResponseDto
            {
                Code = "-1",
                Message = $"Exception: {ex.Message}"
            };
        }
    }
}
