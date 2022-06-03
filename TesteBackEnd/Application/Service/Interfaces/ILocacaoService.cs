using System.Threading.Tasks;
using Application.DataStructure;
using Domain;

namespace Application.Service.Interfaces
{
    public interface ILocacaoService
    {
        Task<ServiceResult> SalvarLocacao(Locacao locacao);
        Task<ServiceResult> ObterLocacoes(); 
    }
}