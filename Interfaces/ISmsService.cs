using SmsVideoDemo.Models;

namespace SmsVideoDemo.Interfaces;

public interface ISmsService
{
    /// <summary>
    /// Send sms message to phone number.
    /// </summary>
    /// <param name="phoneNumber"> phone number to send message. </param>
    /// <param name="message"> message to send. </param>
    /// <returns>SmsResponseDto.</returns>
    Task<SmsResponseDto> SendSmsMessageAsync(string phoneNumber, string message);
}
