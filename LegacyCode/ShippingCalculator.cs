namespace LegacyCode;

using System;


public interface ICalculateShipping
{
    double Calculate(OrderData orderData);
}

public class InternationalShipping : ICalculateShipping
{
    public double Calculate(OrderData orderData)
    {
        return orderData.WeightKg * 1.5;
    }
}

public class OverNightShipping : ICalculateShipping
{
    public double Calculate(OrderData orderData)
    {
        return orderData.WeightKg * 1.2 + 25;
    }
}

public class ExpressShipping : ICalculateShipping
{
    public double Calculate(OrderData orderData)
    {
        return orderData.WeightKg * 0.8
               + orderData.DistanceKm * 0.1;
    }
}

public class StandardShipping : ICalculateShipping
{
    public double Calculate(OrderData orderData)
    {
        return orderData.WeightKg * 0.5;
    }
}

public class EverythingShippingCalculator : ICalculateShipping
{
    public double Calculate(OrderData orderData)
    {
        return Create(orderData).Calculate(orderData);
    }

    private static ICalculateShipping Create(OrderData orderData)
    {
        return orderData.ShippingType switch
        {
            "STANDARD" => new StandardShipping(),
            "EXPRESS" => new ExpressShipping(),
            "OVERNIGHT" => new OverNightShipping(),
            "INTERNATIONAL" => new InternationalShipping(),
            _ => throw new Exception($"Unknown shipping type: {orderData.ShippingType}")
        };
    }
}

public class ShippingCalculator
{
    private readonly EverythingShippingCalculator _shippingCalculator = new();
    public readonly HttpClient _httpClient = new();
    private readonly Order _order;

    public ShippingCalculator()
    {
        _order = new Order(_httpClient);
    }

    public double CalculateShipping(int orderId)
    {
        try
        {
            var orderData = _order.GetOrder(orderId);

            if (orderData == null)
                throw new Exception("Failed to deserialize order");
            
            return _shippingCalculator.Calculate(orderData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return -1;
        }
    }
}

