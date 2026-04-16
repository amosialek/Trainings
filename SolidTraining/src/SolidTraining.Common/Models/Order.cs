namespace SolidTraining.Common.Models;

public class Order
{
    public string Id { get; set; } = string.Empty;
    public Customer Customer { get; set; } = new();
    public List<OrderItem> Items { get; set; } = new();
    public PaymentDetails PaymentDetails { get; set; } = new();
    public string CustomerEmail { get; set; } = string.Empty;

    public decimal Total => Items.Sum(i => i.Price * i.Quantity);
}

public class OrderItem
{
    public string ProductId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class PaymentDetails
{
    public string CardNumber { get; set; } = string.Empty;
    public string CardHolderName { get; set; } = string.Empty;
    public string ExpiryDate { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
}

public class OrderResult
{
    public bool IsSuccess { get; private set; }
    public string OrderId { get; private set; } = string.Empty;
    public string Error { get; private set; } = string.Empty;

    public static OrderResult Completed(string orderId) =>
        new() { IsSuccess = true, OrderId = orderId };

    public static OrderResult Failed(string error) =>
        new() { IsSuccess = false, Error = error };
}

public class PaymentResult
{
    public bool Success { get; private set; }
    public string TransactionId { get; private set; } = string.Empty;
    public string Error { get; private set; } = string.Empty;

    public static PaymentResult Successful(string transactionId) =>
        new() { Success = true, TransactionId = transactionId };

    public static PaymentResult Failed(string error) =>
        new() { Success = false, Error = error };
}
