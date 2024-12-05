namespace ShippingService.Email.Routes;

public static class Routes
{
    private const string IdPlaceholder = $"{{{"id:guid"}}}";
    
    public const string Send = $"email/{IdPlaceholder}/send";
    public const string SendMessages = $"{Send}/messages";
    public const string SendMessagesWithDelay = $"{Send}/messages/delay";
    public const string SendMessagesWithDate = $"{Send}/messages/date";
    
    public const string Login = "login";
}