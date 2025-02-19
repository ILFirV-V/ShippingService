﻿using System.Collections.Frozen;
using ShippingService.Email.SendLogic.Models.DTO.SendModels;
using ShippingService.Email.SendLogic.Models.Requests.Send;

namespace ShippingService.Email.SendLogic.Extensions;

internal static class MapperExtensions
{
    public static IReadOnlyCollection<SendMessage> ToApplicationMessages(this IEnumerable<SendMessageRequest> requests)
    {
        return requests.Select(ToApplicationMessage).ToFrozenSet();
    }
    
    public static SendMessage ToApplicationMessage(this SendMessageRequest request)
    {
        var attachments = request.Attachments.ToApplicationAttachments();
        return new SendMessage()
        {
            Attachments = attachments,
            Title = request.Title,
            Content = request.Content,
            Recipient = request.Recipient
        };
    }

    public static IReadOnlyCollection<SendAttachment> ToApplicationAttachments(this IEnumerable<SendAttachmentRequest> requests)
    {
        return requests.Select(ToApplicationAttachment).ToFrozenSet();
    }

    public static SendAttachment ToApplicationAttachment(this SendAttachmentRequest request)
    {
        return new SendAttachment()
        {
            FileName = request.FileName,
            FileUrl =  request.PublicUrl,
        };
    }
}