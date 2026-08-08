using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it. Remember to both express the solution
    /// in terms of recursive call on a smaller problem and
    /// to identify a base case (terminating case). If the value of
    /// n <= 0, just return 0. A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case
        if (n <= 0)
        {
            return 0;
        }

        // Recursive case
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.
    /// </summary>
    public static void PermutationsChoose(
        List<string> results,
        string letters,
        int size,
        string word = "")
    {
        // Base case: the word has reached the requested size.
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Try each remaining letter.
        for (int i = 0; i < letters.Length; i++)
        {
            char selectedLetter = letters[i];

            // Remove the selected letter from the available letters.
            string remainingLetters =
                letters[..i] + letters[(i + 1)..];

            // Continue building the permutation.
            PermutationsChoose(
                results,
                remainingLetters,
                size,
                word + selectedLetter
            );
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Count how many ways there are to climb a staircase when
    /// a person can move 1, 2, or 3 stairs at a time.
    /// Uses memoization for large values.
    /// </summary>
    public static decimal CountWaysToClimb(
        int s,
        Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;

        if (s == 1)
            return 1;

        if (s == 2)
            return 2;

        if (s == 3)
            return 4;

        // Create the memoization dictionary on the first call.
        remember ??= new Dictionary<int, decimal>();

        // If we already calculated this value, return it.
        if (remember.TryGetValue(s, out decimal rememberedValue))
        {
            return rememberedValue;
        }

        // Recursive solution using the same dictionary.
        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        // Save the answer for future recursive calls.
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Using recursion, insert all possible binary strings for a
    /// given wildcard pattern into the results list.
    /// </summary>
    public static void WildcardBinary(
        string pattern,
        List<string> results)
    {
        // Find the first wildcard.
        int wildcardIndex = pattern.IndexOf('*');

        // Base case: no wildcards remain.
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace the wildcard with 0.
        string patternWithZero =
            pattern[..wildcardIndex] +
            "0" +
            pattern[(wildcardIndex + 1)..];

        // Replace the wildcard with 1.
        string patternWithOne =
            pattern[..wildcardIndex] +
            "1" +
            pattern[(wildcardIndex + 1)..];

        // Recursively process both possibilities.
        WildcardBinary(patternWithZero, results);
        WildcardBinary(patternWithOne, results);
    }

    /// <summary>
    /// #############
    /// # Problem 5 #
    /// #############
    /// Use recursion to insert all paths that start at (0,0)
    /// and end at the end square into the results list.
    /// </summary>
    public static void SolveMaze(
        List<string> results,
        Maze maze,
        int x = 0,
        int y = 0,
        List<ValueTuple<int, int>>? currPath = null)
    {
        // Initialize the path during the first call.
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Add the current location to the path.
        currPath.Add((x, y));

        // Base case: we found the end of the maze.
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());

            // Backtrack before returning.
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Move left.
        if (maze.IsValidMove(currPath, x - 1, y))
        {
            SolveMaze(
                results,
                maze,
                x - 1,
                y,
                currPath
            );
        }

        // Move right.
        if (maze.IsValidMove(currPath, x + 1, y))
        {
            SolveMaze(
                results,
                maze,
                x + 1,
                y,
                currPath
            );
        }

        // Move up.
        if (maze.IsValidMove(currPath, x, y - 1))
        {
            SolveMaze(
                results,
                maze,
                x,
                y - 1,
                currPath
            );
        }

        // Move down.
        if (maze.IsValidMove(currPath, x, y + 1))
        {
            SolveMaze(
                results,
                maze,
                x,
                y + 1,
                currPath
            );
        }

        // Backtrack so other paths can be explored.
        currPath.RemoveAt(currPath.Count - 1);
    }
}