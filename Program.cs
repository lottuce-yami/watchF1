using System.Text;
using F1Project.Data;
using F1Project.Data.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var jwtKey = builder.Configuration
    .GetSection(JwtOptions.Jwt)
    .Get<JwtOptions>()!.Key;
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new 
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddDbContext<WatchF1Context>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDataProtection().SetApplicationName("watchF1");

builder.Services.Configure<LiveOptions>(builder.Configuration.GetSection(LiveOptions.Live));
builder.Services.Configure<PricingOptions>(builder.Configuration.GetSection(PricingOptions.Pricing));
builder.Services.Configure<LinksOptions>(builder.Configuration.GetSection(LinksOptions.Links));
builder.Services.Configure<MiscOptions>(builder.Configuration.GetSection(MiscOptions.Misc));
builder.Services.Configure<BotOptions>(builder.Configuration.GetSection(BotOptions.Bot));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Jwt));

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseAuthentication();
app.UseAuthorization();

app.Run();