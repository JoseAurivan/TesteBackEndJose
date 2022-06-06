using System.Collections.Generic;
using Domain.Interface;

namespace Domain
{
    public class Cliente:IEntity
    {
        public Cliente()
        {
            Locacoes = new List<Locacao>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Idade { get; set; }
        public ICollection<Locacao>? Locacoes { get; set; }
    }
}