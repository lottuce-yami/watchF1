namespace F1Project.Data.Options;

public class PricingOptions
{
    public const string Pricing = "Pricing";

    public double SubscriptionPrice { get; set; } = 150;
    public bool FreeMode { get; set; }
}