using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;
[Route("api/document")]
[ApiController]
public class DocumentController(DocumentRepository documentRepository) : Controller
{
    private readonly DocumentRepository documentRepository = documentRepository;

    [HttpGet]
    public IActionResult Get()
    {
        var documents = documentRepository.GetDocuments().Select(x => new
        {
            x.Id,
            x.Number,
            x.Warehouse,
            x.Year,
            Operator = new { x.Operator.Id, x.Operator.Name, x.Operator.LastName, x.Operator.Email, x.Operator.Login },
            x.CreatedAt,
            x.UpdatedAt,
            x.ClosedAt,
            x.Status,
            x.DocumentsDefinition
        }
        );
        return Ok(documents);
    }
}
