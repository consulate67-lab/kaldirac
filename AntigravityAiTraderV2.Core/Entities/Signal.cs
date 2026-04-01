using AntigravityAiTraderV2.Core.Enums;
using System;

namespace AntigravityAiTraderV2.Core.Entities;

public class Signal
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public SignalType SignalType { get; set; }
    public decimal Confidence { get; set; }
    public DateTime CreatedAt { get; set; }
}
