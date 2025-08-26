using Microsoft.AspNetCore.Mvc;
using PoupAI.Repositories;
using System.Threading.Tasks;
using Api.Comum;

namespace PoupAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoRepository _repo;

        public TransacaoController(TransacaoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("ultimas")]
        public async Task<IActionResult> GetUltimasTransacoes() =>
            Ok(await _repo.GetUltimasTransacoes());
    }
}
