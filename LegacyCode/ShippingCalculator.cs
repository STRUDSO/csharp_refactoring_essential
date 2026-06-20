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

public class ShippingBla
{
    public static double Shipping_(Order order)
    {
        ICalculateShipping? calculateShipping = null;
        switch (order.ShippingType)
        {
            case "STANDARD":
                return order.WeightKg * 0.5;

            case "EXPRESS":
                calculateShipping = new ExpressCalc();
                break;
            case "OVERNIGHT":
                calculateShipping = new OverNight();
                break;
            case "INTERNATIONAL":
                calculateShipping = new InternationalShipping();
                break;
        }
        if(calculateShipping == null)
                throw new Exception($"Unknown shipping type: {order.ShippingType}");
        return calculateShipping.Calculate(order);
    }
}

public class ShippingCalculator
{
    private readonly HttpClient _httpClient = new HttpClient();

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
            
            return TestableShipping(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return -1;
        }
    }

    public static double TestableShipping(Order order)
    {
        return ShippingBla.Shipping_(order);
    }
}

