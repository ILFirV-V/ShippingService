using ShippingService.Email.Core.Common;
using ShippingService.Email.SendLogic.Models.DTO.SendModels;

namespace ShippingService.Email.SendLogic.Interfaces.Services;

public interface ISendService
{
    public Task<OperationResult> Send(SendMessage emailMessage);
    public Task<OperationResult> SendBulk(IEnumerable<SendMessage> messages);
}