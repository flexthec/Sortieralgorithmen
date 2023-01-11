using static SortierAlgorithmen.Application.SortingMethod;

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

        switch (Application.Method)
        {
            case MinMax:
                for (int index = lowIndex; index <= highIndex - 1; index++)
                {
                    if (array[index].CompareTo(pivot) >= 0) continue; // if current element is smaller than or equal to pivot
                    smallIndex++;
                    array.Swap(smallIndex, index); // swap smaller element to the left
                }

                break;
            case MaxMin or ZigZag:
                for (int index = lowIndex; index <= highIndex - 1; index++)
                {
                    if (array[index].CompareTo(pivot) <= 0) continue; // if current element is smaller than or equal to pivot
                    smallIndex++;
                    array.Swap(smallIndex, index); // swap smaller element to the left
                }
                
                break;
        }


        array.Swap(smallIndex + 1, highIndex); // swap pivot to the right
        return smallIndex + 1;
    }

    private static T[] Sort<T>(this T[] array, int lowIndex, int highIndex) where T : IComparable
    {
        if (lowIndex < highIndex)
        {
            var partitionIndex =
                Partition(array, lowIndex, highIndex); // partitioning index, arr[p] is now at right place
            Sort(array, lowIndex, partitionIndex - 1); // before partition
            Sort(array, partitionIndex + 1, highIndex); // after partition
        }

        return array;
    }

    public static T[] Sort<T>(this T[] array) where T : IComparable
    {
        return Sort(array, 0, array.Length - 1);
    }
}