namespace SortierAlgorithmen;

public static class Rearrange
{
    // decreasing array order using CompareTo() method
    private static T[] ReverseSort<T>(this T[] array, int maxIndex) where T : IComparable<T>, IComparable
    {
        for (int i = 0; i < maxIndex / 2; i++)
        {
            (array[i], array[maxIndex - i - 1]) = (array[maxIndex - i - 1], array[i]);
        }
        
        return array;
    }

    // rearrange array with first largest number, smallest number, 2nd largest number, 2nd smallest number, and so on
    private static T[] Zigzag<T>(this T[] array, int maxIndex) where T : IComparable
    {
        var tempArray = new T[maxIndex];
        int index = 0;
        
        // i and j are the indices of the first and last element of the array
        // i and j are incremented and decremented respectively
        for (int i = 0, j = maxIndex - 1;  
             i <= maxIndex / 2
             || j > maxIndex / 2; 
             i++, j--) 
        {
            if (index < maxIndex)
            {
                tempArray[index] = array[i]; // copy the first element
                index++;
            }
            if (index < maxIndex)
            {
                tempArray[index] = array[j]; // copy the last element
                index++;
            }
        }

        // copy the temp array back to the original array
        for (int i = 0; i < maxIndex; i++)
            array[i] = tempArray[i];
        
        return array;
    }
    
    public static T[] Zigzag<T>(this T[] array) where T : IComparable
    {
        return array.Zigzag(array.Length);
    }
    
    public static T[] ReverseSort<T>(this T[] array) where T : IComparable, IComparable<T>
    {
        return array.ReverseSort(array.Length);
    }
}