using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.ViewModels.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using Newtonsoft.Json.Linq;
using Npgsql.Internal.TypeHandlers.NumericHandlers;
using NpgsqlTypes;
using Sicoj.Utils.Models;
using Sicoj.Utils.Postgres;
using System;
using System.Data;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;
using System.Linq.Expressions;

namespace AmparoIndirectoAPI.Model.DAO.Repository
{
    public class AmparoIndirectoAbogadoRepository : IAmparoIndirectoAbogadoRepository
    {

        #region Variables
        private readonly ISqlTools _database;
        #endregion

        #region Constructor
        public AmparoIndirectoAbogadoRepository(ISqlTools database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }
        #endregion

        //Métodos

        #region GetAll CUADERNO CONSTITUCIONAL RECURRENTE
        public async Task<List<RequestCuadernoConstitucional>> GetCuadernoConstitucionalRecurrenteAsync(int id, int idEstadoProcesal)
        {
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id),
                new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
                };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetCuadernoConstitucionalRecurrente,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            List<RequestCuadernoConstitucional> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id_numero_asunto = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_autoridad_responsable = item.IsNull(1) ? 0 : item.Field<int>(1),
                        autoridad_responsable = item.IsNull(2) ? null! : item.Field<string>(2)!,

                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(1) ? 0 : item.Field<int>(1),
                        //    Label = item.IsNull(2) ? null! : item.Field<string>(2)
                        //},
                        //solucion_opinion_tecnica = response.Data.Tables[0].Rows[0].IsNull(3)
                        //? false
                        //: response.Data.Tables[0].Rows[0].Field<bool>(3),

                        // inician pruebas
                        solucion_opinion_tecnica = item.IsNull(3) ? false : item.Field<bool>(3),

                        //terminan cambios


