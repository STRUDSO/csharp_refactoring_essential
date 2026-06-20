namespace Comments.Test;

using NUnit.Framework;

[TestFixture]
public class RangeOfIntegersTests
{
    [Test]
    public void SumSquares()
    {
        const int lower = 7;
        const int upper = 12;

        // Expected: sum of squares from 7 to 12
        var expected = 0;
        for (var i = lower; i <= upper; i++)
        {
            expected += i * i;
        }

        var actual = new RangeOfIntegers(lower, upper).SumSquares();

        Assert.That(actual, Is.EqualTo(expected));
    }
}