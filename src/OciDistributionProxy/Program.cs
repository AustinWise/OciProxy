using Microsoft.AspNetCore.HttpOverrides;
using OciDistributionProxy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHealthChecks("/healthz");

// For discovering the client's real IP address when running behind a proxy.
var forwardOpts = new ForwardedHeadersOptions();
forwardOpts.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
// We are not using the clients IP address for security purposes, just for optimizing bandwidth.
// By clearing these lists, we trust whatever clients send us.
forwardOpts.KnownNetworks.Clear();
forwardOpts.KnownProxies.Clear();
app.UseForwardedHeaders(forwardOpts);

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
