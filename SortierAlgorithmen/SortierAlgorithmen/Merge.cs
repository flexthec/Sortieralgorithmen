namespace SortierAlgorithmen;

public static class Merge
{
    private static void DoMerge<T>(this T[] array, int lowIndex, int middleIndex, int highIndex) where T : IComparable
    {
        var left = lowIndex;
        var right = middleIndex + 1;
        var tempArray = new T[highIndex - lowIndex + 1];
        var index = 0;

        while (left <= middleIndex && right <= highIndex) 
        {
            tempArray[index] = array[left].CompareTo(array[right]) < 0 // sort max to min
                        ? array[left++] 
                        : array[right++];

                    index++;
        }

        for (int i = left; i <= middleIndex; i++)
        {
            tempArray[index] = array[i]; // copy remaining elements
            index++;
        }

        for (int i = right; i <= highIndex; i++)
        {
            tempArray[index] = array[i]; // copy remaining elements
            index++;
        }

        for (int i = 0; i < tempArray.Length; i++)
        {
            array[lowIndex + i] = tempArray[i]; // transfer back to original array
        }
    }

    private static T[] Sort<T>(this T[] array, int lowIndex, int highIndex) where T : IComparable
    {
        if (lowIndex >= highIndex) return array;
        
        var middleIndex = (lowIndex + highIndex) / 2; // calculate middle index
        Sort(array, lowIndex, middleIndex); // sort first half of the array
        Sort(array, middleIndex + 1, highIndex); // sort second half
        DoMerge(array, lowIndex, middleIndex, highIndex); // merge both halves

        return array;
    }

    public static T[] Sort<T>(this T[] array) where T : IComparable
    {
        return Sort(array, 0, array.Length - 1);
    }
}