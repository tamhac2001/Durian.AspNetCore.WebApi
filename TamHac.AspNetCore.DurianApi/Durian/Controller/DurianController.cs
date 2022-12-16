using LanguageExt;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TamHac.AspNetCore.DurianApi.Durian.Service;

namespace TamHac.AspNetCore.DurianApi.Durian.Controller;

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
    public async Task<Seq<Model.Durian>> GetAllDurian()
    {
        return await _service.GetAll();
    }

    [HttpGet]
    [Route("durian/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Model.Durian>> GetDurianById(string id)
    {
        var durianOption = await _service.FindById(id);
        return durianOption.Match<ActionResult>(Ok, NotFound);
    }

    [HttpPost]
    [Route("durian/save")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddDurian([FromBody] Model.Durian durian)
    {
        var result = await _service.Insert(durian);
        return result.Match<IActionResult>(Left: exception => BadRequest("Durian existed"),
            Right: _ => CreatedAtAction(nameof(AddDurian), durian));
    }

    [HttpPut]
    [Route("durian/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutUpdateDurian(string id, [FromBody] Model.Durian durian)
    {
        await _service.Update(id, durian);
        return NoContent();
    }

    [HttpPatch]
    [Route("durian/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PatchUpdateDurian(string id,
        [FromBody] JsonPatchDocument<Model.Durian> patchDocument)
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