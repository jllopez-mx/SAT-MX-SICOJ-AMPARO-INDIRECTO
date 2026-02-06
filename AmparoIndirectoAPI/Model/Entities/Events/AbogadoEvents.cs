using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.ViewModels.Enums;
using Microsoft.IdentityModel.Tokens;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.Entities.Events
{
    public class AbogadoEvents
    {

        #region INFORMACION ADICIONAL

        public static InformacionAdicional CreateInformacionAdicional(
        int? id,
        DateTime? fecha_resolucion_oficio,
        decimal? cuantia,
        string? oficio_resolucion_reclamado,
        int? id_autoridad_emisora_resolucion_oficio,
        List<int> concepto_violacion,
        string? usuario
        )
        {
            string cadena_concepto_violacion = string.Join(",", concepto_violacion);
            InformacionAdicional entity = new()
            {
                id = id,
                fecha_resolucion_oficio = fecha_resolucion_oficio,
                cuantia = cuantia,
                oficio_resolucion_reclamado = oficio_resolucion_reclamado,
                id_autoridad_emisora_resolucion_oficio = id_autoridad_emisora_resolucion_oficio,
                concepto_violacion = cadena_concepto_violacion,
                usuario = usuario

            };

            return entity;
        }

        #endregion

        public static RequestCreateAutoridadResponsableAbogado CreateAutoridadesResponsables(
        ref RequestCreateAutoridadResponsableAbogado idAutoridades,
        ref AmparoIndirecto AmparoIndirectoExists,
        ref List<AutoridadesResponsables> entityAutoridadesExists
)
        {
            for (int i = 0; i < entityAutoridadesExists.Count; i++)
            {
                if (idAutoridades.ids_autoridades_responsables.Contains(entityAutoridadesExists[i].id_autoridad_responsable))
                {
                    throw new Exception("La autoridad a registrar ya existe");
                }
            }

            return idAutoridades;
        }
        public static NotaLitigio CreateNotaLitigio(
            int id_numero_asunto,
            //int id_juicio_amparo, se modifico
            DateTime fecha_registro_nota,
            int id_seccion,
            int id_tipo_documento,
            string usuario
        //ref List<ResponseInformeJustificadoConstitucional> entityCuadernoAutoridadesExists
        )
        {
            NotaLitigio entity = new()
            {
                id_numero_asunto = id_numero_asunto, //se modifico
                fecha_registro_nota = fecha_registro_nota,
                id_tipo_documento = id_tipo_documento,
                id_seccion = id_seccion,
                usuario = usuario
            };

            return entity;
        }
        //EVENTS CONSTITUCIONAL

        public static InformeJustificadoConstitucional CreateInformeJustificado(
            int id,
            int id_numero_asunto,
            //int id_juicio_amparo,se modifico
            int id_autoridad_responsable,
            bool? solucion_opinion_tecnica,
            DateTime fecha_recepcion_demanda,
            DateTime fecha_vencimiento_justificado,
            string numero_oficio_informe_justificado,
            DateTime? fecha_oficio_informe_justificado,
            DateTime fecha_presentacion_informe_justificado,
            string? observaciones_justificado,
            string? usuario
        //ref List<ResponseInformeJustificadoConstitucional> entityCuadernoAutoridadesExists
        )
        {
            InformeJustificadoConstitucional entity = new()
            {
                id = id,
                id_numero_asunto = id_numero_asunto, //se modifico
                id_autoridad_responsable = id_autoridad_responsable,
                solicitud_opinion_tecnica = solucion_opinion_tecnica,
                fecha_recepcion_demanda = fecha_recepcion_demanda,
                fecha_vencimiento_justificado = fecha_vencimiento_justificado,
                numero_oficio_informe_justificado = numero_oficio_informe_justificado,
                fecha_oficio_informe_justificado = fecha_oficio_informe_justificado,
                fecha_presentacion_informe_justificado = fecha_presentacion_informe_justificado,
                observaciones_justificado = observaciones_justificado,
                usuario = usuario,
            };

            return entity; 
        }

        public static SentenciaConstitucional CreateSentenciaConstitucional(
            int id,
            int id_autoridad_responsable,
            DateTime? fecha_notificacion_sentencia,
            int? id_sentido_sentencia,
            int? id_tipo_sentido_sentencia,
            int? id_dictamen_no_revision,
            int? id_sentido_general_asunto,
            int? id_tipo_sentido_general,
            string? oficio_comunicacion_autoridad,
            DateTime? fecha_oficio_comunicacion_sentencia,
            DateTime? fecha_presentacion_oficio_comunicacion,
            DateTime? fecha_recepcion_auto_sentencia_ejecutoria,
            string? comunicado_acuerdo_firmeza,
            DateTime? fecha_comunicacion_acuerdo_firmeza,
            DateTime? fecha_conclusion_expediente,
            string? usuario

        )
        {
            SentenciaConstitucional entity = new()
            {
                id = id,
                id_autoridad_responsable = id_autoridad_responsable,
                fecha_notificacion_sentencia = fecha_notificacion_sentencia,
                id_sentido_sentencia = id_sentido_sentencia,
                id_tipo_sentido_sentencia = id_tipo_sentido_sentencia,
                id_dictamen_no_revision = id_dictamen_no_revision,
                id_sentido_general_asunto = id_sentido_general_asunto,
                id_tipo_sentido_general = id_tipo_sentido_general,
                oficio_comunicacion_autoridad = oficio_comunicacion_autoridad,
                fecha_oficio_comunicacion_sentencia = fecha_oficio_comunicacion_sentencia,
                fecha_presentacion_oficio_comunicacion = fecha_presentacion_oficio_comunicacion,
                fecha_recepcion_auto_sentencia_ejecutoria = fecha_recepcion_auto_sentencia_ejecutoria,
                comunicado_acuerdo_firmeza = comunicado_acuerdo_firmeza,
                fecha_comunicacion_acuerdo_firmeza = fecha_comunicacion_acuerdo_firmeza,
                fecha_conclusion_expediente = fecha_conclusion_expediente,
                usuario = usuario,
            };

            return entity;
        }

        public static RecursoQuejaPrincipalConstitucional CreateRecursoQuejaPrincipalConstitucional(
            int id,
            int id_numero_asunto,
            //int id_juicio_amparo, //SE AGREGA CAMPO PARA ACTUALIZAR se modifico
            int? id_recurrente,
            bool? recurso_queja_principal,
            int? id_autoridad_responsable,
            DateTime fecha_recepcion_acuerdo_queja,
            //DateTime? fecha_vencimiento_recurso_queja,
            string? oficio_recurso_queja,
            DateTime? fecha_presentacion_queja,
            int? id_motivo_recurso_queja,
            DateTime? fecha_admision_queja,
            int? id_organo_radicacion_queja,
            string? toca_queja,
            DateTime? fecha_notificacion_ejecutoria,
            int? id_sentido_resolucion_queja,
            string? oficio_comunicacion_area_correspondiente,
            DateTime? fecha_oficio_comunicacion,
            string? usuario
        )
        {
            RecursoQuejaPrincipalConstitucional entity = new()
            {

                id = id,
                id_numero_asunto = id_numero_asunto, //SE AGREGA CAMPO PARA ACTUALIZAR //se modifico
                id_recurrente = id_recurrente,
                recurso_queja_principal = recurso_queja_principal,
                id_autoridad_responsable = id_autoridad_responsable,
                fecha_recepcion_acuerdo_queja = fecha_recepcion_acuerdo_queja,
                //fecha_vencimiento_recurso_queja = fecha_vencimiento_recurso_queja,
                oficio_recurso_queja = oficio_recurso_queja,
                fecha_presentacion_queja = fecha_presentacion_queja,
                id_motivo_recurso_queja = id_motivo_recurso_queja,
                fecha_admision_queja = fecha_admision_queja,
                id_organo_radicacion_queja = id_organo_radicacion_queja,
                toca_queja = toca_queja,
                fecha_notificacion_ejecutoria = fecha_notificacion_ejecutoria,
                id_sentido_resolucion_queja = id_sentido_resolucion_queja,
                oficio_comunicacion_area_correspondiente = oficio_comunicacion_area_correspondiente,
                fecha_oficio_comunicacion = fecha_oficio_comunicacion,
                usuario = usuario



            };

            return entity;
        }


        public static RecursoRevisionPrincipalConstitucional CreateRecursoRevisionPrincipalConstitucional(
            int id,
            int? id_recurrente,
            bool? recurso_revision_principal,
            int id_autoridad_responsable,
            DateTime fecha_recepcion_sentencia,
            //DateTime? fecha_vencimiento_recurso_revision,
            string? oficio_recurso,
            DateTime? fecha_presentacion_revision,
            DateTime fecha_adminision,
            int? id_organo_radicacion_revision,
            string? toca_revision,
            bool? revision_adhesiva,
            string? oficio_revision_adhesiva,
            //DateTime? fecha_vencimiento_adhesion,
            DateTime? fecha_notificacion_ejecutoria_revision,
            int? id_sentido_resolucion_revision,
            int? id_tipo_sentido_revision,
            int? id_sentido_general,
            int? id_tipo_sentido_general_revision,
            string? oficio_comunicacion_autoridad_revision,
            DateTime? fecha_oficio_comunicacion_revision,
            string? usuario
        )
        {
            RecursoRevisionPrincipalConstitucional entity = new()
            {

                id = id,
                id_recurrente = id_recurrente,
                id_autoridad_responsable = id_autoridad_responsable,
                recurso_revision_principal = recurso_revision_principal,
                fecha_recepcion_sentencia = fecha_recepcion_sentencia,
                //fecha_vencimiento_recurso_revision = fecha_vencimiento_recurso_revision,
                oficio_recurso = oficio_recurso,
                fecha_presentacion_revision = fecha_presentacion_revision,
                fecha_admision = fecha_adminision,
                id_organo_radicacion_revision = id_organo_radicacion_revision,
                toca_revision = toca_revision,
                revision_adhesiva = revision_adhesiva,
                oficio_revision_adhesiva = oficio_revision_adhesiva,
                //fecha_vencimiento_adhesion = fecha_vencimiento_adhesion,
                fecha_notificacion_ejecutoria_revision = fecha_notificacion_ejecutoria_revision,
                id_sentido_resolucion_revision = id_sentido_resolucion_revision,
                id_tipo_sentido_revision = id_tipo_sentido_revision,
                id_sentido_general = id_sentido_general,
                id_tipo_sentido_general_revision = id_tipo_sentido_general_revision,
                oficio_comunicacion_autoridad_revision = oficio_comunicacion_autoridad_revision,
                fecha_oficio_comunicacion_revision = fecha_oficio_comunicacion_revision,
                usuario = usuario

            };

            return entity;
        }


        //        public static bool UpdateRecursoQuejaConstitucional(
        //        ref List<AutoridadesResponsables> entityAutoridadesExists,
        //        ref List<RequestCuadernoConstitucional> entityCuadernoAutoridadesExists
        //)
        //        {
        //            if (entityCuadernoAutoridadesExists.Select() )
        //            {

        //            }
        //            //var idsAutoridadesCuaderno = entityCuadernoAutoridadesExists
        //            //    .Select(r => r.id_autoridad_responsable.Value.GetValueOrDefault())
        //            //    .ToList();

        //            //var idsAutoridadesResponsables = entityAutoridadesExists
        //            //    .Select(r => r.id_autoridad_responsable)
        //            //    .ToList();
        //            ////Obtengo los que no existen
        //            //var valoresNoExistentes = idsAutoridadesCuaderno.Except(idsAutoridadesResponsables).ToList();

        //            var idsConValoresNull = entityCuadernoAutoridadesExists
        //                .Where(o =>
        //                !o.id_autoridad_responsable.Value.HasValue || // Filtra aquellos con id_autoridad_responsable null
        //                string.IsNullOrWhiteSpace(o.fecha_recepcion_demanda) ||
        //                string.IsNullOrWhiteSpace(o.fecha_vencimiento_justificado) ||
        //                string.IsNullOrWhiteSpace(o.numero_oficio_informe_justificado) ||
        //                string.IsNullOrWhiteSpace(o.fecha_presentacion_informe_justificado) ||
        //                string.IsNullOrWhiteSpace(o.observaciones_justificado)
        //            )
        //            .Select(o => new
        //            {
        //                IdAutoridadResponsable = o.id_autoridad_responsable,
        //            })
        //            .ToList();
        //            string autoridadesVacias = string.Empty;
        //            foreach (var item in idsConValoresNull)
        //            {
        //                if (item.IdAutoridadResponsable != null)
        //                {
        //                    autoridadesVacias += item.IdAutoridadResponsable.Label += "'\r\n'";
        //                }
        //            }

        //            if (idsConValoresNull.Any())
        //            {
        //                throw new Exception($"No se puede finalizar el registro porque:  {autoridadesVacias.Trim()} no han terminado de llenar la sentencia constitucional.");
        //            }
        //            bool entity = true;
        //            return entity;
        //        }
        public static bool UpdateInformeJustificadoConstitucional(
        ref List<AutoridadesResponsables> entityAutoridadesExists,
        ref List<ResponseInformeJustificadoConstitucional> entityCuadernoAutoridadesExists
)
        {

            //var idsAutoridadesCuaderno = entityCuadernoAutoridadesExists
            //    .Select(r => r.id_autoridad_responsable.Value.GetValueOrDefault())
            //    .ToList();

            //var idsAutoridadesResponsables = entityAutoridadesExists
            //    .Select(r => r.id_autoridad_responsable)
            //    .ToList();
            ////Obtengo los que no existen
            //var valoresNoExistentes = idsAutoridadesCuaderno.Except(idsAutoridadesResponsables).ToList();

            var idsConValoresNull = entityCuadernoAutoridadesExists
            .Where(o =>
                o.id_autoridad_responsable == 0 && // Validar solo cuando autorizado sea true
                (
                    //!o.id_autoridad_responsable.Value.HasValue || // Filtra aquellos con id_autoridad_responsable null
                    string.IsNullOrWhiteSpace(o.fecha_recepcion_demanda) ||
                    string.IsNullOrWhiteSpace(o.fecha_vencimiento_justificado) ||
                    string.IsNullOrWhiteSpace(o.numero_oficio_informe_justificado) ||
                    string.IsNullOrWhiteSpace(o.fecha_presentacion_informe_justificado) ||
                    string.IsNullOrWhiteSpace(o.observaciones_justificado)
                )
            )
            .Select(o => new
            {
                IdAutoridadResponsable = o.id_autoridad_responsable,
            })
            .ToList();

            string autoridadesVacias = string.Empty;
            foreach (var item in idsConValoresNull)
            {
                if (item.IdAutoridadResponsable.GetHashCode != null)
                {
                    autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'"; // Concatenamos las autoridades que faltan
                }
            }

            if (idsConValoresNull.Any())
            {
                throw new Exception($"No se puede finalizar el registro porque:  {autoridadesVacias.Trim()} no han terminado de llenar el Informe Justificado del cuaderno Constitucional.");
            }

            bool entity = true;
            return entity;
        }


        public static bool UpdateSentenciaConstitucional(
            ref List<ResponseSentenciaConstitucional> entityCuadernoAutoridadesExists
        )
        {
            //var idsAutoridadesCuaderno = entityCuadernoAutoridadesExists
            //    .Select(r => r.id_autoridad_responsable.Value.GetValueOrDefault())
            //    .ToList();

            //var idsAutoridadesResponsables = entityAutoridadesExists
            //    .Select(r => r.id_autoridad_responsable)
            //    .ToList();
            ////Obtengo los que no existen
            //var valoresNoExistentes = idsAutoridadesCuaderno.Except(idsAutoridadesResponsables).ToList();

            //var idsConValoresNull = entityCuadernoAutoridadesExists
            //    .Where(o =>
            //    !o.id_autoridad_responsable.Value.HasValue || // Filtra aquellos con id_autoridad_responsable null
            //    !o.id_sentido_sentencia.Value.HasValue || o.id_sentido_sentencia.Value <= 0 ||
            //    //!o.id_dictamen_no_revision.Value.HasValue || o.id_dictamen_no_revision.Value <= 0 ||
            //    !o.id_tipo_sentido_sentencia.Value.HasValue || o.id_tipo_sentido_sentencia.Value <= 0 ||
            //    string.IsNullOrWhiteSpace(o.fecha_notificacion_sentencia) ||
            //    string.IsNullOrWhiteSpace(o.fecha_conclusion_expediente)
            //)
            //.Select(o => new
            //{
            //    IdAutoridadResponsable = o.id_autoridad_responsable,
            //})
            //.ToList();
            //string autoridadesVacias = string.Empty;
            //foreach (var item in idsConValoresNull)
            //{
            //    if (item.IdAutoridadResponsable != null)
            //    {
            //        autoridadesVacias += item.IdAutoridadResponsable.Label += "'\r\n'";
            //    }
            //}

            //if (idsConValoresNull.Any())
            //{
            //    throw new Exception($"No se puede finalizar el registro porque:  {autoridadesVacias.Trim()} no han terminado de llenar la sentencia constitucional.");
            //}
            bool tieneFechaConclusionValida = entityCuadernoAutoridadesExists
            .Any(o => !string.IsNullOrWhiteSpace(o.fecha_conclusion_expediente)); // Comprobamos si algún registro tiene fecha_conclusion_expediente no vacía

            // Si no hay ninguna fecha válida, lanzar el mensaje de error
            if (!tieneFechaConclusionValida)
            {
                throw new Exception("No se puede concluir el juicio de amparo indirecto porque no se ha capturado la fecha de conclusión del expediente.");
            }
            return tieneFechaConclusionValida;
        }

        //Esta función sirve para el end-point validación
        //public static bool UpdateCumplimientoFalloProtector(
        //    ref List<AutoridadesResponsables> entityAutoridadesExists,
        //    ref List<RequestCuadernoConstitucional> entityCuadernoAutoridadesExists
        //)
        //{
        //    //var idsAutoridadesCuaderno = entityCuadernoAutoridadesExists
        //    //    .Select(r => r.id_autoridad_responsable.Value.GetValueOrDefault())
        //    //    .ToList();

        //    //var idsAutoridadesResponsables = entityAutoridadesExists
        //    //    .Select(r => r.id_autoridad_responsable)
        //    //    .ToList();
        //    ////Obtengo los que no existen
        //    //var valoresNoExistentes = idsAutoridadesCuaderno.Except(idsAutoridadesResponsables).ToList();

        //    var idsConValoresNull = entityCuadernoAutoridadesExists
        //        .Where(o =>
        //        !o.id_autoridad_responsable.Value.HasValue || // Filtra aquellos con id_autoridad_responsable null
        //        !o.id_organo_radicacion_queja.Value.HasValue || o.id_organo_radicacion_queja.Value <= 0 ||
        //        !o.id_sentido_resolucion_queja.Value.HasValue || o.id_sentido_resolucion_queja.Value <= 0 ||
        //        string.IsNullOrWhiteSpace(o.fecha_notificacion_requerimiento) ||
        //        string.IsNullOrWhiteSpace(o.fecha_notificacion_requerimiento) ||
        //        string.IsNullOrWhiteSpace(o.fecha_vencimiento_requerimiento) ||
        //        string.IsNullOrWhiteSpace(o.fecha_presentacion_fallo) ||
        //        string.IsNullOrWhiteSpace(o.numero_oficio_atencion) ||
        //        string.IsNullOrWhiteSpace(o.fecha_notificacion_acuerdo_fallo) ||
        //        string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion_fallo) ||
        //        string.IsNullOrWhiteSpace(o.numero_oficio_comunicacion_autoridad)
        //    )
        //    .Select(o => new
        //    {
        //        IdAutoridadResponsable = o.id_autoridad_responsable,
        //    })
        //    .ToList();
        //    string autoridadesVacias = string.Empty;
        //    foreach (var item in idsConValoresNull)
        //    {
        //        if (item.IdAutoridadResponsable != null)
        //        {
        //            autoridadesVacias += item.IdAutoridadResponsable.Label += "'\r\n'";
        //        }
        //    }

        //    if (idsConValoresNull.Any())
        //    {
        //        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el cumplimiento de fallo protector del libro constitucional.");
        //    }
        //    bool entity = true;
        //    return entity;
        //}

        public static CumplimientoFalloProtectorConstitucional UpdateCumplimientoFalloProtectorAutoridadOp(
            int id_numero_asunto,
            //int id_juicio_amparo, se modifico
            int id_autoridad_responsable,
            //bool cumplimiento_fallo,
            DateTime fecha_notificacion_requerimiento,
            int plazo_fallo,
            //DateTime? fecha_vencimiento_requerimiento,
            DateTime? fecha_presentacion_fallo,
            string? numero_oficio_atencion,
            DateTime? fecha_notificacion_acuerdo_fallo,
            DateTime? fecha_oficio_comunicacion_fallo,
            string? numero_oficio_comunicacion_autoridad,
            string? usuario
        )
        {
            CumplimientoFalloProtectorConstitucional entity = new()
            {
                id_numero_asunto = id_numero_asunto,
                id_autoridad_responsable = id_autoridad_responsable,
                //cumplimiento_fallo = cumplimiento_fallo,
                fecha_notificacion_requerimiento = fecha_notificacion_requerimiento,
                plazo_fallo = plazo_fallo,
                //fecha_vencimiento_requerimiento = fecha_vencimiento_requerimiento,
                fecha_presentacion_fallo = fecha_presentacion_fallo,
                numero_oficio_atencion = numero_oficio_atencion,
                fecha_notificacion_acuerdo_fallo = fecha_notificacion_acuerdo_fallo,
                fecha_oficio_comunicacion_fallo = fecha_oficio_comunicacion_fallo,
                numero_oficio_comunicacion_autoridad = numero_oficio_comunicacion_autoridad,
                usuario = usuario,
            };

            return entity;
        }

        public static bool UpdateRecursoQuejaConstitucional(
            ref List<AutoridadesResponsables> entityAutoridadesExists,
            ref List<RequestCuadernoConstitucional> entityCuadernoAutoridadesExists,
            bool recursoQuejaPrincipalAmparo,
            bool recursoQuejaPrincipal
        )
        {
            if (recursoQuejaPrincipalAmparo == true && recursoQuejaPrincipal == false || recursoQuejaPrincipalAmparo == false && recursoQuejaPrincipal == true)
            {
                throw new Exception($"No se puede finalizar el registro porque seleccionó SI y requiere capturar un recurso de queja.");
            }

            if (recursoQuejaPrincipal == true)
            {
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 1))
                {
                    var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o => o.id_recurrente == 1 && o.recurso_queja_principal == true) // Solo filtra donde recurrente == 1
                    .Where(o =>
                        o.id_autoridad_responsable == 0 || // Filtra aquellos con id_autoridad_responsable null
                                                           //!o.id_organo_radicacion_queja.Value.HasValue ||
                        string.IsNullOrWhiteSpace(o.fecha_recepcion_acuerdo_queja) ||
                        string.IsNullOrWhiteSpace(o.fecha_vencimiento_recurso_queja) ||
                        string.IsNullOrWhiteSpace(o.fecha_admision_queja) ||
                        //string.IsNullOrWhiteSpace(o.toca_queja) ||
                        //string.IsNullOrWhiteSpace(o.fecha_notificacion_ejecutoria) ||
                        //string.IsNullOrWhiteSpace(o.oficio_comunicacion_area_correspondiente) ||
                        string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                    )
                    .Select(o => new
                    {
                        IdAutoridadResponsable = o.id_autoridad_responsable,
                        Recurrente = o.id_recurrente
                    })
                    .ToList();

                    string autoridadesVacias = string.Empty;
                    foreach (var item in idsConValoresNullResponsables)
                    {
                        if (item.IdAutoridadResponsable.GetHashCode != null)
                        {
                            autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                        }
                    }
                    if (idsConValoresNullResponsables.Any())
                    {
                        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso de queja principal del libro constitucional.");
                    }
                }
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 2))
                {
                    var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o => o.id_recurrente == 2 && o.recurso_queja_principal == true) // Solo filtra donde recurrente == 2
                    .Where(o =>
                        o.id_autoridad_responsable == 0 || // Filtra aquellos con id_autoridad_responsable null
                                                           //!o.id_organo_radicacion_queja.Value.HasValue ||
                        string.IsNullOrWhiteSpace(o.fecha_admision_queja) ||
                        //string.IsNullOrWhiteSpace(o.toca_queja) ||
                        //string.IsNullOrWhiteSpace(o.fecha_notificacion_ejecutoria) ||
                        //string.IsNullOrWhiteSpace(o.oficio_comunicacion_area_correspondiente) ||
                        string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                    )
                    .Select(o => new
                    {
                        IdAutoridadResponsable = o.id_autoridad_responsable,
                        Recurrente = o.id_recurrente
                    })
                    .ToList();

                    string autoridadesVacias = string.Empty;
                    foreach (var item in idsConValoresNullResponsables)
                    {
                        if (item.IdAutoridadResponsable.GetHashCode != null)
                        {
                            autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                        }
                    }
                    if (idsConValoresNullResponsables.Any())
                    {
                        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso de queja principal del libro constitucional.");
                    }
                }
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 3))
                {
                    var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o => o.id_recurrente == 3 && o.recurso_queja_principal == true) // Solo filtra donde recurrente == 3
                    .Where(o =>
                        o.id_autoridad_responsable == 0 || // Filtra aquellos con id_autoridad_responsable null
                                                           //!o.id_organo_radicacion_queja.Value.HasValue ||
                        string.IsNullOrWhiteSpace(o.fecha_admision_queja) ||
                        //string.IsNullOrWhiteSpace(o.toca_queja) ||
                        //string.IsNullOrWhiteSpace(o.fecha_notificacion_ejecutoria) ||
                        //string.IsNullOrWhiteSpace(o.oficio_comunicacion_area_correspondiente) ||
                        string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                    )
                    .Select(o => new
                    {
                        IdAutoridadResponsable = o.id_autoridad_responsable,
                        Recurrente = o.id_recurrente
                    })
                    .ToList();

                    string autoridadesVacias = string.Empty;
                    foreach (var item in idsConValoresNullResponsables)
                    {
                        if (item.IdAutoridadResponsable.GetHashCode != null)
                        {
                            autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                        }
                    }
                    if (idsConValoresNullResponsables.Any())
                    {
                        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso de queja principal del libro constitucional.");
                    }
                }
            }
            bool entity = true;
            return entity;
        }

        public static bool UpdateRecursoRevisionConstitucional(
            ref List<AutoridadesResponsables> entityAutoridadesExists,
            ref List<RecursoRevisionPrincipalConstitucionalDisconnected> entityCuadernoAutoridadesExists,
            bool recursoRevisionPrincipalAmparo,
            bool recursoRevisionPrincipal
        )
        {
            if (recursoRevisionPrincipalAmparo == true && recursoRevisionPrincipal == false)
            {
                throw new Exception($"No se puede finalizar el registro porque ya se había capturado una autoridad.");
            }

            if (recursoRevisionPrincipal == true)
            {

                //if (autioridadesDistintas.Any())
                //{


                //var idsList = entityCuadernoAutoridadesExists.Select(item => item.id_autoridad_responsable).ToList();

                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 1))
                {

                    var autoridadesRegistradas = entityAutoridadesExists.Select(item => item.id_autoridad_responsable).ToList();


                    var autoridadesTabla = entityCuadernoAutoridadesExists
                                    .Where(item => item.id_recurrente == 1)
                                    .Select(item => item.id_autoridad_responsable)
                                    .ToList();

                    var autioridadesDistintas = autoridadesRegistradas.Except(autoridadesTabla).ToList();

                    if (autioridadesDistintas == null || !autioridadesDistintas.Any())
                    {
                        var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                        .Where(o => o.id_recurrente == 1) // Solo filtra donde recurrente == 1
                        .Where(o =>
                            //!o.id_autoridad_responsable || // Filtra aquellos con id_autoridad_responsable null
                            string.IsNullOrWhiteSpace(o.fecha_recepcion_sentencia) ||
                            string.IsNullOrWhiteSpace(o.fecha_vencimiento_recurso_revision) ||
                            string.IsNullOrWhiteSpace(o.fecha_admision)
                        )
                        .Select(o => new
                        {
                            IdAutoridadResponsable = o.id_autoridad_responsable,
                            Recurrente = o.id_recurrente
                        })
                        .ToList();

                        string autoridadesVacias = string.Empty;
                        foreach (var item in idsConValoresNullResponsables)
                        {
                            if (item.IdAutoridadResponsable.GetHashCode != null)
                            {
                                autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                            }
                        }
                        if (idsConValoresNullResponsables.Any())
                        {
                            throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso de revisión principal del libro constitucional.");
                        }
                    }
                    else
                    {
                        throw new Exception($"No se puede finalizar el registro porque: no han terminado de llenar el recurso de revisión principal del libro constitucional.");
                    }
                }


                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 2))
                {
                    var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o => o.id_recurrente == 2 && o.recurso_revision_principal == true) // Solo filtra donde recurrente == 2
                    .Where(o =>
                        //!o.id_autoridad_responsable.HasValue || // Filtra aquellos con id_autoridad_responsable null
                        string.IsNullOrWhiteSpace(o.fecha_admision)
                    )
                    .Select(o => new
                    {
                        IdAutoridadResponsable = o.id_autoridad_responsable,
                        Recurrente = o.id_recurrente
                    })
                    .ToList();

                    string autoridadesVacias = string.Empty;
                    foreach (var item in idsConValoresNullResponsables)
                    {
                        if (item.IdAutoridadResponsable.GetHashCode != null)
                        {
                            autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                        }
                    }
                    if (idsConValoresNullResponsables.Any())
                    {
                        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso de revisión principal del libro constitucional.");
                    }
                }
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 3))
                {
                    var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o => o.id_recurrente == 3 && o.recurso_revision_principal == true) // Solo filtra donde recurrente == 3
                    .Where(o => /*!o.id_autoridad_responsable. ||*/ // Filtra aquellos con id_autoridad_responsable null
                        string.IsNullOrWhiteSpace(o.fecha_admision)
                    )
                    .Select(o => new
                    {
                        IdAutoridadResponsable = o.id_autoridad_responsable,
                        Recurrente = o.id_recurrente
                    })
                    .ToList();

                    string autoridadesVacias = string.Empty;
                    foreach (var item in idsConValoresNullResponsables)
                    {
                        if (item.IdAutoridadResponsable.GetHashCode != null)
                        {
                            autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                        }
                    }
                    if (idsConValoresNullResponsables.Any())
                    {
                        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso de revisión principal del libro constitucional.");
                    }
                }
            }
            else
            {
                throw new Exception($"No se puede finalizar el registro porque: no han terminado de llenar el recurso de revisión principal del libro constitucional.");
            }
            //}
            bool entity = true;
            return entity;
        }

        public static RecursoInconformidadConstitucional CreateRecursoInconformidadConstitucional(
            int id,
            DateTime? notificacion_admision_recurso_inconformidad,
            string? numero_recurso_inconformidad,
            int? id_organo_radicacion_inconformidad,
            DateTime? fecha_notificacion_resolucion_inconformidad,
            int? id_sentido_resolucion_inconformidad,
            string? usuario
        )
        {
            //DateTime dateTime = DateTime.Now;
            //if (notificacion_admision_recurso_inconformidad > dateTime.Date)
            //{
            //    throw new Exception("La fecha de notificación de admisión recurso de reclamación no puede ser mayor a la fecha actual.");
            //}
            //if (fecha_notificacion_resolucion_inconformidad > dateTime.Date)
            //{
            //    throw new Exception("La fecha notificación de la resolución no puede ser mayor a la fecha actual.");
            //}

            //if (fecha_notificacion_resolucion_inconformidad <= notificacion_admision_recurso_inconformidad)
            //{
            //    throw new Exception("La fecha notificacion de la resolución no puede ser menor o igual a la fecha de notificacion de admisión");
            //}

            RecursoInconformidadConstitucional entity = new()
            {
                id = id,
                notificacion_admision_recurso_inconformidad = notificacion_admision_recurso_inconformidad,
                numero_recurso_inconformidad = numero_recurso_inconformidad,
                id_organo_radicacion_inconformidad = id_organo_radicacion_inconformidad,
                fecha_notificacion_resolucion_inconformidad = fecha_notificacion_resolucion_inconformidad,
                id_sentido_resolucion_inconformidad = id_sentido_resolucion_inconformidad,
                usuario = usuario,

            };

            return entity;
        }

        public static RecursoReclamacionConstitucional CreateRecursoReclamacionConstitucional(
            int id,
            bool? recurso_reclamacion,
            DateTime fecha_notificacion_acuerdo,
            //DateTime? fecha_vencimiento_recurso_reclamacion,
            DateTime? fecha_presentacion,
            string? oficio_reclamacion,
            DateTime? notificacion_adminsion_recurso_reclamacion,
            string? numero_recurso,
            int? id_organo_radicacion,
            DateTime? fecha_notificacion_resolucion,
            int? id_sentido_resolucion_reclamacion,
            string? usuario
        )
        {
            //DateTime dateTime = DateTime.Now;
            //if (fecha_notificacion_resolucion > dateTime.Date)
            //{
            //    throw new Exception("La fecha de notificacion de la resolucion no puede ser mayor a la fecha actual.");
            //}

            //if (fecha_presentacion <= fecha_notificacion_resolucion)
            //{
            //    throw new Exception("La fecha de presentacion no puede ser menor o igual a la fecha de notificación de resolución.");
            //}

            //if (fecha_presentacion < fecha_vencimiento_recurso_reclamacion)
            //{
            //    throw new Exception("La fecha de presentacion no puede ser menor a la fecha de vencimientodel recurso de reclamacion.");
            //}

            //if (fecha_presentacion <= notificacion_adminsion_recurso_reclamacion)
            //{
            //    throw new Exception("La fecha de notificacion de la resolucion no puede ser menor o igual a la fecha de la notificación de admisión del recurso de reclamación");
            //}

            //if (fecha_notificacion_resolucion <= notificacion_adminsion_recurso_reclamacion)
            //{
            //    throw new Exception("La fecha de notificacion de la resolucion no puede ser menor o igual a la fecha de la notificación de admisión del recurso de reclamación");
            //}


            RecursoReclamacionConstitucional entity = new()
            {

                id = id,
                recurso_reclamacion = recurso_reclamacion,
                fecha_notificacion_acuerdo = fecha_notificacion_acuerdo,
                notificacion_admision_recurso_reclamacion = notificacion_adminsion_recurso_reclamacion,
                //fecha_vencimiento_recurso_reclamacion = fecha_vencimiento_recurso_reclamacion,
                fecha_presentacion = fecha_presentacion,
                oficio_reclamacion = oficio_reclamacion,
                numero_recurso = numero_recurso,
                id_organo_radicacion = id_organo_radicacion,
                fecha_notificacion_resolucion = fecha_notificacion_resolucion,
                id_sentido_resolucion_reclamacion = id_sentido_resolucion_reclamacion,
                usuario = usuario,

            };

            return entity;
        }

        //Cuaderno Incidental
        #region CUADERNO INCIDENTAL

        #region SUSPENCIÓN PROVOSIONAL

        public static SuspensionProvisional CreateSuspensionProvisional(
            int? id,
            bool? suspension_provisional,
            int? otorgamiento_garantia,
            string? oficio_comunicacion,
            DateTime? fecha_comunicacion,
            string? usuario_modificacion
        )
        {

            SuspensionProvisional entity = new()
            {
                id = id,
                suspension_provisional = suspension_provisional,
                otorgamiento_garantia = otorgamiento_garantia,
                oficio_comunicacion = oficio_comunicacion,
                fecha_comunicacion = fecha_comunicacion,
                usuario_modificacion = usuario_modificacion

            };

            return entity;
        }

        #endregion

        #region INFORME PREVIO

        public static InformePrevioIncidental CreateInformePrevioIncidental(

            int id,
            int? id_autoridad_responsable,
            DateTime fecha_apertura_incidente,
            //DateTime? fecha_vencimiento,
            string? numero_oficio_informe_previo,
            DateTime? fecha_presentacion_informe_previo,
            string? usuario
        )
        {
            InformePrevioIncidental entity = new()
            {
                id = id,
                id_autoridad_responsable = id_autoridad_responsable,
                fecha_apertura_incidente = fecha_apertura_incidente,
                //fecha_vencimiento = fecha_vencimiento,
                numero_oficio_informe_previo = numero_oficio_informe_previo,
                fecha_presentacion_informe_previo = fecha_presentacion_informe_previo,
                usuario = usuario
            };

            return entity;
        }

        #endregion
        #region INFORME PREVIO (Validación)
        public static bool UpdateInformePrevio(
            ref List<AutoridadesResponsables> entityAutoridadesExists,
            ref List<ResponseInformePrevioIncidental> entityInformePrevio
        )
        {
            var idsConValoresNull = entityInformePrevio
                .Where(o =>
                o.id_autoridad_responsable == 0 || // Filtra aquellos con id_autoridad_responsable null
                string.IsNullOrWhiteSpace(o.fecha_apertura_incidente) ||
                string.IsNullOrWhiteSpace(o.fecha_vencimiento) ||
                string.IsNullOrWhiteSpace(o.numero_oficio_informe_previo) ||
                string.IsNullOrWhiteSpace(o.fecha_presentacion_informe_previo)
            )
            .Select(o => new
            {
                IdAutoridadResponsable = o.id_autoridad_responsable,
            })
            .ToList();
            string autoridadesVacias = string.Empty;
            foreach (var item in idsConValoresNull)
            {
                if (item.IdAutoridadResponsable.GetHashCode != null)
                {
                    autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                }
            }

            if (idsConValoresNull.Any())
            {
                throw new Exception($"No se puede finalizar el registro porque:  {autoridadesVacias.Trim()} no han terminado de llenar informe previo del cuaderno incidental.");
            }
            bool entity = true;
            return entity;
        }
        #endregion

        #region SENTENCIA INCIDENTAL
        public static SentenciaIncidental CreateSentenciaIncidental(
            int id_numero_asunto,
            //int id_juicio_amparo, se modifico
            int id_autoridad_responsable,
            DateTime? fecha_notificacion_sentencia,
            int? id_sentido_suspencion_definitiva,
            int? id_otorgamiento_garatia,
            int? id_sentido_general_asunto,
            int? id_tipo_sentido_general_asunto,
            string? oficio_comunicacion_area_correspondiente,
            DateTime? fecha_comunicacion_suspencion,
            int? dictamen_no_revision_incidental,
            string? usuario,
            int id_estado_procesal
        //int id_estado_tarea
        )
        {
            //**Cuando se acomomoden los estados procesales descomentar estas validaciones**
            //if (id_estado_procesal == EnumEstadoProcesal.PENDIENTE_DE_RENDIR_INFORME_PREVIO.GetHashCode())
            //    throw new Exception("El Juicio tiene que estar en estado procesal PENDIENTE DE RENDIR INFORME PREVIO para poder capturar la SENTENCIA INCIDENTAL.");
            //if (id_estado_tarea == EnumEstadoTarea.ASIGNADO.GetHashCode())
            //    throw new Exception("El Juicio se encuentra en estado de tarea POR ASIGNAR.");


            SentenciaIncidental entity = new()
            {
                id_numero_asunto = id_numero_asunto,
                fecha_notificacion_sentencia = fecha_notificacion_sentencia,
                id_sentido_suspencion_definitiva = id_sentido_suspencion_definitiva,
                id_otorgamiento_garatia = id_otorgamiento_garatia,
                id_sentido_general_asunto = id_sentido_general_asunto,
                id_tipo_sentido_general_asunto = id_tipo_sentido_general_asunto,
                oficio_comunicacion_area_correspondiente = oficio_comunicacion_area_correspondiente,
                fecha_comunicacion_suspencion = fecha_comunicacion_suspencion,
                id_dictamen_no_revision_incidental = dictamen_no_revision_incidental,
                id_autoridad_responsable = id_autoridad_responsable,
                usuario = usuario
            };

            return entity;
        }

        #endregion

        #region SENTENCIA INCIDENTAL(Validación)
        //public static bool UpdateSentenciaIncidental(
        //    ref List<AutoridadesResponsables> entityAutoridadesExists,
        //    ref List<ResponseSentenciaIncidental> entitySentencia
        //)
        //{
        //    var idsConValoresNull = entitySentencia
        //        .Where(o =>
        //        !o.id_autoridad_responsable.Value.HasValue || // Filtra aquellos con id_autoridad_responsable null
        //        !o.id_sentido_suspencion_definitiva.Value.HasValue ||
        //        //!o.otorgamiento_garatia.Value.HasValue ||
        //        //!o.dictamen_no_revision_incidental.Value.HasValue ||
        //        string.IsNullOrWhiteSpace(o.fecha_notificacion_sentencia) ||
        //        string.IsNullOrWhiteSpace(o.oficio_comunicacion_area_correspondiente) ||
        //        string.IsNullOrWhiteSpace(o.fecha_comunicacion_suspencion)
        //    )
        //    .Select(o => new
        //    {
        //        IdAutoridadResponsable = o.id_autoridad_responsable,
        //    })
        //    .ToList();
        //    string autoridadesVacias = string.Empty;
        //    foreach (var item in idsConValoresNull)
        //    {
        //        if (item.IdAutoridadResponsable != null)
        //        {
        //            autoridadesVacias += item.IdAutoridadResponsable.Label += "'\r\n'";
        //        }
        //    }

        //    if (idsConValoresNull.Any())
        //    {
        //        throw new Exception($"No se puede finalizar el registro porque:  {autoridadesVacias.Trim()} no han terminado de llenar la sentencia del cuaderno incidental.");
        //    }
        //    bool entity = true;
        //    return entity;
        //}
        #endregion

        #region RECURSO QUEJA INCIDENTAL
        public static RecursoQuejaIncidental CreateRecursoQuejaIncidental(
            int? id,
            int? id_numero_asunto,
            //int? id_juicio_amparo, se modifico
            int? id_autoridad_responsable,
            bool? recurso_queja,
            int? recurrente,
            DateTime fecha_recepcion_apertura,
            //DateTime? fecha_vencimiento_recurso_queja,
            string? oficio_queja,
            DateTime? fecha_presentacion_recurso_queja,
            DateTime? fecha_admision,
            int? organo_radicacion,
            string? toca,
            DateTime? fecha_notificacion_ejecutoria,
            int? sentido_resolucion,
            string? oficio_comunicacion_area_correspondiente,
            DateTime? fecha_oficio_comunicacion,
            string? usuario

        )
        {


            RecursoQuejaIncidental entity = new()
            {

                id = id,
                id_numero_asunto = id_numero_asunto,
                id_autoridad_responsable = id_autoridad_responsable,
                recurso_queja = recurso_queja,
                id_recurrente = recurrente,
                fecha_recepcion_apertura = fecha_recepcion_apertura,
                //fecha_vencimiento_recurso_queja = fecha_vencimiento_recurso_queja,
                oficio_queja = oficio_queja,
                fecha_presentacion_recurso_queja = fecha_presentacion_recurso_queja,
                fecha_admision = fecha_admision,
                organo_radicacion = organo_radicacion,
                toca = toca,
                fecha_notificacion_ejecutoria = fecha_notificacion_ejecutoria,
                sentido_resolucion = sentido_resolucion,
                oficio_comunicacion_area_correspondiente = oficio_comunicacion_area_correspondiente,
                fecha_oficio_comunicacion = fecha_oficio_comunicacion,
                usuario = usuario

            };

            return entity;
        }


        #endregion

        #region RECURSO QUEJA INCIDENTAL (Validación)
        public static bool UpdateRecursoQuejaIncidental(
            ref List<AutoridadesResponsables> entityAutoridadesExists,
            ref List<RecursoQuejaIncidentalDisconected> entityCuadernoAutoridadesExists,
            bool recursoRevisionIncidentalAmparo,
            bool recursoRevisionIncidental
        )
        {

            if (recursoRevisionIncidentalAmparo == true && recursoRevisionIncidental == false || recursoRevisionIncidentalAmparo == false && recursoRevisionIncidental == true)
            {
                throw new Exception($"No se puede finalizar el registro porque ya se había capturado una autoridad.");
            }
            if (recursoRevisionIncidental == true)
            {
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 1))
                {
                    var autoridadesRegistradas = entityAutoridadesExists.Select(item => item.id_autoridad_responsable).ToList();

                    var autoridadesTabla = entityCuadernoAutoridadesExists
                                    .Where(item => item.id_recurrente == 1)
                                    .Select(item => item.id_autoridad_responsable)
                                    .ToList();

                    var autioridadesDistintas = autoridadesRegistradas.Except(autoridadesTabla).ToList();

                    if (autioridadesDistintas == null || !autioridadesDistintas.Any())
                    {
                        var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                        .Where(o => o.id_recurrente == 1) // Solo filtra donde recurrente == 1
                        .Where(o =>
                            //!o.id_autoridad_responsable.Value.HasValue || // Filtra aquellos con id_autoridad_responsable null
                            string.IsNullOrWhiteSpace(o.fecha_recepcion_apertura) ||
                            string.IsNullOrWhiteSpace(o.fecha_admision) ||
                            string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                        )
                        .Select(o => new
                        {
                            IdAutoridadResponsable = o.id_autoridad_responsable,
                            Recurrente = o.id_recurrente
                        })
                        .ToList();

                        string autoridadesVacias = string.Empty;
                        foreach (var item in idsConValoresNullResponsables)
                        {
                            if (item.IdAutoridadResponsable.GetHashCode != null)
                            {
                                autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                            }
                        }
                        if (idsConValoresNullResponsables.Any())
                        {
                            throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso revisión incidental del libro constitucional.");
                        }
                    }
                    else
                    {
                        throw new Exception($"No se puede finalizar el registro porque: no han terminado de llenar el recurso revisión incidental del libro constitucional.");
                    }
                }
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 2))
                {
                    var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o => o.id_recurrente == 2) // Solo filtra donde recurrente == 2
                    .Where(o =>
                        //!o.id_autoridad_responsable.HasValue || // Filtra aquellos con id_autoridad_responsable null
                        string.IsNullOrWhiteSpace(o.fecha_admision) ||
                        string.IsNullOrWhiteSpace(o.toca_queja_incidental) ||
                        string.IsNullOrWhiteSpace(o.fecha_notificacion_ejecutoria) ||
                        string.IsNullOrWhiteSpace(o.oficio_comunicacion_area_correspondiente) ||
                        string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                    )
                    .Select(o => new
                    {
                        IdAutoridadResponsable = o.id_autoridad_responsable,
                        Recurrente = o.id_recurrente
                    })
                    .ToList();

                    string autoridadesVacias = string.Empty;
                    foreach (var item in idsConValoresNullResponsables)
                    {
                        if (item.IdAutoridadResponsable.GetHashCode != null)
                        {
                            autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                        }
                    }
                    if (idsConValoresNullResponsables.Any())
                    {
                        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso revisión incidental del libro constitucional.");
                    }
                }
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 3))
                {
                    var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o => o.id_recurrente == 3) // Solo filtra donde recurrente == 3
                    .Where(o => /*!o.id_autoridad_responsable. ||*/ // Filtra aquellos con id_autoridad_responsable null
                        string.IsNullOrWhiteSpace(o.fecha_admision) ||
                        string.IsNullOrWhiteSpace(o.toca_queja_incidental) ||
                        string.IsNullOrWhiteSpace(o.fecha_notificacion_ejecutoria) ||
                        string.IsNullOrWhiteSpace(o.oficio_comunicacion_area_correspondiente) ||
                        string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                    )
                    .Select(o => new
                    {
                        IdAutoridadResponsable = o.id_autoridad_responsable,
                        Recurrente = o.id_recurrente
                    })
                    .ToList();

                    string autoridadesVacias = string.Empty;
                    foreach (var item in idsConValoresNullResponsables)
                    {
                        if (item.IdAutoridadResponsable.GetHashCode != null)
                        {
                            autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                        }
                    }
                    if (idsConValoresNullResponsables.Any())
                    {
                        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso revisión incidental del libro constitucional.");
                    }
                }
            }
            else
            {
                throw new Exception($"No se puede finalizar el registro porque: no han terminado de llenar el recurso revisión incidental del libro constitucional.");
            }
            bool entity = true;
            return entity;

        }
        #endregion

        #region RECURSO REVISION INCIDENTAL (Validación)
        public static bool UpdateRecursoRevisionIncidental(
            ref List<AutoridadesResponsables> entityAutoridadesExists,
            ref List<RecursoRevisionIncidentalDisconnected> entityCuadernoAutoridadesExists,
            bool recursoRevisionIncidentalAmparo,
            bool recursoRevisionIncidental
        )
        {

            if (recursoRevisionIncidentalAmparo == true && recursoRevisionIncidental == false || recursoRevisionIncidentalAmparo == false && recursoRevisionIncidental == true)
            {
                throw new Exception($"No se puede finalizar el registro porque ya se había capturado una autoridad.");
            }
            if (recursoRevisionIncidental == true)
            {
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 1))
                {
                    var autoridadesRegistradas = entityAutoridadesExists.Select(item => item.id_autoridad_responsable).ToList();

                    var autoridadesTabla = entityCuadernoAutoridadesExists
                                    .Where(item => item.id_recurrente == 1)
                                    .Select(item => item.id_autoridad_responsable)
                                    .ToList();

                    var autioridadesDistintas = autoridadesRegistradas.Except(autoridadesTabla).ToList();

                    if (autioridadesDistintas == null || !autioridadesDistintas.Any())
                    {
                        var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                        .Where(o => o.id_recurrente == 1) // Solo filtra donde recurrente == 1
                        .Where(o =>
                            //!o.id_autoridad_responsable.Value.HasValue || // Filtra aquellos con id_autoridad_responsable null
                            string.IsNullOrWhiteSpace(o.fecha_recepcion_sentencia) ||
                            string.IsNullOrWhiteSpace(o.fecha_vencimiento_recurso_revision) ||
                            string.IsNullOrWhiteSpace(o.oficio_recurso) ||
                            string.IsNullOrWhiteSpace(o.fecha_presentacion_recurso_revision) ||
                            string.IsNullOrWhiteSpace(o.fecha_admision) /*||*/
                        //string.IsNullOrWhiteSpace(o.toca_recurso_revision_incidental) ||
                        //string.IsNullOrWhiteSpace(o.fecha_notificacion_resolucion) ||
                        //string.IsNullOrWhiteSpace(o.oficio_comunicacion_autoridad) ||
                        //string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                        )
                        .Select(o => new
                        {
                            IdAutoridadResponsable = o.id_autoridad_responsable,
                            Recurrente = o.id_recurrente
                        })
                        .ToList();

                        string autoridadesVacias = string.Empty;
                        foreach (var item in idsConValoresNullResponsables)
                        {
                            if (item.IdAutoridadResponsable.GetHashCode != null)
                            {
                                autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                            }
                        }
                        if (idsConValoresNullResponsables.Any())
                        {
                            throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso revisión incidental del libro constitucional.");
                        }
                    }
                    else
                    {
                        throw new Exception($"No se puede finalizar el registro porque: no han terminado de llenar el recurso revisión incidental del libro constitucional.");
                    }
                }
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 2))
                {
                    var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o => o.id_recurrente == 2) // Solo filtra donde recurrente == 2
                    .Where(o => //!o.id_autoridad_responsable.HasValue || // Filtra aquellos con id_autoridad_responsable null
                        string.IsNullOrWhiteSpace(o.fecha_admision) ||
                        string.IsNullOrWhiteSpace(o.toca_recurso_revision_incidental) /*||*/
                    //string.IsNullOrWhiteSpace(o.oficio_revision_adhesiva) ||
                    //string.IsNullOrWhiteSpace(o.fecha_presentacion_adhesiva) ||
                    //string.IsNullOrWhiteSpace(o.fecha_vencimiento_adhesion) ||
                    //string.IsNullOrWhiteSpace(o.fecha_notificacion_resolucion) ||
                    //string.IsNullOrWhiteSpace(o.oficio_comunicacion_autoridad) ||
                    //string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                    )
                    .Select(o => new
                    {
                        IdAutoridadResponsable = o.id_autoridad_responsable,
                        Recurrente = o.id_recurrente
                    })
                    .ToList();

                    string autoridadesVacias = string.Empty;
                    foreach (var item in idsConValoresNullResponsables)
                    {
                        if (item.IdAutoridadResponsable.GetHashCode != null)
                        {
                            autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                        }
                    }
                    if (idsConValoresNullResponsables.Any())
                    {
                        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso revisión incidental del libro constitucional.");
                    }
                }
                if (entityCuadernoAutoridadesExists.Any(c => c.id_recurrente == 3))
                {
                    var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o => o.id_recurrente == 3) // Solo filtra donde recurrente == 3
                    .Where(o => /*!o.id_autoridad_responsable. ||*/ // Filtra aquellos con id_autoridad_responsable null
                        string.IsNullOrWhiteSpace(o.fecha_admision) ||
                        string.IsNullOrWhiteSpace(o.toca_recurso_revision_incidental) /*||*/
                    //string.IsNullOrWhiteSpace(o.oficio_revision_adhesiva) ||
                    //string.IsNullOrWhiteSpace(o.fecha_presentacion_adhesiva) ||
                    //string.IsNullOrWhiteSpace(o.fecha_vencimiento_adhesion) ||
                    //string.IsNullOrWhiteSpace(o.fecha_notificacion_resolucion) ||
                    //string.IsNullOrWhiteSpace(o.oficio_comunicacion_autoridad) ||
                    //string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                    )
                    .Select(o => new
                    {
                        IdAutoridadResponsable = o.id_autoridad_responsable,
                        Recurrente = o.id_recurrente
                    })
                    .ToList();

                    string autoridadesVacias = string.Empty;
                    foreach (var item in idsConValoresNullResponsables)
                    {
                        if (item.IdAutoridadResponsable.GetHashCode != null)
                        {
                            autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                        }
                    }
                    if (idsConValoresNullResponsables.Any())
                    {
                        throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso revisión incidental del libro constitucional.");
                    }
                }
            }
            else
            {
                throw new Exception($"No se puede finalizar el registro porque: no han terminado de llenar el recurso revisión incidental del libro constitucional.");
            }
            bool entity = true;
            return entity;

        }
        #endregion

        #region RECURSO REVISION INCIDENTAL

        public static RecursoRevisionIncidental CreateRecursoRevisionIncidental(
            int? id,
            int? id_numero_asunto,
            // int? id_juicio_amparo, se modifico
            int? id_autoridad_responsable,
            bool? recurso_revision_incidental,
            int? id_recurrente,
            DateTime fecha_recepcion_sentencia,
            //DateTime? fecha_vencimiento_recurso_revision,
            string? oficio_recurso,
            DateTime? fecha_presentacion_recurso_revision,
            DateTime fecha_admision,
            int? id_organo_radicacion,
            string? toca_recurso_revision_incidental,
            bool? revision_adhesiva,
            string? oficio_revision_adhesiva,
            //DateTime? fecha_vencimiento_adhesion,
            DateTime? fecha_presentacion_adhesion,
            DateTime? fecha_notificacion_resolucion,
            int? id_sentido_resolucion,
            int? id_tipo_sentido,
            int? id_sentido_general_asunto,
            int? id_tipo_sentido_general,
            string? oficio_comunicacion_autoridad,
            DateTime? fecha_oficio_comunicacion,
            string? usuario

        )
        {
            //cambiamos la entidad
            RecursoRevisionIncidental entity = new()
            {

                id = id,
                id_numero_asunto = id_numero_asunto,
                id_autoridad_responsable = id_autoridad_responsable,
                recurso_revision_incidental = recurso_revision_incidental,
                id_recurrente = id_recurrente,
                fecha_recepcion_sentencia = fecha_recepcion_sentencia,
                //fecha_vencimiento_recurso_revision = fecha_vencimiento_recurso_revision,
                oficio_recurso = oficio_recurso,
                fecha_presentacion_recurso_revision = fecha_presentacion_recurso_revision,
                fecha_admision = fecha_admision,
                id_organo_radicacion = id_organo_radicacion,
                toca_recurso_revision_incidental = toca_recurso_revision_incidental,
                revision_adhesiva = revision_adhesiva,
                oficio_revision_adhesiva = oficio_revision_adhesiva,
                //fecha_vencimiento_adhesion = fecha_vencimiento_adhesion,
                fecha_presentacion_adhesion = fecha_presentacion_adhesion,
                fecha_notificacion_resolucion = fecha_notificacion_resolucion,
                id_sentido_resolucion = id_sentido_resolucion,
                id_tipo_sentido = id_tipo_sentido,
                id_sentido_general_asunto = id_sentido_general_asunto,
                id_tipo_sentido_general = id_tipo_sentido_general,
                oficio_comunicacion_autoridad = oficio_comunicacion_autoridad,
                fecha_oficio_comunicacion = fecha_oficio_comunicacion,
                usuario = usuario,
            };

            return entity;
        }

        #endregion

        #region INCIDENTE por Exceso

        public static IncidenteExcesoIncidental CreateIncidenteExceso(
            int? id,
            int? id_numero_asunto,
            //int? id_juicio_amparo, se modifico
            int? id_autoridad_responsable,
            bool? interposicion_incidente,
            DateTime? fecha_notificacion_acuerdo,
            string? oficio_desahogo,
            DateTime? fecha_oficio_desahogo,
            int? id_sentido,
            DateTime? fecha_notificacion_resolucion,
            string? oficio_comunicacion_autoridad,
            DateTime? fecha_oficio_comunicacion,
            string? usuario
        )
        {


            IncidenteExcesoIncidental entity = new()
            {
                id = id,
                id_numero_asunto = id_numero_asunto,
                id_autoridad_responsable = id_autoridad_responsable,
                interposicion_incidente = interposicion_incidente,
                fecha_notificacion_acuerdo = fecha_notificacion_acuerdo,
                oficio_desahogo = oficio_desahogo,
                fecha_oficio_desahogo = fecha_oficio_desahogo,
                id_sentido = id_sentido,
                fecha_notificacion_resolucion = fecha_notificacion_resolucion,
                oficio_comunicacion_autoridad = oficio_comunicacion_autoridad,
                fecha_oficio_comunicacion = fecha_oficio_comunicacion,
                usuario = usuario

            };

            return entity;
        }

        public static bool UpdateIncidenteExcesoIncidental(
            ref List<IncidenteExcesoIncidentalDisconnected> entityCuadernoAutoridadesExists,
            ref List<AutoridadesResponsables> entityAutoridadesExists
        )
        {
            var autoridadesRegistradas = entityAutoridadesExists.Select(item => item.id_autoridad_responsable).ToList();

            var autoridadesTabla = entityCuadernoAutoridadesExists
                            //.Where(item => item.id_recurrente == 1)
                            .Select(item => item.id_autoridad_responsable)
                            .ToList();

            var autioridadesDistintas = autoridadesRegistradas.Except(autoridadesTabla).ToList();

            if (autioridadesDistintas == null || !autioridadesDistintas.Any())
            {
                var idsConValoresNullResponsables = entityCuadernoAutoridadesExists
                    .Where(o =>
                    //!o.id_autoridad_responsable || // Filtra aquellos con id_autoridad_responsable null
                    !o.id_sentido.HasValue || o.id_sentido.Value <= 0 ||
                    //!o.id_dictamen_no_revision.Value.HasValue || o.id_dictamen_no_revision.Value <= 0 ||
                    //!o.id_tipo_sentido_sentencia.Value.HasValue || o.id_tipo_sentido_sentencia.Value <= 0 ||
                    string.IsNullOrWhiteSpace(o.fecha_notificacion_acuerdo) ||
                    string.IsNullOrWhiteSpace(o.oficio_desahogo) ||
                    string.IsNullOrWhiteSpace(o.fecha_oficio_desahogo) ||
                    string.IsNullOrWhiteSpace(o.fecha_notificacion_resolucion) ||
                    string.IsNullOrWhiteSpace(o.oficio_comunicacion_autoridad) ||
                    string.IsNullOrWhiteSpace(o.fecha_oficio_comunicacion)
                )
                .Select(o => new
                {
                    IdAutoridadResponsable = o.id_autoridad_responsable,
                })
                .ToList();
                string autoridadesVacias = string.Empty;
                foreach (var item in idsConValoresNullResponsables)
                {
                    if (item.IdAutoridadResponsable.GetHashCode != null)
                    {
                        autoridadesVacias += item.IdAutoridadResponsable + "'\r\n'";
                    }
                }
                if (idsConValoresNullResponsables.Any())
                {
                    throw new Exception($"No se puede finalizar el registro porque: {autoridadesVacias.Trim()} no han terminado de llenar el recurso revisión incidental del libro constitucional.");
                }
            }
            else
            {
                throw new Exception($"No se puede finalizar el registro porque: no han terminado de llenar el Incidente por Exceso Incidental");
            }

            //var idsAutoridadesCuaderno = entityCuadernoAutoridadesExists
            //    .Select(r => r.id_autoridad_responsable.Value.GetValueOrDefault())
            //    .ToList();

            //var idsAutoridadesResponsables = entityAutoridadesExists
            //    .Select(r => r.id_autoridad_responsable)
            //    .ToList();
            ////Obtengo los que no existen
            //var valoresNoExistentes = idsAutoridadesCuaderno.Except(idsAutoridadesResponsables).ToList();

            //var idsConValoresNull = entityCuadernoAutoridadesExists
            //    .Where(o =>
            //    !o.id_autoridad_responsable.Value.HasValue || // Filtra aquellos con id_autoridad_responsable null
            //    !o.id_sentido_sentencia.Value.HasValue || o.id_sentido_sentencia.Value <= 0 ||
            //    //!o.id_dictamen_no_revision.Value.HasValue || o.id_dictamen_no_revision.Value <= 0 ||
            //    !o.id_tipo_sentido_sentencia.Value.HasValue || o.id_tipo_sentido_sentencia.Value <= 0 ||
            //    string.IsNullOrWhiteSpace(o.fecha_notificacion_sentencia) ||
            //    string.IsNullOrWhiteSpace(o.fecha_conclusion_expediente)
            //)
            //.Select(o => new
            //{
            //    IdAutoridadResponsable = o.id_autoridad_responsable,
            //})
            //.ToList();
            //string autoridadesVacias = string.Empty;
            //foreach (var item in idsConValoresNull)
            //{
            //    if (item.IdAutoridadResponsable != null)
            //    {
            //        autoridadesVacias += item.IdAutoridadResponsable.Label += "'\r\n'";
            //    }
            //}


            //bool tieneFechaConclusionValida = entityCuadernoAutoridadesExists
            //.Any(o => !string.IsNullOrWhiteSpace(o.fecha_conclusion_expediente)); // Comprobamos si algún registro tiene fecha_conclusion_expediente no vacía

            // Si no hay ninguna fecha válida, lanzar el mensaje de error
            //if (!tieneFechaConclusionValida)
            //{
            //    throw new Exception("No se puede concluir el juicio de amparo indirecto porque no se ha capturado la fecha de conclusión del expediente.");
            //}
            bool entity = true;
            return entity;
        }

        #endregion

        #endregion
    }

}
