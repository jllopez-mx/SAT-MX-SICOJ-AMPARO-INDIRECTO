using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.IDAO.IRepository
{
    public interface IAmparoIndirectoOficialPartesRepository
    {
        Task<List<ResponseAmparoIndirectoBandeja>> GetAllAsyncAI(
            int pageSize,
            int page,
            string orderByColumn,
            bool orderDesc);

        Task<int?> GetAllCountAsyncAI();

        Task<List<ResponseAmparoIndirectoByFilters>> GetAllByFiltersAsyncAI(
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
      );


        Task<int?> GetAllByFiltersCountAsyncAI(
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
        );

        #region Mostrar Consulta antes de Acumular
        Task<List<ResponseAcumularByFilters>> GetExpedientesAntesAcumularByFilters(
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
       );


        Task<int?> GetExpedientesAntesAcumularByFiltersCount(
                    //int Fetch,
                    //int Page,
                    //string OrderByColumn,
                    //bool OrderDesc,
                    string numeroExpediente,
                    string numeroAsunto,
                    string rfcQuejoso,
                    string nombreQuejoso,
                    int? idJuzgado,
                    int? idTipoActo,
                    int? idEstadoTarea
       );
        #endregion

        #region  Get All Solicitud Transparencia
        Task<List<ResponseSolicitudTransparenciaLista>> GetAllSolicitudTransparencia(int pageSize, int page, int? id_numero_asunto);

        Task<int?> GetAllCountSolicitudTransparencia(int? id_numero_asunto);

        #endregion

        Task<ResponseAsuntoControlDocumental> GetAsuntoControlDocumentalAsync(
           string NumeroAsunto);

        Task<ResponseAmparoIndirectoByID> GetByIdAmparoAsync(
           int idNumeroAsunto);
        Task<List<ResponseAutoridadesResponsables>> GetAutoridadesResponsablesAsync(
           int idNumeroAsunto);

        Task<ResponseSolicitudTransparenciaLista> GetByIdTransparenciaAsync(
           int id);
        Task<ResponseAcumularByFilters> GetCabezaSerieFrontById(
           int id);

        #region Get Canalizar Asunto
        Task<List<ResponseAmparoIndirectoRemitir>> GetAllCanalizarAsyncAI(
            int pageSize,
            int page,
            string orderByColumn,
            bool orderDesc);

        Task<int?> GetAllCanalizarCountAsyncAI();

        #endregion

        Task<List<ResponseFechaVencimiento>> GetAllFechaVencimientoSeccionAsync(string fechaInicio, string fechaFinal, List<int> idSecciones);


    }
}
