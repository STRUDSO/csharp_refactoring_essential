namespace Comments;

public class X1
{
    public static int M(int start, int end)
    {
        int accumulatedSum = 0;

        for (int i = start; i <= end; i++)
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