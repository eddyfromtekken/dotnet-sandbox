// See https://aka.ms/new-console-template for more information

using AlgoSandbox;

// int[][] arr = [[0], [1], [2]];
int[][] arr = [[0,5], [1,3], [2,2]];

Console.WriteLine(Depth.Solve(arr));


public class Depth
{
    public static int Solve(int[][] arrays)
    {
        return FindMax(arrays, new int[arrays.Length], 0);
    }

    private static int FindMax(int[][] arrays, int[] currentPositions, int currentLength)
    {
        if (currentLength == arrays[0].Length)
        {
            return arrays.Select((w, i) => currentPositions[i] > 0 ? w[currentPositions[i] - 1] : 0).Sum();
        }

        var nextLength = currentLength + 1;
        return Enumerable.Range(0, arrays.Length).Select(index => FindMax(arrays, Incr(currentPositions, index), nextLength)).Max();
    }

    private static int[] Incr(int[] positions, int index)
    {
        var res = new int[positions.Length];
        positions.CopyTo(res.AsMemory());
        res[index]++;
        return res;
    }
}