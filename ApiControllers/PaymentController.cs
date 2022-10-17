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
    private readonly HttpClient _httpClient = new();
    private readonly ILogger _logger;
    private int SubPrice { get; } = int.Parse(new SettingService().Get("subPrice").Value ?? "150");

    public PaymentController(ILogger<PaymentController> logger)
    {
        _logger = logger;
    }

    public class Payment
    {
        public class AmountModel
        {
            public long Amount { get; set; }
            public string Currency { get; set; } = null!;
        }

        public class CustomModel
        {
            public string? UserId { get; set; }
            public int Quantity { get; set; }
        }
        public string TransactionId { get; set; } = null!;
        public AmountModel Amount { get; set; } = null!;
        public CustomModel? Custom { get; set; }
        public string Status { get; set; } = null!;
    }

    public class Notification
    {
        public string Text { get; set; } = "";
        public string UserId { get; set; } = "";
        public int GrandPrixCount { get; set; }
    }
    
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    public IActionResult Index([FromBody] Payment payment)
    {
        if (Request.Headers.Authorization != $"Bearer {Auth}") return Unauthorized();
        if (payment.Status != "SUCCESS")
        {
            _logger.LogWarning(
                "[{id}] Payment rejected: status {s} != SUCCESS.", 
                payment.TransactionId, payment.Status);
            return Ok();
        }
        if (payment.Amount.Currency != "CURRENCY") 
        {
            _logger.LogWarning(
                "[{id}] Payment rejected: currency {c} != CURRENCY.", 
                payment.TransactionId, payment.Amount.Currency);
            return Ok();
        }
        if (payment.Custom?.UserId == null) 
        {
            _logger.LogWarning(
                "[{id}] Payment rejected: no user ID.", 
                payment.TransactionId);
            return Ok();
        }
        if (payment.Amount.Amount < payment.Custom.Quantity * SubPrice * 100) 
        {
            _logger.LogWarning(
                "[{id}] Payment rejected: partial amount. Quantity: {q}, amount: {a} {c}.", 
                payment.TransactionId, payment.Custom.Quantity, payment.Amount.Amount, payment.Amount.Currency);
            return Ok();
        }

        var message = string.Format(
            "ID: {0} - Доступ выдан.\n(<b>{1}</b> Гран При, <b>{2}</b> у.е.)\n<pre>{3}</pre>",
            payment.Custom.UserId,
            payment.Custom.Quantity,
            $"{payment.Amount.Amount / 100}.{payment.Amount.Amount % 100}",
            payment.TransactionId
            );
        var notification = new Notification
        {
            Text = message,
            UserId = payment.Custom.UserId,
            GrandPrixCount = payment.Custom.Quantity
        };

        try
        {
            UserService.Subscribe(payment.Custom.UserId, payment.Custom.Quantity);
        }
        catch (Exception e)
        {
            _logger.LogError(
                "[DB] Subscription for {id}: {e}", 
                payment.Custom.UserId, e.Message);
            return Ok();
        }
        
        try
        {
            _httpClient.PostAsJsonAsync(BotUrl, notification);
        }
        catch (Exception e)
        {
            _logger.LogError(
                "[Bot] Subscription for {id}: {e}", 
                payment.Custom.UserId, e.Message);
            return Ok();
        }
        
        return Ok();
    }
}