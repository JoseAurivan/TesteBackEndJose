using Domain.Interface;

namespace Domain
{
    public class Filme:IEntity
    {
        public int Id { get; set; }
        public int codFilme { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        [System.Text.Json.Serialization.JsonIgnore] 
        public Locacao? Locacao { get; set; }
        public Status Status { get; set; }
    }
}