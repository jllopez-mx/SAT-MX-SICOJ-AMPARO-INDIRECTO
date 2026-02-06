using AmparoIndirectoAPI.Model.DTO;
using System.Data;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.ViewModels.Enums;
using NpgsqlTypes;
using Sicoj.Utils.Postgres;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;
using Sicoj.Utils.Enums;

namespace AmparoIndirectoAPI.Model.DAO.Repository
{
    public class AmparoIndirectoOficialPartesRepository : IAmparoIndirectoOficialPartesRepository
    {

        #region Variables
        private readonly ISqlTools _database;
        #endregion

        #region Constructor
        public AmparoIndirectoOficialPartesRepository(ISqlTools database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }
        #endregion

        //Métodos

        #region GetAll
        public async Task<int?> GetAllCountAsyncAI()
        {
            ParameterPGsql[] parameters = {
                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.PENDIENTE_DE_TURNAR.GetHashCode()),
                new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, EnumEstadoProcesal.ACTIVO.GetHashCode()),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoCount,
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

        public async Task<List<ResponseAmparoIndirectoBandeja>> GetAllAsyncAI(
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
                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.PENDIENTE_DE_TURNAR.GetHashCode()),
                new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, EnumEstadoProcesal.ACTIVO.GetHashCode()),
                new ParameterPGsql("order_column", NpgsqlDbType.Text, orderByColumn /*null*/),
                new ParameterPGsql("order_desc", NpgsqlDbType.Boolean, orderDesc /*null*/),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetAll,
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

