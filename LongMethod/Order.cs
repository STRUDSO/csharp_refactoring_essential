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
}

public class Order
{
    private readonly List<OrderItem> _items;
    private readonly Customer _customer;

    public Order(OrderItems orderItems, Customer customer)
    {
        var items = orderItems.Items;
        _items = items;
        _customer = customer;
    }

    public OrderSummary Summarise()
    {
        // Validation
        ValidateItems();

        // Subtotal calculation
        var subtotal = CalculateSubtotal();

        // Discount rules
        var discount = CalculateDiscount(subtotal);

        // Tax calculation
        var taxableAmount = CalculateTaxableAmount(subtotal, discount, out var tax);

        // Total calculation
        var total = CalculateTotal(taxableAmount, tax);

        return new OrderSummary(subtotal, discount, tax, total);
    }

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

    private double CalculateDiscount(double subtotal)
    {
        double discount = 0.0;
        if (_customer.IsLoyal)
        {
            discount = subtotal * 0.10;
        }
        else if (subtotal > 100)
        {
            discount = subtotal * 0.05;
        }

        return discount;
    }

    private double CalculateSubtotal()
    {
        double subtotal = 0.0;
        foreach (var item in _items)
        {
            subtotal += item.Price * item.Quantity;
        }

        return subtotal;
    }

    private void ValidateItems()
    {
        if (_items == null)
        {
            throw new InvalidOperationException("Items cannot be null");
        }

        if (_items.Count == 0)
        {
            throw new InvalidOperationException("Order must contain items");
        }
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