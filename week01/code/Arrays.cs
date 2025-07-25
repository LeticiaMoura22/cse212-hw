public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // 1. Create an array of size 'length'.
        // 2. Use a for loop to fill the array.
        // 3. At each index i, store number * (i + 1).
        //    Example: number = 3, length = 5 → {3, 6, 9, 12, 15}
        // 4. Return the filled array.

        double[] result = new double[length]; //  1

        for (int i = 0; i < length; i++)      //  2
        {
            result[i] = number * (i + 1);     //  3
        }

        return result;                        //  4
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // 1. To rotate right by 'amount', we take the last 'amount' elements and move them to the front.
        // 2. Use GetRange to slice the list into two parts:
        //    - lastPart: the last 'amount' elements
        //    - firstPart: the rest of the list before that
        // 3. Clear the original list.
        // 4. Add the two parts back into the list in new order: lastPart + firstPart.

        int n = data.Count;

        //  2: Get the slices
        List<int> lastPart = data.GetRange(n - amount, amount);
        List<int> firstPart = data.GetRange(0, n - amount);

        //  3: Clear the list
        data.Clear();

        //  4: Rebuild the list in rotated order
        data.AddRange(lastPart);
        data.AddRange(firstPart);
    }
}
