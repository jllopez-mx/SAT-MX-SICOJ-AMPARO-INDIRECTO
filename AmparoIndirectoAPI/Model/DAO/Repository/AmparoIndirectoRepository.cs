using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.ViewModels.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql.Internal.TypeHandlers.NumericHandlers;
using NpgsqlTypes;
using Sicoj.Utils.Models;
using Sicoj.Utils.Postgres;
using System.Data;

namespace AmparoIndirectoAPI.Model.DAO.Repository
{
    public class AmparoIndirectoRepository : IAmparoIndirectoRepository
    {

        #region Variables
        private readonly ISqlTools _database;
        #endregion

        #region Constructor
        public AmparoIndirectoRepository(ISqlTools database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }
        #endregion

        #region Update asignar

        public async Task<ResultTransaction> UpdateAsignarAbogadoAdministradorAsync(AmparoIndirecto entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id",NpgsqlDbType.Integer,entity.id!),
                new ParameterPGsql("p_id_abogado",NpgsqlDbType.Varchar,entity.id_abogado!),
                new ParameterPGsql("p_numero_empleado", NpgsqlDbType.Varchar, entity.numero_empleado!),
                new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, entity.id_estado_procesal!),
                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, entity.id_estado_tarea!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AsignarAbogadoAdministrador,
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

        #region Crear registro

