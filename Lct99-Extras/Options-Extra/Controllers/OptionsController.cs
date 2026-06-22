using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Options_Extra.Options;

namespace Options_Extra.Controllers;

[ApiController]
[Route("[controller]")]
public class OptionsController : ControllerBase
{
    private readonly MySubsection _subsection;
    private readonly MySection _section;

    public OptionsController(IOptions<MySubsection> optionsSubsection, IOptions<MySection> optionsSection)
    {
        _subsection = optionsSubsection.Value;
        _section = optionsSection.Value;
    }

    [HttpGet("section")]
    public MySection GetSection() => _section;

    [HttpGet("subsection")]
    public MySubsection GetSubsection() => _subsection;
}
