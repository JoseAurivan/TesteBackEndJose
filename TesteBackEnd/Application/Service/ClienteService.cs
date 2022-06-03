using System.Threading.Tasks;
using Application.DataStructure;
using Application.Service.Interfaces;
using Domain;

namespace Application.Service
{
    public class ClienteService: IClienteService
    {
        public Task<ServiceResult> SalvarClienteAsync(Cliente cliente)
        {
            throw new System.NotImplementedException();
        }
    }
}