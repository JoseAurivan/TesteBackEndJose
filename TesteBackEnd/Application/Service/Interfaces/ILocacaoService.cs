using System.Threading.Tasks;
using Application.DataStructure;
using Domain;

namespace Application.Service.Interfaces
{
    public interface ILocacaoService
    {
        Task<ServiceResult> SalvarLocacao(Locacao locacao, int codFilme, string cpf);
        Task<ServiceResult> ObterLocacoes();
        Task<ServiceResult> BuscarLocacaoCodigo(int codLocacao);
        Task<ServiceResult> EncerrarLocacao(int codLocacao);
    }
}