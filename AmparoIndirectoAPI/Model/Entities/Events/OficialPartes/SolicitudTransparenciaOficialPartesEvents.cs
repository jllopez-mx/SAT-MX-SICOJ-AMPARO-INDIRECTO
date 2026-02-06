using Sicoj.Utils;

namespace AmparoIndirectoAPI.Model.Entities.Events.OficialPartes
{
    public class SolicitudTransparenciaOficialPartesEvents
    {
        #region Crear registro de Solicitud de Transparencia
        public static SolicitudTransparencia CreateSolicitudTransparencia(
                int id,
                string numero_solicitud,
                DateTime fecha_solicitud,
                string usuario
       )
        {
            Guard.ValidateStringEmpty(ref numero_solicitud, "Numero Solicitud");
            SolicitudTransparencia entity = new()
            {
                id = id,
                numero_solicitud = numero_solicitud,
                fecha_solicitud = fecha_solicitud,
                usuario = usuario
            };
            return entity;
        }
        #endregion
    }
}
