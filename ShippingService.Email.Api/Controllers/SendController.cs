using Microsoft.AspNetCore.Mvc;
using ShippingService.Email.Controllers.Base;
using ShippingService.Email.Core.Common;
using ShippingService.Email.Extensions;
using ShippingService.Email.SendLogic.Interfaces.Managers;
using ShippingService.Email.SendLogic.Models.Requests.Send;

namespace ShippingService.Email.Controllers;

public class SendController(ISendManager sendManager) : ApiControllerBase
{
    [HttpPost(Routes.Routes.SendMessages)]
    [ProducesDefaultResponseType(typeof(OperationResult))]
    [ProducesResponseType<OperationResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<OperationResult>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> SendTemplateMessages(IEnumerable<SendMessageRequest> sendMessages)
    {
        var sendResult = await sendManager.SendBulk(sendMessages);
        return sendResult.ToActionResult();
    } 
    
    [HttpPost(Routes.Routes.SendMessagesWithDelay)]
    [ProducesDefaultResponseType(typeof(OperationResult))]
    [ProducesResponseType<OperationResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<OperationResult>(StatusCodes.Status404NotFound)]
    public ActionResult SendTemplateMessagesWithSchedule(IEnumerable<SendMessageRequest> sendMessages, TimeSpan delay)
    {
        var sendResult = sendManager.SendMessagesPlan(sendMessages, delay);
        return sendResult.ToActionResult();
    } 
    
    [HttpPost(Routes.Routes.SendMessagesWithDate)]
    [ProducesDefaultResponseType(typeof(OperationResult))]
    [ProducesResponseType<OperationResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<OperationResult>(StatusCodes.Status404NotFound)]
    public ActionResult SendTemplateMessagesWithSchedule(IEnumerable<SendMessageRequest> sendMessages, DateTimeOffset date)
    {
        var sendResult = sendManager.SendMessagesPlan(sendMessages, date);
        return sendResult.ToActionResult();
    } 
}