using Microsoft.AspNetCore.Mvc;
using PoupAI.Repositories;
using System.Threading.Tasks;
using Api.Comum;

namespace PoupAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitaController : ControllerBase
    {
        private readonly ReceitaRepository _repo;

        public ReceitaController(ReceitaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var receita = await _repo.GetById(id);
            return receita == null ? NotFound() : Ok(receita);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Receita receita)
        {
            await _repo.AddValue(receita);
            return CreatedAtAction(nameof(GetById), new { id = receita.Id }, receita);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Receita receita)
        {
            receita.Id = id;
            await _repo.UpdateValue(receita);
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
