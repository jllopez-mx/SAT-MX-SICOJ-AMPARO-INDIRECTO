using System;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateRecursoReclamacionConstitucional
    {
        //recurso_reclamacion Boolean
        //notificacion_adminsion_recurso_reclamacion Date
        //fecha_vencimiento_recurso_reclamacion Date
        //fecha_presentacion Date
        //oficio_reclamacion Text
        //numero_recurso Text
        //id_organo_radicacion Integer
        //fecha_notificacion_resolucion Date
        //id_sentido_resolucion_reclamacion Integer


        public int id { get; set; }
        //public int id_autoridad_responsable { get; set; }
        public bool recursoReclamacion { get; set; } 
        public string? fechaNotificacionAcuerdo { get; set; } = null!;
        //public string? fecha_vencimiento_recurso_reclamacion { get; set; } = null!;
        public string? fechaPresentacion { get; set; } = null!;
        public string? oficioReclamacion { get; set; } = null!;
        public string? notificacionAdmisionRecursoReclamacion { get; set; } = null!;
        public string? numeroRecurso { get; set; } = null!;
        public int? idOrganoRadicacion { get; set; }
        public string? fechaNotificacionResolucion { get; set; } = null!;
        public int? idSentidoResolucionReclamacion { get; set; }
        public IFormFile? documento { get; set; }
        public int? idTipoArchivo { get; set; }
        public int? idSeccion { get; set; }

    }
}
