namespace AsyncStreams;

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

    static async Task Main(string[] args)
    {
#if true
        var total = 0;
        await foreach (var size in GetPageSizesAsync())
        {
            total += size;
        }
        Console.WriteLine($"Total size: {total}");
#else
        var total = await GetPageSizesAsync().SumAsync();
        Console.WriteLine($"Total size: {total}");
#endif
    }

    private static async IAsyncEnumerable<int> GetPageSizesAsync()
    {
        foreach (string url in _urls)
        {
            Console.WriteLine(url);
            yield return await ProcessUrlAsync(url);
        }
    }

    private static async Task<int> ProcessUrlAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsByteArrayAsync();

        return content.Length;
    }
}
