using DevTrackR.API.Entities;
using DevTrackR.API.Models;
using DevTrackR.API.Persistences.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DevTrackR.API.Controllers
{
  [ApiController]
  [Route("api/packages")]
  public class PackagesController : ControllerBase
  {
    private readonly IPackageRepository _repository;
    private readonly ILogger<PackagesController> _logger;

    public PackagesController(IPackageRepository repository, ILogger<PackagesController> logger)
    {
      _repository = repository;
      _logger = logger;
    }

    [HttpGet]
    public IActionResult GetaAll()
    {
      var packages = _repository.GetAll();

      return Ok(packages);
    }

    [HttpGet("{code}")]
    public IActionResult GetByCode(string code)
    {
      var package = _repository.GetByCode(code);

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

      _repository.Add(package);

      return CreatedAtAction(
        "GetByCode",
        new { code = package.Code },
        package
      );
    }

    [HttpPost("{code}/updates")]
    public IActionResult PostUpdate(string code, PackageUpdateModel model)
    {
      var package = _repository.GetByCode(code);

      if (package == null)
      {
        return NotFound();
      }

      package.AddUpdate(model.Status, model.Delivered);
      _repository.Update(package);

      return NoContent();
    }
  }
}