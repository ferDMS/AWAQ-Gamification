using System;
using APIQuieroSerBiomonitor;

namespace APIQuieroSerBiomonitor
{
    public class Especie : Fuente
    {
        public int EspecieId { get; set; }
        public string NombreEspecie { get; set; }
        public string NombreCientifico { get; set; }
        public TipoEspecie TipoEspecie { get; set; }
        public Herramienta Herramienta { get; set; }
    }
}

