using System.ComponentModel;

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
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /*
            1ª)A empty list will be created to be constructed latter on 
            2ª) Loop i from 1 to count (inclusive).
                2.1) The loop will be : i=1 ; i<= lenght;i++ 
            3ª) On each iteration, compute startingNumber * i and add it to the array.
            4ª) Return the array.
            
        */
        var list = new List<double>();
                                                    
        for(int i=1; i <= length; i++)
        {
            var data = number * i;
            list.Add(data);
        }

        return list.ToArray();
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
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /*
            1) Find the cut point: split = data.Count - amount.
            Everything from `split` to the end is the "tail" and before `split` is the "head".

            2) Use GetRange to copy the two halves into separate lists:
            tail = data.GetRange(split, amount)   
            head = data.GetRange(0, split)        

            3) Clear the original list.

            4) Add the tail first, then the head:
            data.AddRange(tail);
            data.AddRange(head);        */

        int count = data.Count;
        int split = count-amount;

        List<int> tail = data.GetRange(split, amount);
        List<int> head = data.GetRange(0,split);

        data.Clear();
        data.AddRange(tail);
        data.AddRange(head);




    }
}
