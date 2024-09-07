using Microsoft.AspNetCore.Mvc;

namespace Configuration.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigurationController : ControllerBase
{
    private readonly IConfigurationRoot _configurationRoot;

    public ConfigurationController(IConfiguration configuration) => _configurationRoot = (IConfigurationRoot)configuration;

    [HttpGet("providers")]
    public IEnumerable<string?> Providers() => _configurationRoot.Providers.Select(x => x.ToString());

    [HttpGet("all")]
    public IEnumerable<string> All() => _configurationRoot.AsEnumerable().Select(kv => kv.ToString());

    [HttpGet("{key}")]
    public string? Key(string key) => _configurationRoot[key];

    [HttpGet("keyD")]
    public int KeyD() => _configurationRoot.GetSection("MySection").GetSection("MySubsection").GetValue<int>("KeyD");
}
