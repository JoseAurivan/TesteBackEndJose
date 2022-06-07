using System;
using Domain.Interface;

namespace Domain
{
    public class Locacao:IEntity
    {
        public int Id { get; set; }
        public int codLocacao { get; set; }
        public Filme? Filme { get; set; }
        public int? FilmeId { get; set; }
        public Cliente? Cliente { get; set; }
        public DateTime DataDeLocacao { get; set; }
        public DateTime DataEsperadaDeDevolucao { get; set; }
        public DateTime? DataDeDevolucao { get; set; }
        public LocStatus StatusLocacao { get; set; }
    }
}