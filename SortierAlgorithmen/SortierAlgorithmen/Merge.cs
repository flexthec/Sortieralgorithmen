namespace SortierAlgorithmen;

public static class Merge
{
    public static void DoMerge<T>(this T[] array, int lowIndex, int middleIndex, int maxIndex) where T : IComparable
    {
        var rightIndex = middleIndex - lowIndex + 1;
        var leftIndex = maxIndex - middleIndex;
        
        var leftArray = new T[rightIndex];
        var rightArray = new T[leftIndex];
        
        for (int x = 0; x < rightIndex; x++)
            leftArray[x] = array[lowIndex + x]; // copy left half
        
        for (int y = 0; y < leftIndex; y++)
            rightArray[y] = array[middleIndex + 1 + y]; // copy right half
        
        int i = 0;
        int j = 0;
        int k = lowIndex;
        
        // merge the temp arrays back into the original array
        while (i < rightIndex && j < leftIndex)
        {
            array[k] = leftArray[i].CompareTo(rightArray[j]) <= 0  
                ? leftArray[i++] 
                : rightArray[j++]; 

            k++;
        }
        
        // copy remaining elements of left subarray, if any
        while (i < rightIndex) 
            array[k++] = leftArray[i++];

        while (j < leftIndex)
            array[k++] = rightArray[j++];
    }

    private static T[] Sort<T>(this T[] array, int lowIndex, int highIndex) where T : IComparable
    {
        if (lowIndex >= highIndex) return array;
        
        var middleIndex = (lowIndex + highIndex) / 2;
        array.Sort(lowIndex, middleIndex);
        array.Sort(middleIndex + 1, highIndex);
        array.DoMerge(lowIndex, middleIndex, highIndex); 
        
        return array;
    }

    public static T[] MergeSort<T>(this T[] array) where T : IComparable
    {
        return array.Sort(0, array.Length - 1);
    }
}