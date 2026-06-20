namespace LongMethod.Test;

using System;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class OrderTests
{
    [Test]
    public void Summarise_CalculatesCorrectSummary_ForNonLoyalCustomer_UnderThreshold()
    {
        var order = new Order(new OrderItems(new List<OrderItem>
            {
                new OrderItem(10.0, 2), // 20
                new OrderItem(5.0, 2)   // 10
            }),
            new Customer(false));

        var summary = order.Summarise();

        Assert.That(summary.Subtotal, Is.EqualTo(30.0));
        Assert.That(summary.Discount, Is.EqualTo(0.0));
        Assert.That(summary.Tax, Is.EqualTo(6.0));
        Assert.That(summary.Total, Is.EqualTo(36.0));
    }

    [Test]
    public void Summarise_AppliesLoyalCustomerDiscount()
    {
        var order = new Order(new OrderItems(new List<OrderItem>
            {
                new OrderItem(50.0, 1)
            }),
            new Customer(true));

        var summary = order.Summarise();

        Assert.That(summary.Subtotal, Is.EqualTo(50.0));
        Assert.That(summary.Discount, Is.EqualTo(5.0));
        Assert.That(summary.Tax, Is.EqualTo(9.0));
        Assert.That(summary.Total, Is.EqualTo(54.0));
    }

    [Test]
    public void Summarise_AppliesBulkDiscount_ForNonLoyalCustomer_OverThreshold()
    {
        var order = new Order(new OrderItems(new List<OrderItem>
            {
                new OrderItem(120.0, 1)
            }),
            new Customer(false));

        var summary = order.Summarise();

        Assert.That(summary.Subtotal, Is.EqualTo(120.0));
        Assert.That(summary.Discount, Is.EqualTo(6.0));
        Assert.That(summary.Tax, Is.EqualTo(22.8));
        Assert.That(summary.Total, Is.EqualTo(136.8));
    }

    // -------------------------
    // Guard conditions
    // -------------------------

    [Test]
    public void Summarise_ThrowsException_WhenItemsIsNull()
    {
        var order = new Order(new OrderItems(null), new Customer(false));

        var ex = Assert.Throws<InvalidOperationException>(
            () => order.Summarise());

        Assert.That(ex!.Message, Is.EqualTo("Items cannot be null"));
    }

    [Test]
    public void Summarise_ThrowsException_WhenItemsIsEmpty()
    {
        var order = new Order(new OrderItems(new List<OrderItem>()),
            new Customer(false));

        var ex = Assert.Throws<InvalidOperationException>(
            () => order.Summarise());

        Assert.That(ex!.Message, Is.EqualTo("Order must contain items"));
    }

    // -------------------------
    // Boundary test
    // -------------------------

    [Test]
    public void Summarise_NoDiscount_WhenNonLoyalCustomer_AtThreshold()
    {
        var order = new Order(new OrderItems(new List<OrderItem>
            {
                new OrderItem(100.0, 1)
            }),
            new Customer(false));

        var summary = order.Summarise();

        Assert.That(summary.Subtotal, Is.EqualTo(100.0));
        Assert.That(summary.Discount, Is.EqualTo(0.0));
        Assert.That(summary.Tax, Is.EqualTo(20.0));
        Assert.That(summary.Total, Is.EqualTo(120.0));
    }
}