using LegacyCode;

namespace Comments.Test;

[TestFixture]
[TestOf(typeof(ShippingCalculator))]
public class ShippingCalculatorTest
{

    [Test]
    public void ZeroOrderZeroShipping()
    {
        var actual = ShippingCalculator.TestableShipping(new Order
        {
            ShippingType = "STANDARD"
        });
        Assert.That(actual, Is.EqualTo(0));
    }
}