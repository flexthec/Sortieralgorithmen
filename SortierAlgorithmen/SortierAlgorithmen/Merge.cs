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
        
        int i = 0; // index of left subarray
        int j = 0; // initial index of left and right subarrays
        int k = lowIndex; // initial index of merged subarray
        
        while (i < rightIndex && j < leftIndex)
        {
            array[k] = leftArray[i].CompareTo(rightArray[j]) <= 0  // compare left and right subarray
                ? leftArray[i++] // if left subarray is smaller, copy it to merged subarray
                : rightArray[j++]; // if right subarray is smaller, copy it to merged subarray

            k++;
        }
        
        while (i < rightIndex) // copy remaining elements of left subarray
            array[k++] = leftArray[i++];

        while (j < leftIndex) // copy remaining elements of right subarray
            array[k++] = rightArray[j++];
    }

    private static T[] Sort<T>(this T[] array, int lowIndex, int highIndex) where T : IComparable
    {
        if (lowIndex >= highIndex) return array; // if array has only one element, return it
        
        var middleIndex = (lowIndex + highIndex) / 2; // calculate middle index
        array.Sort(lowIndex, middleIndex); // sort first half of the array
        array.Sort(middleIndex + 1, highIndex); // sort second half
        array.DoMerge(lowIndex, middleIndex, highIndex); // merge both halves;
        
        return array;
    }

    public static T[] MergeSort<T>(this T[] array) where T : IComparable
    {
        return array.Sort(0, array.Length - 1);
    }
}