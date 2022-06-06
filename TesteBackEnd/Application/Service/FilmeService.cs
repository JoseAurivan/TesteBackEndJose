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
    public class FilmeService : IFilmeService
    {
        private readonly IContext _context;

        public FilmeService(IContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult> SalvarFilmeAsync(Filme filme)
        {
            try
            {
                return await TentarSalvarFilme(filme);
            }
            catch (Exception e)
            {
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] { "Erro ao fazer Login" }
                };
            }
        }

        private async Task<ServiceResult> TentarSalvarFilme(Filme filme)
        {
            if (filme is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Filme nulo ou invalido"
                    }
                };
            }
            
            var result = await BuscarFilmePorCodigo(filme.codFilme);

            if (result is not null)
            {
                if (result is ServiceResult<Filme> resultado)
                {
                    return new ServiceResult<int>(ServiceResultType.Success)
                    {
                        Result = resultado.Result.Id
                    };
                }
            }


            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = filme.Id
            };
        }

        public async Task<ServiceResult> ObterFilmes()
        {
            var filmes = await _context.Filmes.ToListAsync();
            if (filmes is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Filmes inexistentes"
                    }
                };
            }

            return new ServiceResult<List<Filme>>(ServiceResultType.Success)
            {
                Result = filmes
            };
        }

        public async Task<ServiceResult> ObterFilmesDisponíveis()
        {
            var filmes = await _context.Filmes.Where(filme => filme.Status == Status.Disponivel).ToListAsync();
            if (filmes is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Filmes inexistentes"
                    }
                };
            }

            return new ServiceResult<List<Filme>>(ServiceResultType.Success)
            {
                Result = filmes
            };
        }

        public async Task<ServiceResult> BuscarFilmePorCodigo(int codFilme)
        {
            var filme = await _context.Filmes.Where(c => c.codFilme == codFilme).FirstOrDefaultAsync();
            if (filme is null)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Filmes inexistentes"
                    }
                };
            }

            return new ServiceResult<Filme>(ServiceResultType.Success)
            {
                Result = filme
            };
        }

        public async Task<ServiceResult> IndisponibilizarFilme(Filme filme)
        {
            if (filme.Status == Status.Indisponivel)
            {
                return new ServiceResult(ServiceResultType.NotValid)
                {
                    Messages = new[]
                    {
                        "Filme Locado"
                    }
                };
            }
            filme.Status = Status.Indisponivel;
            await _context.SaveChangesAsync();
            return new ServiceResult<Filme>(ServiceResultType.Success)
            {
                Result = filme
            };

        }
    }
}
