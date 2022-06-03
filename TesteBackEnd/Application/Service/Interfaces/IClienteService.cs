using System.Threading.Tasks;
using Application.DataStructure;
using Domain;

namespace Application.Service.Interfaces
{
    public interface IClienteService
    {
        Task<ServiceResult> SalvarClienteAsync(Cliente cliente);
    }
}