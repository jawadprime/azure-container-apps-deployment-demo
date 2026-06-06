using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/payments/{orderId:int}", (int orderId) =>
{
    var paid = orderId % 2 == 0;

    return Results.Ok(new PaymentInfo(
        orderId,
        paid ? "Paid" : "Pending",
        paid ? "Card" : "Cash",
        paid ? 40 : 0));
});

app.Run();

record PaymentInfo(int OrderId, string Status, string Method, decimal Amount);
