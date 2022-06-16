using System.Net.Mime;
using System.Text.Json.Serialization;
using F1Project.Data.Database.Services;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.ApiControllers;

[ApiController]
[Route("~/capusta/[controller]")]
public class PaymentController : ControllerBase
{
    private const string Auth = 
        "***REMOVED***";
    private const string BotUrl =
        "https://example.com/botBOT_TOKEN/subscription_notice";
    private HttpClient _httpClient = new();
    private int SubPrice { get; } = int.Parse(new SettingService().Get("subPrice").Value);

    public class Payment
    {
        public class AmountModel
        {
            [JsonPropertyName("amount")]
            public long Amount { get; set; }
            
            [JsonPropertyName("commission")]
            public long Commission { get; set; }
            
            [JsonPropertyName("currency")]
            public string Currency { get; set; } = null!;
        }

        public class CustomModel
        {
            [JsonPropertyName("userId")]
            public string? UserId { get; set; }
            
            [JsonPropertyName("quantity")]
            public int Quantity { get; set; }
        }

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; } = null!;
        
        [JsonPropertyName("amount")]
        public AmountModel Amount { get; set; } = null!;
        
        [JsonPropertyName("custom")]
        public CustomModel? Custom { get; set; }
        
        [JsonPropertyName("status")]
        public string Status { get; set; } = null!;
    }

    public class Notification
    {
        [JsonPropertyName("text")]
        public string Message { get; set; } = "";

        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = "";
    }
    
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    public IActionResult Index([FromBody] Payment payment)
    {
        if (Request.Headers.Authorization != $"Bearer {Auth}") return Unauthorized();
        if (payment.Status != "SUCCESS") return Ok();
        if (payment.Amount.Currency != "CURRENCY") return Ok();
        if (payment.Custom?.UserId == null) return Ok();
        if (payment.Amount.Amount < payment.Custom.Quantity * SubPrice) return Ok();

        var message = string.Format(
            "ID: {0} - Доступ выдан.\n(<b>{1}</b> Гран При, <b>{2}</b> у.е.)\n<pre>{3}</pre>",
            payment.Custom.UserId,
            payment.Custom.Quantity,
            $"{payment.Amount.Amount / 100}.{payment.Amount.Amount % 100}",
            payment.TransactionId
            );
        var notification = new Notification
        {
            Message = message,
            UserId = payment.Custom.UserId
        };

        try
        {
            UserService.Subscribe(payment.Custom.UserId, payment.Custom.Quantity);
            
        }
        catch
        {
            Console.WriteLine($"!- SUB ERROR -! ID: {payment.Custom.UserId} [{payment.TransactionId}]");
        }
        
        try
        {
            _httpClient.PostAsJsonAsync(BotUrl, notification);
        }
        catch
        {
            Console.WriteLine("!- BOT ERROR -!");
        }
        
        return Ok();
    }
}