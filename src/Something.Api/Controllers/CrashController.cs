using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CrashController : ControllerBase
{
    private IHostApplicationLifetime _lifetime;

    public CrashController(IHostApplicationLifetime lifetime)
    {
        _lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));    
    }
    [HttpGet]
    public ActionResult CrashThisApp()
    {
        _lifetime.StopApplication();
        throw new ArgumentException(
            "deliberately crashing this app by accessing this endpoint lmao.");
    }
}