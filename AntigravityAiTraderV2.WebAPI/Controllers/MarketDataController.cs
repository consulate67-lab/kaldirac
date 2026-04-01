using AntigravityAiTraderV2.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketDataController : ControllerBase
{
    private readonly IMarketDataRepository _marketDataRepository;

    public MarketDataController(IMarketDataRepository marketDataRepository)
    {
        _marketDataRepository = marketDataRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMarketData([FromQuery] string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return BadRequest(new { Message = "Sembol belirtilmelidir." });
        }

        var data = await _marketDataRepository.GetLatestDataBySymbolAsync(symbol, 100);
        return Ok(data);
    }
}
