using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AmparoIndirectoAPI.Api
{

    [ApiController]
    [Route("sicoj/amparo-indirecto/api/[controller]")]
    public class HealthController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult Status()
        {
            Assembly assembly = Assembly.GetExecutingAssembly(); AssemblyName name = assembly.GetName();
            Version version = name.Version!; return Ok($"Assembly: {name.Name} - Versión: {version}");
        }
    }
}