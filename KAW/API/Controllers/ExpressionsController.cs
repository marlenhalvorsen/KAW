using KAW.Application.Contracts.Request;
using KAW.Application.Ports.Inbound;
using Microsoft.AspNetCore.Mvc;

namespace KAW.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpressionsController : ControllerBase
    {
        private readonly IExpressionService _service;
        public ExpressionsController(IExpressionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateExpressionRequest request,
            CancellationToken ct = default)
        {
            try
            {
                var id = await _service.SaveExpression(request.name, request.description, ct);
                return Created($"api/expressions/{id}", new { id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateExpressionRequest request,
            CancellationToken ct = default)
        {
            try
            {
                await _service.UpdateExpression(id, request.name, request.description, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Expression with id {id} not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken ct = default)
        {
            try
            {
                await _service.DeleteExpression(id, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Expression with id {id} not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            var expressions = await _service.FetchAllExpressions(ct);
            return Ok(expressions);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? query,
            CancellationToken ct = default)
        {
            var expressions = await _service.SearchExpression(query, ct);
            return Ok(expressions);
        }
    }
}
