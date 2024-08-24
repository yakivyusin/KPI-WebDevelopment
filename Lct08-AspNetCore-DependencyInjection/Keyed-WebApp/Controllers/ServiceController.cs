using Keyed_WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keyed_WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceInterface _service;

#if true
    public ServiceController(IServiceInterface service) => _service = service;
#else
    public ServiceController([FromKeyedServices("A")] IServiceInterface service) => _service = service;
#endif

    [HttpGet]
    public string Get() => _service.Message;
}
