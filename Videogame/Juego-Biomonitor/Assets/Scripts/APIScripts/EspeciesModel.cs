using System;

public class EspeciesModel
{
    public int FuenteId { get; set; }
    public int XpDesbloqueo { get; set; }
    public int XpExito { get; set; }
    public int EspecieId { get; set; }
    public string NombreEspecie { get; set; }
    public string NombreCientifico { get; set; }
    public int TipoEspecie { get; set; }
    public Herramienta herramienta { get; set; }
}

