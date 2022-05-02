using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jim.Core.Shared.APIs
{
    public abstract class BaseController<TController> : Controller
        where TController : BaseController<TController>
    {
        private readonly ILogger<TController> _logger;

        public BaseController(ILogger<TController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected internal ILogger<TController> Logger => _logger;
    }
}