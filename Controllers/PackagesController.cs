using DevTrackR.API.Entities;
using DevTrackR.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevTrackR.API.Controllers
{
  [ApiController]
  [Route("api/packages")]
  public class PackagesController : ControllerBase
  {
    private readonly ILogger<PackagesController> _logger;

    public PackagesController(ILogger<PackagesController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public IActionResult GetaAll()
    {
      var packages = new List<Package> {
            new Package("Pack1", 1.3M),
            new Package("Pack2", 0.3M)
        };

      return Ok(packages);
    }

    [HttpGet("{code}")]
    public IActionResult GetByCode(string code)
    {
      var package = new Package("Pack2", 0.3M);

      return Ok(package);
    }
    [HttpPost]
    public IActionResult Post(PackageInputModel model)
    {
      return Ok();
    }

    [HttpPost("{code}/updates")]
    public IActionResult PostUpdate(string code, PackageUpdateModel model)
    {
      return Ok();

    }


  }
}