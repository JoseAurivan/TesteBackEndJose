﻿using System;
using System.Collections.Generic;
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
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoService _locacaoService;

        public LocacaoController(ILocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _locacaoService.ObterLocacoes();
            if (result.Type == ServiceResultType.Success)
            {
                if (result is ServiceResult<List<Locacao>> resultado)
                {
                    return new JsonResult(resultado.Result);
                }
            }

            return BadRequest();
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost("{codFilme:int}")]
        public async Task<IActionResult> Post([FromBody] Locacao locacao, int codFilme, [FromHeader] string cpf)
        {
            if (locacao is not null || codFilme is not 0 || cpf is not null)
            {
                locacao.StatusLocacao = LocStatus.Locado;
                locacao.DataDeLocacao = DateTime.Now;
                locacao.DataEsperadaDeDevolucao = DateTime.Today.AddDays(3);
                var resultado = await _locacaoService.SalvarLocacao(locacao, codFilme, cpf);
                if (resultado.Type == ServiceResultType.Success)
                {
                    if (resultado is ServiceResult<int> result)
                    {
                        return new JsonResult(result.Result);
                    }
                }
            }

            return BadRequest();
        }

        //Método usado para encerrar a locacao
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        
    }
}