using Microsoft.AspNetCore.HttpOverrides;
using OciDistributionProxy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHealthChecks("/healthz");

// For Google Cloud Run headers.
var forwardHeaderOpts = new ForwardedHeadersOptions();
forwardHeaderOpts.ForwardLimit = 1;
forwardHeaderOpts.KnownProxies.Add(System.Net.IPAddress.Parse("169.254.1.1"));
forwardHeaderOpts.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
app.UseForwardedHeaders(forwardHeaderOpts);

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return Results.Text(OciDistributionProxy.Properties.Resources.Index_html, "text/html");
});

app.MapGroup("/v2/").MapRegsitryApi();

var portStr = Environment.GetEnvironmentVariable("PORT");
if (string.IsNullOrEmpty(portStr))
{
    app.Run();
}
else
{
    int port = int.Parse(portStr, System.Globalization.CultureInfo.InvariantCulture);
    app.Run($"http://0.0.0.0:{port}");
}
