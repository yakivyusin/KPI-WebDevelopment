namespace ProgressReporting;

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
        var progress = new Progress<int>(index => Console.WriteLine($"URL No.{index} is processed"));
#else
        var progress = new Progress<int>();
        progress.ProgressChanged += (sender, index) => Console.WriteLine($"URL No.{index} is processed");
#endif

        var result = await SumPageSizesAsync(progress);
        Console.WriteLine($"Total size: {result}");
    }

    private static async Task<int> SumPageSizesAsync(IProgress<int> progress)
    {
        var total = 0;

        foreach (var (url, index) in _urls.Select((url, index) => (url, index)))
        {
            total += await ProcessUrlAsync(url);

            progress?.Report(index + 1);
        }

        return total;
    }

    private static async Task<int> ProcessUrlAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsByteArrayAsync();

        return content.Length;
    }
}
