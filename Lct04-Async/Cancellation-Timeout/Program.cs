namespace Cancellation_Timeout;

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
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
#else
        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(3));
#endif

        var result = await SumPageSizesAsync(cts.Token);
        Console.WriteLine($"Total size: {result}");
    }

    private static async Task<int> SumPageSizesAsync(CancellationToken cancellationToken)
    {
        var total = 0;

        foreach (string url in _urls)
        {
            Console.WriteLine(url);

            try
            {
                total += await ProcessUrlAsync(url, cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine(ex);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return total;
            }
        }

        return total;
    }

    private static async Task<int> ProcessUrlAsync(string url, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(url, cancellationToken);
        var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);

        return content.Length;
    }
}
