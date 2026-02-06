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
    public class AmparoIndirectoAdministradorGlobalRepository : IAmparoIndirectoAdministradorGlobalRepository
    {

        #region Variables
        private readonly ISqlTools _database;
        #endregion

        #region Constructor
        public AmparoIndirectoAdministradorGlobalRepository(ISqlTools database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }
        #endregion

        //Métodos

        #region GetAll Bandeja de Pendientes Administrador Global
        public async Task<int?> GetAllAdministradorGlobalCount()
        {
            //ParameterPGsql[] parameters = {
            //    //new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
            //    //new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, EnumEstadoProcesal.ACTIVO.GetHashCode()),
            //};

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.BandejaAdministradorGlobalGetAllCount
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

        public async Task<List<ResponseAmparoIndirectoAdministradorGlobalBandeja>> GetAllAdministradorGlobal(
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
                EnumFunctions.BandejaAdministradorGlobalGetAll,
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

            List<ResponseAmparoIndirectoAdministradorGlobalBandeja> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {

                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        fecha_recepcion_demanda = item.IsNull(1) ? null! : item.Field<DateTime>(1).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_demanda = item.IsNull(2) ? null! : item.Field<DateTime>(2).ToString("yyyy-MM-dd"),
                        
                        numero_expediente = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        numero_asunto = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        id_juzgado= item.IsNull(5) ? 0 : item.Field<int>(5),
                        juzgado = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        //id_juzgado = new()
                        //{
                        //    Value = item.IsNull(5) ? 0 : item.Field<int>(5),
                        //    Label = item.IsNull(6) ? null! : item.Field<string>(6)
                        //},
                        nombre_quejoso = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        id_materia= item.IsNull(8) ? 0 : item.Field<int>(8),
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
                        //numero_autoridad = item.IsNull(17) ? 0 : item.Field<int>(17),
                        no_autoridades = item.IsNull(15) ? 0 : (int)item.Field<long>(15),


                        //id_autoridad_responsable = new()
                        //{
                        //    Value = item.IsNull(17) ? 0 : item.Field<int>(17),
                        //    Label = item.IsNull(18) ? null! : item.Field<string>(18)
                        //},
                        id_administracion = item.IsNull(16) ? 0 : item.Field<int>(16),
                        administracion = item.IsNull(17) ? null! : item.Field<string>(17)!,
                        //id_administracion = new()
                        //{
                        //    Value = item.IsNull(16) ? 0 : item.Field<int>(16),
                        //    Label = item.IsNull(17) ? null! : item.Field<string>(17)
                        //},
                        id_subadministracion = item.IsNull(18) ? 0 : item.Field<int>(18),
                        subadministracion = item.IsNull(19) ? null! : item.Field<string>(19)!,
                        //id_subadministracion = new()
                        //{
                        //    Value = item.IsNull(18) ? 0 : item.Field<int>(18),
                        //    Label = item.IsNull(19) ? null! : item.Field<string>(19)
                        //},
                        abogado = item.IsNull(20) ? null! : item.Field<string>(20),
                        rfc_quejoso = item.IsNull(21) ? null! : item.Field<string>(21)!,
                        id_estado_tarea = item.IsNull(22) ? 0 : item.Field<int>(22),
                        estado_tarea = item.IsNull(23) ? null! : item.Field<string>(23)!,
                        //id_estado_tarea = new()
                        //{
                        //    Value = item.IsNull(22) ? 0 : item.Field<int>(22),
                        //    Label = item.IsNull(23) ? null! : item.Field<string>(23)
                        //},
                        id_estado_procesal = item.IsNull(24) ? 0 : item.Field<int>(24),
                        estado_procesal = item.IsNull(25) ? null! : item.Field<string>(25)!,
                        //id_estado_procesal = new()
                        //{
                        //    Value = item.IsNull(24) ? 0 : item.Field<int>(24),
                        //    Label = item.IsNull(25) ? null! : item.Field<string>(25)
                        //},
                        id_estado_procesal_incidental = item.IsNull(26) ? 0 : item.Field<int>(26),
                        estado_procesal_incidental = item.IsNull(27) ? null! : item.Field<string>(27)!,
                        //id_estado_procesal_incidental = new()
                        //{
                        //    Value = item.IsNull(26) ? 0 : item.Field<int>(26),
                        //    Label = item.IsNull(27) ? null! : item.Field<string>(27)
                        //},
                        id_alerta = item.IsNull(28) ? 0 : item.Field<int>(28),
                        alerta = item.IsNull(29) ? null! : item.Field<string>(29)!,
                        activo = item.IsNull(30) ? false : item.Field<bool>(30),

                    }
                );
            }

            return resultList;
        }
        #endregion

        //#region Get Histórico ByFilters para Abogado
        //public async Task<List<ResponseHistoricoAbogadoByFilters>> GetHistoricoAbogadoByFilters(
        //    int Fetch,
        //    int Page,
        //    string OrderByColumn,
        //    bool OrderDesc,
        //    DateTime? fechaRecepcionInicial,
        //    DateTime? fechaRecepcionFinal,
        //    DateTime? fechaInicialVencimiento,
        //    DateTime? fechaFinalVencimiento,
        //    string numeroExpediete,
        //    string numeroAsunto,
        //    int? idJuzgado,
        //    string nombreQuejoso,
        //    int? idMateria,
        //    int? idSubmateria,
        //    int? idTipoActo,
        //    string despacho,
        //    int? idAdministracion,
        //    int? idSubadministracion,
        //    int? idAutoridadResponsable,
        //    string rfcQuejoso,
        //    int? idEstadoTarea,
        //    int? idEstadoProcesal,
        //    int? idEstadoProcesalIncidental
        //    )
        //{
        //    ParameterPGsql[] parameters =
        //    {
        //        new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, Fetch),
        //        new ParameterPGsql("p_page", NpgsqlDbType.Integer, Page),
        //        new ParameterPGsql("p_fecha_recepcion_inicial", NpgsqlDbType.Date, fechaRecepcionInicial),
        //        new ParameterPGsql("p_fecha_recepcion_final", NpgsqlDbType.Date, fechaRecepcionFinal),
        //        new ParameterPGsql("p_fecha_inicial_vencimiento", NpgsqlDbType.Date, fechaInicialVencimiento),
        //        new ParameterPGsql("p_fecha_final_vencimiento", NpgsqlDbType.Date, fechaFinalVencimiento),
        //        new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediete),
        //        new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text, numeroAsunto),
        //        new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
        //        new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
        //        new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, idMateria),
        //        new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, idSubmateria),
        //        new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),
        //        new ParameterPGsql("p_despacho", NpgsqlDbType.Text, despacho),
        //        new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer, idAdministracion),
        //        new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer, idSubadministracion),
        //        //new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer, idAutoridadResponsable), //Se agrega autoridad responsable para ABOGADO
        //        new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
        //        //new ParameterPGsql("p_transparencia", NpgsqlDbType.Varchar, transparencia),  // Se agrega Transparencia para ABOGADO
        //        //new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
        //        new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, idEstadoTarea), //ESTE NO VA EN ABOGADO
        //        new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
        //        new ParameterPGsql("p_id_estado_procesal_incidental", NpgsqlDbType.Integer, idEstadoProcesalIncidental),
        //        new ParameterPGsql("order_column", NpgsqlDbType.Text, /*null*/ OrderByColumn),
        //        new ParameterPGsql("order_desc", NpgsqlDbType.Boolean, /*null*/ OrderDesc),

        //    };

        //    var response = await _database.ExecuteFunctionAsync(
        //        EnumFunctions.HistoricoAbogadoByFilters,
        //        parameters!
        //    );
        //    if (response.ExisteError)
        //    {
        //        throw new Exception(response.Mensaje);
        //    }

        //    if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
        //    {
        //        return null!;
        //    }

        //    List<ResponseHistoricoAbogadoByFilters> resultList = new();
        //    foreach (DataRow item in response.Data.Tables[0].Rows)
        //    {
        //        resultList.Add(
        //            new()
        //            {
        //                id = item.IsNull(0) ? 0 : item.Field<int>(0),
        //                fecha_recepcion = item.IsNull(1) ? null! : item.Field<DateTime>(1).ToString("yyyy-MM-dd"),
        //                fecha_vencimiento = item.IsNull(2) ? null! : item.Field<DateTime>(2).ToString("yyyy-MM-dd"),

        //                numero_expediente = item.IsNull(3) ? null! : item.Field<string>(3)!,
        //                juicio_amparo = item.IsNull(4) ? null! : item.Field<string>(4)!,

        //                id_juzgado = new()
        //                {
        //                    Value = item.IsNull(5) ? 0 : item.Field<int>(5),
        //                    Label = item.IsNull(6) ? null! : item.Field<string>(6)
        //                },

        //                nombre_quejoso = item.IsNull(7) ? null! : item.Field<string>(7)!,

        //                id_materia = new()
        //                {
        //                    Value = item.IsNull(8) ? 0 : item.Field<int>(8),
        //                    Label = item.IsNull(9) ? null! : item.Field<string>(9)
        //                },
        //                id_submateria = new()
        //                {
        //                    Value = item.IsNull(10) ? 0 : item.Field<int>(10),
        //                    Label = item.IsNull(11) ? null! : item.Field<string>(11)
        //                },
        //                id_tipo_acto = new()
        //                {
        //                    Value = item.IsNull(12) ? 0 : item.Field<int>(12),
        //                    Label = item.IsNull(13) ? null! : item.Field<string>(13)
        //                },
        //                despacho = item.IsNull(14) ? null! : item.Field<string>(14)!,
        //                id_administracion = new()
        //                {
        //                    Value = item.IsNull(15) ? 0 : item.Field<int>(15),
        //                    Label = item.IsNull(16) ? null! : item.Field<string>(16)
        //                },
        //                id_subadministracion = new()
        //                {
        //                    Value = item.IsNull(17) ? 0 : item.Field<int>(17),
        //                    Label = item.IsNull(18) ? null! : item.Field<string>(18)
        //                },
        //                numero_autoridad = item.IsNull(19) ? 0 : (int)item.Field<long>(19),

        //                //id_autoridad_responsable = new()
        //                //{
        //                //    Value = item.IsNull(19) ? 0 : item.Field<int>(19),
        //                //    Label = item.IsNull(20) ? null! : item.Field<string>(20)
        //                //},

        //                rfc_quejoso = item.IsNull(20) ? null! : item.Field<string>(20)!,

        //                id_estado_tarea = new()
        //                {
        //                    Value = item.IsNull(21) ? 0 : item.Field<int>(21),
        //                    Label = item.IsNull(22) ? null! : item.Field<string>(22)
        //                },
        //                id_estado_procesal = new()
        //                {
        //                    Value = item.IsNull(23) ? 0 : item.Field<int>(23),
        //                    Label = item.IsNull(24) ? null! : item.Field<string>(24)
        //                },
        //                id_estado_procesal_incidental = new()
        //                {
        //                    Value = item.IsNull(25) ? 0 : item.Field<int>(25),
        //                    Label = item.IsNull(26) ? null! : item.Field<string>(26)
        //                },


        //                activo = item.IsNull(27) ? false : item.Field<bool>(27),

        //            }
        //        );
        //    }

        //    return resultList;
        //}
        //#endregion

        //#region Historico Count Abogado

        //public async Task<int?> GetHistoricoAbogadoByFiltersCount(
        //    DateTime? fechaRecepcionInicial,
        //    DateTime? fechaRecepcionFinal,
        //    DateTime? fechaInicialVencimiento,
        //    DateTime? fechaFinalVencimiento,
        //    string numeroExpediete,
        //    string numeroAsunto,
        //    int? idJuzgado,
        //    string nombreQuejoso,
        //    int? idMateria,
        //    int? idSubmateria,
        //    int? idTipoActo,
        //    string despacho,
        //    int? idAdministracion,
        //    int? idSubadministracion,
        //    int? idAutoridadResponsable,
        //    string rfcQuejoso,
        //    int? idEstadoTarea,
        //    int? idEstadoProcesal,
        //    int? idEstadoProcesalIncidental)
        //{
        //    ParameterPGsql[] parameters =
        //    {
        //        //new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, Fetch),
        //        //new ParameterPGsql("p_page", NpgsqlDbType.Integer, Page),
        //        new ParameterPGsql("p_fecha_recepcion_inicial", NpgsqlDbType.Date, fechaRecepcionInicial),
        //        new ParameterPGsql("p_fecha_recepcion_final", NpgsqlDbType.Date, fechaRecepcionFinal),
        //        new ParameterPGsql("p_fecha_inicial_vencimiento", NpgsqlDbType.Date, fechaRecepcionFinal),
        //        new ParameterPGsql("p_fecha_final_vencimiento", NpgsqlDbType.Date, fechaRecepcionFinal),
        //        new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediete),
        //        new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text, numeroAsunto),
        //        new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
        //        new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
        //        new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, idMateria),
        //        new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, idSubmateria),
        //        new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),
        //        new ParameterPGsql("p_despacho", NpgsqlDbType.Text, despacho),
        //        new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer, idAdministracion),
        //        new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer, idSubadministracion),
        //        new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer, idAutoridadResponsable), //Se agrega autoridad responsable para ABOGADO
        //        new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
        //        //new ParameterPGsql("p_transparencia", NpgsqlDbType.Varchar, transparencia),  // Se agrega Transparencia para ABOGADO
        //        new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, idEstadoTarea), //ESTE NO VA EN ABOGADO
        //        //new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),


        //        ////new ParameterPGsql("p_fecha_recepcion_inicial", NpgsqlDbType.Date, fechaRecepcionInicial),
        //        ////new ParameterPGsql("p_fecha_recepcion_final", NpgsqlDbType.Date, fechaRecepcionFinal),
        //        ////new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediete),
        //        ////new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text, numeroAsunto),
        //        ////new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
        //        ////new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
        //        ////new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, idMateria),
        //        ////new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, idSubmateria),
        //        ////new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),
        //        ////new ParameterPGsql("p_despacho", NpgsqlDbType.Text, despacho),
        //        ////new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer, idAdministracion),
        //        ////new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer, idSubmateria),
        //        ////new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
        //        ////new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
        //        //////new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
        //    };

        //    var response = await _database.ExecuteFunctionAsync(
        //        EnumFunctions.HistoricoAbogadoByFiltersCount,
        //        parameters!
        //    );
        //    if (response.ExisteError)
        //    {
        //        throw new Exception(response.Mensaje);
        //    }

        //    if (response.Data.Tables.Count <= 0 || response.Data.Tables[0].Rows.Count <= 0)
        //    {
        //        return null!;
        //    }

        //    return response.Data.Tables[0].Rows[0].IsNull(0)
        //        ? null!
        //        : response.Data.Tables[0].Rows[0].Field<int>(0);
        //}
        //#endregion

        //#region GetById
        //public async Task<ResponseAmparoIndirectoByID> GetByIdAmparoAsync(int idNumeroAsunto)
        //{
        //    ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, idNumeroAsunto), };

        //    var response = await _database.ExecuteFunctionAsync(
        //        EnumFunctions.AmparoIndirectoGetById,
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

        //    return new()
        //    {

        //        id = response.Data.Tables[0].Rows[0].IsNull(0) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(0),
        //        juicio_amparo = response.Data.Tables[0].Rows[0].IsNull(1) ? null! : response.Data.Tables[0].Rows[0].Field<string>(1)!,
        //        numero_expediente = response.Data.Tables[0].Rows[0].IsNull(2) ? null! : response.Data.Tables[0].Rows[0].Field<string>(2)!,
        //        fecha_recepcion_demanda = response.Data.Tables[0].Rows[0].IsNull(3) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(3).ToString("yyyy-MM-dd"),
        //        juzgado = new()
        //        {
        //            Value = response.Data.Tables[0].Rows[0].IsNull(4)
        //             ? 0
        //             : response.Data.Tables[0].Rows[0].Field<int>(4),
        //        },
        //        contribuyente = response.Data.Tables[0].Rows[0].IsNull(5)
        //        ? false
        //        : response.Data.Tables[0].Rows[0].Field<bool>(5),

        //        rfc_quejoso = response.Data.Tables[0].Rows[0].IsNull(6) ? null! : response.Data.Tables[0].Rows[0].Field<string>(6)!,
        //        nombre_quejoso = response.Data.Tables[0].Rows[0].IsNull(7) ? null! : response.Data.Tables[0].Rows[0].Field<string>(7)!,
        //        materia = new()
        //        {
        //            Value = response.Data.Tables[0].Rows[0].IsNull(8)
        //             ? 0
        //             : response.Data.Tables[0].Rows[0].Field<int>(8),
        //        },
        //        submateria = new()
        //        {
        //            Value = response.Data.Tables[0].Rows[0].IsNull(9)
        //             ? 0
        //             : response.Data.Tables[0].Rows[0].Field<int>(9),
        //        },
        //        tipo_acto = new()
        //        {
        //            Value = response.Data.Tables[0].Rows[0].IsNull(10)
        //             ? 0
        //             : response.Data.Tables[0].Rows[0].Field<int>(10),
        //        },
        //        despacho = response.Data.Tables[0].Rows[0].IsNull(11) ? null! : response.Data.Tables[0].Rows[0].Field<string>(11)!,
        //        //autoridad_responsable = new()
        //        //{
        //        //    Value = response.Data.Tables[0].Rows[0].IsNull(12)
        //        //     ? 0
        //        //     : response.Data.Tables[0].Rows[0].Field<int>(12),
        //        //},
        //        administracion = new()
        //        {
        //            Value = response.Data.Tables[0].Rows[0].IsNull(12)
        //             ? 0
        //             : response.Data.Tables[0].Rows[0].Field<int>(12),
        //        },
        //        subadministracion = new()
        //        {
        //            Value = response.Data.Tables[0].Rows[0].IsNull(13)
        //             ? 0
        //             : response.Data.Tables[0].Rows[0].Field<int>(13),
        //        },
        //        estado_tarea = new()
        //        {
        //            Value = response.Data.Tables[0].Rows[0].IsNull(14)
        //             ? 0
        //             : response.Data.Tables[0].Rows[0].Field<int>(14),
        //        },
        //        estado_procesal = new()
        //        {
        //            Value = response.Data.Tables[0].Rows[0].IsNull(15)
        //             ? 0
        //             : response.Data.Tables[0].Rows[0].Field<int>(15),
        //        },
        //        estado_procesal_incidental = new()
        //        {
        //            Value = response.Data.Tables[0].Rows[0].IsNull(16)
        //             ? 0
        //             : response.Data.Tables[0].Rows[0].Field<int>(16),
        //        },
        //        recurso_queja_principal = response.Data.Tables[0].Rows[0].IsNull(17)
        //        ? false
        //         : response.Data.Tables[0].Rows[0].Field<bool>(17),
        //        recurso_revision_principal = response.Data.Tables[0].Rows[0].IsNull(18)
        //        ? false
        //         : response.Data.Tables[0].Rows[0].Field<bool>(18),

        //        recurso_queja_incidental = response.Data.Tables[0].Rows[0].IsNull(19)
        //        ? false
        //        : response.Data.Tables[0].Rows[0].Field<bool>(19),
        //        recurso_revision_incidental = response.Data.Tables[0].Rows[0].IsNull(20)
        //        ? false
        //        : response.Data.Tables[0].Rows[0].Field<bool>(20),
        //        activo = response.Data.Tables[0].Rows[0].IsNull(21)
        //        ? false
        //         : response.Data.Tables[0].Rows[0].Field<bool>(21),
        //        numero_empleado = response.Data.Tables[0].Rows[0].IsNull(22) ? null! : response.Data.Tables[0].Rows[0].Field<string>(22)!,
        //        fecha_turnado = response.Data.Tables[0].Rows[0].IsNull(23) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(23).ToString("yyyy-MM-dd"),
        //        fecha_creacion = response.Data.Tables[0].Rows[0].IsNull(24) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(24).ToString("yyyy-MM-dd"),
        //        fecha_modificacion = response.Data.Tables[0].Rows[0].IsNull(25) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(25).ToString("yyyy-MM-dd"),

        //    };
        //}
        //#endregion

        #region GetByIdInformeJustificadoDescartar
        public async Task<List<ResponseInformeJustificadoConstitucional>> GetByIdInformeJustificadoConstitucionalAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetByIdInformeJustificadoDescartar,
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
                        solicitud_opinion_tecnica = item.IsNull(4) ? false : item.Field<bool>(4),
                        fecha_recepcion_demanda = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_justificado = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        numero_oficio_informe_justificado = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        fecha_presentacion_informe_justificado = item.IsNull(8) ? null! : item.Field<DateTime>(8).ToString("yyyy-MM-dd"),
                        observaciones_justificado = item.IsNull(9) ? null! : item.Field<string>(9)!,

                    }
                );
            }
            return resultList;
        }

        #endregion

        #region GetAll Informe Justificado Descartar

        public async Task<List<ResponseDescartarInformeJustificadoConstitucional>> GetAllInformeJustificadoDescartarAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.GetAllDescartarInformeJustificado,
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

            List<ResponseDescartarInformeJustificadoConstitucional> ResultList = new();
            foreach(DataRow item in response.Data.Tables[0].Rows)
            {
                ResultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        autoridad_responsable = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        solicitud_opinion_tecnica = item.IsNull(4) ? false : item.Field<bool>(4),
                        fecha_recepcion_demanda = item.IsNull(5) ? null! : item.Field<DateTime>(5).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_justificado = item.IsNull(6) ? null! : item.Field<DateTime>(6).ToString("yyyy-MM-dd"),
                        numero_oficio_informe_justificado = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        fecha_presentacion_informe_justificado = item.IsNull(8) ? null! : item.Field<DateTime>(8).ToString("yyyy-MM-dd"),
                    }
                );
            }
            return ResultList;
        }

        #endregion

        #region Descartar Informe Justificado

        public async Task<ResultTransaction> UpdateDescartarInformeJustificado(int idInformeJustificado)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, idInformeJustificado) };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.UpdateDescartarInformeJustificado,
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
    }
}
