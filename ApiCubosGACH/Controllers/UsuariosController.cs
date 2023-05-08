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
    public class UsuariosController : ControllerBase {

        private RepositoryCubos repo;

        public UsuariosController(RepositoryCubos repo) { 
            this.repo = repo;
        }

        [HttpPost] [Route("[action]")]
        public async Task<ActionResult<int>> InsertUsuario(Usuario usuario) {
            return await this.repo.InsertUsuario(usuario.Nombre, usuario.Email, usuario.Password, usuario.Imagen);
        }

        [HttpGet] [Route("[action]")] [Authorize]
        public async Task<ActionResult<Usuario>> PerfilUsuario() {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            return usuario;
        }

    }
}
