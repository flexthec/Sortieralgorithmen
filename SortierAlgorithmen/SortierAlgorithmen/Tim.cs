namespace SortierAlgorithmen;

//--------------------------------------------------------------------------------------------------
// Divide the array into blocks known as runs. Sort runs using insertion sort.
// Merge runs using combine function in merge sort. If the size of the array is less than run,
// then array gets sorted just by using insertion sort. Note that the merge function performs well when
// size subarrays are powers of 2. The idea is based on the fact that insertion sort performs well for small arrays.
// The array is broken into blocks known as runs. Two runs are merged into a single run using combine function.
//
// *** Inventor of the TimSort Algorithm: Tim Peters ***
// *** Used in Java’s Arrays.sort() as well as Python’s sorted() and sort(). ***
//--------------------------------------------------------------------------------------------------
public static class Tim
{
    private const int RUN = 32;

    // Iterative Timsort function to sort the array[0...n-1] (similar to merge sort)
    private static T[] TimSort<T>(this T[] array, int maxIndex) where T : IComparable
    {
        // Sort individual subarrays of size RUN.
        for (int i = 0; i < maxIndex; i++)
        {
            array.InsertionSort(i, Math.Min(i + RUN - 1, maxIndex - 1));
        }
        
        // Start merging from size RUN (32). It will merge to form size 64, then 128, 256 and so on ....
        for (int size = RUN; size < maxIndex; size = 2 * size)
        {
            // Starting point at left subarray.After every merge, we increase left by 2*size
            for (int left = 0; left < maxIndex; left += 2 * size)
            {
                // Find ending point of left subarray. mid+1 is starting point of right subarray.
                int middle = left + size - 1;
                int right = Math.Min(left + 2 * size - 1, maxIndex - 1);
                
                // Merge subarrays array[left...mid] & array[mid+1...right]
                array.DoMerge(left, middle, right);
                //Merge(array, left, middle, right);
            }
        }
        
        return array;
    }
    
    public static T[] TimSort<T>(this T[] array) where T : IComparable
    {
        return array.TimSort(array.Length);
    }
}