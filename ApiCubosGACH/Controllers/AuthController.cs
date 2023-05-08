using ApiCubosGACH.Helpers;
using ApiCubosGACH.Models;
using ApiCubosGACH.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCubosGACH.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private RepositoryCubos repo;
        private HelperOAuthToken helper;

        public AuthController(RepositoryCubos repo, HelperOAuthToken helper) {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost] [Route("[action]")]
        public async Task<ActionResult> LogIn(LogInModel model) {
            Usuario? usuario = await this.repo.LogIn(model.Email, model.Password);
            if (usuario == null) {
                return Unauthorized();
            } else {
                SigningCredentials credentials = new SigningCredentials(this.helper.GetToken(), SecurityAlgorithms.HmacSha256);

                string jsonUsuario = JsonConvert.SerializeObject(usuario);
                Claim[] information = new[] {
                    new Claim("UserData", jsonUsuario)
                };

                JwtSecurityToken token = new JwtSecurityToken(
                    claims: information,
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    notBefore: DateTime.UtcNow
                );

                return Ok(new {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }

    }
}
