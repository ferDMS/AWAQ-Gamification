using System;
namespace APIQuieroSerBiomonitor
{
    public class Desafio : Fuente
    {
        public int DesafioId { get; set; }
        public int XpDesbloqueo { get; set; }
        public int XpExito { get; set; }
        public int XpFallar { get; set; }
        public List<int>? HerramientaIds { get; set; } = new List<int>();
    }
}

