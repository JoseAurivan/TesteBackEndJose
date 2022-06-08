using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.DataStructure;
using Application.Enums;
using Application.Service.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TesteBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        

        public ClienteController(IClienteService clienteservice)
        {
            _clienteService = clienteservice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _clienteService.ListarClientes();
            if (result.Type == ServiceResultType.Success)
            {
                if (result is ServiceResult<List<Cliente>> resultado)
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    var jsonResult = new JsonResult(resultado.Result);
                    jsonResult.SerializerSettings = options;
                    return jsonResult;
                }
            }

            return BadRequest();
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> Get(string cpf)
        {
            var result = await _clienteService.ProcurarClienteCpf(cpf);
            if(result.Type == ServiceResultType.Success)
            {
                if(result is ServiceResult<Cliente> resultado)
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    };
                    var jsonResult = new JsonResult(resultado.Result);
                    jsonResult.SerializerSettings = options;
                    return jsonResult;
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            var resultado = await _clienteService.SalvarClienteAsync(cliente);
            if (resultado.Type == ServiceResultType.Success)
            {
                if (resultado is ServiceResult<int> result)
                {
                    return new JsonResult(result.Result);
                }
            }

            return BadRequest();

        }



    }
}
