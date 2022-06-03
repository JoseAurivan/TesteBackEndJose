using System.Threading.Tasks;
using Application.DataStructure;
using Application.Service.Interfaces;
using Domain;

namespace Application.Service
{
    public class LocacaoService:ILocacaoService
    {
        public Task<ServiceResult> SalvarLocacao(Locacao locacao)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> ObterLocacoes()
        {
            throw new System.NotImplementedException();
        }
    }
}