using AmparoIndirectoAPI.Model.ViewModels.Enums;
using Sicoj.Utils;

namespace AmparoIndirectoAPI.Model.Entities.Events.OficialPartes
{
    public class AmparoIndirectoOficialPartesEvents
    {
        #region Crear registro control documental
        public static AmparoIndirecto CreateAsuntoControlDocumental(
                        DateTime fecha_recepcion_demanda,
                        string numero_asunto,
                        //string juicio_amparo, se modifcio
                        int id_juzgado,
                        string nombre_quejoso,
                        string rfc_quejoso,
                        string usuario,
                        int id_administracion

        )
        {
            Guard.ValidateStringRfc(ref rfc_quejoso!, "RFC Quejoso", false);
            Guard.ValidateStringEmpty(ref nombre_quejoso, "Nombre Quejoso");


            AmparoIndirecto entity = new()
            {
                numero_asunto = numero_asunto,
                fecha_recepcion_demanda = fecha_recepcion_demanda,
                id_juzgado = id_juzgado,
                rfc_quejoso = rfc_quejoso,
                nombre_quejoso = nombre_quejoso,
                numero_empleado = usuario,
                id_estado_tarea = Convert.ToInt32(EnumEstadoTarea.PENDIENTE_DE_REGISTRO),
                id_estado_procesal = Convert.ToInt32(EnumEstadoProcesal.ACTIVO),
                id_administracion = id_administracion,
            };

            return entity;
        }
        #endregion
        #region Crear registro
        public static AmparoIndirecto CreateAmparoIndirecto(
                        string numero_asunto,
                        //string juicio_amparo, se modifico
                        string numero_expediente,
                        DateTime fecha_recepcion_demanda,
                        int id_juzgado,
                        bool contribuyente,
                        string rfc_quejoso,
                        string nombre_quejoso,
                        int id_materia,
                        int id_submateria,
                        int id_tipo_acto,
                        string despacho,
                        int id_autoridad_responsable,
                        int id_administracion_central,
                        string usuario


    )
        {
            Guard.ValidateStringRfc(ref rfc_quejoso!, "RFC Quejoso", false);
            Guard.ValidateStringEmpty(ref nombre_quejoso, "Nombre Quejoso");
            Guard.ValidateStringEmpty(ref despacho, "Despacho");


            AmparoIndirecto entity = new()
            {
                numero_asunto = numero_asunto,
                numero_expediente = numero_expediente,
                fecha_recepcion_demanda = fecha_recepcion_demanda,
                id_juzgado = id_juzgado,
                contribuyente = contribuyente,
                rfc_quejoso = rfc_quejoso,
                nombre_quejoso = nombre_quejoso,
                id_materia = id_materia,
                id_submateria = id_submateria,
                id_tipo_acto = id_tipo_acto,
                despacho = despacho,
                id_autoridad_responsable = id_autoridad_responsable,
                numero_empleado = usuario,
                id_estado_tarea = Convert.ToInt32(EnumEstadoTarea.PENDIENTE_DE_TURNAR),
                id_estado_procesal = Convert.ToInt32(EnumEstadoProcesal.ACTIVO),
                id_administracion = 1,
            };

            return entity;
        }
        #endregion
        #region
        public static AcumularDesacumular CreateAcumularDesacumular(
                        int id_juicio_padre,
                        List<int> id_juicio_acumulado,
                        string numero_oficio,
                        int id_administracion,
                        string usuario
        )
        {
            AcumularDesacumular entity = new()
            {
                id_juicio_padre = id_juicio_padre,
                id_juicio_acumulado = id_juicio_acumulado,
                numero_oficio = numero_oficio,
                id_administracion = id_administracion,
                usuario = usuario
            };

            return entity;
        }
        #endregion

        #region Actualizar registro
        public static AmparoIndirecto UpdateRegistroAI(
            int id_numero_asunto,
            string numero_asunto,
            //int id_juicio_amparo, se modifico
            //string juicio_amparo, se modifico
            string numero_expediente,
            DateTime fecha_recepcion_demanda,
            int id_juzgado,
            //string numero_oficio_segunda_pieza,
            string rfc_quejoso,
            string nombre_quejoso,
            int id_materia,
            int id_submateria,
            int id_tipo_acto,
            int id_administracion_central,
            int id_subadministracion
        )
        {
            Guard.ValidateStringRfc(ref rfc_quejoso!, "RFC Qejoso", false);
            Guard.ValidateStringEmpty(ref nombre_quejoso, "Nombre quejoso");


            AmparoIndirecto entity = new()
            {
                id = id_numero_asunto,
                numero_asunto = numero_asunto,
                numero_expediente = numero_expediente,
                fecha_recepcion_demanda = fecha_recepcion_demanda,
                id_juzgado = id_juzgado,
                //numero_oficio_segunda_pieza = numero_oficio_segunda_pieza,
                rfc_quejoso = rfc_quejoso,
                nombre_quejoso = nombre_quejoso,
                id_materia = id_materia,
                id_submateria = id_submateria,
                id_tipo_acto = id_tipo_acto,
                id_estado_tarea = Convert.ToInt32(EnumEstadoTarea.TURNAR),
                id_estado_procesal = Convert.ToInt32(EnumEstadoProcesal.ACTIVO),
                id_administracion = id_administracion_central,
                id_subadministracion = id_subadministracion,
            };

            return entity;
        }
        #endregion