        public async Task<ResultTransaction> CreateCanalizarAsuntoAdministradorAsync(RequestCreateCanalizarAsuntoAdministrador entity)
        {
            var fechaRemi = DateTime.Parse(entity.fecha_canalizacion);

            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_numero_oficio_canalizacion", NpgsqlDbType.Varchar, entity.numero_oficio_canalizacion!),
                new ParameterPGsql("p_fecha_canalizacion", NpgsqlDbType.Date, DateTime.Parse(entity.fecha_canalizacion)!),
                new ParameterPGsql("p_unidad_administrativa_canaliza", NpgsqlDbType.Text, (entity.unidad_administrativa_canaliza).ToString()!),
                new ParameterPGsql("p_unidad_administrativa_recibe", NpgsqlDbType.Text, (entity.unidad_administrativa_recibe).ToString()!),
                new ParameterPGsql("p_motivo_canalizacion", NpgsqlDbType.Varchar, entity.motivo_canalizacion!),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CanalizarAsuntoCreate,
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

        #region Crear Reasignar

        public async Task<ResultTransaction> CreateReasignarJuicioAsuntoAsync(int[] ids, RequestCreateReasignarJuicioAdministrador request)
        {


            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_ids", NpgsqlDbType.Array | NpgsqlDbType.Integer, ids),
                new ParameterPGsql("p_motivo_reasignacion", NpgsqlDbType.Varchar, (request.motivo_reasignacion).ToString()!),
                //new ParameterPGsql("p_id_abogado", NpgsqlDbType.Varchar, request.rfc_abogado!),
                new ParameterPGsql("p_id_abogado", NpgsqlDbType.Varchar, (request.id_abogado).ToString()!),
                new ParameterPGsql("p_numero_empleado", NpgsqlDbType.Varchar, (request.id_abogado).ToString()!),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateReasignarAbogadoAdministrador,
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

    #region Update registro

        public async Task<ResultTransaction> UpdateAmparoIndirectoAdministradorAsync(AmparoIndirecto entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id",NpgsqlDbType.Integer,entity.id!),
                new ParameterPGsql("p_fecha_recepcion_demanda", NpgsqlDbType.Date, entity.fecha_recepcion_demanda!),
                new ParameterPGsql("p_fecha_vencimiento_demanda", NpgsqlDbType.Date, null),
                new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Varchar, entity.numero_expediente!),
                new ParameterPGsql("p_juicio_amparo",NpgsqlDbType.Varchar,entity.numero_asunto!), //se modifico
                new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, entity.id_juzgado!),
                new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, entity.rfc_quejoso!),
                new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Varchar, entity.nombre_quejoso!),
                new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, entity.id_materia!),
                new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, entity.id_submateria!),
                new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, entity.id_tipo_acto!),
                new ParameterPGsql("p_despacho", NpgsqlDbType.Varchar, entity.despacho!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.UpdateRegistroAdministrador,
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


        #region Crear registro

        public async Task<ResultTransaction> CreateAsignarAsync(RequestAsignarAbogadoOficialPartes entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id",NpgsqlDbType.Integer,entity.id!),
                new ParameterPGsql("p_id_administracion",NpgsqlDbType.Integer,entity.id_administracion_central!),
                new ParameterPGsql("p_id_subadministracion",NpgsqlDbType.Integer,entity.id_subadministracion!),
                new ParameterPGsql("p_id_abogado",NpgsqlDbType.Varchar,entity.id_abogado!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoAsignarAbogadoOficialPartes,
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

        //Métodos
        #region GetById
        public async Task<AmparoIndirecto> GetByIdAsyncAI(int idNumeroAsunto)
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
                fecha_recepcion_demanda = response.Data.Tables[0].Rows[0].IsNull(3) ? new() : response.Data.Tables[0].Rows[0].Field<DateTime>(3),
                id_juzgado = response.Data.Tables[0].Rows[0].IsNull(4) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(4),
                contribuyente = response.Data.Tables[0].Rows[0].IsNull(5)
                ? false
                : response.Data.Tables[0].Rows[0].Field<bool>(5),
                rfc_quejoso = response.Data.Tables[0].Rows[0].IsNull(6) ? null! : response.Data.Tables[0].Rows[0].Field<string>(6)!,
                nombre_quejoso = response.Data.Tables[0].Rows[0].IsNull(7) ? null! : response.Data.Tables[0].Rows[0].Field<string>(7)!,
                id_materia = response.Data.Tables[0].Rows[0].IsNull(8) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(8),
                id_submateria = response.Data.Tables[0].Rows[0].IsNull(9) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(9),
                id_tipo_acto = response.Data.Tables[0].Rows[0].IsNull(10) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(10),
                despacho = response.Data.Tables[0].Rows[0].IsNull(11) ? null! : response.Data.Tables[0].Rows[0].Field<string>(11)!,
                //id_autoridad_responsable = response.Data.Tables[0].Rows[0].IsNull(12) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(12),
                id_administracion = response.Data.Tables[0].Rows[0].IsNull(12) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(12),
                id_subadministracion = response.Data.Tables[0].Rows[0].IsNull(13) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(13),
                id_estado_tarea = response.Data.Tables[0].Rows[0].IsNull(14) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(14),
                id_estado_procesal = response.Data.Tables[0].Rows[0].IsNull(15) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(15),
                id_estado_procesal_incidental = response.Data.Tables[0].Rows[0].IsNull(16) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(16),
                //recurso_queja_principal = response.Data.Tables[0].Rows[0].IsNull(16) ? null! : response.Data.Tables[0].Rows[0].Field<string>(16)!,
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
                fecha_turnado = response.Data.Tables[0].Rows[0].IsNull(23) ? new() : response.Data.Tables[0].Rows[0].Field<DateTime>(23),
                fecha_creacion = response.Data.Tables[0].Rows[0].IsNull(24) ? new() : response.Data.Tables[0].Rows[0].Field<DateTime>(24),
                fecha_modificacion = response.Data.Tables[0].Rows[0].IsNull(25) ? new() : response.Data.Tables[0].Rows[0].Field<DateTime>(25),


            };
        }

        #endregion
        #region GetByIdAutoridadesResposables
        public async Task<List<AutoridadesResponsables>> GetAutoridadesResponsablesIdAsync(int idNumeroAsunto)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, idNumeroAsunto), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AutoridadesResponsablesDisconect,
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
            List<AutoridadesResponsables> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                    }
                );
            }

            return resultList;
        }

        #endregion

        #region GetByIdAutoridadesResposables
        public async Task<List<AutoridadesResponsables>> GetAutoridadesResponsablesIdDisconnectedAsync(int idNumeroAsunto)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, idNumeroAsunto), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AutoridadesResponsablesRealesDisconect,
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
            List<AutoridadesResponsables> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_autoridad_responsable = item.IsNull(2) ? 0 : item.Field<int>(2),
                        id_consecutivo = item.IsNull(3) ? 0 : item.Field<int>(3),
                        id_recurrente = item.IsNull(4) ? 0 : item.Field<int>(4),
                        activo = item.IsNull(5) ? false : item.Field<bool>(5),
                    }
                );
            }

            return resultList;
        }

        #endregion
        #region Eliminar registro

        public async Task<ResultTransaction> UpdateEliminarRegistroAI(int idNumeroAsunto)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, idNumeroAsunto) };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoUpdateDeleteRegistro,
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

        #region Crear registro Control documental

        public async Task<ResultTransaction> CreateAsuntoControlDocumentalAsync(AmparoIndirecto entity)
        {
            ParameterPGsql[] parameters =
{
    new ParameterPGsql("p_juicio_amparo", NpgsqlDbType.Varchar, entity.numero_asunto!),
    new ParameterPGsql("p_fecha_recepcion_demanda", NpgsqlDbType.Date, entity.fecha_recepcion_demanda!),
    new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, entity.id_juzgado!),
    new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, entity.rfc_quejoso!),
    new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Varchar, entity.nombre_quejoso!),
    new ParameterPGsql("p_numero_empleado", NpgsqlDbType.Text, entity.numero_empleado!), 
    new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, entity.id_estado_tarea!),
    new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, entity.id_estado_procesal!),
    new ParameterPGsql("p_id_administracion_central", NpgsqlDbType.Integer, entity.id_administracion!)
};


            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateAsuntoControlDocumental,
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


        #region Crear registro

        public async Task<ResultTransaction> CreateAmparoIndirectoAsync(AmparoIndirecto entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_juicio_amparo",NpgsqlDbType.Varchar,entity.numero_asunto!),
                new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Varchar, entity.numero_expediente!),
                new ParameterPGsql("p_fecha_recepcion_demanda", NpgsqlDbType.Date, entity.fecha_recepcion_demanda!),
                //se agrega para fecha de vencimiento
                 new ParameterPGsql("p_fecha_vencimiento_demanda", NpgsqlDbType.Date,null),
                new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, entity.id_juzgado!),
                new ParameterPGsql("p_contribuyente", NpgsqlDbType.Boolean, entity.contribuyente!),
                new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, entity.rfc_quejoso!),
                new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Varchar, entity.nombre_quejoso!),
                new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, entity.id_materia!),
                new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, entity.id_submateria!),
                new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, entity.id_tipo_acto!),
                new ParameterPGsql("p_despacho", NpgsqlDbType.Varchar, entity.despacho!),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer, entity.id_autoridad_responsable!),
                new ParameterPGsql("p_numero_empleado", NpgsqlDbType.Varchar, entity.numero_empleado!),
                new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, entity.id_estado_tarea!),
                new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer, entity.id_estado_procesal!),
                new ParameterPGsql("p_id_administracion_central", NpgsqlDbType.Integer, entity.id_administracion!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoCreateRegistro,
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

        #region Turnar

        public async Task<ResultTransaction> UpdateTurnarRegistroAI(int id, int idAdministracionCentral, int idAdministracion, int idSubadministracion, string numeroEmpleado)

        {
            ParameterPGsql[] parameters = {
            new ParameterPGsql("p_id", NpgsqlDbType.Integer, id),
            new ParameterPGsql("p_id_administracion_central", NpgsqlDbType.Integer, idAdministracionCentral),
            new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer,idAdministracion),
            new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer,idSubadministracion),
            new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer,EnumEstadoTarea.TURNAR.GetHashCode()),
            new ParameterPGsql("p_id_estado_procesal", NpgsqlDbType.Integer,EnumEstadoProcesal.ACTIVO.GetHashCode()),
            new ParameterPGsql("p_numero_empleado", NpgsqlDbType.Text,numeroEmpleado)

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoUpdateTurnar,
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

        #region Update registro

        public async Task<ResultTransaction> UpdateAmparoIndirectoAsync(AmparoIndirecto entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id",NpgsqlDbType.Integer,entity.id!),
                new ParameterPGsql("p_juicio_amparo",NpgsqlDbType.Varchar,entity.numero_asunto!),
                new ParameterPGsql("p_numero_expediente", NpgsqlDbType.Varchar, entity.numero_expediente!),
                new ParameterPGsql("p_fecha_recepcion_demanda", NpgsqlDbType.Date, entity.fecha_recepcion_demanda!),
                new ParameterPGsql("p_id_juzgado", NpgsqlDbType.Integer, entity.id_juzgado!),
                //new ParameterPGsql("p_no_oficio_segunda_pieza", NpgsqlDbType.Text, entity.numero_oficio_segunda_pieza!),
                new ParameterPGsql("p_rfc_quejoso", NpgsqlDbType.Varchar, entity.rfc_quejoso!),
                new ParameterPGsql("p_nombre_quejoso", NpgsqlDbType.Varchar, entity.nombre_quejoso!),
                new ParameterPGsql("p_id_materia", NpgsqlDbType.Integer, entity.id_materia!),
                new ParameterPGsql("p_id_submateria", NpgsqlDbType.Integer, entity.id_submateria!),
                //new ParameterPGsql("p_id_estado_tarea", NpgsqlDbType.Integer, entity.id_estado_tarea!),
                new ParameterPGsql("p_id_tipo_acto", NpgsqlDbType.Integer, entity.id_tipo_acto!),
                new ParameterPGsql("p_id_administracion", NpgsqlDbType.Integer, entity.id_administracion!),
                new ParameterPGsql("p_id_subadministracion", NpgsqlDbType.Integer, entity.id_subadministracion!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoUpdateRegistro,
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

        #region Create acumular juicio

        public async Task<ResultTransaction> CreateAcumularJuicioAsync(int[] ids, AcumularDesacumular entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ("p_id_juicio_padre", NpgsqlDbType.Integer, entity.id_juicio_padre!),
                new ("p_ids_juicios_acumulados", NpgsqlDbType.Array | NpgsqlDbType.Integer, ids.ToArray()),
                new ("p_numero_oficio",NpgsqlDbType.Varchar,entity.numero_oficio!),
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
                EnumFunctions.AcumularJuicioCreate,
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

        #region GetSolicitudById
        public async Task<ResponseSolicitudTransparenciaLista> GetSolicitudById(int id)
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
                id_numero_asunto = response.Data.Tables[0].Rows[0].IsNull(1) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(1),//se modifico
                numero_solicitud = response.Data.Tables[0].Rows[0].IsNull(2) ? null! : response.Data.Tables[0].Rows[0].Field<string>(2)!,
                fecha_solicitud = response.Data.Tables[0].Rows[0].IsNull(3) ? null! : response.Data.Tables[0].Rows[0].Field<DateTime>(3).ToString("yyyy-MM-dd"),

            };
        }

        #endregion

        #region Create Solicitud de Transparencia
        public async Task<ResultTransaction> CreateSolicitudTransparenciaAsync(SolicitudTransparencia entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ("p_id", NpgsqlDbType.Integer, entity.id!),
                new ("p_numero_solicitud", NpgsqlDbType.Varchar, entity.numero_solicitud!),
                new ("p_fecha_solicitud", NpgsqlDbType.Date, entity.fecha_solicitud!),
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
                EnumFunctions.SolicitudTransparenciaCreate,
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

        #region Delete Solicitud de Transparencia

        public async Task<ResultTransaction> EliminarSolicitudTransparencia(int id)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_id", NpgsqlDbType.Integer, id) };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.SolicitudTransparenciaDelete,
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

        #region Get Cabeza de Serie

        public async Task<Acumular> GetCabezaSerieId(int idNumeroAsunto)
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
                administracion = response.Data.Tables[0].Rows[0].IsNull(1) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(1),
                numero_asunto = response.Data.Tables[0].Rows[0].IsNull(2) ? null! : response.Data.Tables[0].Rows[0].Field<string>(2)!,
                numero_expediente = response.Data.Tables[0].Rows[0].IsNull(3) ? null! : response.Data.Tables[0].Rows[0].Field<string>(3)!,
                rfc_quejoso = response.Data.Tables[0].Rows[0].IsNull(4) ? null! : response.Data.Tables[0].Rows[0].Field<string>(4)!,
                nombre_quejoso = response.Data.Tables[0].Rows[0].IsNull(5) ? null! : response.Data.Tables[0].Rows[0].Field<string>(5)!,
                juzgado = response.Data.Tables[0].Rows[0].IsNull(6) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(6),
            };
        }

        #endregion

        #region CREAR DECLINACIÓN DE COMPETENCIA
        public async Task<ResultTransaction> CreateDeclinarCompetenciaAsync(DeclinarCompetencia entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ("p_id_juicio_amparo",NpgsqlDbType.Integer,entity.id_numero_asunto!),
                new ("p_numero_oficio", NpgsqlDbType.Varchar, entity.numero_oficio!),
                new ("p_fecha_recepcion", NpgsqlDbType.Date, entity.fecha_recepcion_declinacion!),
                new ("p_id_juzgado_original", NpgsqlDbType.Integer, entity.id_juzgado_origen!),
                new ("p_id_juzgado_destino", NpgsqlDbType.Integer, entity.id_juzgado_destino!),
                new ("p_id_juicio_amparo_original", NpgsqlDbType.Text, entity.numero_asunto_origen!),
                new ("p_id_juicio_amparo_destino", NpgsqlDbType.Text, entity.numero_asunto_destino!),
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
                EnumFunctions.DeclinarCompetenciaCreateRegistro,
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

        #region Crear Canalizar Asunto

        public async Task<ResultTransaction> CreateCanalizarAsuntoAsync(CanalizarAsunto entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {

            ParameterPGsql[] parameters =
            {
                new ("p_numero_oficio_canalizacion", NpgsqlDbType.Varchar, entity.numero_oficio_canalizacion!),
                new ("p_fecha_canalizacion", NpgsqlDbType.Date, entity.fecha_canalizacion!),
                new ("p_unidad_administrativa_canaliza", NpgsqlDbType.Text, (entity.unidad_administrativa_canaliza).ToString()!),
                new ("p_unidad_administrativa_recibe", NpgsqlDbType.Text, (entity.unidad_administrativa_recibe).ToString()!),
                new ("p_motivo_canalizacion", NpgsqlDbType.Varchar, entity.motivo_canalizacion!),
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
                EnumFunctions.CanalizarAsuntoCreate,
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

        #region GetByIds
        public async Task<List<AmparoIndirecto>> GetByIdsJuicio(int[] idsNumeroAsunto)
        {
            ParameterPGsql[] parameters = { new ParameterPGsql("p_ids", NpgsqlDbType.Array | NpgsqlDbType.Integer, idsNumeroAsunto), };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetByIds,
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

            List<AmparoIndirecto> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                   new()
                   {
                       id = item.IsNull(0) ? 0 : item.Field<int>(0),
                       numero_asunto = item.IsNull(1) ? null! : item.Field<string>(1)!,
                       numero_expediente = item.IsNull(2) ? null! : item.Field<string>(2)!,
                       fecha_recepcion_demanda = item.IsNull(3) ? new() : item.Field<DateTime>(3),
                       id_juzgado = item.IsNull(4) ? 0 : item.Field<int>(4),
                       contribuyente = item.IsNull(5)
                        ? false
                        : item.Field<bool>(5),
                       rfc_quejoso = item.IsNull(6) ? null! : item.Field<string>(6)!,
                       nombre_quejoso = item.IsNull(7) ? null! : item.Field<string>(7)!,
                       id_materia = item.IsNull(8) ? 0 : item.Field<int>(8),
                       id_submateria = item.IsNull(9) ? 0 : item.Field<int>(9),
                       id_tipo_acto = item.IsNull(10) ? 0 : item.Field<int>(10),
                       despacho = item.IsNull(11) ? null! : item.Field<string>(11)!,
                       //id_autoridad_responsable = item.IsNull(12) ? 0 : item.Field<int>(12),
                       id_administracion = item.IsNull(12) ? 0 : item.Field<int>(12),
                       id_subadministracion = item.IsNull(13) ? 0 : item.Field<int>(13),
                       id_estado_tarea = item.IsNull(14) ? 0 : item.Field<int>(14),
                       id_estado_procesal = item.IsNull(15) ? 0 : item.Field<int>(15),
                       //recurso_queja_principal = response.Data.Tables[0].Rows[0].IsNull(16) ? null! : response.Data.Tables[0].Rows[0].Field<string>(16)!,
                       recurso_queja_principal = item.IsNull(16)
                       ? false
                       : item.Field<bool>(16),
                       recurso_revision_principal = item.IsNull(17)
                       ? false
                       : item.Field<bool>(17),
                       activo = item.IsNull(18)
                       ? false
                       : item.Field<bool>(18),
                       numero_empleado = item.IsNull(19) ? null! : item.Field<string>(19)!,
                       fecha_turnado = item.IsNull(20) ? new() : item.Field<DateTime>(20),
                       fecha_creacion = item.IsNull(21) ? new() : item.Field<DateTime>(21),
                       fecha_modificacion = item.IsNull(22) ? new() : item.Field<DateTime>(22),
                   });

            }
            return resultList;
        }
        #endregion


        #region ABOGADO

        #region Informe Justificado constitucional

        public async Task<ResultTransaction> UpdateInformeJustificadoConstitucionalAsync(int id, string usuario)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Varchar, usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.UpdateInformeJustificado,
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

        #region Recurso queja constitucional validacion

        public async Task<ResultTransaction> UpdateRecursoQuejaConstitucionalAsync(int id, bool recursoQuejaPrincipal)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
                new ParameterPGsql("p_recurso_queja_principal", NpgsqlDbType.Boolean, recursoQuejaPrincipal!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.UpdateRecursoQuejaConstitucional,
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

        #region Recurso queja constitucional validacion

        public async Task<ResultTransaction> UpdateRecursoRevisionConstitucionalAsync(int id, bool recursoRevisionPrincipal)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
                new ParameterPGsql("p_recurso_revision_principal", NpgsqlDbType.Boolean, recursoRevisionPrincipal!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.UpdateRecursoRevisionConstitucional,
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

        #region Cumplimiento fallo protector constitucional

        public async Task<ResultTransaction> UpdateCumplimientoFalloProtectorAsync(int id)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.UpdateCumplimientoFalloProtector,
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

        #region Cumplimiento fallo protector constitucional

        public async Task<ResultTransaction> CreateCumplimientoFalloProtectorAsync(CumplimientoFalloProtectorConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {

            ParameterPGsql[] parameters =
            {
                new ("p_id", NpgsqlDbType.Integer, entity.id_numero_asunto!),
                new ("p_id_autoridad_responsable", NpgsqlDbType.Integer, entity.id_autoridad_responsable!),
                //new ("p_cumplimiento_fallo", NpgsqlDbType.Boolean, entity.cumplimiento_fallo!),
                new ("p_fecha_notificacion_requerimiento", NpgsqlDbType.Date, entity.fecha_notificacion_requerimiento!),
                new ("p_plazo_fallo", NpgsqlDbType.Integer, entity.plazo_fallo!),
                new ("p_fecha_vencimiento_requerimiento", NpgsqlDbType.Date, entity.fecha_vencimiento_requerimiento!),
                new ("p_fecha_presentacion_fallo", NpgsqlDbType.Date, entity.fecha_presentacion_fallo!),
                new ("p_numero_oficio_atencion", NpgsqlDbType.Text, entity.numero_oficio_atencion),
                new ("p_fecha_notificacion_acuerdo_fallo", NpgsqlDbType.Date, entity.fecha_notificacion_acuerdo_fallo!),
                new ("p_fecha_oficio_comunicacion_fallo", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion_fallo!),
                new ("p_numero_oficio_comunicacion_autoridad", NpgsqlDbType.Text, entity.numero_oficio_comunicacion_autoridad),
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
                EnumFunctions.CreateCumplimientoFalloProtector,
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

        #region Sentencia Constitucional

        public async Task<ResultTransaction> UpdateSentenciaConstitucionalAsync(int id, string usuario)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Varchar, usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.UpdateSentenciaConstitucional,
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

        #region Incidente exceso

        public async Task<ResultTransaction> UpdateIncidenteExcesoAsync(int id, string usuario)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Varchar, usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.UpdateIncidenteExceso,
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

        #region Sentencia Constitucional

        public async Task<ResultTransaction> CreateSentenciaConstitucionalAsync(SentenciaConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {

            ParameterPGsql[] parameters =
            {
                new ("p_id_juicio_amparo", NpgsqlDbType.Integer, entity.id!),
                new ("p_id_autoridad_responsable", NpgsqlDbType.Integer, entity.id_autoridad_responsable!),
                new ("p_fecha_notificacion_sentencia", NpgsqlDbType.Date, entity.fecha_notificacion_sentencia!),
                new ("p_id_sentido_sentencia", NpgsqlDbType.Integer, entity.id_sentido_sentencia!),
                new ("p_id_tipo_sentido_sentencia", NpgsqlDbType.Integer, entity.id_tipo_sentido_sentencia!),
                new ("p_id_dictamen_no_revision", NpgsqlDbType.Integer, entity.id_dictamen_no_revision!),
                new ("p_id_sentido_general_asunto", NpgsqlDbType.Integer, entity.id_sentido_general_asunto!),
                new ("p_id_tipo_sentido_general", NpgsqlDbType.Integer, entity.id_tipo_sentido_general!),
                new ("p_oficio_comunicacion_autoridad", NpgsqlDbType.Text, entity.oficio_comunicacion_autoridad),
                new ("p_fecha_oficio_comunicacion_sentencia", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion_sentencia!),
                new ("p_fecha_presentacion_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_presentacion_oficio_comunicacion!),
                new ("p_fecha_recepcion_auto_sentencia_ejecutoria", NpgsqlDbType.Date, entity.fecha_recepcion_auto_sentencia_ejecutoria!),
                new ("p_comunicado_acuerdo_firmeza", NpgsqlDbType.Text, entity.comunicado_acuerdo_firmeza),
                new ("p_fecha_comunicacion_acuerdo_firmeza", NpgsqlDbType.Date, entity.fecha_comunicacion_acuerdo_firmeza!),
                new ("p_fecha_conclusion_expediente", NpgsqlDbType.Date, entity.fecha_conclusion_expediente!),
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
                EnumFunctions.CreateSentenciaConstitucional,
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

        #region Sentencia Incidental

        public async Task<ResultTransaction> CreateSentenciaIncidentalAsync(SentenciaIncidental entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, entity.id_numero_asunto!),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer, entity.id_autoridad_responsable!),
                new ParameterPGsql("p_fecha_notificacion_sentencia", NpgsqlDbType.Date, entity.fecha_notificacion_sentencia!),
                new ParameterPGsql("p_id_sentido_suspension_definitiva", NpgsqlDbType.Integer, entity.id_sentido_suspencion_definitiva!),
                new ParameterPGsql("p_id_otorgamiento_garantia", NpgsqlDbType.Integer, entity.id_otorgamiento_garatia!),
                new ParameterPGsql("p_id_sentido_general_asunto", NpgsqlDbType.Integer, entity.id_sentido_general_asunto!),
                new ParameterPGsql("p_id_tipo_sentido_general_asunto", NpgsqlDbType.Integer, entity.id_tipo_sentido_general_asunto!),
                new ParameterPGsql("p_oficio_comunicacion_area_correspondiente", NpgsqlDbType.Text, entity.oficio_comunicacion_area_correspondiente),
                new ParameterPGsql("p_fecha_comunicacion_suspension", NpgsqlDbType.Date, entity.fecha_comunicacion_suspencion!),
                new ParameterPGsql("p_id_dictamen_no_revision_incidental", NpgsqlDbType.Integer, entity.id_dictamen_no_revision_incidental!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.SentenciaIncidentalCreate,
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

        #region Recurso queja Incidental (Autoridades resoonsables)

        public async Task<ResultTransaction> CreateRecursoQuejaIncidentalAsync(RecursoQuejaIncidental entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, entity.id!),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, entity.id_numero_asunto!),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer, entity.id_autoridad_responsable!),
                new ParameterPGsql("p_recurso_queja_incidental", NpgsqlDbType.Boolean, entity.recurso_queja!),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer, entity.id_recurrente!),
                new ParameterPGsql("p_fecha_recepcion_apertura", NpgsqlDbType.Date, entity.fecha_recepcion_apertura!),
                new ParameterPGsql("p_fecha_vencimiento_recurso_queja", NpgsqlDbType.Date, entity.fecha_vencimiento_recurso_queja!),
                new ParameterPGsql("p_oficio_queja", NpgsqlDbType.Text, entity.oficio_queja!),
                new ParameterPGsql("p_fecha_presentacion_recurso_queja", NpgsqlDbType.Date, entity.fecha_presentacion_recurso_queja!),
                new ParameterPGsql("p_fecha_admision_queja", NpgsqlDbType.Date, entity.fecha_admision!),
                new ParameterPGsql("p_id_organo_radicacion", NpgsqlDbType.Integer, entity.organo_radicacion!),
                new ParameterPGsql("p_toca_queja_incidental", NpgsqlDbType.Text, entity.toca),
                new ParameterPGsql("p_fecha_notificacion_ejecutoria", NpgsqlDbType.Date, entity.fecha_notificacion_ejecutoria!),
                new ParameterPGsql("p_id_sentido_resolucion_ejecutoria", NpgsqlDbType.Integer, entity.sentido_resolucion!),
                new ParameterPGsql("p_oficio_comunicacion_area_correspondiente", NpgsqlDbType.Text, entity.oficio_comunicacion_area_correspondiente!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.RecursoQuejaIncidentalAutoriadesResponsablesCreate,
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

        public async Task<ResultTransaction> CreateRecursoQuejaIncidentalQuejosoAsync(RecursoQuejaIncidental entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, entity.id!),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, entity.id_numero_asunto!),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer, entity.id_autoridad_responsable!),
                new ParameterPGsql("p_recurso_queja_incidental", NpgsqlDbType.Boolean, entity.recurso_queja!),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer, entity.id_recurrente!),
                new ParameterPGsql("p_fecha_admision_queja", NpgsqlDbType.Date, entity.fecha_admision!),
                new ParameterPGsql("p_id_organo_radicacion", NpgsqlDbType.Integer, entity.organo_radicacion!),
                new ParameterPGsql("p_toca_queja_incidental", NpgsqlDbType.Text, entity.toca),
                new ParameterPGsql("p_fecha_notificacion_ejecutoria", NpgsqlDbType.Date, entity.fecha_notificacion_ejecutoria!),
                new ParameterPGsql("p_id_sentido_resolucion_ejecutoria", NpgsqlDbType.Integer, entity.sentido_resolucion!),
                new ParameterPGsql("p_oficio_comunicacion_area_correspondiente", NpgsqlDbType.Text, entity.oficio_comunicacion_area_correspondiente!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.RecursoQuejaIncidentalQuejosoCreate,
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

        public async Task<ResultTransaction> CreateRecursoQuejaIncidentalOtrasAutoridadesAsync(RecursoQuejaIncidental entity)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, entity.id_numero_asunto!),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, entity.id!),
                new ParameterPGsql("p_id_autoridad_responsable", NpgsqlDbType.Integer, entity.id_autoridad_responsable!),
                new ParameterPGsql("p_recurso_queja_incidental", NpgsqlDbType.Boolean, entity.recurso_queja!),
                new ParameterPGsql("p_id_recurrente", NpgsqlDbType.Integer, entity.id_recurrente!),
                new ParameterPGsql("p_fecha_admision_queja", NpgsqlDbType.Date, entity.fecha_admision!),
                new ParameterPGsql("p_id_organo_radicacion", NpgsqlDbType.Integer, entity.organo_radicacion!),
                new ParameterPGsql("p_toca_queja_incidental", NpgsqlDbType.Text, entity.toca),
                new ParameterPGsql("p_fecha_notificacion_ejecutoria", NpgsqlDbType.Date, entity.fecha_notificacion_ejecutoria!),
                new ParameterPGsql("p_id_sentido_resolucion_ejecutoria", NpgsqlDbType.Integer, entity.sentido_resolucion!),
                new ParameterPGsql("p_oficio_comunicacion_area_correspondiente", NpgsqlDbType.Text, entity.oficio_comunicacion_area_correspondiente!),
                new ParameterPGsql("p_fecha_oficio_comunicacion", NpgsqlDbType.Date, entity.fecha_oficio_comunicacion!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, entity.usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.RecursoQuejaIncidentalOtrasAutoridadesCreate,
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

        #region Informe Previo Incidental

        public async Task<ResultTransaction> CreateInformePrevioIncidentalAsync(InformePrevioIncidental entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ("p_id", NpgsqlDbType.Integer, entity.id!),
                new ("p_id_autoridad_responsable", NpgsqlDbType.Integer, entity.id_autoridad_responsable!),
                new ("p_fecha_recepcion_apertura", NpgsqlDbType.Date, entity.fecha_apertura_incidente!),
                new ("p_fecha_vencimiento", NpgsqlDbType.Date, entity.fecha_vencimiento!),
                new ("p_numero_oficio_informe_previo", NpgsqlDbType.Text, entity.numero_oficio_informe_previo),
                new ("p_fecha_presentacion_informe_previo", NpgsqlDbType.Date, entity.fecha_presentacion_informe_previo!),
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
                EnumFunctions.InformePrevioIncidentalCreate,
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

        #region Informe Previo Incidental

        public async Task<ResultTransaction> UpdateInformePrevioIncidentalAsync(int id, string usuario)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.InformePrevioIncidentalUpdate,
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

        #region Sentencia Incidental

        public async Task<ResultTransaction> UpdateSentenciaIncidentalAsync(int id, string usuario)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.InformeSentenciaIncidentalUpdate,
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

        #region Recurso queja Incidental

        public async Task<ResultTransaction> UpdateRecursoQuejaIncidentalAsync(int id, string usuario)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, usuario!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.RecursoQuejaIncidentalUpdate,
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

        #region Recurso revision Incidental

        public async Task<ResultTransaction> UpdateRecursoRevisionIncidentalAsync(int id, bool recursoRevisionIncidental /*string usuario*/)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, id!),
                new ParameterPGsql("p_recurso_revision_incidental", NpgsqlDbType.Boolean, recursoRevisionIncidental!),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.RecursoRevisionIncidentalUpdate,
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

        #region Create Autoridades Responsables

        public async Task<ResultTransaction> CreateAutoridadesResponsablesAsync(RequestCreateAutoridadResponsableAbogado request)
        {
            ParameterPGsql[] parameters =
           {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, request.id),
                new ParameterPGsql("p_ids_autoridades_responsables", NpgsqlDbType.Array | NpgsqlDbType.Integer, request.ids_autoridades_responsables),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateAutoridadesResponsables,
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

        #region Create Información adicional

        public async Task<ResultTransaction> CreateInformacionAdicionalAsync(InformacionAdicional request)
        {
            ParameterPGsql[] parameters =
           {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, request.id),
                new ParameterPGsql("p_fecha_resolucion_oficio", NpgsqlDbType.Date, request.fecha_resolucion_oficio),
                new ParameterPGsql("p_cuantia", NpgsqlDbType.Numeric, request.cuantia),
                new ParameterPGsql("p_oficio_resolucion_reclamado", NpgsqlDbType.Text, request.oficio_resolucion_reclamado),
                new ParameterPGsql("p_id_autoridad_emisora_resolucion_oficio", NpgsqlDbType.Integer, request.id_autoridad_emisora_resolucion_oficio),
                new ParameterPGsql("p_concepto_violacion", NpgsqlDbType.Text, request.concepto_violacion),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Text, request.usuario),

            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.CreateInformacionAdicional,
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
    }
}
