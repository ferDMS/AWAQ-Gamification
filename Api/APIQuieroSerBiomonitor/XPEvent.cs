using System;
namespace APIQuieroSerBiomonitor
{
	public class XPEvent
    {
		
        public int UserId { get; set; }
            public int FuenteId { get; set; }
            public DateTime Fecha { get; set; }
            public bool IsSuccessful { get; set; }
    
    }
}
