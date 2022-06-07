using Application;
using Application.DataStructure;
using Application.Enums;
using Application.Service;
using Application.Service.Interfaces;
using Database;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteBackEnd.Controllers;
using Xunit;

namespace Tests
{
    public class ClienteControllerTest
    {
        private readonly ClienteController _clienteController;
        private readonly IClienteService _clienteService = new ClienteService(new Context());
        public ClienteControllerTest()
        {
            _clienteController = new ClienteController(_clienteService);
            
        }
        [Fact]
        public async void Test1()
        {
            
        }


    }
}