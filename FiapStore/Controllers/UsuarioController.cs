using FiapStore.DTO;
using FiapStore.Entities;
using FiapStore.Enums;
using FiapStore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapStore.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }
        /// <summary>
        /// Obter Todos os Uaurios com pedidos
        /// </summary>
        /// <param name="id"></param>
        /// <remarks> Exemplo : Enviar Id para requisição</remarks>
        /// <returns></returns>
        [Authorize]
        [Authorize(Roles = Permissoes.Funcionario)]
        [HttpGet("obter-todos-com-pedidos/{id}")]
        public IActionResult ObterTodosComPedidos(int id)
        {
            _logger.LogInformation("Obter com todos com pedidos iniciado");
            var usuario = _usuarioRepository.ObterComPedidos(id);
            return Ok(usuario);
        }

        [Authorize]
        [Authorize(Roles = Permissoes.Administrador)]
        [HttpGet("obter-todos-usuario")]
        public IActionResult ObterTodosUsuario()
        {
            try
            {
                var usuario = _usuarioRepository.ObterTodos();

                return Ok(usuario);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);

                return BadRequest();
            }

        }

        [Authorize]
        [HttpGet("obter-por-usuario-id/{id}")]
        public IActionResult ObterUsuarioPorId([FromRoute] int id)
        {
            _logger.LogDebug($"Log Debug iniciado  - {DateTime.Now}");
            _logger.LogWarning($"Log Warning iniciado - {DateTime.Now}");
            var usuario = _usuarioRepository.ObterPorId(id);
            return Ok(usuario);
        }

        [Authorize]
        [Authorize(Roles = $"{Permissoes.Administrador},{Permissoes.Funcionario}")]
        [HttpPost()]
        public IActionResult CriarUsuario([FromBody] CadastrarUsuarioDTO usuarioDto)
        {
            _usuarioRepository.Cadastrar(new Usuario(usuarioDto));

            _logger.LogWarning($"Log Warning iniciado - {DateTime.Now}");
            _logger.LogWarning($"Usuario criado com sucesso - Nome: {usuarioDto.Nome} - {DateTime.Now}");

            return Ok("Usuário criado com sucesso");

        }

        [HttpPut()]
        public IActionResult AlterarUsuario([FromBody] AlterarUsuarioDTO usuarioDto)
        {
            _usuarioRepository.Alterar(new Usuario(usuarioDto));
            return Ok("Usuario alterado com sucesso");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario([FromRoute] int id)
        {
            _usuarioRepository.Deletar(id);
            return Ok($"Usuario deletado com sucesso - {DateTime.Now}");
        }
    }
}

