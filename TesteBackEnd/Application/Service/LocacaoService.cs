using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DataStructure;
using Application.Enums;
using Application.Service.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class LocacaoService : ILocacaoService
    {
        private readonly IContext _context;
        private readonly IFilmeService filmeService;
        private readonly IClienteService clienteService;

        public LocacaoService(IContext context, IFilmeService _filmeService, IClienteService _clienteService)
        {
            _context = context;
            filmeService = _filmeService;
            clienteService = _clienteService;
        }

        public async Task<ServiceResult> SalvarLocacao(Locacao locacao, int codFilme, string cpf)
        {
            try
            {
                return await TentarSalvarLocacao(locacao, codFilme, cpf);
            }
            catch (Exception e)
            {
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Erro ao fazer Locacao"}
                };
            }
        }

        private async Task<ServiceResult> TentarSalvarLocacao(Locacao locacao, int codFilme, string cpf)
        {
            if (locacao is null || codFilme is 0 || cpf is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Locacao nulo ou invalido"
                    }
                };
            }

            var cliente = await ValidarCpfAsync(cpf);
            var filme = await AlugarFilme(codFilme);
            if (cliente is not null && filme is not null)
            {
                cliente.Locacoes.Add(locacao);
                locacao.FilmeId = filme.Id;
                locacao.Cliente = cliente;
                locacao.Filme = filme;
                filme.Locacao = locacao;
                _context.Entry(cliente).State = EntityState.Modified;
                _context.Entry(filme).State = EntityState.Modified;
                _context.Clientes.Update(cliente);
                _context.Filmes.Update(filme);
                _context.Locacoes.Add(locacao);
                await _context.SaveChangesAsync();
                return new ServiceResult<int>(ServiceResultType.Success)
                {
                    Result = locacao.Id
                };
            }

            return new ServiceResult(ServiceResultType.NotValid)
            {
                Messages = new[]
                {
                    "Locacao impossibilitada"
                }
            };
        }

        public async Task<ServiceResult> ObterLocacoes()
        {
            var locacoes = await _context.Locacoes
                .Include(c => c.Cliente)
                .Include(c => c.Filme)
                .ToListAsync();
            if (locacoes is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Clientes inexistentes"
                    }
                };
            }

            return new ServiceResult<List<Locacao>>(ServiceResultType.Success)
            {
                Result = locacoes
            };
        }


        private async Task<Cliente> ValidarCpfAsync(string cpf)
        {
            var resultado = await clienteService.ProcurarClienteCpf(cpf);
            if (resultado.Type == ServiceResultType.Success)
            {
                if (resultado is ServiceResult<Cliente> result)
                {
                    return result.Result;
                }
            }

            return null;
        }

        private async Task<Filme> AlugarFilme(int codFilme)
        {
            var busca = await filmeService.BuscarFilmePorCodigo(codFilme);
            if (busca.Type == ServiceResultType.Success)
            {
                if (busca is ServiceResult<Filme> buscado)
                {
                    var resultado = await filmeService.IndisponibilizarFilme(buscado.Result);
                    if (resultado.Type == ServiceResultType.Success)
                    {
                        if (resultado is ServiceResult<Filme> result)
                        {
                            return result.Result;
                        }
                    }
                }
            }

            return null;
        }

        public async Task<ServiceResult> BuscarLocacaoCodigo(int codLocacao)
        {
            var locacao = await _context.Locacoes.Where(c => c.codLocacao == codLocacao).FirstOrDefaultAsync();
            if (locacao is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Locacao inexistente"
                    }
                };
            }

            return new ServiceResult<Locacao>(ServiceResultType.Success)
            {
                Result = locacao
            };
        }

        public async Task<ServiceResult> EncerrarLocacao(int codLocacao)
        {
            var result = await BuscarLocacaoCodigo(codLocacao);
            if (result.Type is ServiceResultType.Success)
            {
                if (result is ServiceResult<Locacao> resultado)
                {
                    var locacao = resultado.Result;
                    locacao.DataDeDevolucao = DateTime.Now;
                    if (DateTime.Compare((DateTime) locacao.DataDeDevolucao,locacao.DataEsperadaDeDevolucao) <= 0)
                        locacao.StatusLocacao = LocStatus.Entregue;
                    else
                        locacao.StatusLocacao = LocStatus.EntregueComAtraso;
                    
                    locacao.Filme.Status = Status.Disponivel;
                    _context.Entry(locacao.Filme).State = EntityState.Modified;
                    _context.Entry(locacao).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            return new ServiceResult(ServiceResultType.NotValid)
            {
                Messages = new[]
                {
                    "Locacao inexistente"
                }
            };
        }
    }
}