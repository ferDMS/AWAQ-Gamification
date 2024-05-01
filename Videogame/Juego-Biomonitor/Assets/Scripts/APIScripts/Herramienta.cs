using System;
using System.Collections.Generic;

[Serializable]
public class Herramienta
{
    public int HerramientaId { get; set; }
    public string NombreHerramienta { get; set; }
    public string Descripcion { get; set; }
    public int XpDesbloqueo { get; set; }
}

