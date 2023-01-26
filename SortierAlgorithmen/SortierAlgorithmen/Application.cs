using System.Diagnostics;

namespace SortierAlgorithmen;

#region Stopwatch Test

// System: Windows 11, Ryzen 7 5800X, DDR4 32GB Ram @ CL16 3600MHz (4 x 8GB), Crucial P5 Plus 1TB SSD (r:6600 MB/s | w:5000 MB/s)

// tested with an array based of 1000 elements !!! INVALID !!!

// merge     -> Dauer: minmax: 00:00:00.0009133 | zigzag: 00:00:00.0013622 --/1.
// quick     -> Dauer: minmax: 00:00:00.0004470 | zigzag: 00:00:00.0014448 --/4.
// tim       -> Dauer: minmax: 00:00:00.0011532 | zigzag: 00:00:00.0011200 --/3.
// cube      -> Dauer: minmax: 00:00:00.0005244 | zigzag: 00:00:00.0005542 --/5.
// insertion -> Dauer: minmax: 00:00:00.0004373 | zigzag: 00:00:00.0005920 --/2.

#endregion

public static class Application
{
    private enum InputType { Number, Algorithm, Order, Restart }
    private static InputType _inputType;

    private static readonly Stopwatch Timer = new ();

    public static void Start()
    {
        Selection();
    }

    private static void Selection()
    {
        Intro();
        
        #region Test Fields
        //var array = new []{12, 3, 5, 7, 9, 1, 2, 4, 6, 8, 10, 11};
        //const string path = "E:\\GitFork\\Sortieralgorithmen\\Numbers.txt";
        //var textArray = File.ReadAllText(path).Split(' ', StringSplitOptions.RemoveEmptyEntries);
        //textArray.PrintArray();
        #endregion

        // 1. choose number input or generate random numbers
        var array = NumberInput().PrintArray();
        //var array = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        
        var arrayCopy = CopyFrom(array); // copy array for later comparison and usage in other algorithms

        // 2. choose sort algorithm
        AlgorithmInput(arrayCopy);

        // 3. choose sort order
        OrderInput(arrayCopy);
        
        // 4. print sorted array
        arrayCopy.PrintArray();
        Console.WriteLine("\n\n\tDauer der Sortierung : {0}", Timer.Elapsed);

        // 5. print original array
        Console.WriteLine("\tUnsortierter ursprünglicher array");
        array.PrintArray();
        
        RestartOptions();
    }

    private static void OrderInput<T>(T[] arrayCopy) where T : IComparable, IComparable<T>
    {
        _inputType = InputType.Order;
        Console.WriteLine("\n\n\tWähle die Sortierreihenfolge:\n\n\t1 für Min -> Max\n\t2 für Max -> Min\n\t3 für Zickzack\n");
        switch (KeyInput())
        {
            case ConsoleKey.D1:
                break;
            case ConsoleKey.D2:
                Timer.Restart();
                arrayCopy.ReverseSort();
                Timer.Stop();
                break;
            case ConsoleKey.D3:
                Timer.Restart();
                arrayCopy.ReverseSort().Zigzag();
                Timer.Stop();
                break;
        }
    }

    private static void AlgorithmInput<T>(T[] arrayCopy) where T : IComparable
    {
        _inputType = InputType.Algorithm;
        Console.WriteLine("\n\n\tWähle den Sortieralgorithmus:\n\n\t1 für Mergesort\n\t2 für Quicksort\n\t3 für Timsort\n\t4 für Cubesort\n\t5 für Insertionsort\n");
        switch (KeyInput())
        {
            case ConsoleKey.D1:
                Timer.Start();
                arrayCopy.MergeSort();
                Timer.Stop();
                break;
            case ConsoleKey.D2:
                Timer.Start();
                arrayCopy.QuickSort();
                Timer.Stop();
                break;
            case ConsoleKey.D3:
                Timer.Start();
                arrayCopy.TimSort();
                Timer.Stop();
                break;
            case ConsoleKey.D4:
                Timer.Start();
                arrayCopy.CubeSort();
                Timer.Stop();
                break;
            case ConsoleKey.D5:
                Timer.Start();
                arrayCopy.InsertionSort();
                Timer.Stop();
                break;
        }
    }
    
