using DevTrackR.API.Entities;
using DevTrackR.API.Models;
using DevTrackR.API.Persistences.Repository;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DevTrackR.API.Controllers
{
  [ApiController]
  [Route("api/packages")]
  public class PackagesController : ControllerBase
  {
    private readonly IPackageRepository _repository;
    private readonly ILogger<PackagesController> _logger;
    private readonly ISendGridClient _client;

    public PackagesController(IPackageRepository repository,
      ILogger<PackagesController> logger,
      ISendGridClient client)
    {
      _repository = repository;
      _logger = logger;
      _client = client;
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

    /// <summary>
    ///  Cadastro de um pacote
    /// </summary>
    /// <remarks>
    /// {
    /// "title": "Pacote Demonstracao",
    /// "weight": 1.5,
    /// "senderName": "Luis Carlos",
    /// "senderEmail": "test@example.com"
    /// }
    /// </remarks>
    /// <param name="model">Dados de um pacote</param>
    /// <returns>Objeto recem-criado</returns>
    /// <response code="201">Cadastro realizado com sucesso</response>
    /// <response code="400">Dados Invalidos</response>
    [HttpPost]
    public async Task<IActionResult> Post(PackageInputModel model)
    {
      if (model.Title.Length < 10)
      {
        return BadRequest("Title length must be at least 10 characters long.");
      }

      var package = new Package(model.Title, model.Weight);

      _repository.Add(package);

      var message = new SendGridMessage
      {
        From = new EmailAddress("roxisxprol@gmail.com", "luisroxis"),
        Subject = "Your package was dispatched",
        PlainTextContent = $"Your package with code {package.Code} was dispatched"
      };

      message.AddTo(model.SenderEmail, model.SenderName);

      await _client.SendEmailAsync(message);

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