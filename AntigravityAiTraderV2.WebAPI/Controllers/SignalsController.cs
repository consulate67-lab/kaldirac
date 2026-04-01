using AntigravityAiTraderV2.Application.Interfaces;
using AntigravityAiTraderV2.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SignalsController : ControllerBase
{
    private readonly ISignalRepository _signalRepository;

    public SignalsController(ISignalRepository signalRepository)
    {
        _signalRepository = signalRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetLatestSignals()
    {
        // 50 son sinyali getir (sayfalama için count parametrik alınabilir)
        var signals = await _signalRepository.GetLatestSignalsAsync(50);
        return Ok(signals);
    }
}
