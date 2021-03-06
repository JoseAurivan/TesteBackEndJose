using System.Threading.Tasks;
using Application.DataStructure;
using Domain;

namespace Application.Service.Interfaces
{
    public interface IFilmeService
    {
        Task<ServiceResult> SalvarFilmeAsync(Filme filme);
        Task<ServiceResult> ObterFilmes();
        Task<ServiceResult> ObterFilmesDisponíveis();
        Task<ServiceResult> BuscarFilmePorCodigo(int codFilme);
        Task<ServiceResult> IndisponibilizarFilme(Filme filme);
    }
}