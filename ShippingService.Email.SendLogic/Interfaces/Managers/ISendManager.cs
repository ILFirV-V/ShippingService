using ShippingService.Email.Core.Common;
using ShippingService.Email.SendLogic.Models.Requests.Send;

namespace ShippingService.Email.SendLogic.Interfaces.Managers;

public interface ISendManager
{
    public OperationResult SendMessagesPlan(IEnumerable<SendMessageRequest> messages, TimeSpan delay);
    public OperationResult SendMessagesPlan(IEnumerable<SendMessageRequest> messages, DateTimeOffset date);
    public Task<OperationResult> SendBulk(IEnumerable<SendMessageRequest> messages);
}