using LanguageExt;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TamHac.AspNetCore.DurianApi.Application.Durian;
using TamHac.AspNetCore.DurianApi.Domain;

namespace TamHac.AspNetCore.DurianApi.Presentation;

[Route("api/")]
[ApiController]
public class DurianController : ControllerBase
{
    private readonly IDurianService _service;

    public DurianController(IDurianService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("durians")]
    public async Task<Seq<Durian>> GetAllDurian()
    {
        return await _service.GetAllDurians();
    }

    [HttpGet]
    [Route("durian/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Durian>> GetDurianById(string id)
    {
        var durianOption = await _service.FindById(id);
        return durianOption.Match<ActionResult>(Ok, NotFound);
    }

    [HttpPost]
    [Route("durian/save")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddDurian([FromBody] Durian durian)
    {
        await _service.Insert(durian);
        return CreatedAtAction(nameof(GetAllDurian), durian);
    }

    [HttpPut]
    [Route("durian/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutUpdateDurian(string id, [FromBody] Durian durian)
    {
        await _service.Update(id, durian);
        return NoContent();
    }

    [HttpPatch]
    [Route("durian/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PatchUpdateDurian(string id, [FromBody] JsonPatchDocument<Durian> patchDocument)
    {
        var durianOption = await _service.FindById(id);
        return await durianOption.Match<Task<IActionResult>>(None: async () => BadRequest(), Some: async durian =>
        {
            patchDocument.ApplyTo(durian, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.Update(id, durian);
            return NoContent();
        });
    }

    [HttpDelete]
    [Route("durian/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteDurian(string id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}