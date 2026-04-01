using AntigravityAiTraderV2.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyzeController : ControllerBase
{
    private readonly ISignalEngineService _signalEngineService;
    private readonly INotificationService _notificationService;

    public AnalyzeController(ISignalEngineService signalEngineService, INotificationService notificationService)
    {
        _signalEngineService = signalEngineService;
        _notificationService = notificationService;
    }

    [HttpPost]
    public async Task<IActionResult> AnalyzeSymbol([FromQuery] string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return BadRequest(new { Message = "Sembol belirtilmelidir." });
        }

        var signal = await _signalEngineService.AnalyzeAndGenerateSignalAsync(symbol);
        
        if (signal == null)
            return Ok(new { Message = $"{symbol} için analiz yapılamadı (yetersiz veri)." });

        // Bildirim gönder
        await _notificationService.SendSignalNotificationAsync(signal);

        return Ok(signal);
    }
}
