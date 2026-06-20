// See https://aka.ms/new-console-template for more information

using System;
using LegacyCode;

public class ShippingApp
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: ShippingApp <orderId>");
            return;
        }

        if (!int.TryParse(args[0], out int orderId))
        {
            Console.WriteLine("Invalid orderId. Must be an integer.");
            return;
        }

        var httpClient = new System.Net.Http.HttpClient();
        var order = new Order(httpClient);

        try
        {
            OrderData? orderData = order.GetOrder(orderId);

            if (orderData == null)
                throw new Exception("Failed to fetch order");

            var calculator = new EverythingShippingCalculator();
            double cost = calculator.Calculate(orderData);

            Console.WriteLine($"Order ID: {orderId}");
            Console.WriteLine($"Shipping cost: {cost}");
        }
        catch (Exception e)
        {
            Consol.WriteLine($"Failed to calculate shipping for order {orderId}");
            Console.WriteLine(e);
        }
    }
}