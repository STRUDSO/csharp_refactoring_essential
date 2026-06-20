using LegacyCode;

namespace Comments.Test;

[TestFixture]
[TestOf(typeof(ShippingCalculator))]
public class ShippingCalculatorTest
{

    [TestCase("STANDARD", 5, 2.5)]
    public void Order1001(string shippingType, double weightKg, double expected)
    {
        var actual = ShippingCalculator.TestableShipping(new Order
        {
            ShippingType = shippingType,
            WeightKg = weightKg
        });
        Assert.That(actual, Is.EqualTo(expected));
    }
}