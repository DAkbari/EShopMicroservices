using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
//Add Services

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("Fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5; // Allow 5 requests per window
    });
});

var app = builder.Build();
//Add Pipelines
app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
