using System.Collections;

namespace SortierAlgorithmen;

public class Comparer : IComparer
{
    public int Compare(object? x, object? y)
    {
        return new CaseInsensitiveComparer().Compare(x, y);
    }
}