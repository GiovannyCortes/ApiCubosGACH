using ApiCubosGACH.Models;
using ApiCubosGACH.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiCubosGACH.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase {

        private RepositoryCubos repo;

        public PedidosController(RepositoryCubos repo) {
            this.repo = repo;
        }

        [HttpGet] [Route("[action]/{idUsuario}")] [Authorize]
        public async Task<ActionResult<List<Pedido>>> PedidosUsuario(int idUsuario) {
            return await this.repo.PedidosUsuario(idUsuario);
        }

        [HttpPost] [Route("[action]/{idCubo}")] 
        public async Task<int> InsertPedido(int idCubo) {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            return await this.repo.InsertPedido(usuario.IdUsuario, idCubo);
        }

    }
}
