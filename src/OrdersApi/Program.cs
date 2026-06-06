using Scalar.AspNetCore;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHttpClient("PaymentsApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PaymentsApi:BaseUrl"] ?? "http://paymentsapi:8080");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/orders", async (IHttpClientFactory httpClientFactory) =>
{
    var orders = GetOrders();
    var paymentsClient = httpClientFactory.CreateClient("PaymentsApi");
    var response = new List<OrderResponse>();

    foreach (var order in orders)
    {
        var payment = await paymentsClient.GetFromJsonAsync<PaymentInfo>($"/payments/{order.Id}")
            ?? new PaymentInfo(order.Id, "Unknown", "Unknown", 0);

        response.Add(new OrderResponse(order.Id, order.CustomerName, order.Total, payment));
    }

    return Results.Ok(response);
});

app.Run();

static List<Order> GetOrders() =>
[
    new(1, "AliceV4", 25),
    new(2, "BobV4", 40)
];

record Order(int Id, string CustomerName, decimal Total);
record PaymentInfo(int OrderId, string Status, string Method, decimal Amount);
record OrderResponse(int Id, string CustomerName, decimal Total, PaymentInfo Payment);
