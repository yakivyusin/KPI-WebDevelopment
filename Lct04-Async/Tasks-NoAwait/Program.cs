namespace Tasks_NoAwait;

internal class Program
{
    private static readonly HttpClient _httpClient = new();
    private static readonly IEnumerable<string> _urls =
    [
        "https://learn.microsoft.com",
        "https://learn.microsoft.com/aspnet/core",
        "https://learn.microsoft.com/azure",
        "https://learn.microsoft.com/azure/devops",
        "https://learn.microsoft.com/dotnet",
        "https://learn.microsoft.com/dynamics365",
        "https://learn.microsoft.com/education",
        "https://learn.microsoft.com/enterprise-mobility-security",
        "https://learn.microsoft.com/gaming",
        "https://learn.microsoft.com/graph",
        "https://learn.microsoft.com/microsoft-365",
        "https://learn.microsoft.com/office",
        "https://learn.microsoft.com/powershell",
        "https://learn.microsoft.com/sql",
        "https://learn.microsoft.com/surface",
        "https://learn.microsoft.com/system-center",
        "https://learn.microsoft.com/visualstudio",
        "https://learn.microsoft.com/windows",
        "https://learn.microsoft.com/maui"
    ];

#if true
    static void Main(string[] args)
    {
        var t = SumPageSizesAsync();
        t.Wait();

        Console.WriteLine($"Total size: {t.Result}");
    }
#else
    static Task Main(string[] args)
    {
        return SumPageSizesAsync().ContinueWith(t => Console.WriteLine($"Total size: {t.Result}"));
    }
#endif

    private static Task<int> SumPageSizesAsync() => Task.WhenAll(_urls.Select(x => ProcessUrlAsync(x)))
        .ContinueWith(t => t.Result.Sum());

    private static Task<int> ProcessUrlAsync(string url)
    {
        Console.WriteLine(url);

        return _httpClient.GetAsync(url)
            .ContinueWith(t => t.Result.Content.ReadAsByteArrayAsync())
            .Unwrap()
            .ContinueWith(t => t.Result.Length);
    }
}
