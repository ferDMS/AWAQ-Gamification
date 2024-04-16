using System;
namespace APIQuieroSerBiomonitor
{
    public class Desafio : Fuente
    {
        public int XpFallar { get; set; }
        public List<Herramienta>? Herramientas { get; set; }
        public int DesafioId { get; set; }
    }
}

