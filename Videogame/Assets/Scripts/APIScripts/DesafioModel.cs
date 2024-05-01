using System;
using System.Collections.Generic;

[Serializable]
public class DesafioModel
{
    public int fuenteId;
    public int desafioId;
    public int xpDesbloqueo;
    public int xpExito;
    public int xpFallar;
    public List<int> herramientaIds;
}
