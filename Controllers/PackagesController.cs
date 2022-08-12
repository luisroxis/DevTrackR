using DevTrackR.API.Entities;
using DevTrackR.API.Models;
using DevTrackR.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevTrackR.API.Controllers
{
  [ApiController]
  [Route("api/packages")]
  public class PackagesController : ControllerBase
  {
    private readonly DevTrackRContext _context;
    private readonly ILogger<PackagesController> _logger;

    public PackagesController(DevTrackRContext context, ILogger<PackagesController> logger)
    {
      _context = context;
      _logger = logger;
    }

    [HttpGet]
    public IActionResult GetaAll()
    {
      var packages = _context.Packages;

      return Ok(packages);
    }

    [HttpGet("{code}")]
    public IActionResult GetByCode(string code)
    {
      var package = _context.Packages.SingleOrDefault(p => p.Code == code);

      if (package == null)
      {
        return NotFound();
      }

      return Ok(package);
    }

    [HttpPost]
    public IActionResult Post(PackageInputModel model)
    {
      if (model.Title.Length < 10)
      {
        return BadRequest("Title length must be at least 10 characters long.");
      }

      var package = new Package(model.Title, model.Weight);

      _context.Packages.Add(package);

      return CreatedAtAction(
        "GetByCode",
        new { code = package.Code },
        package
      );
    }

    [HttpPost("{code}/updates")]
    public IActionResult PostUpdate(string code, PackageUpdateModel model)
    {
      var package = _context.Packages.SingleOrDefault(p => p.Code == code);

      if (package == null)
      {
        return NotFound();
      }

      package.AddUpdate(model.Status, model.Delivered);

      return NoContent();

    }


  }
}