                        fecha_recepcion_demanda = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),

                        fecha_vencimiento_justificado = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        numero_oficio_informe_justificado = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        fecha_presentacion_informe_justificado = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        observaciones_justificado = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        fecha_notificacion_sentencia = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),
                        id_sentido_sentencia = item.IsNull(10) ? 0 : item.Field<int>(10),
                        sentido_sentencia = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        //id_sentido_sentencia = new()
                        //{
                        //    Value = item.IsNull(10) ? 0 : item.Field<int>(10),
                        //    Label = item.IsNull(11) ? null! : item.Field<string>(11)
                        //},
                        id_tipo_sentido_sentencia = item.IsNull(12) ? 0 : item.Field<int>(12),
                        tipo_sentido_sentencia = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        //id_tipo_sentido_sentencia = new()
                        //{
                        //    Value = item.IsNull(12) ? 0 : item.Field<int>(12),
                        //    Label = item.IsNull(13) ? null! : item.Field<string>(13)
                        //},
                        id_dictamen_no_revision = item.IsNull(14) ? 0 : item.Field<int>(14),
                        dictamen_no_revision = item.IsNull(15) ? null! : item.Field<string>(15)!,
                        //id_dictamen_no_revision = new()
                        //{
                        //    Value = item.IsNull(14) ? 0 : item.Field<int>(14),
                        //    Label = item.IsNull(15) ? null! : item.Field<string>(15)
                        //},
                        id_sentido_general_asunto = item.IsNull(16) ? 0 : item.Field<int>(16),
                        sentido_general_asunto = item.IsNull(17) ? null! : item.Field<string>(17)!,
                        //id_sentido_general_asunto = new()
                        //{
                        //    Value = item.IsNull(16) ? 0 : item.Field<int>(16),
                        //    Label = item.IsNull(17) ? null! : item.Field<string>(17)
                        //},
                        id_tipo_sentido_general = item.IsNull(18) ? 0 : item.Field<int>(18),
                        tipo_sentido_general = item.IsNull(19) ? null! : item.Field<string>(19)!,
                        //id_tipo_sentido_general = new()
                        //{
                        //    Value = item.IsNull(18) ? 0 : item.Field<int>(18),
                        //    Label = item.IsNull(19) ? null! : item.Field<string>(19)
                        //},
                        oficio_comunicacion_autoridad = item.IsNull(20) ? null! : item.Field<string>(20)!,
                        fecha_oficio_comunicacion_sentencia = item.IsNull(21) ? null! : item.Field<DateTime>(21).ToString("yyyy-MM-dd"),
                        fecha_recepcion_auto_sentencia_ejecutoria = item.IsNull(22) ? null! : item.Field<DateTime>(22).ToString("yyyy-MM-dd"),
                        comunicado_acuerdo_firmeza = item.IsNull(23) ? null! : item.Field<string>(23)!,
                        fecha_conclusion_expediente = item.IsNull(24) ? null! : item.Field<DateTime>(24).ToString("yyyy-MM-dd"),
                        //cumplimiento = response.Data.Tables[0].Rows[0].IsNull(25)
                        //? false
                        //: response.Data.Tables[0].Rows[0].Field<bool>(25),
                        //fecha_notificacion_requerimiento = item.IsNull(26) ? null! : item.Field<DateTime>(26).ToString("yyyy-MM-dd"),
                        //plazo_fallo = item.IsNull(27) ? 0 : item.Field<int>(27),
                        //fecha_vencimiento_requerimiento = item.IsNull(28) ? null! : item.Field<DateTime>(28).ToString("yyyy-MM-dd"),
                        //fecha_presentacion_fallo = item.IsNull(29) ? null! : item.Field<DateTime>(29).ToString("yyyy-MM-dd"),
                        //numero_oficio_atencion = item.IsNull(30) ? null! : item.Field<string>(30)!,
                        //fecha_notificacion_acuerdo_fallo = item.IsNull(31) ? null! : item.Field<DateTime>(31).ToString("yyyy-MM-dd"),
                        //fecha_oficio_comunicacion_fallo = item.IsNull(32) ? null! : item.Field<DateTime>(32).ToString("yyyy-MM-dd"),
                        //numero_oficio_comunicacion_autoridad = item.IsNull(33) ? null! : item.Field<string>(33)!,

                        //aqui se agregaron los datos adicionales
                        //recurso_queja_principal = response.Data.Tables[0].Rows[0].IsNull(25)
                        //    ? false
                        //    : response.Data.Tables[0].Rows[0].Field<bool>(25),
                        recurso_queja_principal = item.IsNull(25) ? false : item.Field<bool>(25),

                        id_recurrente = item.IsNull(26) ? 0 : item.Field<int>(26),
                        fecha_recepcion_acuerdo_queja = item.IsNull(27) ? null! : item.Field<DateTime>(27).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_queja = item.IsNull(28) ? null! : item.Field<DateTime>(28).ToString("yyyy-MM-dd"),
                        oficio_recurso_queja = item.IsNull(29) ? null! : item.Field<string>(29)!,
                        fecha_presentacion_queja = item.IsNull(30) ? null! : item.Field<DateTime>(30).ToString("yyyy-MM-dd"),
                        id_motivo_recurso_queja = item.IsNull(31) ? 0 : item.Field<int>(31),
                        motivo_recurso_queja = item.IsNull(32) ? null! : item.Field<string>(32)!,
                        //id_motivo_recurso_queja = new()
                        //{
                        //    Value = item.IsNull(31) ? 0 : item.Field<int>(31),
                        //    Label = item.IsNull(32) ? null! : item.Field<string>(32)
                        //},
                        fecha_admision_queja = item.IsNull(33) ? null! : item.Field<DateTime>(33).ToString("yyyy-MM-dd"),
                        id_organo_radicacion_queja = item.IsNull(34) ? 0 : item.Field<int>(34),
                        organo_radicacion_queja = item.IsNull(35) ? null! : item.Field<string>(35)!,
                        //id_organo_radicacion_queja = new()
                        //{
                        //    Value = item.IsNull(34) ? 0 : item.Field<int>(34),
                        //    Label = item.IsNull(35) ? null! : item.Field<string>(35)
                        //},
                        toca_queja = item.IsNull(36) ? null! : item.Field<string>(36)!,
                        fecha_notificacion_ejecutoria = item.IsNull(37) ? null! : item.Field<DateTime>(37).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion_queja = item.IsNull(38) ? 0 : item.Field<int>(38),
                        sentido_resolucion_queja = item.IsNull(39) ? null! : item.Field<string>(39)!,
                        //id_sentido_resolucion_queja = new()
                        //{
                        //    Value = item.IsNull(38) ? 0 : item.Field<int>(38),
                        //    Label = item.IsNull(39) ? null! : item.Field<string>(39)
                        //},
                        oficio_comunicacion_area_correspondiente = item.IsNull(40) ? null! : item.Field<string>(40)!,
                        fecha_oficio_comunicacion = item.IsNull(41) ? null! : item.Field<DateTime>(41).ToString("yyyy-MM-dd"),
                        //AQUI SE AGREGO UN CAMPO
                        //recurso_revision_principal = response.Data.Tables[0].Rows[0].IsNull(42)
                        //    ? false
                        //    : response.Data.Tables[0].Rows[0].Field<bool>(42),
                        recurso_revision_principal = item.IsNull(42) ? false : item.Field<bool>(42),

                        fecha_recepcion_sentencia = item.IsNull(43) ? null! : item.Field<DateTime>(43).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_revision = item.IsNull(44) ? null! : item.Field<DateTime>(44).ToString("yyyy-MM-dd"),
                        oficio_recurso = item.IsNull(45) ? null! : item.Field<string>(45)!,
                        fecha_presentacion_revision = item.IsNull(46) ? null! : item.Field<DateTime>(46).ToString("yyyy-MM-dd"),
                        fecha_admision = item.IsNull(47) ? null! : item.Field<DateTime>(47).ToString("yyyy-MM-dd"),
                        id_organo_radicacion_revision = item.IsNull(48) ? 0 : item.Field<int>(48),
                        organo_radicacion_revision = item.IsNull(49) ? null! : item.Field<string>(49)!,
                        //id_organo_radicacion_revision = new()
                        //{
                        //    Value = item.IsNull(48) ? 0 : item.Field<int>(48),
                        //    Label = item.IsNull(49) ? null! : item.Field<string>(49)
                        //},
                        toca_revision = item.IsNull(50) ? null! : item.Field<string>(50)!,
                        //revision_adhesiva = response.Data.Tables[0].Rows[0].IsNull(51)
                        //? false
                        //: response.Data.Tables[0].Rows[0].Field<bool>(51),
                        revision_adhesiva = item.IsNull(51) ? false : item.Field<bool>(51),

                        oficio_revision_adhesiva = item.IsNull(52) ? null! : item.Field<string>(52)!,
                        fecha_vecimiento_adhesion = item.IsNull(53) ? null! : item.Field<DateTime>(53).ToString("yyyy-MM-dd"),
                        fecha_notificacion_ejecutoria_revision = item.IsNull(54) ? null! : item.Field<DateTime>(54).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion_revision = item.IsNull(55) ? 0 : item.Field<int>(55),
                        sentido_resolucion_revision = item.IsNull(56) ? null! : item.Field<string>(56)!,
                        //id_sentido_resolucion_revision = new()
                        //{
                        //    Value = item.IsNull(55) ? 0 : item.Field<int>(55),
                        //    Label = item.IsNull(56) ? null! : item.Field<string>(56)
                        //},
                        id_tipo_sentido_revision = item.IsNull(57) ? 0 : item.Field<int>(57),
                        tipo_sentido_revision = item.IsNull(58) ? null! : item.Field<string>(58)!,
                        //id_tipo_sentido_revision = new()
                        //{
                        //    Value = item.IsNull(57) ? 0 : item.Field<int>(57),
                        //    Label = item.IsNull(58) ? null! : item.Field<string>(58)
                        //},
                        id_sentido_general = item.IsNull(59) ? 0 : item.Field<int>(59),
                        sentido_general = item.IsNull(60) ? null! : item.Field<string>(60)!,
                        //id_sentido_general = new()
                        //{
                        //    Value = item.IsNull(59) ? 0 : item.Field<int>(59),
                        //    Label = item.IsNull(60) ? null! : item.Field<string>(60)
                        //},
                        id_tipo_sentido_general_revision = item.IsNull(61) ? 0 : item.Field<int>(61),
                        tipo_sentido_general_revision = item.IsNull(62) ? null! : item.Field<string>(62)!,
                        //id_tipo_sentido_general_revision = new()
                        //{
                        //    Value = item.IsNull(61) ? 0 : item.Field<int>(61),
                        //    Label = item.IsNull(62) ? null! : item.Field<string>(62)
                        //},
                        oficio_comunicacion_autoridad_revision = item.IsNull(63) ? null! : item.Field<string>(63)!,
                        fecha_oficio_comunicacion_revision = item.IsNull(64) ? null! : item.Field<DateTime>(64).ToString("yyyy-MM-dd"),
                        //notificacion_admision_recurso_inconformidad = item.IsNull(73) ? null! : item.Field<DateTime>(73).ToString("yyyy-MM-dd"),
                        //numero_recurso_inconformidad = item.IsNull(74) ? null! : item.Field<string>(74)!,

                        //id_organo_radicacion_inconformidad = new()
                        //{
                        //    Value = item.IsNull(75) ? 0 : item.Field<int>(75),
                        //    Label = item.IsNull(76) ? null! : item.Field<string>(76)
                        //},
                        //fecha_notificacion_resolucion_inconformidad = item.IsNull(77) ? null! : item.Field<DateTime>(77).ToString("yyyy-MM-dd"),

                        //id_sentido_resolucion_inconformidad = new()
                        //{
                        //    Value = item.IsNull(78) ? 0 : item.Field<int>(78),
                        //    Label = item.IsNull(79) ? null! : item.Field<string>(79)
                        //},
                        //recurso_reclamacion = response.Data.Tables[0].Rows[0].IsNull(80)
                        //    ? false
                        //    : response.Data.Tables[0].Rows[0].Field<bool>(80),
                        //notificacion_adminsion_recurso_reclamacion = item.IsNull(81) ? null! : item.Field<DateTime>(81).ToString("yyyy-MM-dd"),
                        //fecha_vencimiento_recurso_reclamacion = item.IsNull(82) ? null! : item.Field<DateTime>(82).ToString("yyyy-MM-dd"),
                        //fecha_presentacion = item.IsNull(83) ? null! : item.Field<DateTime>(83).ToString("yyyy-MM-dd"),
                        //oficio_reclamacion = item.IsNull(84) ? null! : item.Field<string>(84)!,
                        //numero_recurso = item.IsNull(85) ? null! : item.Field<string>(85)!,
                        //id_organo_radicacion = new()
                        //{
                        //    Value = item.IsNull(86) ? 0 : item.Field<int>(86),
                        //    Label = item.IsNull(87) ? null! : item.Field<string>(87)
                        //},
                        //fecha_notificacion_resolucion = item.IsNull(88) ? null! : item.Field<DateTime>(88).ToString("yyyy-MM-dd"),
                        //id_sentido_resolucion_reclamacion = new()
                        //{
                        //    Value = item.IsNull(89) ? 0 : item.Field<int>(89),
                        //    Label = item.IsNull(90) ? null! : item.Field<string>(90)
                        //},

                        //activo = response.Data.Tables[0].Rows[0].IsNull(65)
                        //    ? false
                        //    : response.Data.Tables[0].Rows[0].Field<bool>(65),
                        //Prueba
                        activo = item.IsNull(65) ? false : item.Field<bool>(65),

                    }
                );
            }

            return resultList;
        }
        #endregion

        #region GetAll CUADERNO CONSTITUCIONAL
        public async Task<List<RequestCuadernoConstitucional>> GetCuadernoConstitucionalAsync(int id)
        {
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id),
                //new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
                };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetCuadernoConstitucional,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }



            List<RequestCuadernoConstitucional> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id_numero_asunto = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_autoridad_responsable = item.IsNull(1) ? 0 : item.Field<int>(1),
                        autoridad_responsable = item.IsNull(2) ? null! : item.Field<string>(2)!,

                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(1) ? 0 : item.Field<int>(1),
                        //    Label = item.IsNull(2) ? null! : item.Field<string>(2)
                        //},
                        //solucion_opinion_tecnica = response.Data.Tables[0].Rows[0].IsNull(3)
                        //? false
                        //: response.Data.Tables[0].Rows[0].Field<bool>(3),

                        // inician pruebas
                        solucion_opinion_tecnica = item.IsNull(3) ? false : item.Field<bool>(3),

                        //terminan cambios


                        fecha_recepcion_demanda = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),

                        fecha_vencimiento_justificado = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        numero_oficio_informe_justificado = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        fecha_presentacion_informe_justificado = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        observaciones_justificado = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        fecha_notificacion_sentencia = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),

                        id_sentido_sentencia = item.IsNull(10) ? 0 : item.Field<int>(10),
                        sentido_sentencia = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        //id_sentido_sentencia = new()
                        //{
                        //    Value = item.IsNull(10) ? 0 : item.Field<int>(10),
                        //    Label = item.IsNull(11) ? null! : item.Field<string>(11)
                        //},
                        id_tipo_sentido_sentencia = item.IsNull(12) ? 0 : item.Field<int>(12),
                        tipo_sentido_sentencia = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        //id_tipo_sentido_sentencia = new()
                        //{
                        //    Value = item.IsNull(12) ? 0 : item.Field<int>(12),
                        //    Label = item.IsNull(13) ? null! : item.Field<string>(13)
                        //},
                        id_dictamen_no_revision = item.IsNull(14) ? 0 : item.Field<int>(14),
                        dictamen_no_revision = item.IsNull(15) ? null! : item.Field<string>(15)!,
                        //id_dictamen_no_revision = new()
                        //{
                        //    Value = item.IsNull(14) ? 0 : item.Field<int>(14),
                        //    Label = item.IsNull(15) ? null! : item.Field<string>(15)
                        //},
                        id_sentido_general_asunto = item.IsNull(16) ? 0 : item.Field<int>(16),
                        sentido_general_asunto = item.IsNull(17) ? null! : item.Field<string>(17)!,
                        //id_sentido_general_asunto = new()
                        //{
                        //    Value = item.IsNull(16) ? 0 : item.Field<int>(16),
                        //    Label = item.IsNull(17) ? null! : item.Field<string>(17)
                        //},
                        id_tipo_sentido_general = item.IsNull(18) ? 0 : item.Field<int>(18),
                        tipo_sentido_general = item.IsNull(19) ? null! : item.Field<string>(19)!,
                        //id_tipo_sentido_general = new()
                        //{
                        //    Value = item.IsNull(18) ? 0 : item.Field<int>(18),
                        //    Label = item.IsNull(19) ? null! : item.Field<string>(19)
                        //},
                        oficio_comunicacion_autoridad = item.IsNull(20) ? null! : item.Field<string>(20)!,
                        fecha_oficio_comunicacion_sentencia = item.IsNull(21) ? null! : item.Field<DateTime>(21).ToString("yyyy-MM-dd"),
                        fecha_recepcion_auto_sentencia_ejecutoria = item.IsNull(22) ? null! : item.Field<DateTime>(22).ToString("yyyy-MM-dd"),
                        comunicado_acuerdo_firmeza = item.IsNull(23) ? null! : item.Field<string>(23)!,
                        fecha_conclusion_expediente = item.IsNull(24) ? null! : item.Field<DateTime>(24).ToString("yyyy-MM-dd"),
                        //cumplimiento = response.Data.Tables[0].Rows[0].IsNull(25)
                        //? false
                        //: response.Data.Tables[0].Rows[0].Field<bool>(25),
                        //fecha_notificacion_requerimiento = item.IsNull(26) ? null! : item.Field<DateTime>(26).ToString("yyyy-MM-dd"),
                        //plazo_fallo = item.IsNull(27) ? 0 : item.Field<int>(27),
                        //fecha_vencimiento_requerimiento = item.IsNull(28) ? null! : item.Field<DateTime>(28).ToString("yyyy-MM-dd"),
                        //fecha_presentacion_fallo = item.IsNull(29) ? null! : item.Field<DateTime>(29).ToString("yyyy-MM-dd"),
                        //numero_oficio_atencion = item.IsNull(30) ? null! : item.Field<string>(30)!,
                        //fecha_notificacion_acuerdo_fallo = item.IsNull(31) ? null! : item.Field<DateTime>(31).ToString("yyyy-MM-dd"),
                        //fecha_oficio_comunicacion_fallo = item.IsNull(32) ? null! : item.Field<DateTime>(32).ToString("yyyy-MM-dd"),
                        //numero_oficio_comunicacion_autoridad = item.IsNull(33) ? null! : item.Field<string>(33)!,

                        //aqui se agregaron los datos adicionales
                        //recurso_queja_principal = response.Data.Tables[0].Rows[0].IsNull(25)
                        //    ? false
                        //    : response.Data.Tables[0].Rows[0].Field<bool>(25),
                        recurso_queja_principal = item.IsNull(25) ? false : item.Field<bool>(25),

                        id_recurrente = item.IsNull(26) ? 0 : item.Field<int>(26),
                        fecha_recepcion_acuerdo_queja = item.IsNull(27) ? null! : item.Field<DateTime>(27).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_queja = item.IsNull(28) ? null! : item.Field<DateTime>(28).ToString("yyyy-MM-dd"),
                        oficio_recurso_queja = item.IsNull(29) ? null! : item.Field<string>(29)!,
                        fecha_presentacion_queja = item.IsNull(30) ? null! : item.Field<DateTime>(30).ToString("yyyy-MM-dd"),
                        id_motivo_recurso_queja = item.IsNull(31) ? 0 : item.Field<int>(31),
                        motivo_recurso_queja = item.IsNull(32) ? null! : item.Field<string>(32)!,
                        //id_motivo_recurso_queja = new()
                        //{
                        //    Value = item.IsNull(31) ? 0 : item.Field<int>(31),
                        //    Label = item.IsNull(32) ? null! : item.Field<string>(32)
                        //},
                        fecha_admision_queja = item.IsNull(33) ? null! : item.Field<DateTime>(33).ToString("yyyy-MM-dd"),
                        id_organo_radicacion_queja = item.IsNull(34) ? 0 : item.Field<int>(34),
                        organo_radicacion_queja = item.IsNull(35) ? null! : item.Field<string>(35)!,
                        //id_organo_radicacion_queja = new()
                        //{
                        //    Value = item.IsNull(34) ? 0 : item.Field<int>(34),
                        //    Label = item.IsNull(35) ? null! : item.Field<string>(35)
                        //},
                        toca_queja = item.IsNull(36) ? null! : item.Field<string>(36)!,
                        fecha_notificacion_ejecutoria = item.IsNull(37) ? null! : item.Field<DateTime>(37).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion_queja = item.IsNull(38) ? 0 : item.Field<int>(38),
                        sentido_resolucion_queja = item.IsNull(39) ? null! : item.Field<string>(39)!,
                        //id_sentido_resolucion_queja = new()
                        //{
                        //    Value = item.IsNull(38) ? 0 : item.Field<int>(38),
                        //    Label = item.IsNull(39) ? null! : item.Field<string>(39)
                        //},
                        oficio_comunicacion_area_correspondiente = item.IsNull(40) ? null! : item.Field<string>(40)!,
                        fecha_oficio_comunicacion = item.IsNull(41) ? null! : item.Field<DateTime>(41).ToString("yyyy-MM-dd"),
                        //AQUI SE AGREGO UN CAMPO
                        //recurso_revision_principal = response.Data.Tables[0].Rows[0].IsNull(42)
                        //    ? false
                        //    : response.Data.Tables[0].Rows[0].Field<bool>(42),
                        recurso_revision_principal = item.IsNull(42) ? false : item.Field<bool>(42),

                        fecha_recepcion_sentencia = item.IsNull(43) ? null! : item.Field<DateTime>(43).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_revision = item.IsNull(44) ? null! : item.Field<DateTime>(44).ToString("yyyy-MM-dd"),
                        oficio_recurso = item.IsNull(45) ? null! : item.Field<string>(45)!,
                        fecha_presentacion_revision = item.IsNull(46) ? null! : item.Field<DateTime>(46).ToString("yyyy-MM-dd"),
                        fecha_admision = item.IsNull(47) ? null! : item.Field<DateTime>(47).ToString("yyyy-MM-dd"),
                        id_organo_radicacion_revision = item.IsNull(48) ? 0 : item.Field<int>(48),
                        organo_radicacion_revision = item.IsNull(49) ? null! : item.Field<string>(49)!,
                        //id_organo_radicacion_revision = new()
                        //{
                        //    Value = item.IsNull(48) ? 0 : item.Field<int>(48),
                        //    Label = item.IsNull(49) ? null! : item.Field<string>(49)
                        //},
                        toca_revision = item.IsNull(50) ? null! : item.Field<string>(50)!,
                        //revision_adhesiva = response.Data.Tables[0].Rows[0].IsNull(51)
                        //? false
                        //: response.Data.Tables[0].Rows[0].Field<bool>(51),
                        revision_adhesiva = item.IsNull(51) ? false : item.Field<bool>(51),

                        oficio_revision_adhesiva = item.IsNull(52) ? null! : item.Field<string>(52)!,
                        fecha_vecimiento_adhesion = item.IsNull(53) ? null! : item.Field<DateTime>(53).ToString("yyyy-MM-dd"),
                        fecha_notificacion_ejecutoria_revision = item.IsNull(54) ? null! : item.Field<DateTime>(54).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion_revision = item.IsNull(55) ? 0 : item.Field<int>(55),
                        sentido_resolucion_revision = item.IsNull(56) ? null! : item.Field<string>(56)!,
                        //id_sentido_resolucion_revision = new()
                        //{
                        //    Value = item.IsNull(55) ? 0 : item.Field<int>(55),
                        //    Label = item.IsNull(56) ? null! : item.Field<string>(56)
                        //},
                        id_tipo_sentido_revision = item.IsNull(57) ? 0 : item.Field<int>(57),
                        tipo_sentido_revision = item.IsNull(58) ? null! : item.Field<string>(58)!,
                        //id_tipo_sentido_revision = new()
                        //{
                        //    Value = item.IsNull(57) ? 0 : item.Field<int>(57),
                        //    Label = item.IsNull(58) ? null! : item.Field<string>(58)
                        //},
                        id_sentido_general = item.IsNull(59) ? 0 : item.Field<int>(59),
                        sentido_general = item.IsNull(60) ? null! : item.Field<string>(60)!,
                        //id_sentido_general = new()
                        //{
                        //    Value = item.IsNull(59) ? 0 : item.Field<int>(59),
                        //    Label = item.IsNull(60) ? null! : item.Field<string>(60)
                        //},
                        id_tipo_sentido_general_revision = item.IsNull(61) ? 0 : item.Field<int>(61),
                        tipo_sentido_general_revision = item.IsNull(62) ? null! : item.Field<string>(62)!,
                        //id_tipo_sentido_general_revision = new()
                        //{
                        //    Value = item.IsNull(61) ? 0 : item.Field<int>(61),
                        //    Label = item.IsNull(62) ? null! : item.Field<string>(62)
                        //},
                        oficio_comunicacion_autoridad_revision = item.IsNull(63) ? null! : item.Field<string>(63)!,
                        fecha_oficio_comunicacion_revision = item.IsNull(64) ? null! : item.Field<DateTime>(64).ToString("yyyy-MM-dd"),
                        //notificacion_admision_recurso_inconformidad = item.IsNull(73) ? null! : item.Field<DateTime>(73).ToString("yyyy-MM-dd"),
                        //numero_recurso_inconformidad = item.IsNull(74) ? null! : item.Field<string>(74)!,

                        //id_organo_radicacion_inconformidad = new()
                        //{
                        //    Value = item.IsNull(75) ? 0 : item.Field<int>(75),
                        //    Label = item.IsNull(76) ? null! : item.Field<string>(76)
                        //},
                        //fecha_notificacion_resolucion_inconformidad = item.IsNull(77) ? null! : item.Field<DateTime>(77).ToString("yyyy-MM-dd"),

                        //id_sentido_resolucion_inconformidad = new()
                        //{
                        //    Value = item.IsNull(78) ? 0 : item.Field<int>(78),
                        //    Label = item.IsNull(79) ? null! : item.Field<string>(79)
                        //},
                        //recurso_reclamacion = response.Data.Tables[0].Rows[0].IsNull(80)
                        //    ? false
                        //    : response.Data.Tables[0].Rows[0].Field<bool>(80),
                        //notificacion_adminsion_recurso_reclamacion = item.IsNull(81) ? null! : item.Field<DateTime>(81).ToString("yyyy-MM-dd"),
                        //fecha_vencimiento_recurso_reclamacion = item.IsNull(82) ? null! : item.Field<DateTime>(82).ToString("yyyy-MM-dd"),
                        //fecha_presentacion = item.IsNull(83) ? null! : item.Field<DateTime>(83).ToString("yyyy-MM-dd"),
                        //oficio_reclamacion = item.IsNull(84) ? null! : item.Field<string>(84)!,
                        //numero_recurso = item.IsNull(85) ? null! : item.Field<string>(85)!,
                        //id_organo_radicacion = new()
                        //{
                        //    Value = item.IsNull(86) ? 0 : item.Field<int>(86),
                        //    Label = item.IsNull(87) ? null! : item.Field<string>(87)
                        //},
                        //fecha_notificacion_resolucion = item.IsNull(88) ? null! : item.Field<DateTime>(88).ToString("yyyy-MM-dd"),
                        //id_sentido_resolucion_reclamacion = new()
                        //{
                        //    Value = item.IsNull(89) ? 0 : item.Field<int>(89),
                        //    Label = item.IsNull(90) ? null! : item.Field<string>(90)
                        //},

                        //activo = response.Data.Tables[0].Rows[0].IsNull(65)
                        //    ? false
                        //    : response.Data.Tables[0].Rows[0].Field<bool>(65),
                        //Prueba
                        activo = item.IsNull(65) ? false : item.Field<bool>(65),

                    }
                );
            }

            return resultList;
        }
        #endregion

        #region GetAll RECURSO REVISION DISCONNECTED
        public async Task<List<RecursoRevisionPrincipalConstitucionalDisconnected>> GetRecursoRevisionPrincipalIdDisconnectedAsync(int id)
        {
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id),
                //new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
                };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetRecursoRevisionConstitucionalDisconnected,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }



            List<RecursoRevisionPrincipalConstitucionalDisconnected> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        recurso_revision_principal = item.IsNull(3) ? false : item.Field<bool>(3),
                        id_recurrente = item.IsNull(4) ? 0 : item.Field<int>(4),


                        fecha_recepcion_sentencia = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_revision = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        oficio_recurso = item.IsNull(7) ? null! : item.Field<string>(7)!,

                        fecha_presentacion_revision = item.IsNull(8) ? null! : item.Field<DateTime>(8).ToString("yyyy-MM-dd"),
                        fecha_admision = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),
                        id_organo_radicacion_revision = item.IsNull(10) ? 0 : item.Field<int>(10),

                        toca_revision = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        revision_adhesiva = item.IsNull(12) ? false : item.Field<bool>(12),
                        oficio_revision_adhesiva = item.IsNull(13) ? null! : item.Field<string>(13)!,

                        fecha_vencimiento_adhesion = item.IsNull(14) ? null! : item.Field<DateTime>(14).ToString("yyyy-MM-dd"),
                        fecha_notificacion_ejecutoria_revision = item.IsNull(15) ? null! : item.Field<DateTime>(15).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion_revision = item.IsNull(16) ? 0 : item.Field<int>(16),
                        id_tipo_sentido_revision = item.IsNull(17) ? 0 : item.Field<int>(17),
                        id_tipo_sentido_general_revision = item.IsNull(18) ? 0 : item.Field<int>(18),
                        id_sentido_general = item.IsNull(19) ? 0 : item.Field<int>(19),
                        oficio_comunicacion_autoridad_revision = item.IsNull(20) ? null! : item.Field<string>(20)!,
                        fecha_oficio_comunicacion_revision = item.IsNull(21) ? null! : item.Field<DateTime>(21).ToString("yyyy-MM-dd"),

                    }
                );
            }

            return resultList;
        }
        #endregion

        #region GetAll Informe previo incidental
        public async Task<List<ResponseInformePrevioIncidental>> GetAllInformePrevioIncidentalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.InformePrevioIncidentalGetAll,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseInformePrevioIncidental> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        fecha_apertura_incidente = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),
                        fecha_vencimiento = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        numero_oficio_informe_previo = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        fecha_presentacion_informe_previo = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        activo = item.IsNull(8) ? false : item.Field<bool>(8),



                        //id_juicio_amparo = item.IsNull(0) ? 0 : item.Field<int>(0),
                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(1) ? 0 : item.Field<int>(1),
                        //    Label = item.IsNull(2) ? null! : item.Field<string>(2)
                        //},
                        //fecha_apertura_incidente = item.IsNull(3) ? null! : item.Field<DateTime>(3).ToString("yyyy-MM-dd"),
                        //fecha_vencimiento = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),
                        //numero_oficio_informe_previo = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        //fecha_presentacion_informe_previo = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        //activo = item.IsNull(7) ? false : item.Field<bool>(7),
                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll Sentencia Incidental
        public async Task<List<ResponseSentenciaIncidental>> GetAllSentenciaIncidentalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.SentenciaIncidentalGetAll,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseSentenciaIncidental> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,

                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        fecha_notificacion_sentencia = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),
                        id_sentido_suspencion_definitiva = item.IsNull(5) ? 0 : item.Field<int>(5),
                        sentido_suspencion_definitiva = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        //id_sentido_suspencion_definitiva = new()
                        //{
                        //    Value = item.IsNull(5) ? 0 : item.Field<int>(5),
                        //    Label = item.IsNull(6) ? null! : item.Field<string>(6)
                        //},
                        id_otorgamiento_garatia = item.IsNull(7) ? 0 : item.Field<int>(7),
                        id_sentido_general_asunto = item.IsNull(8) ? 0 : item.Field<int>(8),
                        sentido_general_asunto = item.IsNull(9) ? null! : item.Field<string>(9)!,
                        //id_sentido_general_asunto = new()
                        //{
                        //    Value = item.IsNull(8) ? 0 : item.Field<int>(8),
                        //    Label = item.IsNull(9) ? null! : item.Field<string>(9)
                        //},
                        id_tipo_sentido_general_asunto = item.IsNull(10) ? 0 : item.Field<int>(10),
                        tipo_sentido_general_asunto = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        //id_tipo_sentido_general_asunto = new()
                        //{
                        //    Value = item.IsNull(10) ? 0 : item.Field<int>(10),
                        //    Label = item.IsNull(11) ? null! : item.Field<string>(11)
                        //},

                        oficio_comunicacion_area_correspondiente = item.IsNull(12) ? null! : item.Field<string>(12)!,
                        fecha_comunicacion_suspencion = item.IsNull(13) ? null! : item.Field<DateTime>(13).ToString("yyyy-MM-dd"),
                        id_dictamen_revision_incidental = item.IsNull(14) ? 0 : item.Field<int>(14),
                        activo = item.IsNull(15) ? false : item.Field<bool>(15),
                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll recurso queja incidental
        public async Task<List<RecursoQuejaIncidentalDisconected>> GetAllRecursoQuejaIncidentalDisconnectedAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.RecursoQuejaIncidentalGetAllDisconnectInterno,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<RecursoQuejaIncidentalDisconected> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        recurso_queja_incidental = item.IsNull(3) ? false : item.Field<bool>(3),
                        id_recurrente = item.IsNull(4) ? 0 : item.Field<int>(4),
                        fecha_recepcion_apertura = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_queja = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        oficio_queja = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        fecha_presentacion_recurso_queja = item.IsNull(8) ? null! : item.Field<DateTime>(8).ToString("yyyy-MM-dd"),
                        fecha_admision = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),
                        id_organo_radicacion = item.IsNull(10) ? 0 : item.Field<int>(10),
                        toca_queja_incidental = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        fecha_notificacion_ejecutoria = item.IsNull(12) ? null! : item.Field<DateTime>(12).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion_ejecutoria = item.IsNull(13) ? 0 : item.Field<int>(13),
                        oficio_comunicacion_area_correspondiente = item.IsNull(14) ? null! : item.Field<string>(14)!,
                        fecha_oficio_comunicacion = item.IsNull(15) ? null! : item.Field<DateTime>(15).ToString("yyyy-MM-dd"),
                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll recurso revision incidental
        public async Task<List<RecursoRevisionIncidentalDisconnected>> GetAllRecursoRevisionIncidentalDisconnectedAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.RecursoRevisionIncidentalGetAllDisconnectInterno,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<RecursoRevisionIncidentalDisconnected> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        recurso_revision_incidental = item.IsNull(3) ? false : item.Field<bool>(3),
                        id_recurrente = item.IsNull(4) ? 0 : item.Field<int>(4),
                        fecha_recepcion_sentencia = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_revision = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        oficio_recurso = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        fecha_presentacion_recurso_revision = item.IsNull(8) ? null! : item.Field<DateTime>(8).ToString("yyyy-MM-dd"),
                        fecha_admision = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),
                        id_organo_radicacion = item.IsNull(10) ? 0 : item.Field<int>(10),
                        toca_recurso_revision_incidental = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        revision_adhesiva = item.IsNull(12) ? false : item.Field<bool>(12),
                        oficio_revision_adhesiva = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        fecha_vencimiento_adhesion = item.IsNull(14) ? null! : item.Field<DateTime>(14).ToString("yyyy-MM-dd"),
                        fecha_presentacion_adhesiva = item.IsNull(15) ? null! : item.Field<DateTime>(15).ToString("yyyy-MM-dd"),
                        fecha_notificacion_resolucion = item.IsNull(16) ? null! : item.Field<DateTime>(16).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion = item.IsNull(17) ? 0 : item.Field<int>(17),
                        id_sentido_general_asunto = item.IsNull(18) ? 0 : item.Field<int>(18),
                        id_tipo_sentido = item.IsNull(19) ? 0 : item.Field<int>(19),
                        oficio_comunicacion_autoridad = item.IsNull(20) ? null! : item.Field<string>(20)!,
                        fecha_oficio_comunicacion = item.IsNull(21) ? null! : item.Field<DateTime>(21).ToString("yyyy-MM-dd"),
                        activo = item.IsNull(22) ? false : item.Field<bool>(22),
                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll recurso queja incidental
        public async Task<List<ResponseRecursoQuejaIncidental>> GetAllRecursoQuejaIncidentalAsync(int id/*, int recurrente*/)
        {
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id),
                //new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer, recurrente),
                };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.RecursoQuejaIncidentalGetAll,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseRecursoQuejaIncidental> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,

                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        recurso_queja = item.IsNull(4) ? false : item.Field<bool>(4),
                        recurrente = item.IsNull(5) ? 0 : item.Field<int>(5),
                        fecha_recepcion_apertura = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_queja = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        oficio_queja = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        fecha_presentacion_recurso_queja = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),
                        fecha_admision = item.IsNull(10) ? null! : item.Field<DateTime>(10).ToString("yyyy-MM-dd"),

                        id_organo_radicacion = item.IsNull(11) ? 0 : item.Field<int>(11),
                        organo_radicacion = item.IsNull(12) ? null! : item.Field<string>(12)!,
                        //organo_radicacion = new()
                        //{
                        //    Value = item.IsNull(11) ? 0 : item.Field<int>(11),
                        //    Label = item.IsNull(12) ? null! : item.Field<string>(12)
                        //},
                        toca = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        fecha_notificacion_ejecutoria = item.IsNull(14) ? null! : item.Field<DateTime>(14).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion = item.IsNull(15) ? 0 : item.Field<int>(15),
                        sentido_resolucion = item.IsNull(16) ? null! : item.Field<string>(16)!,
                        //sentido_resolucion = new()
                        //{
                        //    Value = item.IsNull(15) ? 0 : item.Field<int>(15),
                        //    Label = item.IsNull(16) ? null! : item.Field<string>(16)
                        //},
                        oficio_comunicacion_area_correspondiente = item.IsNull(17) ? null! : item.Field<string>(17)!,
                        fecha_oficio_comunicacion = item.IsNull(18) ? null! : item.Field<DateTime>(18).ToString("yyyy-MM-dd"),
                        ////usuario = item.IsNull(19) ? null! : item.Field<string>(19)!,
                        ////fecha_modificacion = item.IsNull(20) ? null! : item.Field<DateTime>(20).ToString("yyyy-MM-dd"),
                        activo = item.IsNull(19) ? false : item.Field<bool>(19),
                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll recurso revision incidental
        public async Task<List<ResponseRecursoQuejaRevisionIncidental>> GetAllRecursoRevisionIncidentalAsync(int id)
        {
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id),
                //new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer, recurrente),
                };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.RecursoRevisionIncidentalGetAll,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseRecursoQuejaRevisionIncidental> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1), //se modifico
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        recurso_revision_incidental = item.IsNull(4) ? false : item.Field<bool>(4),
                        id_recurrente = item.IsNull(5) ? 0 : item.Field<int>(5),
                        fecha_recepcion_sentencia = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_revision = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        oficio_recurso = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        fecha_presentacion_recurso_revision = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),
                        fecha_admision = item.IsNull(10) ? null! : item.Field<DateTime>(10).ToString("yyyy-MM-dd"),

                        id_organo_radicacion = item.IsNull(11) ? 0 : item.Field<int>(11),
                        organo_radicacion = item.IsNull(12) ? null! : item.Field<string>(12)!,
                        //id_organo_radicacion = new()
                        //{
                        //    Value = item.IsNull(11) ? 0 : item.Field<int>(11),
                        //    Label = item.IsNull(12) ? null! : item.Field<string>(12)
                        //},

                        toca_recurso_revision_incidental = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        revision_adhesiva = item.IsNull(14) ? false : item.Field<bool>(14),
                        oficio_revision_adhesiva = item.IsNull(15) ? null! : item.Field<string>(15)!,
                        fecha_vencimiento_adhesion = item.IsNull(16) ? null! : item.Field<DateTime>(16).ToString("yyyy-MM-dd"),
                        fecha_presentacion_adhesiva = item.IsNull(17) ? null! : item.Field<DateTime>(17).ToString("yyyy-MM-dd"),
                        fecha_notificacion_ejecutoria = item.IsNull(18) ? null! : item.Field<DateTime>(18).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion = item.IsNull(19) ? 0 : item.Field<int>(19),
                        sentido_resolucion = item.IsNull(20) ? null! : item.Field<string>(20)!,
                        //id_sentido_resolucion = new()
                        //{
                        //    Value = item.IsNull(19) ? 0 : item.Field<int>(19),
                        //    Label = item.IsNull(20) ? null! : item.Field<string>(20)
                        //},
                        id_tipo_sentido = item.IsNull(21) ? 0 : item.Field<int>(21),
                        tipo_sentido = item.IsNull(22) ? null! : item.Field<string>(22)!,
                        //id_tipo_sentido = new()
                        //{
                        //    Value = item.IsNull(21) ? 0 : item.Field<int>(21),
                        //    Label = item.IsNull(22) ? null! : item.Field<string>(22)
                        //},
                        id_sentido_general_asunto = item.IsNull(23) ? 0 : item.Field<int>(23),
                        sentido_general_asunto = item.IsNull(24) ? null! : item.Field<string>(24)!,
                        //id_sentido_general_asunto = new()
                        //{
                        //    Value = item.IsNull(23) ? 0 : item.Field<int>(23),
                        //    Label = item.IsNull(24) ? null! : item.Field<string>(24)
                        //},
                        id_tipo_sentido_general = item.IsNull(25) ? 0 : item.Field<int>(25),
                        tipo_sentido_general = item.IsNull(26) ? null! : item.Field<string>(26)!,
                        //id_tipo_sentido_general = new()
                        //{
                        //    Value = item.IsNull(25) ? 0 : item.Field<int>(25),
                        //    Label = item.IsNull(26) ? null! : item.Field<string>(26)
                        //},
                        oficio_comunicacion_autoridad = item.IsNull(27) ? null! : item.Field<string>(27)!,

                        fecha_oficio_comunicacion = item.IsNull(28) ? null! : item.Field<DateTime>(28).ToString("yyyy-MM-dd"),
                        ////usuario = item.IsNull(19) ? null! : item.Field<string>(19)!,
                        ////fecha_modificacion = item.IsNull(20) ? null! : item.Field<DateTime>(20).ToString("yyyy-MM-dd"),
                        activo = item.IsNull(29) ? false : item.Field<bool>(29),
                    }
                );
            }
            return resultList;
        }
        #endregion

        #region Recurso de revision incidental
        #region Recurso revision incidental autoriades responsables 
        public async Task<ResultTransaction> CreateRecursoRevisionIncidentalResponsableAsync(RecursoRevisionIncidental entity)
        {

            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer,  entity.id_numero_asunto),//id juicio de amparo
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable!),
                new ParameterPGsql("p_recurso_revision_incidental", NpgsqlDbType.Boolean,  entity.recurso_revision_incidental!),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer,  entity.id_recurrente!),
                new ParameterPGsql("p_fecha_recepcion_sentencia", NpgsqlDbType.Date, entity.fecha_recepcion_sentencia!),
                new ParameterPGsql("p_fecha_vencimiento_recurso_revision", NpgsqlDbType.Date, entity.fecha_vencimiento_recurso_revision!),
                new ParameterPGsql("p_oficio_recurso", NpgsqlDbType.Text, entity.oficio_recurso!),
                new ParameterPGsql("p_fecha_presentacion_recurso_revision", NpgsqlDbType.Date, entity.fecha_presentacion_recurso_revision!),
                new ParameterPGsql("p_fecha_admision", NpgsqlDbType.Date, entity.fecha_admision!),
                new ParameterPGsql("p_id_organo_radicacion", NpgsqlDbType.Integer, entity.id_organo_radicacion),
                new ParameterPGsql("p_toca_recurso_revision_incidental", NpgsqlDbType.Text, entity.toca_recurso_revision_incidental!),
                new ParameterPGsql("p_revision_adhesiva", NpgsqlDbType.Boolean,  entity.revision_adhesiva),
                new ParameterPGsql("p_oficio_revision_adhesiva", NpgsqlDbType.Text, entity.oficio_revision_adhesiva!),
                new ParameterPGsql("p_fecha_vencimiento_adhesion", NpgsqlDbType.Date, entity.fecha_vencimiento_adhesion!),
                new ParameterPGsql("p_fecha_presentacion_adhesiva", NpgsqlDbType.Date, entity.fecha_presentacion_adhesion!),
                new ParameterPGsql("p_fecha_notificacion_resolucion", NpgsqlDbType.Date,  entity.fecha_notificacion_resolucion),
                new ParameterPGsql("p_id_sentido_resolucion", NpgsqlDbType.Integer, entity.id_sentido_resolucion!),
                new ParameterPGsql("p_id_tipo_sentido", NpgsqlDbType.Integer, entity.id_tipo_sentido!),
                new ParameterPGsql("p_id_sentido_general_asunto", NpgsqlDbType.Integer, entity.id_tipo_sentido_general!),
                new ParameterPGsql("p_id_tipo_sentido_general", NpgsqlDbType.Integer, entity.id_tipo_sentido_general!),
                new ParameterPGsql("p_oficio_comunicacion_autoridad", NpgsqlDbType.Text, entity.oficio_comunicacion_autoridad!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoRevisionIncidentalResponsable,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }
        #endregion
        #region Recurso revision incidental Otras Autoridades
        public async Task<ResultTransaction> CreateRecursoRevisionIncidentalOtrosAsync(RecursoRevisionIncidental entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer,  entity.id_numero_asunto),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable!),
                new ParameterPGsql("p_recurso_revision_incidental", NpgsqlDbType.Boolean,  entity.recurso_revision_incidental!),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer,  entity.id_recurrente!),
                new ParameterPGsql("p_fecha_admision", NpgsqlDbType.Date, entity.fecha_admision!),
                new ParameterPGsql("p_id_organo_radicacion", NpgsqlDbType.Integer, entity.id_organo_radicacion),
                new ParameterPGsql("p_toca_recurso_revision_incidental", NpgsqlDbType.Text, entity.toca_recurso_revision_incidental!),
                new ParameterPGsql("p_fecha_notificacion_resolucion", NpgsqlDbType.Date,  entity.fecha_notificacion_resolucion),
                new ParameterPGsql("p_id_sentido_resolucion", NpgsqlDbType.Integer, entity.id_sentido_resolucion!),
                new ParameterPGsql("p_id_tipo_sentido", NpgsqlDbType.Integer, entity.id_tipo_sentido!),
                new ParameterPGsql("p_id_sentido_general_asunto", NpgsqlDbType.Integer, entity.id_tipo_sentido_general!),
                new ParameterPGsql("p_id_tipo_sentido_general", NpgsqlDbType.Integer, entity.id_tipo_sentido_general!),
                new ParameterPGsql("p_oficio_comunicacion_autoridad", NpgsqlDbType.Text, entity.oficio_comunicacion_autoridad!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoRevisionIncidentalOtros,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }
        #endregion
        #region Recurso revision incidental quejoso
        public async Task<ResultTransaction> CreateRecursoRevisionIncidentalQuejosoAsync(RecursoRevisionIncidental entity)
        {

            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer,  entity.id_numero_asunto),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable!),
                new ParameterPGsql("p_recurso_revision_incidental", NpgsqlDbType.Boolean,  entity.recurso_revision_incidental!),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer,  entity.id_recurrente!),
                new ParameterPGsql("p_fecha_admision", NpgsqlDbType.Date, entity.fecha_admision!),
                new ParameterPGsql("p_id_organo_radicacion", NpgsqlDbType.Integer, entity.id_organo_radicacion),
                new ParameterPGsql("p_toca_recurso_revision_incidental", NpgsqlDbType.Text, entity.toca_recurso_revision_incidental!),
                new ParameterPGsql("p_fecha_notificacion_resolucion", NpgsqlDbType.Date,  entity.fecha_notificacion_resolucion),
                new ParameterPGsql("p_id_sentido_resolucion", NpgsqlDbType.Integer, entity.id_sentido_resolucion!),
                new ParameterPGsql("p_id_tipo_sentido", NpgsqlDbType.Integer, entity.id_tipo_sentido!),
                new ParameterPGsql("p_id_sentido_general_asunto", NpgsqlDbType.Integer, entity.id_tipo_sentido_general!),
                new ParameterPGsql("p_id_tipo_sentido_general", NpgsqlDbType.Integer, entity.id_tipo_sentido_general!),
                new ParameterPGsql("p_oficio_comunicacion_autoridad", NpgsqlDbType.Text, entity.oficio_comunicacion_autoridad!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoRevisionIncidentalQuejoso,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }
        #endregion


        #endregion

        #region Incidente por Exceso o Defecto de Cumplimiento
        public async Task<ResultTransaction> CreateIncidenteExcesoIncidentalAsync(IncidenteExcesoIncidental entity)
        {

            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer,  entity.id_numero_asunto),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable),
                new ParameterPGsql("p_interposicion_incidente", NpgsqlDbType.Boolean, entity.interposicion_incidente),
                new ParameterPGsql("p_fecha_notificacion_acuerdo", NpgsqlDbType.Date, entity.fecha_notificacion_acuerdo!),
                new ParameterPGsql("p_oficio_desahogo", NpgsqlDbType.Text, entity.oficio_desahogo!),
                new ParameterPGsql("p_fecha_oficio_desahogo", NpgsqlDbType.Date, entity.fecha_oficio_desahogo!),
                new ParameterPGsql("p_id_sentido", NpgsqlDbType.Integer, entity.id_sentido),
                new ParameterPGsql("p_fecha_notificacion_resolucion", NpgsqlDbType.Date, entity.fecha_notificacion_resolucion!),
                new ParameterPGsql("p_oficio_comunicacion_autoridad", NpgsqlDbType.Text, entity.oficio_comunicacion_autoridad!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateIncidenteExceso,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }

        #endregion

        #region GetAll Incidente por Exceso Incidental
        public async Task<List<ResponseIncidenteExcesoIncidental>> GetAllIncidenteExcesoIncidentalAsync(int id)
        {
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id),
                };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetIncidenteExceso,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseIncidenteExcesoIncidental> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        interposicion_incidente = item.IsNull(4) ? false : item.Field<bool>(4),
                        fecha_notificacion_acuerdo = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        oficio_desahogo = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        fecha_oficio_desahogo = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        id_sentido = item.IsNull(8) ? 0 : item.Field<int>(8),
                        sentido = item.IsNull(9) ? null! : item.Field<string>(9)!,
                        //id_sentido = new()
                        //{
                        //    Value = item.IsNull(8) ? 0 : item.Field<int>(8),
                        //    Label = item.IsNull(9) ? null! : item.Field<string>(9)
                        //},

                        fecha_notificacion_resolucion = item.IsNull(10) ? null! : item.Field<DateTime>(10).ToString("yyyy-MM-dd"),

                        oficio_comunicacion_autoridad = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        fecha_oficio_comunicacion = item.IsNull(12) ? null! : item.Field<DateTime>(12).ToString("yyyy-MM-dd"),

                    }
                );
            }
            return resultList;
        }
        #endregion

        //#region GetAll Suspension Provisional Incidental
        //public async Task<List<ResponseSuspensionProvisionalIncidental>> GetAllSuspensionProvisionalIncidentalAsync(int id)
        //{
        //    ParameterPGsql[] parameters =
        //        {
        //        new ParameterPGsql("p_id", NpgsqlDbType.Integer, id),
        //        };

        //    var response = await _database.ExecuteFunctionAsync(
        //        EnumFunctions.GetAllSuspensionProvisional,
        //        parameters
        //    );
        //    if (response.ExisteError)
        //    {
        //        throw new Exception(response.Mensaje);
        //    }

        //    if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
        //    {
        //        return null!;
        //    }
        //    List<ResponseSuspensionProvisionalIncidental> resultList = new();
        //    foreach (DataRow item in response.Data.Tables[0].Rows)
        //    {
        //        resultList.Add(
        //            new()
        //            {
        //                id = item.IsNull(0) ? 0 : item.Field<int>(0),
        //                id_juicio_amparo = item.IsNull(1) ? 0 : item.Field<int>(1),
        //                suspension_provisional = item.IsNull(2) ? false : item.Field<bool>(2),
        //                otorgamiento_garantia = item.IsNull(3) ? 0 : item.Field<int>(3),
        //                oficio_comunicacion_suspension = item.IsNull(4) ? null! : item.Field<string>(4)!,
        //                fecha_comunicacion = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
        //                activo = item.IsNull(6) ? false : item.Field<bool>(6),
        //            }
        //        );
        //    }
        //    return resultList;
        //}
        //#endregion

        //comentar inicio

        #region GetAll Suspension Provisional Incidental           
        public async Task<ResponseSuspensionProvisionalIncidental> GetAllSuspensionProvisionalIncidentalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllSuspensionProvisional,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            return new()
            {
                //id = item.IsNull(0) ? 0 : item.Field<int>(0),
                //id_juicio_amparo = item.IsNull(1) ? 0 : item.Field<int>(1),
                //suspension_provisional = item.IsNull(2) ? false : item.Field<bool>(2),
                //otorgamiento_garantia = item.IsNull(3) ? 0 : item.Field<int>(3),
                //oficio_comunicacion_suspension = item.IsNull(4) ? null! : item.Field<string>(4)!,
                //fecha_comunicacion = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                //activo = item.IsNull(6) ? false : item.Field<bool>(6),

                id = response.Data.Tables[0].Rows[0].IsNull(0) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(0),
                id_numero_asunto = response.Data.Tables[0].Rows[0].IsNull(1) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(1),
                suspension_provisional = response.Data.Tables[0].Rows[0].IsNull(2) ? false : response.Data.Tables[0].Rows[0].Field<bool>(2),
                otorgamiento_garantia = response.Data.Tables[0].Rows[0].IsNull(3) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(3),
                oficio_comunicacion_suspension = response.Data.Tables[0].Rows[0].IsNull(4) ? null! : response.Data.Tables[0].Rows[0].Field<string>(4)!,
                fecha_comunicacion = response.Data.Tables[0].Rows[0].IsNull(5) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(5).ToString("yyyy-MM-dd"),
                activo = response.Data.Tables[0].Rows[0].IsNull(6) ? false : response.Data.Tables[0].Rows[0].Field<bool>(6),

            };
        }
        #endregion

        //comentar fin

        #region GetAll Bandeja de Pendientes Abogado
        public async Task<int?> GetAllAbogadoCount()
        {
            //ParameterPGsql[] parameters = {
            //    //new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
            //    //new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, EnumEstadoProcesal.ACTIVO.GetHashCode()),
            //};

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.BandejaAbogadoGetAllCount
            //,parameters!
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            return response.Data.Tables[0].Rows[0].IsNull(0)
                ? null!
                : response.Data.Tables[0].Rows[0].Field<int>(0);
        }

        public async Task<List<ResponseAmparoIndirectoAbogadoBandeja>> GetAllAbogado(
                int pageSize,
                int page,
                string orderByColumn,
                bool orderDesc
            )
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, pageSize),
                new ParameterPGsql("p_page", NpgsqlDbType.Integer, page),
                //new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
                //new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, EnumEstadoProcesal.ACTIVO.GetHashCode()),
                new ParameterPGsql("order_column", NpgsqlDbType.Text, orderByColumn /*null*/),
                new ParameterPGsql("order_desc", NpgsqlDbType.Boolean, orderDesc /*null*/),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.BandejaAbogadoGetAll,
                parameters!
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            List<ResponseAmparoIndirectoAbogadoBandeja> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {

                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        fecha_recepcion = item.IsNull(1) ? null! : item.Field<DateTime>(1).ToString("yyyy-MM-dd"),
                        fecha_vencimiento = item.IsNull(2) ? null! : item.Field<DateTime>(2).ToString("yyyy-MM-dd"),
                        id_alerta = item.IsNull(3) ? 0 : item.Field<int>(3),
                        alerta = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        numero_expediente = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        numero_asunto = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        id_juzgado = item.IsNull(7) ? 0 : item.Field<int>(7),
                        juzgado = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        //id_juzgado = new()
                        //{
                        //    Value = item.IsNull(7) ? 0 : item.Field<int>(7),
                        //    Label = item.IsNull(8) ? null! : item.Field<string>(8)
                        //},
                        nombre_quejoso = item.IsNull(9) ? null! : item.Field<string>(9)!,
                        id_materia = item.IsNull(10) ? 0 : item.Field<int>(10),
                        materia = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        //id_materia = new()
                        //{
                        //    Value = item.IsNull(10) ? 0 : item.Field<int>(10),
                        //    Label = item.IsNull(11) ? null! : item.Field<string>(11)
                        //},
                        id_submateria = item.IsNull(12) ? 0 : item.Field<int>(12),
                        submateria = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        //id_submateria = new()
                        //{
                        //    Value = item.IsNull(12) ? 0 : item.Field<int>(12),
                        //    Label = item.IsNull(13) ? null! : item.Field<string>(13)
                        //},
                        id_tipo_acto = item.IsNull(14) ? 0 : item.Field<int>(14),
                        tipo_acto = item.IsNull(15) ? null! : item.Field<string>(15)!,
                        //id_tipo_acto = new()
                        //{
                        //    Value = item.IsNull(14) ? 0 : item.Field<int>(14),
                        //    Label = item.IsNull(15) ? null! : item.Field<string>(15)
                        //},

                        despacho = item.IsNull(16) ? null! : item.Field<string>(16)!,
                        //numero_autoridad = item.IsNull(17) ? 0 : item.Field<int>(17),
                        numero_autoridad = item.IsNull(17) ? 0 : (int)item.Field<long>(17),


                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(17) ? 0 : item.Field<int>(17),
                        //    Label = item.IsNull(18) ? null! : item.Field<string>(18)
                        //},
                        id_administracion = item.IsNull(18) ? 0 : item.Field<int>(18),
                        administracion = item.IsNull(19) ? null! : item.Field<string>(19)!,
                        //id_administracion = new()
                        //{
                        //    Value = item.IsNull(18) ? 0 : item.Field<int>(18),
                        //    Label = item.IsNull(19) ? null! : item.Field<string>(19)
                        //},
                        id_subadministracion = item.IsNull(20) ? 0 : item.Field<int>(20),
                        subadministracion = item.IsNull(21) ? null! : item.Field<string>(21)!,
                        //id_subadministracion = new()
                        //{
                        //    Value = item.IsNull(20) ? 0 : item.Field<int>(20),
                        //    Label = item.IsNull(21) ? null! : item.Field<string>(21)
                        //},
                        id_tipo_cuaderno = item.IsNull(22) ? 0 : item.Field<int>(22),
                        tipo_cuaderno = item.IsNull(23) ? null! : item.Field<string>(23)!,
                        //id_tipo_cuaderno = new()
                        //{
                        //    Value = item.IsNull(22) ? 0 : item.Field<int>(22),
                        //    Label = item.IsNull(23) ? null! : item.Field<string>(23)
                        //},
                        rfc_quejoso = item.IsNull(24) ? null! : item.Field<string>(24)!,
                        id_estado_tarea = item.IsNull(25) ? 0 : item.Field<int>(25),
                        estado_tarea = item.IsNull(26) ? null! : item.Field<string>(26)!,
                        //id_estado_tarea = new()
                        //{
                        //    Value = item.IsNull(25) ? 0 : item.Field<int>(25),
                        //    Label = item.IsNull(26) ? null! : item.Field<string>(26)
                        //},
                        id_estado_procesal = item.IsNull(27) ? 0 : item.Field<int>(27),
                        estado_procesal = item.IsNull(28) ? null! : item.Field<string>(28)!,
                        //id_estado_procesal = new()
                        //{
                        //    Value = item.IsNull(27) ? 0 : item.Field<int>(27),
                        //    Label = item.IsNull(28) ? null! : item.Field<string>(28)
                        //},
                        id_estado_procesal_incidental = item.IsNull(29) ? 0 : item.Field<int>(29),
                        estado_procesal_incidental = item.IsNull(30) ? null! : item.Field<string>(30)!,
                        //id_estado_procesal_incidental = new()
                        //{
                        //    Value = item.IsNull(29) ? 0 : item.Field<int>(29),
                        //    Label = item.IsNull(30) ? null! : item.Field<string>(30)
                        //},
                        //activo = response.Data.Tables[0].Rows[0].IsNull(30)
                        //    ? false
                        //: response.Data.Tables[0].Rows[0].Field<bool>(30),
                        activo = item.IsNull(31) ? false : item.Field<bool>(31),

                    }
                );
            }

            return resultList;
        }
        #endregion

        #region Get Histórico ByFilters para Abogado
        public async Task<List<ResponseHistoricoAbogadoByFilters>> GetHistoricoAbogadoByFilters(
            int Fetch,
            int Page,
            string OrderByColumn,
            bool OrderDesc,
            DateTime? fechaRecepcionInicial,
            DateTime? fechaRecepcionFinal,
            DateTime? fechaInicialVencimiento,
            DateTime? fechaFinalVencimiento,
            string numeroExpediete,
            string numeroAsunto,
            int? idJuzgado,
            string nombreQuejoso,
            int? idMateria,
            int? idSubmateria,
            int? idTipoActo,
            string despacho,
            int? idAdministracion,
            int? idSubadministracion,
            int? idAutoridadResponsable,
            string rfcQuejoso,
            int? idEstadoTarea,
            int? idEstadoProcesal,
            int? idEstadoProcesalIncidental
            )
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, Fetch),
                new ParameterPGsql("p_page", NpgsqlDbType.Integer, Page),
                new ParameterPGsql("p_fecha_recepcion_inicial", NpgsqlDbType.Date, fechaRecepcionInicial),
                new ParameterPGsql("p_fecha_recepcion_final", NpgsqlDbType.Date, fechaRecepcionFinal),
                new ParameterPGsql("p_fecha_inicial_vencimiento", NpgsqlDbType.Date, fechaInicialVencimiento),
                new ParameterPGsql("p_fecha_final_vencimiento", NpgsqlDbType.Date, fechaFinalVencimiento),
                new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediete),
                new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text, numeroAsunto),
                new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
                new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
                new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, idMateria),
                new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, idSubmateria),
                new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),
                new ParameterPGsql("p_despacho", NpgsqlDbType.Text, despacho),
                new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer, idAdministracion),
                new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer, idSubadministracion),
                //new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer, idAutoridadResponsable), //Se agrega autoridad responsable para ABOGADO
                new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
                //new ParameterPGsql("p_transparencia", NpgsqlDbType.Varchar, transparencia),  // Se agrega Transparencia para ABOGADO
                //new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, idEstadoTarea), //ESTE NO VA EN ABOGADO
                new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
                new ParameterPGsql("p_id_estado_procesal_incidental", NpgsqlDbType.Integer, idEstadoProcesalIncidental),
                new ParameterPGsql("order_column", NpgsqlDbType.Text, /*null*/ OrderByColumn),
                new ParameterPGsql("order_desc", NpgsqlDbType.Boolean, /*null*/ OrderDesc),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.HistoricoAbogadoByFilters,
                parameters!
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            List<ResponseHistoricoAbogadoByFilters> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        fecha_recepcion = item.IsNull(1) ? null! : item.Field<DateTime>(1).ToString("yyyy-MM-dd"),
                        fecha_vencimiento = item.IsNull(2) ? null! : item.Field<DateTime>(2).ToString("yyyy-MM-dd"),

                        numero_expediente = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        numero_asunto = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        id_juzgado = item.IsNull(5) ? 0 : item.Field<int>(5),
                        juzgado = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        //id_juzgado = new()
                        //{
                        //    Value = item.IsNull(5) ? 0 : item.Field<int>(5),
                        //    Label = item.IsNull(6) ? null! : item.Field<string>(6)
                        //},

                        nombre_quejoso = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        id_materia = item.IsNull(8) ? 0 : item.Field<int>(8),
                        materia = item.IsNull(9) ? null! : item.Field<string>(9)!,
                        //id_materia = new()
                        //{
                        //    Value = item.IsNull(8) ? 0 : item.Field<int>(8),
                        //    Label = item.IsNull(9) ? null! : item.Field<string>(9)
                        //},
                        id_submateria = item.IsNull(10) ? 0 : item.Field<int>(10),
                        submateria = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        //id_submateria = new()
                        //{
                        //    Value = item.IsNull(10) ? 0 : item.Field<int>(10),
                        //    Label = item.IsNull(11) ? null! : item.Field<string>(11)
                        //},
                        id_tipo_acto = item.IsNull(12) ? 0 : item.Field<int>(12),
                        tipo_acto = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        //id_tipo_acto = new()
                        //{
                        //    Value = item.IsNull(12) ? 0 : item.Field<int>(12),
                        //    Label = item.IsNull(13) ? null! : item.Field<string>(13)
                        //},
                        despacho = item.IsNull(14) ? null! : item.Field<string>(14)!,
                        id_administracion = item.IsNull(15) ? 0 : item.Field<int>(15),
                        administracion = item.IsNull(16) ? null! : item.Field<string>(16)!,
                        //id_administracion = new()
                        //{
                        //    Value = item.IsNull(15) ? 0 : item.Field<int>(15),
                        //    Label = item.IsNull(16) ? null! : item.Field<string>(16)
                        //},
                        id_subadministracion = item.IsNull(17) ? 0 : item.Field<int>(17),
                        subadministracion = item.IsNull(18) ? null! : item.Field<string>(18)!,
                        //id_subadministracion = new()
                        //{
                        //    Value = item.IsNull(17) ? 0 : item.Field<int>(17),
                        //    Label = item.IsNull(18) ? null! : item.Field<string>(18)
                        //},
                        numero_autoridad = item.IsNull(19) ? 0 : (int)item.Field<long>(19),

                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(19) ? 0 : item.Field<int>(19),
                        //    Label = item.IsNull(20) ? null! : item.Field<string>(20)
                        //},

                        rfc_quejoso = item.IsNull(20) ? null! : item.Field<string>(20)!,
                        id_estado_tarea = item.IsNull(21) ? 0 : item.Field<int>(21),
                        estado_tarea = item.IsNull(22) ? null! : item.Field<string>(22)!,
                        //id_estado_tarea = new()
                        //{
                        //    Value = item.IsNull(21) ? 0 : item.Field<int>(21),
                        //    Label = item.IsNull(22) ? null! : item.Field<string>(22)
                        //},
                        id_estado_procesal = item.IsNull(23) ? 0 : item.Field<int>(23),
                        estado_procesal = item.IsNull(24) ? null! : item.Field<string>(24)!,
                        //id_estado_procesal = new()
                        //{
                        //    Value = item.IsNull(23) ? 0 : item.Field<int>(23),
                        //    Label = item.IsNull(24) ? null! : item.Field<string>(24)
                        //},
                        id_estado_procesal_incidental = item.IsNull(25) ? 0 : item.Field<int>(25),
                        estado_procesal_incidental = item.IsNull(26) ? null! : item.Field<string>(26)!,
                        //id_estado_procesal_incidental = new()
                        //{
                        //    Value = item.IsNull(25) ? 0 : item.Field<int>(25),
                        //    Label = item.IsNull(26) ? null! : item.Field<string>(26)
                        //},


                        activo = item.IsNull(27) ? false : item.Field<bool>(27),

                    }
                );
            }

            return resultList;
        }
        #endregion

        #region Historico Count Abogado

        public async Task<int?> GetHistoricoAbogadoByFiltersCount(
            DateTime? fechaRecepcionInicial,
            DateTime? fechaRecepcionFinal,
            DateTime? fechaInicialVencimiento,
            DateTime? fechaFinalVencimiento,
            string numeroExpediete,
            string numeroAsunto,
            int? idJuzgado,
            string nombreQuejoso,
            int? idMateria,
            int? idSubmateria,
            int? idTipoActo,
            string despacho,
            int? idAdministracion,
            int? idSubadministracion,
            int? idAutoridadResponsable,
            string rfcQuejoso,
            int? idEstadoTarea,
            int? idEstadoProcesal,
            int? idEstadoProcesalIncidental)
        {
            ParameterPGsql[] parameters =
            {
                //new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, Fetch),
                //new ParameterPGsql("p_page", NpgsqlDbType.Integer, Page),
                //falta cambiar el parametro de base p_juicio_amparo
                new ParameterPGsql("p_fecha_recepcion_inicial", NpgsqlDbType.Date, fechaRecepcionInicial),
                new ParameterPGsql("p_fecha_recepcion_final", NpgsqlDbType.Date, fechaRecepcionFinal),
                new ParameterPGsql("p_fecha_inicial_vencimiento", NpgsqlDbType.Date, fechaRecepcionFinal),
                new ParameterPGsql("p_fecha_final_vencimiento", NpgsqlDbType.Date, fechaRecepcionFinal),
                new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediete),
                new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text,numeroAsunto),
                new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
                new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
                new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, idMateria),
                new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, idSubmateria),
                new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),
                new ParameterPGsql("p_despacho", NpgsqlDbType.Text, despacho),
                new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer, idAdministracion),
                new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer, idSubadministracion),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer, idAutoridadResponsable), //Se agrega autoridad responsable para ABOGADO
                new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
                //new ParameterPGsql("p_transparencia", NpgsqlDbType.Varchar, transparencia),  // Se agrega Transparencia para ABOGADO
                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, idEstadoTarea), //ESTE NO VA EN ABOGADO
                //new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
                

                ////new ParameterPGsql("p_fecha_recepcion_inicial", NpgsqlDbType.Date, fechaRecepcionInicial),
                ////new ParameterPGsql("p_fecha_recepcion_final", NpgsqlDbType.Date, fechaRecepcionFinal),
                ////new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediete),
                ////new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text, numeroAsunto),
                ////new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
                ////new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
                ////new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, idMateria),
                ////new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, idSubmateria),
                ////new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),
                ////new ParameterPGsql("p_despacho", NpgsqlDbType.Text, despacho),
                ////new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer, idAdministracion),
                ////new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer, idSubmateria),
                ////new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
                ////new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
                //////new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.HistoricoAbogadoByFiltersCount,
                parameters!
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            return response.Data.Tables[0].Rows[0].IsNull(0)
                ? null!
                : response.Data.Tables[0].Rows[0].Field<int>(0);
        }
        #endregion

        #region Informe Justificado Constitucional
        public async Task<ResultTransaction> CreateInformeJustificadoAsync(InformeJustificadoConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ("p_id", NpgsqlDbType.Integer,  entity.id),
                new ("p_id_juicio_amparo", NpgsqlDbType.Integer,  entity.id_numero_asunto),
                new ("p_id_autoridad_responsable", NpgsqlDbType.Integer, entity.id_autoridad_responsable),
                new ("p_solicitud_opinion_tecnica", NpgsqlDbType.Boolean, entity.solicitud_opinion_tecnica),
                new ("p_fecha_recepcion_demanda", NpgsqlDbType.Date, entity.fecha_recepcion_demanda!),
                new ("p_fecha_vencimiento_justificado", NpgsqlDbType.Date, entity.fecha_vencimiento_justificado!),
                new ("p_numero_oficio_informe_justificado", NpgsqlDbType.Text, entity.numero_oficio_informe_justificado!),
                new ("p_fecha_oficio_informe_justificado", NpgsqlDbType.Date, entity.fecha_oficio_informe_justificado!),
                new ("p_fecha_presentacion_informe_justificado", NpgsqlDbType.Date, entity.fecha_presentacion_informe_justificado!),
                new ("p_observaciones_justificado", NpgsqlDbType.Text, entity.observaciones_justificado!),
                new ("p_id_tipo_documento", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_tipo_documento!),
                new ("p_id_seccion", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_seccion!),
                new ("p_file_name", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_name!),
                new ("p_file_path", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_path!),
                new ("p_content_type", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.content_type!),
                new ("p_file_size", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_size!),
                new ("p_permanente", NpgsqlDbType.Boolean, entityDocumento is null ? DBNull.Value : entityDocumento.permanente!),
                new ("p_id_rol", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_rol!),
                new ("p_id_unidad_administrativa", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_unidad_administrativa!),
                new ("p_usuario", NpgsqlDbType.Text, entity.usuario!),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateInformeJustificado,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }

        #endregion



        #region GetAll Informe Justificado Constitucional
        public async Task<List<ResponseInformeJustificadoConstitucional>> GetAllInformeJustificadoConstitucionalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllInformeJustificado,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseInformeJustificadoConstitucional> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        solicitud_opinion_tecnica = item.IsNull(4) ? false : item.Field<bool>(4),
                        fecha_recepcion_demanda = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_justificado = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        numero_oficio_informe_justificado = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        fecha_oficio_informe_justificado = item.IsNull(8) ? null! : item.Field<DateTime>(8).ToString("yyyy-MM-dd"),
                        fecha_presentacion_informe_justificado = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),
                        observaciones_justificado = item.IsNull(10) ? null! : item.Field<string>(10)!,

                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll Información Adicional
        public async Task<List<ResponseInformacionAdicional>> GetAllInformacionAdicionalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllInformacionAdicional,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseInformacionAdicional> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        fecha_resolucion_oficio = item.IsNull(2) ? null! : item.Field<DateTime>(2).ToString("yyyy-MM-dd"),
                        cuantia = item.IsNull(3) ? 0 : item.Field<decimal>(3),
                        oficio_resolucion_reclamado = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        id_autoridad_emisora_resolucion_oficio = item.IsNull(5) ? 0 : item.Field<int>(5),
                        autoridad_emisora_resolucion_oficio = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        //id_autoridad_emisora_resolucion_oficio = new()
                        //{
                        //    Value = item.IsNull(5) ? 0 : item.Field<int>(5),
                        //    Label = item.IsNull(6) ? null! : item.Field<string>(6)
                        //},
                        concepto_violacion = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        id_tipo_acto = item.IsNull(8) ? 0 : item.Field<int>(8),
                        usuario = item.IsNull(9) ? null! : item.Field<string>(9)!,
                        fecha_modificacion = item.IsNull(10) ? null! : item.Field<DateTime>(10).ToString("yyyy-MM-dd"),
                        activo = item.IsNull(11) ? false : item.Field<bool>(11),

                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll Recurso Inconformidad
        public async Task<ResponseRecursoIncondormidad> GetAllRecursoInconformidadAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllRecursoInconformidad,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            return new()
            {
                id = response.Data.Tables[0].Rows[0].IsNull(0) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(0),
                id_numero_asunto = response.Data.Tables[0].Rows[0].IsNull(1) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(1),
                notificacion_admision_recurso_inconformidad = response.Data.Tables[0].Rows[0].IsNull(2) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(2).ToString("yyyy-MM-dd"),
                numero_recurso_inconformidad = response.Data.Tables[0].Rows[0].IsNull(3) ? null! : response.Data.Tables[0].Rows[0].Field<string>(3)!,
                id_organo_radicacion_inconformidad = response.Data.Tables[0].Rows[0].IsNull(4) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(4),
                organo_radicacion_inconformidad = response.Data.Tables[0].Rows[0].IsNull(5) ? null! : response.Data.Tables[0].Rows[0].Field<string>(5)!,
                //id_organo_radicacion_inconformidad = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(4) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(4),
                //    Label = response.Data.Tables[0].Rows[0].IsNull(5) ? null! : response.Data.Tables[0].Rows[0].Field<string>(5)
                //},
                fecha_notificacion_resolucion_inconformidad = response.Data.Tables[0].Rows[0].IsNull(6) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(6).ToString("yyyy-MM-dd"),
                id_sentido_resolucion_inconformidad = response.Data.Tables[0].Rows[0].IsNull(7) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(7),
                sentido_resolucion_inconformidad = response.Data.Tables[0].Rows[0].IsNull(8) ? null! : response.Data.Tables[0].Rows[0].Field<string>(8)!,
                //id_sentido_resolucion_inconformidad = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(7) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(7),
                //    Label = response.Data.Tables[0].Rows[0].IsNull(8) ? null! : response.Data.Tables[0].Rows[0].Field<string>(8)
                //},
                usuario = response.Data.Tables[0].Rows[0].IsNull(9) ? null! : response.Data.Tables[0].Rows[0].Field<string>(9)!,
                fecha_modificacion = response.Data.Tables[0].Rows[0].IsNull(10) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(10).ToString("yyyy-MM-dd"),
                activo = response.Data.Tables[0].Rows[0].IsNull(11) ? false : response.Data.Tables[0].Rows[0].Field<bool>(11),

            };
        }
        #endregion

        #region GetAll Recurso Reclamacion
        public async Task<List<ResponseRecursoReclamacion>> GetAllRecursoReclamacionAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllRecursoReclamacion,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseRecursoReclamacion> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        recurso_reclamacion = item.IsNull(2) ? false : item.Field<bool>(2),
                        fecha_notificacion_acuerdo = item.IsNull(3) ? null! : item.Field<DateTime>(3).ToString("yyyy-MM-dd"),


                        fecha_vencimiento_recurso_reclamacion = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),
                        fecha_presentacion = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        oficio_reclamacion = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        notificacion_admision_recurso_reclamacion = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        numero_recurso = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        id_organo_radicacion = item.IsNull(9) ? 0 : item.Field<int>(9),
                        organo_radicacion = item.IsNull(10) ? null! : item.Field<string>(10)!,
                        //organo_radicacion = new()
                        //{
                        //    Value = item.IsNull(9) ? 0 : item.Field<int>(9),
                        //    Label = item.IsNull(10) ? null! : item.Field<string>(10)
                        //},
                        fecha_notificacion_resolucion = item.IsNull(11) ? null! : item.Field<DateTime>(11).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion_reclamacion = item.IsNull(12) ? 0 : item.Field<int>(12),
                        sentido_resolucion_reclamacion = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        //sentido_resolucion_reclamacion = new()
                        //{
                        //    Value = item.IsNull(12) ? 0 : item.Field<int>(12),
                        //    Label = item.IsNull(13) ? null! : item.Field<string>(13)
                        //},
                        usuario = item.IsNull(14) ? null! : item.Field<string>(14)!,
                        activo = item.IsNull(15) ? false : item.Field<bool>(15),

                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll Recurso QuejaPrincipal Constitucional
        public async Task<List<ResponseRecursoQuejaPrincipalConstitucional>> GetAllRecursoQuejaPrincipalConstitucionalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllRecursoQuejaConstitucionalResponsable,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseRecursoQuejaPrincipalConstitucional> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,

                        //autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        recurso_queja_principal = item.IsNull(4) ? false : item.Field<bool>(4),
                        id_recurrente = item.IsNull(5) ? 0 : item.Field<int>(5),
                        fecha_recepcion_acuerdo_queja = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_queja = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        oficio_recurso_queja = item.IsNull(8) ? null! : item.Field<string>(8)!,

                        fecha_admision_queja = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),
                        id_motivo_recurso_queja = item.IsNull(10) ? 0 : item.Field<int>(10),
                        motivo_recurso_queja = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        //id_motivo_recurso_queja = new()
                        //{
                        //    Value = item.IsNull(10) ? 0 : item.Field<int>(10),
                        //    Label = item.IsNull(11) ? null! : item.Field<string>(11)
                        //},
                        fecha_notificacion_ejecutoria = item.IsNull(12) ? null! : item.Field<DateTime>(12).ToString("yyyy-MM-dd"),

                        fecha_presentacion_queja = item.IsNull(13) ? null! : item.Field<DateTime>(13).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion_queja = item.IsNull(14) ? 0 : item.Field<int>(14),
                        sentido_resolucion_queja = item.IsNull(15) ? null! : item.Field<string>(15)!,
                        //id_sentido_resolucion_queja = new()
                        //{
                        //    Value = item.IsNull(14) ? 0 : item.Field<int>(14),
                        //    Label = item.IsNull(15) ? null! : item.Field<string>(15)
                        //},
                        id_organo_radicacion_queja = item.IsNull(16) ? 0 : item.Field<int>(16),
                        organo_radicacion_queja = item.IsNull(17) ? null! : item.Field<string>(17)!,
                        //id_organo_radicacion_queja = new()
                        //{
                        //    Value = item.IsNull(16) ? 0 : item.Field<int>(16),
                        //    Label = item.IsNull(17) ? null! : item.Field<string>(17)
                        //},
                        toca_queja = item.IsNull(18) ? null! : item.Field<string>(18)!,

                        oficio_comunicacion_area_correspondiente = item.IsNull(19) ? null! : item.Field<string>(19)!,
                        fecha_oficio_comunicacion = item.IsNull(20) ? null! : item.Field<DateTime>(20).ToString("yyyy-MM-dd"),
                        activo = item.IsNull(21) ? false : item.Field<bool>(21),
                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll Recurso Revision Constitucional
        public async Task<List<ResponseRecursoRevisionPrincipal>> GetAllRecursoRevisionConstitucionalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllRecursoRevisionConstitucional,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseRecursoRevisionPrincipal> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,

                        //autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        recurso_revision_principal = item.IsNull(4) ? false : item.Field<bool>(4),
                        id_recurrente = item.IsNull(5) ? 0 : item.Field<int>(5),

                        fecha_recepcion_sentencia = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_recurso_revision = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        oficio_recurso = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        fecha_presentacion_revision = item.IsNull(9) ? null! : item.Field<DateTime>(9).ToString("yyyy-MM-dd"),
                        fecha_admision = item.IsNull(10) ? null! : item.Field<DateTime>(10).ToString("yyyy-MM-dd"),
                        id_organo_radicacion_revision = item.IsNull(11) ? 0 : item.Field<int>(11),
                        organo_radicacion_revision = item.IsNull(12) ? null! : item.Field<string>(12)!,
                        //id_organo_radicacion_revision = new()
                        //{
                        //    Value = item.IsNull(11) ? 0 : item.Field<int>(11),
                        //    Label = item.IsNull(12) ? null! : item.Field<string>(12)
                        //},
                        toca_revision = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        revision_adhesiva = item.IsNull(14) ? false : item.Field<bool>(14),
                        oficio_revision_adhesiva = item.IsNull(15) ? null! : item.Field<string>(15)!,
                        fecha_vencimiento_adhesion = item.IsNull(16) ? null! : item.Field<DateTime>(16).ToString("yyyy-MM-dd"),
                        fecha_notificacion_ejecutoria_revision = item.IsNull(17) ? null! : item.Field<DateTime>(17).ToString("yyyy-MM-dd"),
                        id_sentido_resolucion_revision = item.IsNull(18) ? 0 : item.Field<int>(18),
                        sentido_resolucion_revision = item.IsNull(19) ? null! : item.Field<string>(19)!,
                        //id_sentido_resolucion_revision = new()
                        //{
                        //    Value = item.IsNull(18) ? 0 : item.Field<int>(18),
                        //    Label = item.IsNull(19) ? null! : item.Field<string>(19)
                        //},
                        id_tipo_sentido_revision = item.IsNull(20) ? 0 : item.Field<int>(20),
                        tipo_sentido_revision = item.IsNull(21) ? null! : item.Field<string>(21)!,
                        //id_tipo_sentido_revision = new()
                        //{
                        //    Value = item.IsNull(20) ? 0 : item.Field<int>(20),
                        //    Label = item.IsNull(21) ? null! : item.Field<string>(21)
                        //},
                        id_tipo_sentido_general_revision = item.IsNull(22) ? 0 : item.Field<int>(22),
                        tipo_sentido_general_revision = item.IsNull(23) ? null! : item.Field<string>(23)!,
                        //id_tipo_sentido_general_revision = new()
                        //{
                        //    Value = item.IsNull(22) ? 0 : item.Field<int>(22),
                        //    Label = item.IsNull(23) ? null! : item.Field<string>(23)
                        //},
                        id_sentido_general = item.IsNull(24) ? 0 : item.Field<int>(24),
                        sentido_general = item.IsNull(25) ? null! : item.Field<string>(25)!,
                        //id_sentido_general = new()
                        //{
                        //    Value = item.IsNull(24) ? 0 : item.Field<int>(24),
                        //    Label = item.IsNull(25) ? null! : item.Field<string>(25)
                        //},
                        oficio_comunicacion_autoridad_revision = item.IsNull(26) ? null! : item.Field<string>(26)!,
                        fecha_oficio_comunicacion_revision = item.IsNull(27) ? null! : item.Field<DateTime>(27).ToString("yyyy-MM-dd"),
                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll Sentencia Constitucional
        public async Task<List<ResponseSentenciaConstitucional>> GetAllSentenciaConstitucionalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllSentenciaConstitucional,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseSentenciaConstitucional> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        fecha_notificacion_sentencia = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),
                        id_sentido_sentencia = item.IsNull(5) ? 0 : item.Field<int>(5),
                        sentido_sentencia = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        //id_sentido_sentencia = new()
                        //{
                        //    Value = item.IsNull(5) ? 0 : item.Field<int>(5),
                        //    Label = item.IsNull(6) ? null! : item.Field<string>(6)
                        //},
                        id_tipo_sentido_sentencia = item.IsNull(7) ? 0 : item.Field<int>(7),
                        tipo_sentido_sentencia = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        //id_tipo_sentido_sentencia = new()
                        //{
                        //    Value = item.IsNull(7) ? 0 : item.Field<int>(7),
                        //    Label = item.IsNull(8) ? null! : item.Field<string>(8)
                        //},
                        id_dictamen_no_revision = item.IsNull(9) ? 0 : item.Field<int>(9),
                        id_sentido_general_asunto = item.IsNull(10) ? 0 : item.Field<int>(10),
                        sentido_general_asunto = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        //id_sentido_general_asunto = new()
                        //{
                        //    Value = item.IsNull(10) ? 0 : item.Field<int>(10),
                        //    Label = item.IsNull(11) ? null! : item.Field<string>(11)
                        //},
                        id_tipo_sentido_general = item.IsNull(12) ? 0 : item.Field<int>(12),
                        tipo_sentido_general = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        //id_tipo_sentido_general = new()
                        //{
                        //    Value = item.IsNull(12) ? 0 : item.Field<int>(12),
                        //    Label = item.IsNull(13) ? null! : item.Field<string>(13)
                        //},

                        oficio_comunicacion_autoridad = item.IsNull(14) ? null! : item.Field<string>(14)!,

                        fecha_oficio_comunicacion_sentencia = item.IsNull(15) ? null! : item.Field<DateTime>(15).ToString("yyyy-MM-dd"),
                        fecha_recepcion_auto_sentencia_ejecutoria = item.IsNull(16) ? null! : item.Field<DateTime>(16).ToString("yyyy-MM-dd"),
                        fecha_presentacion_oficio_comunicacion = item.IsNull(17) ? null! : item.Field<DateTime>(17).ToString("yyyy-MM-dd"),
                        comunicado_acuerdo_firmeza = item.IsNull(18) ? null! : item.Field<string>(18)!,

                        fecha_comunicacion_acuerdo_firmeza = item.IsNull(19) ? null! : item.Field<DateTime>(19).ToString("yyyy-MM-dd"),
                        fecha_conclusion_expediente = item.IsNull(20) ? null! : item.Field<DateTime>(20).ToString("yyyy-MM-dd"),

                    }
                );
            }
            return resultList;
        }
        #endregion

        #region GetAll Incidente por Exceso Incidental
        public async Task<List<IncidenteExcesoIncidentalDisconnected>> GetAllIncidentePorExcesoIncidentalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllIncidenteExceso,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<IncidenteExcesoIncidentalDisconnected> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        interposicion_incidente = response.Data.Tables[0].Rows[0].IsNull(3)
                        ? false
                        : response.Data.Tables[0].Rows[0].Field<bool>(3),
                        fecha_notificacion_acuerdo = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),
                        oficio_desahogo = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        fecha_oficio_desahogo = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        id_sentido = item.IsNull(7) ? 0 : item.Field<int>(7),
                        fecha_notificacion_resolucion = item.IsNull(8) ? null! : item.Field<DateTime>(8).ToString("yyyy-MM-dd"),
                        oficio_comunicacion_autoridad = item.IsNull(9) ? null! : item.Field<string>(9)!,
                        fecha_oficio_comunicacion = item.IsNull(10) ? null! : item.Field<DateTime>(10).ToString("yyyy-MM-dd"),


                    }
                );
            }
            return resultList;
        }
        #endregion


        #region GetAll Cumplimiento Fallo Protector Constitucional
        public async Task<List<ResponseCumplimientoFalloProtector>> GetAllCumplimientoFalloConstitucionalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllCumplimientoFalloProtector,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }
            List<ResponseCumplimientoFalloProtector> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,

                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                        fecha_notificacion_requerimiento = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),
                        id_plazo_fallo = item.IsNull(5) ? 0 : item.Field<int>(5),
                        plazo_fallo = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        //plazo_fallo = new()
                        //{
                        //    Value = item.IsNull(5) ? 0 : item.Field<int>(5),
                        //    Label = item.IsNull(6) ? null! : item.Field<string>(6)
                        //},
                        fecha_vencimiento_requerimiento = item.IsNull(7) ? null! : item.Field<DateTime>(7).ToString("yyyy-MM-dd"),
                        fecha_presentacion_fallo = item.IsNull(8) ? null! : item.Field<DateTime>(8).ToString("yyyy-MM-dd"),
                        numero_oficio_atencion = item.IsNull(9) ? null! : item.Field<string>(9)!,
                        fecha_notificacion_acuerdo_fallo = item.IsNull(10) ? null! : item.Field<DateTime>(10).ToString("yyyy-MM-dd"),
                        fecha_oficio_comunicacion_fallo = item.IsNull(11) ? null! : item.Field<DateTime>(11).ToString("yyyy-MM-dd"),
                        numero_oficio_comunicacion_autoridad = item.IsNull(12) ? null! : item.Field<string>(12)!,
                        activo = item.IsNull(11) ? false : item.Field<bool>(13),

                    }
                );
            }
            return resultList;
        }
        #endregion

        #region Recurso de Queja Constitucional
        #region Recurso Queja Autoridad Responsable 
        public async Task<ResultTransaction> CreateRecursoQuejaConstitucionalResponsableAsync(RecursoQuejaPrincipalConstitucional entity)
        {

            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer,  entity.id_numero_asunto), //SE AGREGA CAMPO PARA ACTUALIZAR
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable!),
                new ParameterPGsql("p_recurso_queja_principal", NpgsqlDbType.Boolean,  entity.recurso_queja_principal!),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer,  entity.id_recurrente!),
                new ParameterPGsql("p_fecha_recepcion_acuerdo_queja", NpgsqlDbType.Date, entity.fecha_recepcion_acuerdo_queja!),
                new ParameterPGsql("p_fecha_vencimiento_recurso_queja", NpgsqlDbType.Date, entity.fecha_vencimiento_recurso_queja!),
                new ParameterPGsql("p_oficio_recurso_queja", NpgsqlDbType.Text, entity.oficio_recurso_queja!),
                new ParameterPGsql("p_fecha_presentacion_queja", NpgsqlDbType.Date, entity.fecha_presentacion_queja!),
                new ParameterPGsql("p_id_motivo_recurso_queja", NpgsqlDbType.Integer, entity.id_motivo_recurso_queja),
                new ParameterPGsql("p_fecha_admision_queja", NpgsqlDbType.Date, entity.fecha_admision_queja!),
                new ParameterPGsql("p_id_organo_radicacion_queja", NpgsqlDbType.Integer,  entity.id_organo_radicacion_queja),
                new ParameterPGsql("p_toca_queja", NpgsqlDbType.Text, entity.toca_queja!),
                new ParameterPGsql("p_fecha_notificacion_ejecutoria", NpgsqlDbType.Date, entity.fecha_notificacion_ejecutoria!),
                new ParameterPGsql("p_id_sentido_resolucion_queja", NpgsqlDbType.Integer,  entity.id_sentido_resolucion_queja),
                new ParameterPGsql("p_oficio_comunicacion_area_correspondiente", NpgsqlDbType.Text, entity.oficio_comunicacion_area_correspondiente!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),


            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoQuejaConstitucionalResponsable,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }
        #endregion
        #region Recurso queja Otras Autoridades
        public async Task<ResultTransaction> CreateRecursoQuejaConstitucionalOtrosAsync(RecursoQuejaPrincipalConstitucional entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer,  entity.id_numero_asunto), //SE AGREGA CAMPO PARA ACTUALIZAR
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable),
                new ParameterPGsql("p_recurso_queja_principal", NpgsqlDbType.Boolean,  entity.recurso_queja_principal!),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer,  entity.id_recurrente!),
                new ParameterPGsql("p_fecha_admision_queja", NpgsqlDbType.Date, entity.fecha_admision_queja!),
                new ParameterPGsql("p_id_organo_radicacion_queja", NpgsqlDbType.Integer,  entity.id_organo_radicacion_queja),
                new ParameterPGsql("p_toca_queja", NpgsqlDbType.Text, entity.toca_queja!),
                new ParameterPGsql("p_fecha_notificacion_ejecutoria", NpgsqlDbType.Date, entity.fecha_notificacion_ejecutoria!),
                new ParameterPGsql("p_id_sentido_resolucion_queja", NpgsqlDbType.Integer,  entity.id_sentido_resolucion_queja),
                new ParameterPGsql("p_oficio_comunicacion_area_correspondiente", NpgsqlDbType.Text, entity.oficio_comunicacion_area_correspondiente!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),


            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoQuejaConstitucionalOtros,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }
        #endregion
        #region Recurso queja quejoso
        public async Task<ResultTransaction> CreateRecursoQuejaConstitucionalQuejosoAsync(RecursoQuejaPrincipalConstitucional entity)
        {

            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer,  entity.id_numero_asunto), //SE AGREGA CAMPO PARA ACTUALIZAR
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable),
                new ParameterPGsql("p_recurso_queja_principal", NpgsqlDbType.Boolean,  entity.recurso_queja_principal!),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer,  entity.id_recurrente!),
                new ParameterPGsql("p_fecha_admision_queja", NpgsqlDbType.Date, entity.fecha_admision_queja!),
                new ParameterPGsql("p_id_organo_radicacion_queja", NpgsqlDbType.Integer,  entity.id_organo_radicacion_queja),
                new ParameterPGsql("p_toca_queja", NpgsqlDbType.Text, entity.toca_queja!),
                new ParameterPGsql("p_fecha_notificacion_ejecutoria", NpgsqlDbType.Date, entity.fecha_notificacion_ejecutoria!),
                new ParameterPGsql("p_id_sentido_resolucion_queja", NpgsqlDbType.Integer,  entity.id_sentido_resolucion_queja),
                new ParameterPGsql("p_oficio_comunicacion_area_correspondiente", NpgsqlDbType.Text, entity.oficio_comunicacion_area_correspondiente!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),


            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoQuejaConstitucionalQuejoso,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }
        #endregion


        #endregion

        #region Nota de litigio
        public async Task<ResultTransaction> CreateNotaLitigioAsync(NotaLitigio entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ("p_id_juicio_amparo", NpgsqlDbType.Integer, entity.id_numero_asunto),
                new ("p_fecha_registro_nota", NpgsqlDbType.Date, entity.fecha_registro_nota!),
                new ("p_seccion", NpgsqlDbType.Integer, entity.id_seccion),
                new ("p_tipo_documento", NpgsqlDbType.Integer, entity.id_tipo_documento),
                new ("p_id_tipo_documento", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_tipo_documento!),
                new ("p_id_seccion", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_seccion!),
                new ("p_file_name", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_name!),
                new ("p_file_path", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_path!),
                new ("p_content_type", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.content_type!),
                new ("p_file_size", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_size!),
                new ("p_permanente", NpgsqlDbType.Boolean, entityDocumento is null ? DBNull.Value : entityDocumento.permanente!),
                new ("p_id_rol", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_rol!),
                new ("p_id_unidad_administrativa", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_unidad_administrativa!),
                new ("p_usuario_modificacion", NpgsqlDbType.Text, entity.usuario!),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateNotaLitigio,
                parameters
            );

            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }

        #endregion

        #region Eliminar Nota Litigio

        public async Task<ResultTransaction> UpdateEliminarNotaLitigio(int idNotaLitigio)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, idNotaLitigio) };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.UpdateDeleteNotaLitigio,
                parameters
             );

            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string>(4)!,
            };
        }

        #endregion

        #region GetById Nota de Litigio

        public async Task<List<ResponseNotaLitigioById>> GetByIdNotaLitigioAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetNotaLitigio,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            List<ResponseNotaLitigioById> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        fecha_registro_nota = item.IsNull(2) ? null! : item.Field<DateTime>(2).ToString("dd-MM-yyyy"),
                        seccion = item.IsNull(3) ? 0 : item.Field<int>(3)
                    }
                );
            };
            return resultList;
        }

        #endregion

        #region Recurso de Revision Constitucional
        #region Recurso Revision Autoridad Responsable 
        public async Task<ResultTransaction> CreateRecursoRevisionConstitucionalResponsableAsync(RecursoRevisionPrincipalConstitucional entity)
        {

            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable),
                new ParameterPGsql("p_recurso_revision_principal", NpgsqlDbType.Boolean,  entity.recurso_revision_principal),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer,  entity.id_recurrente),
                new ParameterPGsql("p_fecha_recepcion_sentencia", NpgsqlDbType.Date, entity.fecha_recepcion_sentencia!),
                new ParameterPGsql("p_fecha_vencimiento_recurso_revision", NpgsqlDbType.Date, entity.fecha_vencimiento_recurso_revision!),
                new ParameterPGsql("p_oficio_recurso", NpgsqlDbType.Text, entity.oficio_recurso!),
                new ParameterPGsql("p_fecha_presentacion_revision", NpgsqlDbType.Date, entity.fecha_presentacion_revision!),
                new ParameterPGsql("p_fecha_admision", NpgsqlDbType.Date, entity.fecha_admision!),
                new ParameterPGsql("p_id_organo_radicacion_revision", NpgsqlDbType.Integer, entity.id_organo_radicacion_revision),
                new ParameterPGsql("p_toca_revision", NpgsqlDbType.Text, entity.toca_revision!),
                new ParameterPGsql("p_revision_adhesiva", NpgsqlDbType.Boolean, entity.revision_adhesiva),
                new ParameterPGsql("p_oficio_revision_adhesiva", NpgsqlDbType.Text, entity.oficio_revision_adhesiva!),
                new ParameterPGsql("p_fecha_vencimiento_adhesion", NpgsqlDbType.Date, entity.fecha_vencimiento_adhesion!),
                new ParameterPGsql("p_fecha_notificacion_ejecutoria_revision", NpgsqlDbType.Date, entity.fecha_notificacion_ejecutoria_revision!),
                new ParameterPGsql("p_id_sentido_resolucion_revision", NpgsqlDbType.Integer, entity.id_sentido_resolucion_revision),
                new ParameterPGsql("p_id_tipo_sentido_revision", NpgsqlDbType.Integer, entity.id_tipo_sentido_revision),
                new ParameterPGsql("p_id_sentido_general", NpgsqlDbType.Integer, entity.id_sentido_general),
                new ParameterPGsql("p_id_tipo_sentido_general_revision", NpgsqlDbType.Integer, entity.id_tipo_sentido_general_revision),
                new ParameterPGsql("p_oficio_comunicacion_autoridad_revision", NpgsqlDbType.Text, entity.oficio_comunicacion_autoridad_revision!),
                new ParameterPGsql("p_fecha_oficio_comunicacion_revision", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion_revision!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoRevisionConstitucionalResponsable,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }
        #endregion
        #region Recurso Revision Otras Autoridades
        public async Task<ResultTransaction> CreateRecursoRevisionConstitucionalOtrosAsync(RecursoRevisionPrincipalConstitucional entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable),
                new ParameterPGsql("p_recurso_revision_principal", NpgsqlDbType.Boolean,  entity.recurso_revision_principal),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer,  entity.id_recurrente),
                new ParameterPGsql("p_fecha_admision", NpgsqlDbType.Date, entity.fecha_admision!),
                new ParameterPGsql("p_id_organo_radicacion_revision", NpgsqlDbType.Integer, entity.id_organo_radicacion_revision),
                new ParameterPGsql("p_toca_revision", NpgsqlDbType.Text, entity.toca_revision!),
                new ParameterPGsql("p_fecha_notificacion_ejecutoria_revision", NpgsqlDbType.Date, entity.fecha_notificacion_ejecutoria_revision!),
                new ParameterPGsql("p_id_sentido_resolucion_revision", NpgsqlDbType.Integer, entity.id_sentido_resolucion_revision),
                new ParameterPGsql("p_id_tipo_sentido_revision", NpgsqlDbType.Integer, entity.id_tipo_sentido_revision),
                new ParameterPGsql("p_id_sentido_general", NpgsqlDbType.Integer, entity.id_sentido_general),
                new ParameterPGsql("p_id_tipo_sentido_general_revision", NpgsqlDbType.Integer, entity.id_tipo_sentido_general_revision),
                new ParameterPGsql("p_oficio_comunicacion_autoridad_revision", NpgsqlDbType.Text, entity.oficio_comunicacion_autoridad_revision!),
                new ParameterPGsql("p_fecha_oficio_comunicacion_revision", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion_revision!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoRevisionConstitucionalOtros,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }
        #endregion
        #region Recurso Revision quejoso
        public async Task<ResultTransaction> CreateRecursoRevisionConstitucionalQuejosoAsync(RecursoRevisionPrincipalConstitucional entity)
        {

            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable),
                new ParameterPGsql("p_recurso_revision_principal", NpgsqlDbType.Boolean,  entity.recurso_revision_principal),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer,  entity.id_recurrente),
                new ParameterPGsql("p_fecha_admision", NpgsqlDbType.Date, entity.fecha_admision!),
                new ParameterPGsql("p_id_organo_radicacion_revision", NpgsqlDbType.Integer, entity.id_organo_radicacion_revision),
                new ParameterPGsql("p_toca_revision", NpgsqlDbType.Text, entity.toca_revision!),
                new ParameterPGsql("p_fecha_notificacion_ejecutoria_revision", NpgsqlDbType.Date, entity.fecha_notificacion_ejecutoria_revision!),
                new ParameterPGsql("p_id_sentido_resolucion_revision", NpgsqlDbType.Integer, entity.id_sentido_resolucion_revision),
                new ParameterPGsql("p_id_tipo_sentido_revision", NpgsqlDbType.Integer, entity.id_tipo_sentido_revision),
                new ParameterPGsql("p_id_sentido_general", NpgsqlDbType.Integer, entity.id_sentido_general),
                new ParameterPGsql("p_id_tipo_sentido_general_revision", NpgsqlDbType.Integer, entity.id_tipo_sentido_general_revision),
                new ParameterPGsql("p_oficio_comunicacion_autoridad_revision", NpgsqlDbType.Text, entity.oficio_comunicacion_autoridad_revision!),
                new ParameterPGsql("p_fecha_oficio_comunicacion_revision", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion_revision!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Varchar, entity.usuario!),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoRevisionConstitucionalQuejoso,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }
        #endregion
        #endregion

        #region Recurso de Inconformidad Constitucional
        public async Task<ResultTransaction> CreateRecursoInconformidadConstitucionalAsync(RecursoInconformidadConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ("p_id", NpgsqlDbType.Integer,  entity.id),
                //new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable),
                new ("p_notificacion_admision_recurso_inconformidad", NpgsqlDbType.Date, entity.notificacion_admision_recurso_inconformidad!),
                new ("p_numero_recurso_inconformidad", NpgsqlDbType.Text, entity.numero_recurso_inconformidad!),
                new ("p_id_organo_radicacion_inconformidad", NpgsqlDbType.Integer,  entity.id_organo_radicacion_inconformidad),
                new ("p_fecha_notificacion_resolucion_inconformidad", NpgsqlDbType.Date, entity.fecha_notificacion_resolucion_inconformidad!),
                new ("p_id_sentido_resolucion_inconformidad", NpgsqlDbType.Integer,  entity.id_sentido_resolucion_inconformidad),
                new ("p_id_tipo_documento", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_tipo_documento!),
                new ("p_id_seccion", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_seccion!),
                new ("p_file_name", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_name!),
                new ("p_file_path", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_path!),
                new ("p_content_type", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.content_type!),
                new ("p_file_size", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_size!),
                new ("p_permanente", NpgsqlDbType.Boolean, entityDocumento is null ? DBNull.Value : entityDocumento.permanente!),
                new ("p_id_rol", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_rol!),
                new ("p_id_unidad_administrativa", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_unidad_administrativa!),
                new ("p_usuario", NpgsqlDbType.Text, entity.usuario),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoInconformidadConstitucional,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }

        #endregion

        #region Recurso de Reclamación Constitucional
        public async Task<ResultTransaction> CreateRecursoReclamacionConstitucionalAsync(RecursoReclamacionConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ("p_id", NpgsqlDbType.Integer,  entity.id),
                //new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer,  entity.id_autoridad_responsable),
                new ("p_recurso_reclamacion", NpgsqlDbType.Boolean, entity.recurso_reclamacion),
                new ("p_fecha_notificacion_acuerdo", NpgsqlDbType.Date, entity.fecha_notificacion_acuerdo!),
                new ("p_fecha_vencimiento_recurso_reclamacion", NpgsqlDbType.Date, entity.fecha_vencimiento_recurso_reclamacion!),
                new ("p_fecha_presentacion", NpgsqlDbType.Date, entity.fecha_presentacion!),
                new ("p_oficio_reclamacion", NpgsqlDbType.Text, entity.oficio_reclamacion!),
                new ("p_notificacion_admision_recurso_reclamacion", NpgsqlDbType.Date, entity.notificacion_admision_recurso_reclamacion!),
                new ("p_numero_recurso", NpgsqlDbType.Text, entity.numero_recurso!),
                new ("p_id_organo_radicacion", NpgsqlDbType.Integer,  entity.id_organo_radicacion),
                new ("p_fecha_notificacion_resolucion", NpgsqlDbType.Date, entity.fecha_notificacion_resolucion!),
                new ("p_id_sentido_resolucion_reclamacion", NpgsqlDbType.Integer,  entity.id_sentido_resolucion_reclamacion),
                new ("p_id_tipo_documento", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_tipo_documento!),
                new ("p_id_seccion", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_seccion!),
                new ("p_file_name", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_name!),
                new ("p_file_path", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_path!),
                new ("p_content_type", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.content_type!),
                new ("p_file_size", NpgsqlDbType.Text, entityDocumento is null ? DBNull.Value : entityDocumento.file_size!),
                new ("p_permanente", NpgsqlDbType.Boolean, entityDocumento is null ? DBNull.Value : entityDocumento.permanente!),
                new ("p_id_rol", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_rol!),
                new ("p_id_unidad_administrativa", NpgsqlDbType.Integer, entityDocumento is null ? DBNull.Value : entityDocumento.id_unidad_administrativa!),
                new ("p_usuario", NpgsqlDbType.Varchar,  entity.usuario),


            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateRecursoReclamacionConstitucional,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }

        #endregion


        #region Suspensión Provisional
        public async Task<ResultTransaction> CreateSuspensionProvisionalAbogadoAsync(SuspensionProvisional entity)
        {

            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer,  entity.id),
                new ParameterPGsql("p_suspension_provisional", NpgsqlDbType.Boolean, entity.suspension_provisional),
                new ParameterPGsql("p_otorgamiento_garantia", NpgsqlDbType.Integer, entity.otorgamiento_garantia),
                new ParameterPGsql("p_oficio_couminicacion_suspension", NpgsqlDbType.Text, entity.oficio_comunicacion!),
                new ParameterPGsql("p_fecha_comunicacion", NpgsqlDbType.Date, entity.fecha_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario_modificacion!),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateSuspensionProvisional,
                parameters
            );
            if (response.ExisteError)
            {
                return new()
                {
                    Success = false,
                    MsgError = response.Mensaje,
                    NoError = response.CodeSqlError,
                };
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return new()
                {
                    Success = false,
                    MsgError = "No se pudo obtener la respuesta de la operación en base de datos.",
                };
            }

            return new()
            {
                Result = response.Data.Tables[0].Rows[0].Field<int?>(0),
                Success = response.Data.Tables[0].Rows[0].Field<bool>(1),
                MsgError = response.Data.Tables[0].Rows[0].Field<string?>(2)!,
                DetailError = response.Data.Tables[0].Rows[0].Field<string?>(3)!,
                NoError = response.Data.Tables[0].Rows[0].Field<string?>(4)!,
            };
        }

        #endregion

        #region GetAutoridadesResponsables
        public async Task<List<ResponseAutoridadesResponsables>> GetAutoridadesResponsablesAsync(
                int idNumeroAsunto
            )
        {
            ParameterPGsql[] parameters =
            {

        new ParameterPGsql("p_id", NpgsqlDbType.Integer,  idNumeroAsunto),

    };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AutoridadesResponsables,
                parameters!
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            List<ResponseAutoridadesResponsables> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        //autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(2) ? 0 : item.Field<int>(2),
                        //    Label = item.IsNull(3) ? null! : item.Field<string>(3)
                        //},
                    }
                );
            }

            return resultList;
        }
        #endregion


        #region GetById
        public async Task<ResponseAmparoIndirectoByID> GetByIdAmparoAsync(int idNumeroAsunto)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, idNumeroAsunto), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetById,
                parameters
            );
            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
            {
                return null!;
            }

            return new()
            {

                id = response.Data.Tables[0].Rows[0].IsNull(0) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(0),
                numero_asunto = response.Data.Tables[0].Rows[0].IsNull(1) ? null! : response.Data.Tables[0].Rows[0].Field<string>(1)!,
                numero_expediente = response.Data.Tables[0].Rows[0].IsNull(2) ? null! : response.Data.Tables[0].Rows[0].Field<string>(2)!,
                fecha_recepcion_demanda = response.Data.Tables[0].Rows[0].IsNull(3) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(3).ToString("yyyy-MM-dd"),
                id_juzgado = response.Data.Tables[0].Rows[0].IsNull(4) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(4),
                //juzgado = response.Data.Tables[0].Rows[0].IsNull(6) ? null! : response.Data.Tables[0].Rows[0].Field<string>(6)!,

                //juzgado = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(4)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(4),
                //},
                contribuyente = response.Data.Tables[0].Rows[0].IsNull(5)
                ? false
                : response.Data.Tables[0].Rows[0].Field<bool>(5),

                rfc_quejoso = response.Data.Tables[0].Rows[0].IsNull(6) ? null! : response.Data.Tables[0].Rows[0].Field<string>(6)!,
                nombre_quejoso = response.Data.Tables[0].Rows[0].IsNull(7) ? null! : response.Data.Tables[0].Rows[0].Field<string>(7)!,
                id_materia = response.Data.Tables[0].Rows[0].IsNull(8) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(8),
                //materia = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(8)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(8),
                //},
                id_submateria = response.Data.Tables[0].Rows[0].IsNull(9) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(9),
                //submateria = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(9)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(9),
                //},
                id_tipo_acto = response.Data.Tables[0].Rows[0].IsNull(10) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(10),
                //tipo_acto = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(10)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(10),
                //},
                despacho = response.Data.Tables[0].Rows[0].IsNull(11) ? null! : response.Data.Tables[0].Rows[0].Field<string>(11)!,
                //autoridad_responsable = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(12)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(12),
                //},
                id_administracion = response.Data.Tables[0].Rows[0].IsNull(12) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(12),
                //administracion = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(12)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(12),
                //},
                id_subadministracion = response.Data.Tables[0].Rows[0].IsNull(13) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(13),
                //subadministracion = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(13)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(13),
                //},
                id_estado_tarea = response.Data.Tables[0].Rows[0].IsNull(14) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(14),
                //estado_tarea = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(14)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(14),
                //},
                id_estado_procesal = response.Data.Tables[0].Rows[0].IsNull(15) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(15),
                //estado_procesal = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(15)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(15),
                //},
                id_estado_procesal_incidental = response.Data.Tables[0].Rows[0].IsNull(16) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(16),
                //estado_procesal_incidental = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(16)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(16),
                //},
                recurso_queja_principal = response.Data.Tables[0].Rows[0].IsNull(17)
                ? false
                 : response.Data.Tables[0].Rows[0].Field<bool>(17),
                recurso_revision_principal = response.Data.Tables[0].Rows[0].IsNull(18)
                ? false
                 : response.Data.Tables[0].Rows[0].Field<bool>(18),

                recurso_queja_incidental = response.Data.Tables[0].Rows[0].IsNull(19)
                ? false
                : response.Data.Tables[0].Rows[0].Field<bool>(19),
                recurso_revision_incidental = response.Data.Tables[0].Rows[0].IsNull(20)
                ? false
                : response.Data.Tables[0].Rows[0].Field<bool>(20),
                activo = response.Data.Tables[0].Rows[0].IsNull(21)
                ? false
                 : response.Data.Tables[0].Rows[0].Field<bool>(21),
                numero_empleado = response.Data.Tables[0].Rows[0].IsNull(22) ? null! : response.Data.Tables[0].Rows[0].Field<string>(22)!,
                fecha_turnado = response.Data.Tables[0].Rows[0].IsNull(23) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(23).ToString("yyyy-MM-dd"),
                fecha_creacion = response.Data.Tables[0].Rows[0].IsNull(24) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(24).ToString("yyyy-MM-dd"),
                fecha_modificacion = response.Data.Tables[0].Rows[0].IsNull(25) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(25).ToString("yyyy-MM-dd"),

            };
        }
        #endregion
    }
}
