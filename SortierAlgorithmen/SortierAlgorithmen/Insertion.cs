namespace SortierAlgorithmen;

public static class Insertion
{
    public static T[] InsertionSort<T>(this T[] array, int left, int right) where T : IComparable
    {
        for (int i = 1; i < right; i++)
        {
            T tempArray = array[i];
            int j = i; // j is the number of items sorted so far
            while (j > 0 && array[j - 1].CompareTo(tempArray) > 0) // if the item to the left is greater than the item to the right
            {
                array[j] = array[j - 1]; // shift item to the right
                j--; // go left one position
            }
            
            array[j] = tempArray; // insert the saved item
        }
        return array;
    }

    public static T[] InsertionSort<T>(this T[] array) where T : IComparable
    {
        return InsertionSort(array, 0, array.Length);
    }
}