namespace LongMethod;

using System;
using System.Collections.Generic;

public class OrderItems
{
    public OrderItems(List<OrderItem> items)
    {
        Items = items;
    }

    public List<OrderItem> Items { get; }

    public void ValidateItems()
    {
        if (Items == null)
        {
            throw new InvalidOperationException("Items cannot be null");
        }

        if (Items.Count == 0)
        {
            throw new InvalidOperationException("Order must contain items");
        }
    }

    public double CalculateSubtotal()
    {
        return Items.Sum(item => item.Price * item.Quantity);
    }
}

public class Order
{
    private readonly List<OrderItem> _items;
    private readonly OrderItems _orderItems;
    private readonly Customer _customer;

    public Order(OrderItems orderItems, Customer customer)
    {
        _orderItems = orderItems;
        _items = orderItems.Items;
        _orderItems = orderItems;
        _customer = customer;
    }

    public OrderSummary Summarise()
    {
        // Validation
        _orderItems.ValidateItems();

        // Subtotal calculation
        var subtotal = _orderItems.CalculateSubtotal();

        // Discount rules
        var discount = CustomerBasedDiscount(subtotal);

        // Tax calculation
        var taxableAmount = CalculateTaxableAmount(subtotal, discount, out var tax);

        // Total calculation
        var total = CalculateTotal(taxableAmount, tax);

        return new OrderSummary(subtotal, discount, tax, total);
    }

    private double CustomerBasedDiscount(double subtotal)
    {
        Func<double, double> discountStrategy;
        if(_customer.IsLoyal)
        {
            discountStrategy = CalculateLoyalDiscount;
        }
        else
        {
            discountStrategy = CalculateILoyalDiscount;
            
        }

        return discountStrategy(subtotal);
    }

    private double CalculateILoyalDiscount(double subtotal)
    {
        const double discountLowerLimit = 100;
        if (discountLowerLimit < subtotal)
        {
            return subtotal * 0.05;
        }

        return 0.0;
    }

    private double CalculateLoyalDiscount(double subtotal) => subtotal * 0.10;

    private static double CalculateTotal(double taxableAmount, double tax)
    {
        double total = taxableAmount + tax;
        return total;
    }

    private static double CalculateTaxableAmount(double subtotal, double discount, out double tax)
    {
        double taxableAmount = subtotal - discount;
        tax = taxableAmount * 0.20;
        return taxableAmount;
    }
}

public class Customer
{
    public bool IsLoyal { get; }

    public Customer(bool loyal)
    {
        IsLoyal = loyal;
    }
}

public class OrderItem
{
    public double Price { get; }
    public double Quantity { get; }

    public OrderItem(double price, double quantity)
    {
        Price = price;
        Quantity = quantity;
    }
}

public class OrderSummary
{
    public double Subtotal { get; }
    public double Discount { get; }
    public double Tax { get; }
    public double Total { get; }

    public OrderSummary(double subtotal, double discount, double tax, double total)
    {
        Subtotal = subtotal;
        Discount = discount;
        Tax = tax;
        Total = total;
    }
}