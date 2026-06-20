namespace Comments;

public class X1
{
    public static int M(int q, int z)
    {
        int accumulatedSum = 0;

        for (int i = q; i <= z; i++)
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