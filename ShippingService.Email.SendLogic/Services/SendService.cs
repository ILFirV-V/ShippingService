using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using ShippingService.Email.Core.Common;
using ShippingService.Email.Domain.Entities.Settings;
using ShippingService.Email.SendLogic.Interfaces.Services;
using ShippingService.Email.SendLogic.Models.DTO.SendModels;

namespace ShippingService.Email.SendLogic.Services;

//todo: дописать проверки и обработку ошибок на OperationResult
public sealed class SendService(IOptions<EmailSettings> settingProvider) : ISendService
{
    //todo: добавить в di
    private readonly EmailSettings settings = settingProvider.Value;

    public async Task<OperationResult> Send(SendMessage emailMessage)
    {
        using var message = GetMimeMessage(emailMessage);
        using var client = new SmtpClient();
        await client.ConnectAsync(settings.Host, settings.Port);
        await client.AuthenticateAsync(settings.Address, settings.Password);
        message.To.Clear();
        message.To.Add(new MailboxAddress(string.Empty, emailMessage.Recipient));
        OperationResult result;
        try
        {
            await client.SendAsync(message);
            result = OperationResult.Success();
        }
        catch (Exception ex)
        {
            result = Error.BadRequest($"Ошибка при отправке сообщения пользователю {emailMessage.Recipient} " +
                                      $"с заголовком {emailMessage.Title} и телом {emailMessage.Content} {ex.Message}");
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
        return result;
    }

    public async Task<OperationResult> SendBulk(IEnumerable<SendMessage> messages)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(settings.Host, settings.Port);
        await client.AuthenticateAsync(settings.Address, settings.Password);
        foreach (var message in messages)
        {
            await SendSingleFromBulkAsync(message, client);
        }
        await client.DisconnectAsync(true);
        return OperationResult.Success();
    }

    private async Task SendSingleFromBulkAsync(SendMessage emailMessage, SmtpClient client)
    {
        using var message = GetMimeMessage(emailMessage);
        message.To.Clear();
        message.To.Add(new MailboxAddress(string.Empty, emailMessage.Recipient));
        try
        {
            await client.SendAsync(message);
        }
        catch (Exception ex)
        {
            //todo: дописать
        }
    }

    private MimeMessage GetMimeMessage(SendMessage emailMessage)
    {
        var message = new MimeMessage();

        var from = new MailboxAddress(settings.Name, settings.Address);
        message.From.Add(from);
        message.Subject = emailMessage.Title;

        var builder = new BodyBuilder
        {
            HtmlBody = emailMessage.Content
        };

        foreach (var attachment in emailMessage.Attachments)
        {
            builder.Attachments.Add(attachment.FileUrl);
        }

        message.Body = builder.ToMessageBody();
        return message;
    }
}