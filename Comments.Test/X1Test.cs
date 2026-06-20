namespace Comments.Test;

using NUnit.Framework;

[TestFixture]
public class X1Test
{
    [Test]
    public void T1()
    {
        int a = 7;
        int b = 12;

        // Expected: sum of squares from 7 to 12
        int expected = 0;
        for (int i = a; i <= b; i++)
        {
            expected += i * i;
        }

        int actual = X1.SumSquaresOf(new Range(a, b));

        Assert.That(actual, Is.EqualTo(expected));
    }
}