namespace SortierAlgorithmen;

public static class Quick
{
    public static void Swap<T>(this T[] array, int left, int right) where T : IComparable
    {
        (array[left], array[right]) = (array[right], array[left]);
    }
    
    private static int Partition<T>(this T[] array, int lowIndex, int highIndex) where T : IComparable
    {
        var pivot = array[highIndex]; 
        var smallIndex = lowIndex - 1;
      
        for (int index = lowIndex; index <= highIndex - 1; index++)
        {
            if (array[index].CompareTo(pivot) >= 0) continue;
            smallIndex++;
            array.Swap(smallIndex, index);
        }
      
        array.Swap(smallIndex + 1, highIndex);
        return smallIndex + 1;
    }
    
    private static T[] QuickSort<T>(this T[] array, int lowIndex, int highIndex) where T : IComparable
    {
        if (lowIndex >= highIndex) return array;
        
        var partitionIndex = Partition(array, lowIndex, highIndex);
        array.QuickSort(lowIndex, partitionIndex - 1); 
        array.QuickSort(partitionIndex + 1, highIndex); 

        return array;
    }

    public static T[] QuickSort<T>(this T[] array) where T : IComparable
    {
        return array.QuickSort(0, array.Length - 1);
    }
}