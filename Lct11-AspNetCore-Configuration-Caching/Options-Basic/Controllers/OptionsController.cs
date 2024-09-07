using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Options_Basic.Options;

namespace Options_Basic.Controllers;

[ApiController]
[Route("[controller]")]
public class OptionsController : ControllerBase
{
    private readonly IOptions<MySubsection> _options;
    private readonly IOptionsSnapshot<MySubsection> _snapshot;
    private readonly IOptionsMonitor<MySubsection> _monitor;
    private readonly IOptionsFactory<MySection> _factory;

    public OptionsController(IOptions<MySubsection> options, IOptionsSnapshot<MySubsection> snapshot,
        IOptionsMonitor<MySubsection> monitor, IOptionsFactory<MySection> factory)
    {
        _options = options;
        _snapshot = snapshot;
        _monitor = monitor;
        _factory = factory;
    }

    [HttpGet]
    public object Get() => new
    {
        Options = _options.Value,
        Snapshot = _snapshot.Value,
        Monitor = _monitor.CurrentValue,
        Factory = _factory.Create(string.Empty)
    };
}
