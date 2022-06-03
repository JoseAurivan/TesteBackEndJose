using System.Threading.Tasks;
using Application.DataStructure;
using Application.Service.Interfaces;
using Domain;

namespace Application.Service
{
    public class FilmeService:IFilmeService
    {
        public Task<ServiceResult> SalvarFilmeAsync(Filme filme)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> ObterFilmes()
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> ObterFilmesDisponíveis(Status status)
        {
            throw new System.NotImplementedException();
        }
    }
}