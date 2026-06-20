namespace Comments;

public class Range
{
    public Range(int start, int end)
    {
        Start = start;
        End = end;
    }

    public int Start { get; }
    public int End { get; }
}

public class X1
{
    public static int M(Range range)
    {
        int accumulatedSum = 0;

        for (int i = range.Start; i <= range.End; i++)
        {
            // Add square of each number in the range
            accumulatedSum += Square(i);
        }

        return accumulatedSum;
    }

    private static int Square(int k)
    {
        return k * k;
    }
}