namespace Comments;

public class RangeOfIntegers
{
    public RangeOfIntegers(int lower, int upper)
    {
        Lower = lower;
        Upper = upper;
    }

    private int Lower { get; }
    private int Upper { get; }

    public int SumSquares()
    {
        int accumulatedSum = 0;

        for (int i = Lower; i <= Upper; i++)
        {
            accumulatedSum += Square(i);
        }

        return accumulatedSum;
    }

    private static int Square(int k)
    {
        return k * k;
    }
}