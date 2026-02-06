using AmparoIndirectoAPI.Model.ViewModels.Enums;
using Sicoj.Utils;

namespace AmparoIndirectoAPI.Model.Entities.Events
{
    public class AmparoIndirectoAdministradorEvents
    {
        #region Actualizar registro Administrador
        public static AmparoIndirecto UpdateRegistroAdministradorAI(
            int id_numero_asunto,
            string numero_asunto,
            //int id_juicio_amparo, se modifico
            //string juicio_amparo, se modifcio
            string numero_expediente,
            DateTime fecha_recepcion_demanda,
            //DateTime fecha_vencimiento,
            int id_juzgado,
            string rfc_quejoso,
            string nombre_quejoso,
            int id_materia,
            int id_submateria,
            int id_tipo_acto,
            string despacho
        )
        {
            Guard.ValidateStringEmpty(ref nombre_quejoso, "Nombre quejoso");


            AmparoIndirecto entity = new()
            {
                id = id_numero_asunto,
                numero_asunto = numero_asunto,
                numero_expediente = numero_expediente,
                fecha_recepcion_demanda = fecha_recepcion_demanda,
                //fecha_vencimiento = fecha_vencimiento,
                id_juzgado = id_juzgado,
                rfc_quejoso = rfc_quejoso,
                nombre_quejoso = nombre_quejoso,
                id_materia = id_materia,
                id_submateria = id_submateria,
                id_tipo_acto = id_tipo_acto,
                //id_estado_tarea = Convert.ToInt32(EnumEstadoTarea.TURNAR),
                //id_estado_procesal = Convert.ToInt32(EnumEstadoProcesal.ACTIVO),
                despacho = despacho,
            };

            return entity;
        }
        #endregion

        #region Actualizar Asignar
        public static AmparoIndirecto UpdateAsignar(
            ref AmparoIndirecto entityExist,
            string rfcAbogado
        )
        {
            //Guard.ValidateStringEmpty(ref nombre_quejoso, "Nombre quejoso");


            AmparoIndirecto entity = new()
            {
                id = entityExist.id,
                id_abogado = rfcAbogado,
                id_subadministracion = entityExist.id_subadministracion,
                id_administracion = entityExist.id_administracion,
                numero_asunto = entityExist.numero_asunto,
                numero_empleado = rfcAbogado,
                id_estado_procesal = EnumEstadoProcesal.ACTIVO.GetHashCode(),
                id_estado_tarea = EnumEstadoTarea.ASIGNADO.GetHashCode(),
            };

            return entity;
        }
        #endregion


        #region Actualizar Reasignar
        public static AmparoIndirecto UpdateReasignar(
            ref AmparoIndirecto entity,
            string rfcAbogado,
            string motivo_reasignacion
        )
        {
            //Guard.ValidateStringEmpty(ref nombre_quejoso, "Nombre quejoso");

            if (entity.id_abogado == rfcAbogado)
                throw new Exception("El Abogado a reasignar es el mismo.");

            //ESTADO TAREA

            if (entity.id_estado_tarea == EnumEstadoTarea.PENDIENTE_DE_TURNAR.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea PENDIENTE DE TURNAR.");
            if (entity.id_estado_tarea == EnumEstadoTarea.POR_ASIGNAR.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea POR ASIGNAR.");
            if (entity.id_estado_tarea == EnumEstadoTarea.REACTIVADO.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea REACTIVAR.");
            if (entity.id_estado_tarea == EnumEstadoTarea.CLONCLUIDO.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea CONCLUIDO.");
            if (entity.id_estado_tarea == EnumEstadoTarea.ATENDIDO.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea ATENDIDO.");
            if (entity.id_estado_tarea == EnumEstadoTarea.TURNAR.GetHashCode())
                throw new Exception("El Juicio se encuentra en estado de tarea TURNAR.");

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
    }
}
