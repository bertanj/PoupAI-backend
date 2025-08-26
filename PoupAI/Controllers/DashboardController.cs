using Microsoft.AspNetCore.Mvc;
using PoupAI.Repositories;
using System.Threading.Tasks;
using Api.Comum;

namespace PoupAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardRepository _repo;

        public DashboardController(DashboardRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("resumo/{ano}/{mes}")]
        public async Task<IActionResult> GetResumoMensal(int ano, int mes) =>
            Ok(await _repo.GetResumoMensal(ano, mes));

        [HttpGet("gastos-por-categoria/{ano}/{mes}")]
        public async Task<IActionResult> GetGastosPorCategoria(int ano, int mes) =>
            Ok(await _repo.GetGastosPorCategoria(ano, mes));
    }
}
