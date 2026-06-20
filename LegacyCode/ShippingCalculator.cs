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

public class ShippingCalculator : ICalculateShipping
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
