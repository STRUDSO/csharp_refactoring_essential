namespace Comments;

public class X1
{
    public static int M(int q, int z)
    {
        int p = 0;

        for (int i = q; i <= z; i++)
        {
            // Add square of each number in the range
            p += N(i);
        }

        // Return accumulated sum
        return p;
    }

    static int N(int k)
    {
        // Return square of input
        return k * k;
    }
}