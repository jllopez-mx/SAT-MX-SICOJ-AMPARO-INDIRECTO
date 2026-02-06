using System.Threading.Tasks;
using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.IDAO.IServiceDAO
{
    public interface IAmparoIndirectoOficialPartesService
    {
        Task<ResultOperation> UpdateFechaVencimiento(string fechaVencimiento, int idAsunto, int idModulo, int idSeccion,  int idSeccionRenglon);//se agregó
        Task<ResultOperation> GetAllFechaVencimientoSeccion(string fechaInicio, string fechaFinal, List<int> idSecciones);//se agregó
        
        Task<ResultOperation> GetAmparoIndirectoDisconnectedByFilters(int Fetch, int Page, string OrderByColumn, bool OrderDesc, DateTime? fechaRecepcionInicial, DateTime? fechaRecepcionFinal, DateTime? fechaVencimientoDemanda, string numeroExpediete, string numeroAsunto, int? idJuzgado, string nombreQuejoso, int? idMateria, int? idSubmateria, int? idTipoActo, string despacho, int? idAdministracion, int? idSubadministracion, string rfcQuejoso, bool? transperencia, int? idEstadoTarea, int? idEstadoProcesal);
        Task<ResultOperation> GetAmparoIndirectoDisconnectedGetAll(int Fetch, int Page, string orderByColumn, bool orderDesc);
        Task<AmparoIndirecto> GetAmparoIndirectoByIdDisconnected(int id);
        Task<ResultOperation> GetExpedienteAntesAcumularByFilters(int Fetch, int Page, string OrderByColumn, bool OrderDesc, string numeroExpediete, string numeroAsunto, string rfcQuejoso, string nombreQuejoso, int? idJuzgado, int? idTipoActo, int? idEstadoTarea);
        Task<Acumular> GetCabezaSerieById(int id);
        Task<ResultOperation> GetCabezaSerieFrontById(int id);
        Task<ResultOperation<bool>> UpdateDeleteAmparoIndirecto(int idNumeroAsunto);
        Task<ResultOperation> CreateAsuntoControlDocumental(AmparoIndirecto entity);
        Task<ResultOperation> CreateAmparoIndirecto(AmparoIndirecto entity);
        Task<ResultOperation> UpdateTurnarAmparoIndirecto(int id, int idAdministracionCentral, int idAdministracion, int idSubadministracion, string numeroEmpleado);
        Task<ResultOperation> UpdateAmparoIndirecto(AmparoIndirecto entity);
        Task<ResultOperation> CreateAcumularJuicio(AcumularDesacumular entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation> GetSolicitudTransparenciaGetAll(int Fetch, int Page, int? id_numero_asunto);
        Task<ResultOperation> GetSolicitudTransparenciaById(int id);
        Task<ResultOperation> CreateSolicitudTransparencia(SolicitudTransparencia entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation<bool>> DeleteSolicitudTransparencia(int id);
        Task<ResultOperation> GetByIdAmparo(int idNumeroAsunto);
        Task<ResultOperation> GetAsuntoControlDocumental(string NumeroAsunto);
        Task<ResultOperation> GetByIdTransparencia(int id);
        Task<ResultOperation> CreateDeclinarCompetencia(DeclinarCompetencia entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation> CreateCanalizarAsunto(CanalizarAsunto requestRemitirAsunto, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation> CreateAsignar(RequestAsignarAbogadoOficialPartes requestAsignarAsunto);
        Task<ResultOperation> GetAmparoIndirectoCanalizarAsuntoGetAll(int Fetch, int Page, string orderByColumn, bool orderDesc);
        Task<List<ArchivosAmparoIndirecto>> GetArchivobyIds(int[] ids);
        Task<ResponseDocumentoAmparoIndirecto> GetArchivoAmparoIndirectoById(int id);
        Task<ResponseDocumentoAmparoIndirecto> GetArchivoDiscconected(int id);
        Task<ResultOperation<bool>> DeleteArchivo(int[] ids, string usuarioModificacion);
        Task<ResultOperation> CreateDocumentoAmparoIndirecto(ArchivosAmparoIndirecto entity, DataFile dataFile);
        Task<ResultOperation> UpdateDocumentoAmparoIndirecto(ArchivosAmparoIndirecto entity, DataFile dataFile);
        Task<ResultOperation<bool>> CreateDocumentoList(List<ArchivosAmparoIndirecto> entityList, List<DataFile> dataFileList);
        Task<ResultOperation> GetAllArchivoAmparoIndirecto(int idNumeroAsunto);
        Task<ResultOperation> GetAllArchivoSeccion(int idNumeroAsunto, int idSeccion);
        Task<ResultOperation> GetArchivo(int id);
        Task<ResultOperation> GetDocumentosHistoricoAsync(int Fetch, int Page, string? OrderByColumn, bool OrderDesc, int id);
        Task<ResultOperation> GetDocumentosHistoricoSeccionAsync(int Fetch, int Page, string? OrderByColumn, bool OrderDesc, int id, int idSeccion);
        Task<ResultOperation> EnvioEmail(Email entity);

    }
}
