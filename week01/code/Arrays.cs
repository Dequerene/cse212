public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number'
    /// followed by multiples of 'number'. For example, MultiplesOf(7, 5)
    /// will result in: {7, 14, 21, 28, 35}.
    /// Assume that length is a positive integer greater than 0.
    /// </summary>
    /// <returns>
    /// Array of doubles that are the multiples of the supplied number.
    /// </returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Plan:
        // 1. Create a new double array with the size provided by length.
        // 2. Use a loop to visit every index in the array.
        // 3. Calculate each multiple by multiplying number by index + 1.
        //    We add 1 because array indexes begin at 0, but the first
        //    value should be number multiplied by 1.
        // 4. Store each calculated multiple in its corresponding index.
        // 5. Return the completed array.

        double[] multiples = new double[length];

        for (int index = 0; index < length; index++)
        {
            multiples[index] = number * (index + 1);
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'. For example, if the data is
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3, then the list
    /// after the function runs should be
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.
    /// The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data
    /// list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Plan:
        // 1. Find the index where the rotated list should begin by subtracting
        //    the rotation amount from the number of items in the list.
        // 2. Copy the items from that index to the end into a temporary list.
        //    These are the items that must move to the beginning.
        // 3. Copy the items before the starting index into another temporary list.
        //    These items will move to the end.
        // 4. Clear the original list.
        // 5. Add the items from the end section first.
        // 6. Add the original beginning section after them.
        // 7. The original list is now rotated to the right by the given amount.

        int startIndex = data.Count - amount;

        List<int> endSection = data.GetRange(startIndex, amount);
        List<int> beginningSection = data.GetRange(0, startIndex);

        data.Clear();
        data.AddRange(endSection);
        data.AddRange(beginningSection);
    }
}