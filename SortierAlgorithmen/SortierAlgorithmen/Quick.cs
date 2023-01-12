namespace SortierAlgorithmen;

public static class Quick
{
    private static void Swap<T>(this T[] array, int left, int right) where T : IComparable
    {
        (array[left], array[right]) = (array[right], array[left]);
    }

    private static int Partition<T>(this T[] array, int lowIndex, int highIndex) where T : IComparable
    {
        var pivot = array[highIndex]; // pivot is the last element
        var smallIndex = lowIndex - 1; // index of smaller elements
      
        for (int index = lowIndex; index <= highIndex - 1; index++)
        {
            if (array[index].CompareTo(pivot) >= 0) continue; // if current element is smaller than or equal to pivot
            smallIndex++;
            array.Swap(smallIndex, index); // swap smaller element to the left
        }
      
        array.Swap(smallIndex + 1, highIndex); // swap pivot to the right
        return smallIndex + 1;
    }

    private static T[] QuickSort<T>(this T[] array, int lowIndex, int highIndex) where T : IComparable
    {
        if (lowIndex >= highIndex) return array;
        
        var partitionIndex = Partition(array, lowIndex, highIndex); // partitioning index, array[p] is now at right place
        array.QuickSort(lowIndex, partitionIndex - 1); // before partition
        array.QuickSort(partitionIndex + 1, highIndex); // after partition

        return array;
    }

    public static T[] QuickSort<T>(this T[] array) where T : IComparable
    {
        return array.QuickSort(0, array.Length - 1);
    }
}