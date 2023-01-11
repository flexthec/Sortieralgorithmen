using static SortierAlgorithmen.Application.SortingMethod;

namespace SortierAlgorithmen;

public static class Application
{
    public enum SortingMethod
    {
        MinMax,
        MaxMin,
        ZigZag
    }
    
    public static SortingMethod Method { get; set; }

    public static void Start()
    {
        Selection();
        Console.ReadKey();
    }

    private static void Selection()
    {
        // intro
        Console.WriteLine("\n\n\tWillkommen zu der Sortieralgorithmus-Application!");
        Console.WriteLine("\n\n\tEs gibt die Möglichkeit die Zahlen auf unterschiedliche Art und Weise zu sortieren.");
        Console.WriteLine("\n\n\tDie Zahlen können manuell eingegeben oder generiert werden.");
        Console.WriteLine("\n\n\tBeliebige Taste zum fortfahren.");
        Console.ReadKey();
        
        // 1. choose number input or generate random numbers
        var array = NumberInput();

        // 2. choose sorting method
        Console.WriteLine("\n\n\tWähle die Sortierreihenfolgen:\n\n\t 1 für Min -> Max\n\t 2 für Max -> Min\n\t 3 für Zig-Zag");
        switch (KeyInput())
        {
            case ConsoleKey.D1:
                Method = MinMax;
                break;
            case ConsoleKey.D2:
                Method = MaxMin;
                break;
            case ConsoleKey.D3:
                Method = ZigZag;
                break;
        }
        
        // 3. choose sorting algorithm
        Console.WriteLine("\n\n\tWähle den Sortieralgorithmus:\n\n\t 1 für Mergesort\n\t 2 für Quicksort");
        switch (KeyInput())
        {
            case ConsoleKey.D1:
                Console.WriteLine(Method is ZigZag
                    ? $"Sortierter Mergesort-Array: {string.Join(", ", Merge.Sort(array).Zigzag())}"
                    : $"Sortierter Mergesort-Array: {string.Join(", ", Merge.Sort(array))}");
                break;
            case ConsoleKey.D2:
                Console.WriteLine(Method is ZigZag
                    ? $"Sortierter Quicksort-Array: {string.Join(", ", Quick.Sort(array).Zigzag())}"
                    : $"Sortierter Quicksort-Array: {string.Join(", ", Quick.Sort(array))}");
                break;
        }
    }

    private static int[] NumberInput()
    {
        Console.WriteLine("\n\t 1 für manuelle Eingabe\n\t 2 für Zufallszahlen");
        switch (KeyInput())
        {
            case ConsoleKey.D1:
                return ManualInput();
            case ConsoleKey.D2:
                return RandomInput();
        }

        return null!;
    }

    private static int[] ManualInput()
    {
        Console.WriteLine("\n\n\tFüge Zahlen zum sortieren hinzu(Leerzeichen, Komma oder Punkt zum Trennen): ");
        var separators = new[] { ' ', ',', '.', ';', ':' };
        var elements = Console.ReadLine()!.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        var array = new int[elements.Length];
        for (int i = 0; i < elements.Length; i++)
        {
            array[i] = int.Parse(elements[i]);
        }

        return array;
    }
    
    private static int[] RandomInput()
    {
        Console.WriteLine("\n\n\tWähle 10 bis 20 Zahlen die generiert werden sollen: ");
        var array = GenerateNumbers(int.Parse(Console.ReadLine()!));
        
        return array;
    }
    
    private static int[] GenerateNumbers(int elementSize)
    {
        var numbers = new int[elementSize];
        var range = new Random();

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = range.Next(-1000, 1000);
        }

        return numbers;
    }
    
    private static ConsoleKey KeyInput()
    {
        var inputKey = Console.ReadKey().Key;

        while (!ValidInput(inputKey)) // Input validation 
        {
            Console.WriteLine("\tDie Eingabe war falsch.");
            Console.WriteLine("\n\n\tWähle die Sortierreihenfolgen:\n 1 für Min -> Max\n 2 für Max -> Min\n 3 für Zig-Zag");
            inputKey = Console.ReadKey().Key;
        }

        Console.ReadKey();
        Console.Clear();

        return inputKey;
    }

    private static bool ValidInput(ConsoleKey input)
    {
        return input is ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3;
    }
}