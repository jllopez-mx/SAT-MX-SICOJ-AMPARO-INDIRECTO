using System.Data;
using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.ViewModels.Enums;
using NpgsqlTypes;
using Sicoj.Utils.Models;
using Sicoj.Utils.Postgres;

namespace AmparoIndirectoAPI.Model.DAO.Repository
{
    public class ArchivosAmparoIndirectoRepository : IArchivosAmparoIndirectoRepository
    {
        #region Variables
        private readonly ISqlTools _database;
        #endregion

        #region Constructor
        public ArchivosAmparoIndirectoRepository(ISqlTools database)
        {
            _database =
                database ?? throw new ArgumentNullException(nameof(database));
        }
        #endregion

        #region Get All Documentos

        public async Task<List<ResponseDocumentoAmparoIndirecto>> GetAllArchivoAmparoIndirectoAsync(int idNumeroAsunto)
        {
            //string idDecodificado = HttpUtility.UrlDecode(id);
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id_juicio", NpgsqlDbType.Integer, idNumeroAsunto),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetAllDocumento,
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

            List<ResponseDocumentoAmparoIndirecto> resultList = new();

            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_seccion = item.IsNull(2) ? 0 : item.Field<int>(2),
                        id_tipo_documento = item.IsNull(3) ? 0 : item.Field<int>(3),
                        file_name = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        file_path = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        content_type = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        file_size = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        //id_unidad_administrativa = item.IsNull(8) ? 0 : item.Field<int>(8),
                        id_rol = item.IsNull(8) ? 0 : item.Field<int>(8),
                        permanente = item.IsNull(9) ? false : item.Field<bool>(9),
                        remplazable = item.IsNull(10) ? false : item.Field<bool>(10),
                        //fecha_creacion = item.IsNull(12) ? null! : item.Field<DateTime>(12).ToString("yyyy-MM-dd"),
                        //usuario = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        //fecha_modificacion = item.IsNull(14) ? null! : item.Field<DateTime>(14).ToString("yyyy-MM-dd"),
                        usuario = item.IsNull(11) ? null! : item.Field<string>(11)!,
                        activo = item.IsNull(12) ? false : item.Field<bool>(12),
                    }
                );
            }
            return resultList;
        }

        #endregion

        #region Get All Documentos Seccion

        public async Task<List<ResponseDocumentoAmparoIndirecto>> GetAllArchivoSeccionAsync(int idNumeroAsunto, int idSeccion)
        {
            //string idDecodificado = HttpUtility.UrlDecode(id);
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, idNumeroAsunto),
                new ParameterPGsql("p_id_seccion", NpgsqlDbType.Integer, idSeccion),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetAllDocumentoSeccion,
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

            List<ResponseDocumentoAmparoIndirecto> resultList = new();

            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_seccion = item.IsNull(2) ? 0 : item.Field<int>(2),
                        id_tipo_documento = item.IsNull(3) ? 0 : item.Field<int>(3),
                        file_name = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        file_path = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        content_type = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        file_size = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        id_unidad_administrativa = item.IsNull(8) ? 0 : item.Field<int>(8),
                        id_rol = item.IsNull(9) ? 0 : item.Field<int>(9),
                        permanente = item.IsNull(10) ? false : item.Field<bool>(10),
                        remplazable = item.IsNull(11) ? false : item.Field<bool>(11),
                        fecha_creacion = item.IsNull(12) ? null! : item.Field<DateTime>(12).ToString("yyyy-MM-dd"),
                        usuario = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        fecha_modificacion = item.IsNull(14) ? null! : item.Field<DateTime>(14).ToString("yyyy-MM-dd"),
                        usuario_modificacion = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        activo = item.IsNull(16) ? false : item.Field<bool>(16),
                    }
                );
            }
            return resultList;
        }

        #endregion

        #region Get Documento by id

        public async Task<ResponseDocumentoAmparoIndirecto> GetArchivoAsync(int id)
        {
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id_documento", NpgsqlDbType.Integer, id),
            };
            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetDocumento,
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
            return new()
            {
                id = response.Data.Tables[0].Rows[0].IsNull(0) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(0),
                id_numero_asunto = response.Data.Tables[0].Rows[0].IsNull(1) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(1),
                id_seccion = response.Data.Tables[0].Rows[0].IsNull(2) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(2),
                id_tipo_documento = response.Data.Tables[0].Rows[0].IsNull(3) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(3),
                file_name = response.Data.Tables[0].Rows[0].IsNull(4) ? null! : response.Data.Tables[0].Rows[0].Field<string>(4)!,
                file_path = response.Data.Tables[0].Rows[0].IsNull(5) ? null! : response.Data.Tables[0].Rows[0].Field<string>(5)!,
                content_type = response.Data.Tables[0].Rows[0].IsNull(6) ? null! : response.Data.Tables[0].Rows[0].Field<string>(6)!,
                file_size = response.Data.Tables[0].Rows[0].IsNull(7) ? null! : response.Data.Tables[0].Rows[0].Field<string>(7)!,
                id_rol = response.Data.Tables[0].Rows[0].IsNull(8) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(8),
                permanente = response.Data.Tables[0].Rows[0].IsNull(9) ? false : response.Data.Tables[0].Rows[0].Field<bool>(9),
                remplazable = response.Data.Tables[0].Rows[0].IsNull(10) ? false : response.Data.Tables[0].Rows[0].Field<bool>(10),
                usuario = response.Data.Tables[0].Rows[0].IsNull(11) ? null! : response.Data.Tables[0].Rows[0].Field<string?>(11)!,
                activo = response.Data.Tables[0].Rows[0].IsNull(12) ? false : response.Data.Tables[0].Rows[0].Field<bool>(12),
            };
        }
        #endregion

        public async Task<int?> GetDocumentosDisconnectedCount(int id /*List<int>? idSeccion,*/ /*int? idRenglonSeccion,*/ /*bool activo*/)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, id),
                //new ParameterPGsql("p_id_seccion", NpgsqlDbType.Array | NpgsqlDbType.Integer, (idSeccion is null || !idSeccion.Any()) ? DBNull.Value :  idSeccion.ToArray()),
                //new ParameterPGsql("p_id_renglon_seccion", NpgsqlDbType.Integer, idRenglonSeccion),
                //new ParameterPGsql("p_activo", NpgsqlDbType.Boolean, activo),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetHistoricoCountDocumentoId,
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

            return response.Data.Tables[0].Rows[0].IsNull(0)
                ? null!
                : response.Data.Tables[0].Rows[0].Field<int>(0);
        }

        public async Task<List<ResponseDocumentoAmparoIndirecto>> GetDocumentosHistoricoDisconnected(/*bool paginado,*/ int id, /*List<int>? idSeccion,*/ /*int? idRenglonSeccion,*/ /*bool activo,*/ int? Page_size = null, int? Page = null!, string? OrderByColumn = null!, bool? OrderDesc = null!)
        {
            ParameterPGsql[] parameters =
            {
                //new ParameterPGsql("p_paginado", NpgsqlDbType.Boolean, paginado),
                new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, Page_size),
                new ParameterPGsql("p_page", NpgsqlDbType.Integer, Page),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, id),
                new ParameterPGsql("order_column", NpgsqlDbType.Varchar, OrderByColumn),
                new ParameterPGsql("order_desc", NpgsqlDbType.Boolean, OrderDesc),
                //new ParameterPGsql("p_id_seccion", NpgsqlDbType.Array | NpgsqlDbType.Integer, (idSeccion is null || !idSeccion.Any()) ? DBNull.Value :  idSeccion.ToArray()),
                //new ParameterPGsql("p_id_renglon_seccion", NpgsqlDbType.Integer, idRenglonSeccion),
                //new ParameterPGsql("p_activo", NpgsqlDbType.Boolean, activo),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetHistoricoDocumentoId,
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

            List<ResponseDocumentoAmparoIndirecto> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_seccion = item.IsNull(2) ? 0 : item.Field<int>(2),
                        id_tipo_documento = item.IsNull(3) ? 0 : item.Field<int>(3),
                        file_name = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        file_path = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        content_type = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        file_size = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        id_unidad_administrativa = item.IsNull(8) ? 0 : item.Field<int>(8),
                        id_rol = item.IsNull(9) ? 0 : item.Field<int>(9),
                        permanente = item.IsNull(10) ? false : item.Field<bool>(10),
                        remplazable = item.IsNull(11) ? false : item.Field<bool>(11),
                        fecha_creacion = item.IsNull(12) ? null! : item.Field<DateTime>(12).ToString("yyyy-MM-dd"),
                        usuario = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        fecha_modificacion = item.IsNull(14) ? null! : item.Field<DateTime>(14).ToString("yyyy-MM-dd"),
                        usuario_modificacion = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        activo = item.IsNull(16) ? false : item.Field<bool>(16),
                    }
                );
            }

            return resultList;
        }

        #region Historico Documentos Seccion

        public async Task<int?> GetDocumentosSeccionDisconnectedCount(int id, int idSeccion /*List<int>? idSeccion,*/ /*int? idRenglonSeccion,*/ /*bool activo*/)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, id),
                new ParameterPGsql("p_id_seccion", NpgsqlDbType.Integer, idSeccion),
                //new ParameterPGsql("p_id_seccion", NpgsqlDbType.Array | NpgsqlDbType.Integer, (idSeccion is null || !idSeccion.Any()) ? DBNull.Value :  idSeccion.ToArray()),
                //new ParameterPGsql("p_id_renglon_seccion", NpgsqlDbType.Integer, idRenglonSeccion),
                //new ParameterPGsql("p_activo", NpgsqlDbType.Boolean, activo),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetHistoricoCountDocumentoIdSeccion,
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

            return response.Data.Tables[0].Rows[0].IsNull(0)
                ? null!
                : response.Data.Tables[0].Rows[0].Field<int>(0);
        }

        public async Task<List<ResponseDocumentoAmparoIndirecto>> GetDocumentosHistoricoSeccionDisconnected(/*bool paginado,*/ int id, int idSeccion, /*List<int>? idSeccion,*/ /*int? idRenglonSeccion,*/ /*bool activo,*/ int? Page_size = null, int? Page = null!, string? OrderByColumn = null!, bool? OrderDesc = null!)
        {
            ParameterPGsql[] parameters =
            {
                //new ParameterPGsql("p_paginado", NpgsqlDbType.Boolean, paginado),
                new ParameterPGsql("p_page_size", NpgsqlDbType.Integer, Page_size),
                new ParameterPGsql("p_page", NpgsqlDbType.Integer, Page),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, id),
                new ParameterPGsql("p_id_seccion", NpgsqlDbType.Integer, idSeccion),
                new ParameterPGsql("order_column", NpgsqlDbType.Varchar, OrderByColumn),
                new ParameterPGsql("order_desc", NpgsqlDbType.Boolean, OrderDesc),
                //new ParameterPGsql("p_id_seccion", NpgsqlDbType.Array | NpgsqlDbType.Integer, (idSeccion is null || !idSeccion.Any()) ? DBNull.Value :  idSeccion.ToArray()),
                //new ParameterPGsql("p_id_renglon_seccion", NpgsqlDbType.Integer, idRenglonSeccion),
                //new ParameterPGsql("p_activo", NpgsqlDbType.Boolean, activo),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetHistoricoDocumentoIdSeccion,
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

            List<ResponseDocumentoAmparoIndirecto> resultList = new();
            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_seccion = item.IsNull(2) ? 0 : item.Field<int>(2),
                        id_tipo_documento = item.IsNull(3) ? 0 : item.Field<int>(3),
                        file_name = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        file_path = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        content_type = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        file_size = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        id_unidad_administrativa = item.IsNull(8) ? 0 : item.Field<int>(8),
                        id_rol = item.IsNull(9) ? 0 : item.Field<int>(9),
                        permanente = item.IsNull(10) ? false : item.Field<bool>(10),
                        remplazable = item.IsNull(11) ? false : item.Field<bool>(11),
                        fecha_creacion = item.IsNull(12) ? null! : item.Field<DateTime>(12).ToString("yyyy-MM-dd"),
                        usuario = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        fecha_modificacion = item.IsNull(14) ? null! : item.Field<DateTime>(14).ToString("yyyy-MM-dd"),
                        usuario_modificacion = item.IsNull(13) ? null! : item.Field<string>(13)!,
                        activo = item.IsNull(16) ? false : item.Field<bool>(16),
                    }
                );
            }

            return resultList;
        }

        #endregion

        #region Get Documentos by ids

        public async Task<List<ArchivosAmparoIndirecto>> GetArchivosByIdsAsync(int[] id)
        {
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_ids_documentos", NpgsqlDbType.Array | NpgsqlDbType.Integer, id.ToArray()),
            };
            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetDocumentoByIds,
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

            List<ArchivosAmparoIndirecto> resultList = new();

            foreach (DataRow item in response.Data.Tables[0].Rows)
            {
                resultList.Add(
                    new()
                    {
                        id = item.IsNull(0) ? 0 : item.Field<int>(0),
                        id_numero_asunto = item.IsNull(1) ? 0 : item.Field<int>(1),
                        id_seccion = item.IsNull(2) ? 0 : item.Field<int>(2),
                        id_tipo_documento = item.IsNull(3) ? 0 : item.Field<int>(3),
                        file_name = item.IsNull(4) ? null! : item.Field<string>(4)!,
                        file_path = item.IsNull(5) ? null! : item.Field<string>(5)!,
                        content_type = item.IsNull(6) ? null! : item.Field<string>(6)!,
                        file_size = item.IsNull(7) ? null! : item.Field<string>(7)!,
                        id_unidad_administrativa = item.IsNull(8) ? 0 : item.Field<int>(8),
                        id_rol = item.IsNull(9) ? 0 : item.Field<int>(9),
                        permanente = item.IsNull(10) ? false : item.Field<bool>(10),
                        reemplazable = item.IsNull(11) ? false : item.Field<bool>(11),
                        activo = item.IsNull(12) ? false : item.Field<bool>(12),
                    }
                );
            }
            return resultList;
        }
        #endregion


        #region Eliminar documento

        public async Task<ResultTransaction> EliminarDocumentoAsyncAI(int[] idDocumento, string usuarioModificacion)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_ids", NpgsqlDbType.Array | NpgsqlDbType.Integer, idDocumento.ToArray()),
                new ParameterPGsql("p_usuario", NpgsqlDbType.Varchar, usuarioModificacion)
            };
            var response = await _database.ExecuteFunctionAsync(EnumFunctions.AmparoIndirectoDeleteDocumento, parameters);
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

        #region Add Documento
        public async Task<ResultTransaction> CreateDocumentoAmparoIndirectoAsync(ArchivosAmparoIndirecto entity, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, entity.id_numero_asunto!),
                new ParameterPGsql("p_id_seccion", NpgsqlDbType.Integer, entity.id_seccion!),
                new ParameterPGsql("p_id_tipo_documento", NpgsqlDbType.Integer, entity.id_tipo_documento!),
                new ParameterPGsql("p_file_name", NpgsqlDbType.Text, entity.file_name!),
                new ParameterPGsql("p_file_path", NpgsqlDbType.Text, entity.file_path!),
                new ParameterPGsql("p_content_type", NpgsqlDbType.Text, entity.content_type!),
                new ParameterPGsql("p_file_size", NpgsqlDbType.Text, entity.file_size!),
                new ParameterPGsql("p_id_unidad_administrativa", NpgsqlDbType.Integer, entity.id_unidad_administrativa!),
                new ParameterPGsql("p_id_rol", NpgsqlDbType.Integer, entity.id_rol!),
                new ParameterPGsql("p_permanente", NpgsqlDbType.Boolean, entity.permanente),
                new ParameterPGsql("p_reemplazable", NpgsqlDbType.Boolean, entity.reemplazable),
                new ParameterPGsql("p_usuario_creacion", NpgsqlDbType.Text, entity.usuario!),
            };
            var response = await _database.ExecuteFunctionFileAsync(EnumFunctions.AmparoIndirectoCreateDocumento, dataFile, parameters);
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

        #region Update Documento
        public async Task<ResultTransaction> UpdateDocumentoAmparoIndirectoAsync(ArchivosAmparoIndirecto entity, DataFile dataFile)
        {
            ParameterPGsql[] parameters =
            {
                new ParameterPGsql("p_id", NpgsqlDbType.Integer, entity.id!),
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, entity.id_numero_asunto!),
                new ParameterPGsql("p_id_tipo_documento", NpgsqlDbType.Integer, entity.id_tipo_documento!),
                new ParameterPGsql("p_id_seccion", NpgsqlDbType.Integer, entity.id_seccion!),
                new ParameterPGsql("p_id_unidad_administrativa", NpgsqlDbType.Integer, entity.id_unidad_administrativa!),
                new ParameterPGsql("p_file_name", NpgsqlDbType.Text, entity.file_name!),
                new ParameterPGsql("p_file_path", NpgsqlDbType.Text, entity.file_path!),
                new ParameterPGsql("p_content_type", NpgsqlDbType.Text, entity.content_type!),
                new ParameterPGsql("p_file_size", NpgsqlDbType.Text, entity.file_size!),
                new ParameterPGsql("p_usuario_creacion", NpgsqlDbType.Text, entity.usuario!),
                new ParameterPGsql("p_usuario_modificacion", NpgsqlDbType.Text, entity.usuario_modificacion!),
                new ParameterPGsql("p_permanente", NpgsqlDbType.Boolean, entity.permanente),
                new ParameterPGsql("p_id_rol", NpgsqlDbType.Integer, entity.id_rol!),

            };

            var response = await _database.ExecuteFunctionFileAsync(EnumFunctions.AmparoIndirectoUpdateDocumento, dataFile, parameters);
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

        public async Task<ResponseDocumentoAmparoIndirecto> GetArchivosByIdAsyncAI(int idNumeroAsunto)
        {
            //string idDecodificado = HttpUtility.UrlDecode(id);
            ParameterPGsql[] parameters =
                {
                new ParameterPGsql("p_id_juicio_amparo", NpgsqlDbType.Integer, idNumeroAsunto),
            };

            var response = await _database.ExecuteFunctionAsync(
                EnumFunctions.AmparoIndirectoGetAllDocumento,
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

            return new()
            {
                id = response.Data.Tables[0].Rows[0].IsNull(0) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(0),
                id_numero_asunto = response.Data.Tables[0].Rows[0].IsNull(1) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(1),
                id_seccion = response.Data.Tables[0].Rows[0].IsNull(1) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(1),
                id_tipo_documento = response.Data.Tables[0].Rows[0].IsNull(3) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(3),


                file_name = response.Data.Tables[0].Rows[0].IsNull(2) ? null! : response.Data.Tables[0].Rows[0].Field<string>(2)!,
                file_path = response.Data.Tables[0].Rows[0].IsNull(4) ? null! : response.Data.Tables[0].Rows[0].Field<string>(4)!,
                content_type = response.Data.Tables[0].Rows[0].IsNull(6) ? null! : response.Data.Tables[0].Rows[0].Field<string>(6)!,
                file_size = response.Data.Tables[0].Rows[0].IsNull(5) ? null! : response.Data.Tables[0].Rows[0].Field<string>(5)!,
                id_unidad_administrativa = response.Data.Tables[0].Rows[0].IsNull(3) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(3),
                id_rol = response.Data.Tables[0].Rows[0].IsNull(3) ? 0 : response.Data.Tables[0].Rows[0].Field<int>(3),
                permanente = response.Data.Tables[0].Rows[0].IsNull(7)
                ? false
                : response.Data.Tables[0].Rows[0].Field<bool>(7),

                remplazable = response.Data.Tables[0].Rows[0].IsNull(7)
                ? false
                : response.Data.Tables[0].Rows[0].Field<bool>(7),

                usuario = response.Data.Tables[0].Rows[0].IsNull(7) ? null! : response.Data.Tables[0].Rows[0].Field<string>(7)!,

                activo = response.Data.Tables[0].Rows[0].IsNull(8)
                ? false
                : response.Data.Tables[0].Rows[0].Field<bool>(8),

            };
        }

        public async Task<ResultTransaction> UpdateFechaVencimientoAsync(string fechaVencimiento, int idAsunto, int idModulo, int idSeccion,  int idSeccionRenglon)
        {//aqui hacer la imple a la BD
            ParameterPGsql[] parameters =
          {
                new ParameterPGsql("p_fecha_vencimiento", NpgsqlDbType.Date, Convert.ToDateTime(fechaVencimiento)),
                new ParameterPGsql("p_id_asunto", NpgsqlDbType.Integer, idAsunto!),
                new ParameterPGsql("p_id_modulo", NpgsqlDbType.Integer, idModulo!),
                new ParameterPGsql("p_id_seccion", NpgsqlDbType.Integer, idSeccion!),
                new ParameterPGsql("p_id_seccion_renglon", NpgsqlDbType.Integer, idSeccionRenglon!),
            };

            var response = await _database.ExecuteFunctionAsync(EnumFunctions.FechasVencimientoUpdate,parameters!);
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
    }
}