        #region Crear Delinar Competencia
        public static DeclinarCompetencia CreateDeclinarCompetencia(
                        int id_numero_asunto,
                        //int id_juicio_amparo, se modifico
                        string numero_oficio,
                        DateTime fecha_recepcion_declinacion,
                        string numero_asunto_origen,
                        string numero_asunto_destino,
                        //string juicio_amparo_origen, se modifico
                        //string juicio_amparo_destino, se modifico
                        int id_juzgado_origen,
                        int id_juzgado_destino,
                        string usuario
        )
        {
            DeclinarCompetencia entity = new()
            {
                id_numero_asunto = id_numero_asunto,
                numero_oficio = numero_oficio,
                fecha_recepcion_declinacion = fecha_recepcion_declinacion,
                numero_asunto_origen = numero_asunto_origen,
                numero_asunto_destino = numero_asunto_destino,
                id_juzgado_origen = id_juzgado_origen,
                id_juzgado_destino = id_juzgado_destino,
                usuario = usuario
            };

            return entity;
        }
        #endregion

        #region Crear registro
        public static CanalizarAsunto CreateCanalizarAsunto(
                        DateTime fecha_canalizacion,
                        string numero_oficio_canalizacion,
                        int unidad_administrativa_canaliza,
                        int unidad_administrativa_recibe,
                        string motivo_canalizacion,
                        string usuario
        )
        {
            CanalizarAsunto entity = new()
            {
                fecha_canalizacion = fecha_canalizacion,
                numero_oficio_canalizacion = numero_oficio_canalizacion,
                unidad_administrativa_canaliza = unidad_administrativa_canaliza,
                unidad_administrativa_recibe = unidad_administrativa_recibe,
                motivo_canalizacion = motivo_canalizacion,
                usuario = usuario
            };

            return entity;
        }
        #endregion

        #region Acumular juicio

        public static AmparoIndirecto CreateAcumular(
            ref AmparoIndirecto entity
        )
        {
            //Guard.ValidateStringEmpty(ref nombre_quejoso, "Nombre quejoso");


            //ESTADO TAREA

            if (entity.id_estado_tarea == EnumEstadoTarea.PENDIENTE_DE_TURNAR.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea PENDIENTE DE TURNAR.");
            if (entity.id_estado_tarea == EnumEstadoTarea.POR_ASIGNAR.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea POR ASIGNAR.");
            if (entity.id_estado_tarea == EnumEstadoTarea.ASIGNADO.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea ASIGNADO.");
            if (entity.id_estado_tarea == EnumEstadoTarea.REASIGNADO.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea REASIGNADO.");
            if (entity.id_estado_tarea == EnumEstadoTarea.REACTIVADO.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea REACTIVAR.");
            if (entity.id_estado_tarea == EnumEstadoTarea.CLONCLUIDO.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea CONCLUIDO.");
            if (entity.id_estado_tarea == EnumEstadoTarea.ATENDIDO.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea ATENDIDO.");
            //if (entity.id_estado_tarea == EnumEstadoTarea.TURNAR.GetHashCode())
            //    throw new Exception("El Juicio se encuentra en estado de tarea TURNAR.");

            //ESTADO PROCESAL

            if (entity.id_estado_procesal == EnumEstadoProcesal.ACUMULADO.GetHashCode())
                throw new Exception("El Juicio ya ha sido ACUMULADO.");
            if (entity.id_estado_procesal == EnumEstadoProcesal.DECLINADO.GetHashCode())
                throw new Exception("El Juicio ya ha sido DECLINADO.");
            if (entity.id_estado_procesal == EnumEstadoProcesal.REACTIVADO.GetHashCode())
                throw new Exception("El Juicio ya ha sido REACTIVADO.");
            if (entity.id_estado_procesal == EnumEstadoProcesal.EN_REPARACION.GetHashCode())
                throw new Exception("El Juicio ya ha sido EN REPARACIÓN.");
            if (entity.id_estado_procesal == EnumEstadoProcesal.CONCLUIDO.GetHashCode())
                throw new Exception("El Juicio ya ha sido CONCLUIDO.");

            //AmparoIndirecto entity = new()
            //{
            //    id = id,
            //    id_abogado = rfcAbogado,
            //    numero_empleado = "EMPLEADO01",
            //    motivo_reasignacion = motivo_reasignacion,
            //};

            return entity;
        }

        #endregion

        #region Envío e-mail
        public static Email CreaCorreo(
        List<string> to,
         List<string> Cc,
         string Subject,
         bool IsHtml,
         string Body

     )
        {

            Email entity = new()
            {
                To = to,
                Cc = Cc,
                Subject = Subject,
                IsHtml = IsHtml,
                Body = Body,
                Priority = (System.Net.Mail.MailPriority?)1,
            };


            return entity;
        }

        #endregion

    }
}
