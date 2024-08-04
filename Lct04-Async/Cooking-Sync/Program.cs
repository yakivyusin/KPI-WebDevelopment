// Source: https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/

namespace Cooking_Sync;

internal record Bacon;
internal record Coffee;
internal record Egg;
internal record Juice;
internal record Toast;

internal class Program
{
    static void Main(string[] args)
    {
        var cup = PourCoffee();
        Console.WriteLine("coffee is ready");

        var eggs = FryEggs(2);
        Console.WriteLine("eggs are ready");

        var bacon = FryBacon(3);
        Console.WriteLine("bacon is ready");

        var toast = ToastBread(2);
        ApplyButter(toast);
        ApplyJam(toast);
        Console.WriteLine("toast is ready");

        var oj = PourOJ();
        Console.WriteLine("oj is ready");
        Console.WriteLine("Breakfast is ready!");
    }

    private static Juice PourOJ()
    {
        Console.WriteLine("Pouring orange juice");
        return new();
    }

    private static void ApplyJam(Toast toast) => Console.WriteLine("Putting jam on the toast");

    private static void ApplyButter(Toast toast) => Console.WriteLine("Putting butter on the toast");

    private static Toast ToastBread(int slices)
    {
        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine("Putting a slice of bread in the toaster");
        }

        Console.WriteLine("Start toasting...");
        Thread.Sleep(3000);
        Console.WriteLine("Remove toast from toaster");

        return new();
    }

    private static Bacon FryBacon(int slices)
    {
        Console.WriteLine($"putting {slices} slices of bacon in the pan");
        Console.WriteLine("cooking first side of bacon...");
        Thread.Sleep(3000);

        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine("flipping a slice of bacon");
        }

        Console.WriteLine("cooking the second side of bacon...");
        Thread.Sleep(3000);
        Console.WriteLine("Put bacon on plate");

        return new();
    }

    private static Egg FryEggs(int howMany)
    {
        Console.WriteLine("Warming the egg pan...");
        Thread.Sleep(3000);
        Console.WriteLine($"cracking {howMany} eggs");
        Console.WriteLine("cooking the eggs ...");
        Thread.Sleep(3000);
        Console.WriteLine("Put eggs on plate");

        return new();
    }

    private static Coffee PourCoffee()
    {
        Console.WriteLine("Pouring coffee");
        return new();
    }
}
