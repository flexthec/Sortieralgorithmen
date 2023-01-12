using System.Collections;

namespace SortierAlgorithmen;

// The idea is to use the Comparator function with an inbuilt sort function() to sort the array according to the cubes of its elements.
// The Comparator function is used to compare two elements of the array and return an integer value. The sort function uses this integer value to sort the array.
public static class Cube
{
    private static T[] CubeSort<T>(this T[] array, int maxIndex)
    {
        var arrayCopy = new T[maxIndex];

        for (int i = 0; i < maxIndex; i++) 
            arrayCopy[i] = array[i]; // copy array
        
        IComparer comparer = new Comparer(); // create comparer
        
        Array.Sort(arrayCopy, comparer);

        return array;
    }
    
    public static T[] CubeSort<T>(this T[] array)
    {
        return array.CubeSort(array.Length);
    }
}