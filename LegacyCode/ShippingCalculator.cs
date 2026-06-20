namespace LegacyCode;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Order
{
    public int OrderId { get; set; }
    public string ShippingType { get; set; }
    public double WeightKg { get; set; }
    public double DistanceKm { get; set; }
    public bool Fragile { get; set; }
}

public interface ICalculateShipping
{
    double Calculate(Order order);
}

public class InternationalShipping : ICalculateShipping
{
    public double Calculate(Order order)
    {
        return order.WeightKg * 1.5;
    }
}

public class OverNight : ICalculateShipping
{
    public double Calculate(Order order)
    {
        return order.WeightKg * 1.2 + 25;
    }
}

public class ExpressCalc : ICalculateShipping
{
    public double Calculate(Order order)
    {
        return order.WeightKg * 0.8
               + order.DistanceKm * 0.1;
    }
}

public class StandardCal : ICalculateShipping
{
    public double Calculate(Order order)
    {
        return order.WeightKg * 0.5;
    }
}

public class EverythingShippingCalculator : ICalculateShipping
{
    public double Calculate(Order order)
    {
        return Calculator(order).Calculate(order);
    }

    private static ICalculateShipping Calculator(Order order)
    {
        ICalculateShipping calculateShipping = order.ShippingType switch
        {
            "STANDARD" => new StandardCal(),
            "EXPRESS" => new ExpressCalc(),
            "OVERNIGHT" => new OverNight(),
            "INTERNATIONAL" => new InternationalShipping(),
            _ => throw new Exception($"Unknown shipping type: {order.ShippingType}")
        };
        return calculateShipping;
    }
}

public class ShippingCalculator
{
    private readonly EverythingShippingCalculator _shippingCalculator = new();
    private readonly HttpClient _httpClient = new();

    public double CalculateShipping(int orderId)
    {
        try
        {
            var url = $"https://codemanship.co.uk/api/orders.php?orderId={orderId}";

            var response = _httpClient
                .GetAsync(url)
                .GetAwaiter()
                .GetResult();

            response.EnsureSuccessStatusCode();

            var json = response.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            var order = JsonSerializer.Deserialize<Order>(json, options);

            if (order == null)
                throw new Exception("Failed to deserialize order");
            
            return _shippingCalculator.Calculate(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return -1;
        }
    }
}

