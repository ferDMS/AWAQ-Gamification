using System;
namespace APIQuieroSerBiomonitor
{
    public class Especie : Fuente
    {
        public string NombreEspecie { get; set; }
        public string NombreCientifico { get; set; }
        public Region Region { get; set; }
        public TipoEspecie TipoEspecie { get; set; }
        public Herramienta Herramienta { get; set; }
    }
}

