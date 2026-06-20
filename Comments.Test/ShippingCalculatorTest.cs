using LegacyCode;

namespace Comments.Test;

[TestFixture]
[TestOf(typeof(ShippingCalculator))]
public class ShippingCalculatorTest {

    [TestCase("STANDARD",          5,   120,  2.5)]
    [TestCase("OVERNIGHT",         2,    50, 27.4)]
    [TestCase("EXPRESS",         8.5,   300, 36.8)]
    [TestCase("INTERNATIONAL",     2,    50,  3.0)]
    public void Order1001(string shippingType, double weightKg, double distanceKm, double expected)
    {
        Order order = new Order
        {
            ShippingType = shippingType,
            WeightKg = weightKg,
            DistanceKm = distanceKm
        };
        var actual = new EverythingShippingCalculator().Calculate(order);
        Assert.That(actual, Is.EqualTo(expected));
    }
}