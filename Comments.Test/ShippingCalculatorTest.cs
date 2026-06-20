using LegacyCode;

namespace Comments.Test;

[TestFixture]
[TestOf(typeof(ShippingCalculator))]
public class ShippingCalculatorTest
{

    [Test]
    public void Order1001()
    {
        var actual = ShippingCalculator.TestableShipping(new Order
        {
            ShippingType = "STANDARD",
            WeightKg = 5
        });
        Assert.That(actual, Is.EqualTo(2.5));
    }
}