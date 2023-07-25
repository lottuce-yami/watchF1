using System.Text;
using F1Project.Data;
using F1Project.Data.AppSettings;
using F1Project.Data.Database.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Configuration.AddJsonFile("standingsDictionary.json", optional: false, reloadOnChange: true);
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new 
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWTKey")!))
    };
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDataProtection().SetApplicationName("watchF1");
builder.Services.Configure<BotOptions>(builder.Configuration.GetSection(BotOptions.Bot));
builder.Services.AddSingleton<SettingService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<VideoService>();
builder.Services.AddSingleton<ServerService>();
builder.Services.AddHttpClient<StandingsController>();
builder.Services.AddHttpClient<ScheduleController>();
builder.Services.AddHostedService<TimedHostedService>();
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