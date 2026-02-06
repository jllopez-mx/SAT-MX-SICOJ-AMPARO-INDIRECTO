using System;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateSuspensionProvisionalAbogado
    {

        //id_juicio_amparo integer,
        //suspension_provisional boolean,
        //otorgamiento_garantia text,
        //oficio_comunicacion text,
        //fecha_comunicacion timestamp with time zone,
        //fecha_creacion timestamp without time zone,
        //fecha_modificacion timestamp without time zone,
        //usuario_modificacion text,
        //activo boolean NOT NULL DEFAULT true
        public int id { get; set; } 
        public bool? suspension_provisional { get; set; } 
        public int? otorgamiento_garantia { get; set; } 
        public string? oficio_comunicacion { get; set; } = null!;

        public string? fecha_comunicacion { get; set; } = null!;
        //public string fecha_creacion { get; set; } = null!;
        //public string fecha_modificacion {  get; set; } = null!;
        //public string usuario_modificacion { get; set; } = null!;
        //public string id_estado_procesal { get; set; } = null!;
        //public bool activo {  get; set; }
    }
}
