namespace AlgoSandbox;

public class Depth2
{
    public static int Solve(int[][] arrays)
    {
        return FindMax(arrays, new int[arrays.Length], arrays[0].Length);
    }

    private static int FindMax(int[][] arrays, int[] currentPositions, int currentLength)
    {
        if (currentLength == arrays[0].Length)
        {
            return arrays.Select((w, i) => w[currentPositions[i]]).Sum();
        }

        return Enumerable.Range(0, arrays.Length).Select(index => FindMax(arrays, Incr(currentPositions, index), currentLength++)).Max();
    }

    private static int[] Incr(int[] positions, int index)
    {
        var res = new int[positions.Length];
        positions.CopyTo(res.AsMemory());
        res[index]++;
        return res;
    }
}