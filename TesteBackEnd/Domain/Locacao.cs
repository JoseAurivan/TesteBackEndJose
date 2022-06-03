using Domain.Interface;

namespace Domain
{
    public class Locacao:IEntity
    {
        public int Id { get; set; }
        public Filme Filme { get; set; }
        public Cliente Cliente { get; set; }
        public string DataDeLocacao { get; set; }
        public string DataEsperadaDeDevolucao { get; set; }
        public string DataDeDevolucao { get; set; }
        public LocStatus StatusLocacao { get; set; }
    }
}