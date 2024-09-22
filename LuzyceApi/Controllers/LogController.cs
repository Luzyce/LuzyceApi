using Luzyce.Core.Models.Log;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[Route("api/log")]
public class LogController(LogRepository logRepository) : Controller
{
    private readonly LogRepository logRepository = logRepository;

    [HttpGet("{offset:int}/{limit:int}")]
    [Authorize]
    public IActionResult Get(int offset, int limit)
    {
        return Ok(logRepository.GetLogs(offset, limit));
    }

    [HttpGet("unidentified/{offset:int}/{limit:int}")]
    [Authorize]
    public IActionResult GetUnidentified(int offset, int limit)
    {
        return Ok(logRepository.GetUnidentifiedLogs(offset, limit));
    }

    [HttpPut("assignUser")]
    [Authorize]
    public IActionResult AssignUser([FromBody] AssignUserDto assignUserDto)
    {
        logRepository.AssignUser(assignUserDto);
        return Ok();
    }

}