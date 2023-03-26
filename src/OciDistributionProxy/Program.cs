using OciDistributionProxy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return Results.Text(OciDistributionProxy.Properties.Resources.Index_html, "text/html");
});

app.MapGroup("/v2/").MapRegsitryApi();

app.Run();
