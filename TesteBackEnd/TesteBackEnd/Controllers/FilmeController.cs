using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Service.Interfaces;
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
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeService _filmeService;

        public FilmeController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _filmeService.ObterFilmesDisponíveis();
            if (result.Type == ServiceResultType.Success)
            {
                if (result is ServiceResult<List<Filme>> resultado)
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

        //Busca apenas filmes disponíveis
        [HttpGet("{codFilme:int}")]
        public async Task<IActionResult> Get(int codFilme)
        {
            var result = await _filmeService.BuscarFilmePorCodigo(codFilme);
            if(result.Type == ServiceResultType.Success)
            {
                if(result is ServiceResult<Filme> resultado)
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
        public async Task<IActionResult> Post(Filme filme)
        {
            var resultado = await _filmeService.SalvarFilmeAsync(filme);
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
