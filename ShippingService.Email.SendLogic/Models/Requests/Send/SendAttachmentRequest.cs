﻿namespace ShippingService.Email.SendLogic.Models.Requests.Send;

public class SendAttachmentRequest
{
    public string FileName { get; init; } = string.Empty;
    public string PublicUrl { get; init; } = string.Empty;
}