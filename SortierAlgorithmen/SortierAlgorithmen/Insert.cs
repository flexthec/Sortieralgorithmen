namespace SortierAlgorithmen;

public static class Insert
{
    public static int Sort<T>(this T[] data) where T : IComparable
    {
        var comparisionNum = 0;

        for (int i = 0; i < data.Length; i++)
        {
            var iTemp = i;

            while (iTemp > 0 && data[i].CompareTo(data[iTemp - 1]) < 0)
            {
                data[iTemp] = data[iTemp - 1];
                iTemp--;
                comparisionNum++;
            }

            data[iTemp] = data[i];
        }

        return comparisionNum;
    }

}