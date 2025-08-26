using Microsoft.AspNetCore.Mvc;
using PoupAI.Repositories;
using System.Threading.Tasks;
using Api.Comum;

namespace PoupAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DespesaController : ControllerBase
    {
        private readonly DespesaRepository _repo;

        public DespesaController(DespesaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var despesa = await _repo.GetById(id);
            return despesa == null ? NotFound() : Ok(despesa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Despesa despesa)
        {
            await _repo.AddValue(despesa);
            return CreatedAtAction(nameof(GetById), new { id = despesa.Id }, despesa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Despesa despesa)
        {
            despesa.Id = id;
            await _repo.UpdateValue(despesa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteValue(id);
            return NoContent();
        }
    }
}
