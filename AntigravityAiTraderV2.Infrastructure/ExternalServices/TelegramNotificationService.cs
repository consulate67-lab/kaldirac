using AntigravityAiTraderV2.Application.Interfaces;
using AntigravityAiTraderV2.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Infrastructure.ExternalServices;

public class TelegramNotificationService : INotificationService
{
    private readonly HttpClient _httpClient;
    private readonly string _botToken;
    private readonly string _chatId;

    public TelegramNotificationService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _botToken = configuration["TelegramSettings:BotToken"] ?? string.Empty;
        _chatId = configuration["TelegramSettings:ChatId"] ?? string.Empty;
    }

    public async Task SendSignalNotificationAsync(Signal signal)
    {
        if (string.IsNullOrEmpty(_botToken) || string.IsNullOrEmpty(_chatId))
        {
            // Telegram settings not configured, just return.
            return;
        }

        var message = $"🚨 *Yeni Varant Sinyali* 🚨\n\n" +
                      $"*Sembol:* {signal.Symbol}\n" +
                      $"*Sinyal:* {signal.SignalType.ToString().ToUpper()}\n" +
                      $"*Güven:* %{signal.Confidence:F2}\n" +
                      $"*Zaman:* {signal.CreatedAt:yyyy-MM-dd HH:mm:ss} UTC";

        var url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={_chatId}&text={Uri.EscapeDataString(message)}&parse_mode=Markdown";

        try
        {
            await _httpClient.GetAsync(url);
        }
        catch (Exception)
        {
            // Handle exceptions in a real-world scenario (e.g. logging)
        }
    }
}
