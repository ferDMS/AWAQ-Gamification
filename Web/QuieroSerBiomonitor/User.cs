using System;
namespace QuieroSerBiomonitor
{
	public class User
	{
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Genero { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string Password;
        public DateTime? LastLogin { get; set; }
    }
}

