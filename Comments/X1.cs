namespace Comments;

public class Range
{
    public Range(int start, int end)
    {
        Start = start;
        End = end;
    }

    private int Start { get; }
    private int End { get; }

    public int SumSquaresOf()
    {
        int accumulatedSum = 0;

        for (int i = Start; i <= End; i++)
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