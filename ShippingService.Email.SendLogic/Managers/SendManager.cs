using Hangfire;
using ShippingService.Email.Core.Common;
using ShippingService.Email.Domain.Entities.Settings;
using ShippingService.Email.SendLogic.Extensions;
using ShippingService.Email.SendLogic.Interfaces.Managers;
using ShippingService.Email.SendLogic.Interfaces.Services;
using ShippingService.Email.SendLogic.Models.DTO.SendModels;
using ShippingService.Email.SendLogic.Models.Requests.Send;

namespace ShippingService.Email.SendLogic.Managers;

public class SendManager(ISendService sendService) : ISendManager
{
    public OperationResult SendMessagesPlan(IEnumerable<SendMessageRequest> messages, TimeSpan delay)
    {
        var sendMessages = messages.ToApplicationMessages();
        var sendResult = ScheduleSend(sendMessages, delay);
        return sendResult;
    }

    public OperationResult SendMessagesPlan(IEnumerable<SendMessageRequest> messages, DateTimeOffset date)
    {
        var sendMessages = messages.ToApplicationMessages();
        var sendResult = ScheduleSend(sendMessages, date);
        return sendResult;
    }
    
    public async Task<OperationResult> SendBulk(IEnumerable<SendMessageRequest> messages)
    {
        var sendMessages = messages.ToApplicationMessages();
        var sendResult = await sendService.SendBulk(sendMessages);
        return sendResult;
    }
    
    public async Task<OperationResult> SendMessage(SendMessageRequest message)
    {
        var sendMessage = message.ToApplicationMessage();
        var sendResult = await sendService.Send(sendMessage);
        return sendResult;
    }
    
    protected virtual OperationResult ScheduleSend(IEnumerable<SendMessage> messages, TimeSpan delay)
    {
        try
        {
            BackgroundJob.Schedule(() => SendOrException(messages), delay);
            return OperationResult.Success();
        }
        catch (Exception e)
        {
            return Error.Internal($"Failed to schedule message with hangfire to send (TimeSpan): {e.Message}");
        }
    }

    protected virtual OperationResult ScheduleSend(IEnumerable<SendMessage> messages, DateTimeOffset date)
    {
        try
        {
            BackgroundJob.Schedule(() => SendOrException(messages), date);
            return OperationResult.Success();
        }
        catch (Exception e)
        {
            return Error.Internal($"Failed to schedule message with hangfire to send (DateTimeOffset): {e.Message}");
        }
    }
    
    // !!! Нельзя делать приватным !!! Hangfire иначе упадет
    public async Task SendOrException(IEnumerable<SendMessage> messages)
    {
        var sendResult = await sendService.SendBulk(messages);
        if (sendResult.IsFail)
        {
            throw new InvalidOperationException($"Failed to schedule message in send method: {sendResult.Error}");
        }
    }
}