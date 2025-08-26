using Microsoft.AspNetCore.Mvc;
using PoupAI.Repositories;
using System.Threading.Tasks;
using Api.Comum;

namespace PoupAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetaController : ControllerBase
    {
        private readonly MetaRepository _repo;

        public MetaController(MetaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAll());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Meta meta)
        {
            await _repo.AddValue(meta);
            return Ok(meta);
        }
    }
}
