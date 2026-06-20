namespace Comments.Test;

using NUnit.Framework;

[TestFixture]
public class RangeOfIntegersTests
{
    [Test]
    public void SumSquares()
    {
        int a = 7;
        int b = 12;

        // Expected: sum of squares from 7 to 12
        int expected = 0;
        for (int i = a; i <= b; i++)
        {
            expected += i * i;
        }

        int actual = new RangeOfIntegers(a, b).SumSquares();

        Assert.That(actual, Is.EqualTo(expected));
    }
}