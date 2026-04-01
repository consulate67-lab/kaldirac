using AntigravityAiTraderV2.Core.Entities;
using System.Threading.Tasks;

namespace AntigravityAiTraderV2.Application.Interfaces;

public interface INotificationService
{
    Task SendSignalNotificationAsync(Signal signal);
}
