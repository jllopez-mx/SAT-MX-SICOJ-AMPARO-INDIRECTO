using System;

namespace AmparoIndirectoAPI.Model.DTO
{
    public class RequestCreateRecursoInconformidadConstitucional
    {
        //notificacion_admision_recurso_inconformidad Date
        //numero_recurso_inconformidad Text
        //id_organo_radicacion_inconformidad Integer
        //fecha_notificacion_resolucion_inconformidad Date
        //id_sentido_resolucion_inconformidad Integer

        public int id { get; set; } 
        //public int id_autoridad_responsable { get; set; }
        public string notificacionAdmisionRecursInconformidad { get; set; } = null!;
        public string numeroRecursoInconformidad { get; set; } = null!;
        public int idOrganoRadicacioInconformidad { get; set; }
        public string fechaNotificacionResolucionInconformidad { get; set; } = null!;
        public int idSentidoResolucionInconformidad { get; set; }
        public IFormFile? documento { get; set; }
        public int? idTipoArchivo { get; set; }
        public int? idSeccion { get; set; }


    }
}