            List<ResponseAmparoIndirectoBandeja> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {

                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        fecha_recepcion = item.IsNull(1) ? null! : item.Field<DateTime>(1).ToString("yyyy-MM-dd"),
                        numero_expediente = item.IsNull(2) ? null! : item.Field<string>(2)!,
                        numero_asunto = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        id_juzgado = item.IsNull(4) ? 0 : item.Field<int>(4),
                        juzgado = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        //id_juzgado = new()
                        //{
                        //    Value = item.IsNull(4) ? 0 : item.Field<int>(4),
                        //    Label = item.IsNull(5) ? null! : item.Field<string>(5)
                        //},
                        nombre_quejoso = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        id_materia = item.IsNull(7) ? 0 : item.Field<int>(7),
                        materia = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        //id_materia = new()
                        //{
                        //    Value = item.IsNull(7) ? 0 : item.Field<int>(7),
                        //    Label = item.IsNull(8) ? null! : item.Field<string>(8)
                        //},
                        id_submateria = item.IsNull(9) ? 0 : item.Field<int>(9),
                        submateria = item.IsNull(10) ? null! : item.Field<string>(10)!,
                        //id_submateria = new()
                        //{
                        //    Value = item.IsNull(9) ? 0 : item.Field<int>(9),
                        //    Label = item.IsNull(10) ? null! : item.Field<string>(10)
                        //},
                        id_tipo_acto = item.IsNull(11) ? 0 : item.Field<int>(11),
                        tipo_acto = item.IsNull(12) ? null! : item.Field<string>(12)!,
                        //id_tipo_acto = new()
                        //{
                        //    Value = item.IsNull(11) ? 0 : item.Field<int>(11),
                        //    Label = item.IsNull(12) ? null! : item.Field<string>(12)
                        //},

                        despacho = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        numero_autoridades = item.IsNull(14) ? 0 : (int)item.Field<long>(14),
                        rfc_quejoso = item.IsNull(15) ? null! : item.Field<string>(15)!,
                        id_estado_tarea = item.IsNull(16) ? 0 : item.Field<int>(16),
                        estado_tarea = item.IsNull(17) ? null! : item.Field<string>(17)!,
                        //id_estado_tarea = new()
                        //{
                        //    Value = item.IsNull(16) ? 0 : item.Field<int>(16),
                        //    Label = item.IsNull(17) ? null! : item.Field<string>(17)
                        //},
                        id_estado_procesal = item.IsNull(18) ? 0 : item.Field<int>(18),
                        estado_procesal = item.IsNull(19) ? null! : item.Field<string>(19)!,
                        //id_estado_procesal = new()
                        //{
                        //    Value = item.IsNull(18) ? 0 : item.Field<int>(18),
                        //    Label = item.IsNull(19) ? null! : item.Field<string>(19)
                        //},
                        activo = response.Data.Tables[0].Rows[0].IsNull(20)
                        ? false
                        : response.Data.Tables[0].Rows[0].Field<bool>(20),

                    }
                );
            }

            return resultList;
        }
        #endregion

        #region GetByFilters
        public async Task<List<ResponseAmparoIndirectoByFilters>> GetAllByFiltersAsyncAI(
                int Fetch,
                int Page,
                string OrderByColumn,
                bool OrderDesc,
                DateTime? fechaRecepcionInicial,
                DateTime? fechaRecepcionFinal,
                DateTime? fechaVencimientoDemanda,
                string numeroExpediente,
                string numeroAsunto,
                int? idJuzgado,
                string nombreQuejoso,
                int? idMateria,
                int? idSubmateria,
                int? idTipoActo,
                string despacho,
                int? idAdministracion,
                int? idSubadministracion,
                string rfcQuejoso,
                int? idEstadoTarea,
                int? idEstadoProcesal
            )
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, Fetch),
                new ParameterPGsql("p_page", NpgsqlDbType.Integer, Page),
                new ParameterPGsql("p_fecha_recepcion_inicial", NpgsqlDbType.Date, fechaRecepcionInicial),
                new ParameterPGsql("p_fecha_recepcion_final", NpgsqlDbType.Date, fechaRecepcionFinal),
                new ParameterPGsql("p_fecha_vencimiento_demanda", NpgsqlDbType.Date, fechaVencimientoDemanda),
                new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediente),
                new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text, numeroAsunto),
                new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
                new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
                new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, idMateria),
                new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, idSubmateria),
                new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),
                new ParameterPGsql("p_despacho", NpgsqlDbType.Text, despacho),
                new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer, idAdministracion),
                new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer, idSubadministracion),
                new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
                new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
                new ParameterPGsql("order_column", NpgsqlDbType.Text, /*null*/ OrderByColumn),
                new ParameterPGsql("order_desc", NpgsqlDbType.Boolean, /*null*/ OrderDesc),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoByFilters,
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

            List<ResponseAmparoIndirectoByFilters> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        numero_asunto = item.IsNull(1) ? null! : item.Field<string>(1)!,
                        numero_expediente = item.IsNull(2) ? null! : item.Field<string>(2)!,
                        fecha_recepcion = item.IsNull(3) ? null! : item.Field<DateTime>(3).ToString("yyyy-MM-dd"),
                        fecha_vencimiento_demanda = item.IsNull(4) ? null! : item.Field<DateTime>(4).ToString("yyyy-MM-dd"),
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
                        numero_autoridad = item.IsNull(15) ? 0 : (int)item.Field<long>(15),
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

                        activo = response.Data.Tables[0].Rows[0].IsNull(25)
                     ? false
                     : response.Data.Tables[0].Rows[0].Field<bool>(25),

                    }
                );
            }

            return resultList;
        }

        public async Task<int?> GetAllByFiltersCountAsyncAI(
        DateTime? fechaRecepcionInicial,
        DateTime? fechaRecepcionFinal,
        DateTime? fechaVencimientoDemanda,
        string numeroExpediente,
        string numeroAsunto,
        int? idJuzgado,
        string nombreQuejoso,
        int? idMateria,
        int? idSubmateria,
        int? idTipoActo,
        string despacho,
        int? idAdministracion,
        int? idSubadministracion,
        string rfcQuejoso,
        int? idEstadoTarea,
        int? idEstadoProcesal
        )
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_fecha_recepcion_inicial", NpgsqlDbType.Date, fechaRecepcionInicial),
                new ParameterPGsql("p_fecha_recepcion_final", NpgsqlDbType.Date, fechaRecepcionFinal),
                new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediente),
                new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text, numeroAsunto),
                new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
                new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
                new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, idMateria),
                new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, idSubmateria),
                new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),
                new ParameterPGsql("p_despacho", NpgsqlDbType.Text, despacho),
                new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer, idAdministracion),
                new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer, idSubmateria),
                new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
                new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, idEstadoProcesal),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoByFiltersCount,
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

        #region Get Expedientes antes de Acumular

        public async Task<List<ResponseAcumularByFilters>> GetExpedientesAntesAcumularByFilters(
                int Fetch,
                int Page,
                string OrderByColumn,
                bool OrderDesc,
                string numeroExpediente,
                string numeroAsunto,
                string rfcQuejoso,
                string nombreQuejoso,
                int? idJuzgado,
                int? idTipoActo,
                int? idEstadoTarea
            )
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, Fetch),
                new ParameterPGsql("p_page", NpgsqlDbType.Integer, Page),
                new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediente),
                new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text, numeroAsunto),
                new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
                new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
                new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
                new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),
                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
                new ParameterPGsql("order_column", NpgsqlDbType.Text, null /*OrderByColumn*/),
                new ParameterPGsql("order_desc", NpgsqlDbType.Boolean, null /*OrderDesc*/),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AcumularSelect,
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

            List<ResponseAcumularByFilters> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {

                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_administracion = item.IsNull(1) ? 0 : item.Field<int>(1),
                        administracion = item.IsNull(2) ? null! : item.Field<string>(2)!,

                        //administracion = new()
                        //{
                        //    Value = item.IsNull(1) ? 0 : item.Field<int>(1),
                        //    Label = item.IsNull(2) ? null! : item.Field<string>(2)
                        //},
                        numero_asunto = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        numero_expediente = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        rfc_quejoso = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        nombre_quejoso = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        id_juzgado = item.IsNull(7) ? 0 : item.Field<int>(7),
                        juzgado = item.IsNull(8) ? null! : item.Field<string>(8)!,
                        //juzgado = new()
                        //{
                        //    Value = item.IsNull(7) ? 0 : item.Field<int>(7),
                        //    Label = item.IsNull(8) ? null! : item.Field<string>(8)
                        //},

                    }
                );
            }

            return resultList;
        }

        public async Task<int?> GetExpedientesAntesAcumularByFiltersCount(
            string numeroExpediente,
            string numeroAsunto,
            string rfcQuejoso,
            string nombreQuejoso,
            int? idJuzgado,
            int? idTipoActo,
            int? idEstadoTarea
        )
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Text, numeroExpediente),
                new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Text, numeroAsunto),
                new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, rfcQuejoso),
                new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Text, nombreQuejoso),
                new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, idJuzgado),
                new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, idTipoActo),

                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, EnumEstadoTarea.TURNAR.GetHashCode()),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AcumularSelectCount,
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

        #region Get All Solicitud de Transparencia
        public async Task<int?> GetAllCountSolicitudTransparencia(int? id_numero_asunto)
        {
            ParameterPGsql[] parameters = {
             new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, id_numero_asunto),
            };


            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.SolicitudTransparenciaCount,
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

        public async Task<List<ResponseSolicitudTransparenciaLista>> GetAllSolicitudTransparencia(
                int pageSize,
                int page,
                int? id_numero_asunto
            )
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, id_numero_asunto),
                new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, pageSize),
                new ParameterPGsql("p_page", NpgsqlDbType.Integer, page),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.SolicitudTransparenciaSelect,
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

            List<ResponseSolicitudTransparenciaLista> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {

                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        numero_solicitud = item.IsNull(2) ? null! : item.Field<string>(2)!,
                        fecha_solicitud = item.IsNull(3) ? null! : item.Field<DateTime>(3).ToString("yyyy-MM-dd"),
                    }
                );
            }

            return resultList;
        }
        #endregion

        #region GetAutoridadesResponsables
        public async Task<List<ResponseAutoridadesResponsables>> GetAutoridadesResponsablesAsync(
                int idNumeroAsunto
            )
        {
            ParameterPGsql[] parameters =
            {

                new ParameterPGsql("p_id", NpgsqlDbType.Integer, idNumeroAsunto),

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

        #region GET ASUNTO CONTROL DOCUMENTAL
        public async Task<ResponseAsuntoControlDocumental> GetAsuntoControlDocumentalAsync(string NumeroAsunto)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_numer_Asunto", NpgsqlDbType.Varchar, NumeroAsunto), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.ConsultaAsuntoControlDocumental,
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

                id_asunto = response.Data.Tables[0].Rows[0].IsNull(0) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(0),
                numero_asunto = response.Data.Tables[0].Rows[0].IsNull(1) ? null! : response.Data.Tables[0].Rows[0].Field<string>(1)!,
                id_administracion = response.Data.Tables[0].Rows[0].IsNull(2) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(2),
                //activo = response.Data.Tables[0].Rows[0].IsNull(3)
                //? false
                // : response.Data.Tables[0].Rows[0].Field<bool>(3),
                //numero_empleado = response.Data.Tables[0].Rows[0].IsNull(4) ? null! : response.Data.Tables[0].Rows[0].Field<string>(4)!,
                //fecha_creacion = response.Data.Tables[0].Rows[0].IsNull(5) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(5).ToString("yyyy-MM-dd"),
                //fecha_modificacion = response.Data.Tables[0].Rows[0].IsNull(6) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(6).ToString("yyyy-MM-dd"),
            };
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

        #region Get Transparencia
        public async Task<ResponseSolicitudTransparenciaLista> GetByIdTransparenciaAsync(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.SolicitudTransparenciaByID,
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
                numero_solicitud = response.Data.Tables[0].Rows[0].IsNull(2) ? null! : response.Data.Tables[0].Rows[0].Field<string>(2)!,
                fecha_solicitud = response.Data.Tables[0].Rows[0].IsNull(3) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(3).ToString("yyyy-MM-dd"),

            };
        }
        #endregion

        #region Get Cabeza de Serie

        public async Task<ResponseAcumularByFilters> GetCabezaSerieFrontById(int idNumeroAsunto)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, idNumeroAsunto), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AcumularCabezaSerie,
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
                id_administracion = response.Data.Tables[0].Rows[0].IsNull(1) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(1),
                //administracion = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(1)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(1),
                //},
                numero_asunto = response.Data.Tables[0].Rows[0].IsNull(2) ? null! : response.Data.Tables[0].Rows[0].Field<string>(2)!,
                numero_expediente = response.Data.Tables[0].Rows[0].IsNull(3) ? null! : response.Data.Tables[0].Rows[0].Field<string>(3)!,
                rfc_quejoso = response.Data.Tables[0].Rows[0].IsNull(4) ? null! : response.Data.Tables[0].Rows[0].Field<string>(4)!,
                nombre_quejoso = response.Data.Tables[0].Rows[0].IsNull(5) ? null! : response.Data.Tables[0].Rows[0].Field<string>(5)!,
                id_juzgado = response.Data.Tables[0].Rows[0].IsNull(6) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(6),
                //juzgado = new()
                //{
                //    Value = response.Data.Tables[0].Rows[0].IsNull(6)
                //     ? 0
                //     : response.Data.Tables[0].Rows[0].Field<int>(6),
                //},
            };
        }

        #endregion

        #region GetAll Canalizar
        public async Task<int?> GetAllCanalizarCountAsyncAI()
        {
            ParameterPGsql[] parameters = { };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CanalizarGetCount,
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

        public async Task<List<ResponseAmparoIndirectoRemitir>> GetAllCanalizarAsyncAI(
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
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CanalizarGetAll,
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

            List<ResponseAmparoIndirectoRemitir> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {

                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        numero_oficio_canalizacion = item.IsNull(1) ? null! : item.Field<string>(1)!,
                        fecha_canalizacion = item.IsNull(2) ? null! : item.Field<DateTime>(2).ToString("dd/MM/yyyy"),
                        unidad_administrativa_recibe_asunto = item.IsNull(3) ? null! : item.Field<string>(3)!,
                        unidad_administrativa_canaliza_asunto = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        motivo_canaliza = item.IsNull(5) ? null! : item.Field<string>(5)!,
                    }
                );
            }

            return resultList;
        }


        #endregion

        public async Task<List<ResponseFechaVencimiento>> GetAllFechaVencimientoSeccionAsync(
    string fechaInicio,
    string fechaFinal,
    List<int> idsSeccion
)   
        {
            ParameterPGsql[] parameters =
            {
        new ParameterPGsql("p_fecha_inicio", NpgsqlDbType.Date, Convert.ToDateTime(fechaInicio)),
        new ParameterPGsql("p_fecha_final", NpgsqlDbType.Date, Convert.ToDateTime(fechaFinal)),
        new ParameterPGsql("p_ids_secciones", NpgsqlDbType.Array | NpgsqlDbType.Integer, idsSeccion),
    };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.FechasVencimientoSeccionGetAll,
                parameters!
            );

            if (response.ExisteError)
            {
                throw new Exception(response.Mensaje);
            }

            if (response.Data.Tables.Count == 0 || response.Data.Tables[0].Rows.Count == 0)
            {
                return new List<ResponseFechaVencimiento>();
            }

            List<ResponseFechaVencimiento> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(new ResponseFechaVencimiento
                {
                    IdSeccionRenglon = item.IsNull("id") ? 0 : item.Field<int>("id"),
                    IdAsunto = item.IsNull("id_juicio_amparo") ? 0 : item.Field<int>("id_juicio_amparo"),
                    FechaVencimiento = item.IsNull("fecha_vencimiento")
                        ? null
                        : item.Field<DateTime>("fecha_vencimiento").ToString("dd/MM/yyyy"),
                    IdSeccion = item.IsNull("id_seccion") ? 0 : item.Field<int>("id_seccion"),

                    // Opcional: si tus propiedades extra no aplican, déjalas en 0 o elimínalas
                    IdModulo = EnumModulosRedis.AMPARO_INDIRECTO.GetHashCode(),
                    MesesCalendario = 0
                });
            }

            return resultList;
        }

    }
}
