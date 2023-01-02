using Microsoft.AspNetCore.Mvc;

namespace SmartBooks.Controllers;


[Route("/api/[controller]")]
public abstract class BaseController<T> : ControllerBase
{
    private ILogger<T>? _logger;
    protected ILogger<T> Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<T>>()) ?? throw new InvalidOperationException();
}