    private static int[] NumberInput()
    { 
        _inputType = InputType.Number;
        Console.WriteLine("\n\n\t1 für manuelle Eingabe\n\t2 für Zufallszahlen\n");
        switch (KeyInput())
        {
            case ConsoleKey.D1:
                return ManualInput();
            case ConsoleKey.D2:
                return RandomInput();
        }

        return null!;
    }

    private static void Intro()
    {
        Console.WriteLine("\n\n\tWillkommen zu der Sortieralgorithmus-Application!");
        Console.WriteLine("\n\n\tEs gibt die Möglichkeit die Zahlen auf unterschiedliche Art und Weise zu sortieren.");
        Console.WriteLine("\n\n\tDie Zahlen können manuell eingegeben oder generiert werden.");
        Console.WriteLine("\n\n\tBeliebige Taste zum fortfahren.");
        Console.ReadKey();
        Console.Clear();
    }

    private static T[] CopyFrom<T>(T[] array)
    {
        var arrayCopy = new T[array.Length];
        array.CopyTo(arrayCopy, 0);
        
        return arrayCopy;
    }

    private static T[] PrintArray<T>(this T[] array) where T : IComparable<T>
    {
        Console.WriteLine($"\n\n\t{string.Join(", ", array)}");

        Console.WriteLine("\n\n\tBeliebige Taste zum fortfahren.");
        Console.ReadKey();
        Console.Clear();
        
        return array;
    }

    private static int[] ManualInput()
    {
        Console.WriteLine("\n\n\tFüge Zahlen zum sortieren hinzu(Leerzeichen, Komma oder Punkt zum Trennen): ");
        var separators = new[] { ' ', ',', '.', ';', ':' };
        var elements = Console.ReadLine()!.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        ClearCurrentConsoleLine();
        
        return elements.Select(int.Parse).ToArray();
    }
    
    private static int[] RandomInput()
    {
        Console.WriteLine("\n\n\tWähle eine Ganzzahl von 10 bis 100.000 die generiert werden sollen: ");
        var array = GenerateNumbers(ValidNumber());
        
        return array;
    }
    
    private static ConsoleKey KeyInput()
    {
        var inputKey = Console.ReadKey().Key;

        while (!ValidInput(inputKey)) // Input validation 
        {
            Console.WriteLine("\tDie Eingabe war keine Ganzzahl.");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            
            inputKey = Console.ReadKey().Key;
        }

        Console.ReadKey();
        Console.Clear();

        return inputKey;
    }

    private static bool ValidInput(ConsoleKey input)
    {
        switch (_inputType)
        {
            case InputType.Number or InputType.Restart:
                return input is ConsoleKey.D1 or ConsoleKey.D2;
            case InputType.Algorithm:
                return input is ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3 or ConsoleKey.D4 or ConsoleKey.D5;
            case InputType.Order:
                return input is ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3;
        }
        
        return false;
    }

    private static int ValidNumber()
    {
        var success = int.TryParse(Console.ReadLine(), out var inputValue);
        var valid = success && inputValue is >= 10 and <= 100000;
        while (!valid)
        {
            Console.WriteLine("\tDie Eingabe war keine Ganzzahl und/oder die Zahl war nicht zwischen 10 und 100.000.");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            success = int.TryParse(Console.ReadLine(), out inputValue);
            valid = success && 0 <= inputValue && inputValue <= 100;
        }

        return inputValue;
    }
    
    private static void RestartOptions()
    {
        _inputType = InputType.Restart;
        Console.WriteLine($"\n\n\n\t1. Neu starten\n\t2. Beenden\n");
        switch (KeyInput())
        {
            case ConsoleKey.D1:
                Console.Clear();
                Start();
                break;
            case ConsoleKey.D2:
                Environment.Exit(0);
                break;
        }
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
    
    public static void ClearCurrentConsoleLine()
    {
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth)); 
        Console.SetCursorPosition(0, currentLineCursor);
    }
}