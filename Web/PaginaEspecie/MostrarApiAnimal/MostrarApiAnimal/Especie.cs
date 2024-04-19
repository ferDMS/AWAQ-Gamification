namespace MostrarApiAnimal
{
    public class Especie : Fuente
    {
        public int EspecieId { get; set; }
        public string NombreEspecie { get; set; }
        public string NombreCientifico { get; set; }
        public string? Descripcion {get; set;}
        public string? Url_img {get; set;}
        public string? Color {get; set;}
        public string? Tamagno {get; set;}
        public Region? Region {get; set;}
        public TipoEspecie? TipoEspecie { get; set; }
        public Herramienta Herramienta { get; set; }
    }
}

