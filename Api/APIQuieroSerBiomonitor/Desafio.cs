using System;
namespace APIQuieroSerBiomonitor
{
    public class Desafio : Fuente
    {
        public int DesafioId { get; set; }
        public List<int>? HerramientaIds { get; set; } = new List<int>();
    }
}

