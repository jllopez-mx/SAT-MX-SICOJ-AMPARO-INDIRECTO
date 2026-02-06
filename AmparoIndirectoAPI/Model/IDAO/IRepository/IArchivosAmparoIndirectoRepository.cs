using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.DTO.ContractsValidations;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.IDAO.IRepository
{
    public interface IArchivosAmparoIndirectoRepository
    {
        #region DeteleDocumentoAI
        Task<ResultTransaction> EliminarDocumentoAsyncAI(
           int[] idDocumento, string usuarioModificacion
       );
        #endregion

        #region CreateDocumentoAI
        Task<ResultTransaction> UpdateFechaVencimientoAsync(string fechaVencimiento, int idAsunto, int idModulo, int idSeccion,  int idSeccionRenglon);
        
        Task<ResultTransaction> CreateDocumentoAmparoIndirectoAsync(ArchivosAmparoIndirecto entity, DataFile dataFile);
        Task<ResultTransaction> UpdateDocumentoAmparoIndirectoAsync(ArchivosAmparoIndirecto entity, DataFile dataFile);
        #endregion

        Task<List<ResponseDocumentoAmparoIndirecto>> GetAllArchivoAmparoIndirectoAsync(int idNumeroAsunto);
        Task<List<ResponseDocumentoAmparoIndirecto>> GetAllArchivoSeccionAsync(int idNumeroAsunto, int idSeccion);

        Task<ResponseDocumentoAmparoIndirecto> GetArchivoAsync(
          int idNumeroAsunto);

        Task<int?> GetDocumentosDisconnectedCount(int id /*List<int>? idSeccion,*/ /*int? idRenglonSeccion, bool activo*/);
        Task<List<ResponseDocumentoAmparoIndirecto>> GetDocumentosHistoricoDisconnected(/*bool paginado,*/ int id, /*List<int>? idSeccion,*/ /*int? idRenglonSeccion, bool activo,*/ int? page_size = null, int? Page = null!, string? OrderByColumn = null!, bool? OrderDesc = null!);
        Task<int?> GetDocumentosSeccionDisconnectedCount(int id, int idSeccion /*List<int>? idSeccion,*/ /*int? idRenglonSeccion, bool activo*/);
        Task<List<ResponseDocumentoAmparoIndirecto>> GetDocumentosHistoricoSeccionDisconnected(/*bool paginado,*/ int id, int idSeccion, /*List<int>? idSeccion,*/ /*int? idRenglonSeccion, bool activo,*/ int? page_size = null, int? Page = null!, string? OrderByColumn = null!, bool? OrderDesc = null!);


        Task<List<ArchivosAmparoIndirecto>> GetArchivosByIdsAsync(int[] id);
        Task<ResponseDocumentoAmparoIndirecto> GetArchivosByIdAsyncAI(
           int id
       );


    }
}
