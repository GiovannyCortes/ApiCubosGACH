using ApiCubosGACH.Models;
using ApiCubosGACH.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCubosGACH.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase {

        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo) {
            this.repo = repo;
        }

        [HttpGet] [Route("[action]")]
        public async Task<ActionResult<List<Cubo>>> GetCubos() {
            return await this.repo.GetCubosAsync();
        }

        [HttpGet] [Route("[action]/{marca}")]
        public async Task<ActionResult<List<Cubo>>> GetCubosByMarca(string marca) {
            return await this.repo.GetCubosByMarcaAsync(marca);
        }

        [HttpPost] [Route("[action]")]
        public async Task<ActionResult<int>> InsertCubo(Cubo cubo) {
            return await this.repo.InsertCubo(cubo.Nombre, cubo.Marca, cubo.Imagen);
        } 

    }
}
