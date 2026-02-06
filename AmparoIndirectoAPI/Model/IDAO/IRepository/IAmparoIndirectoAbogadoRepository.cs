using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.IDAO.IRepository
{
    public interface IAmparoIndirectoAbogadoRepository
    {
        Task<List<RequestCuadernoConstitucional>> GetCuadernoConstitucionalAsync(
            int id);
        Task<List<RecursoRevisionPrincipalConstitucionalDisconnected>> GetRecursoRevisionPrincipalIdDisconnectedAsync(
            int id);

        Task<List<RequestCuadernoConstitucional>> GetCuadernoConstitucionalRecurrenteAsync(
            int id, int idEstadoProcesal);

        //Task<List<ResponseSuspensionProvisionalIncidental>> GetAllSuspensionProvisionalIncidentalAsync(
        //  int id);
        Task<ResponseSuspensionProvisionalIncidental> GetAllSuspensionProvisionalIncidentalAsync(int id);
        Task<List<ResponseInformePrevioIncidental>> GetAllInformePrevioIncidentalAsync(
            int id);
        Task<List<ResponseSentenciaIncidental>> GetAllSentenciaIncidentalAsync(
            int id);
        Task<List<RecursoQuejaIncidentalDisconected>> GetAllRecursoQuejaIncidentalDisconnectedAsync(int id);
        Task<List<RecursoRevisionIncidentalDisconnected>> GetAllRecursoRevisionIncidentalDisconnectedAsync(int id);
        Task<List<ResponseRecursoQuejaIncidental>> GetAllRecursoQuejaIncidentalAsync(int id);
        Task<List<ResponseRecursoQuejaRevisionIncidental>> GetAllRecursoRevisionIncidentalAsync(int id);
        Task<List<ResponseIncidenteExcesoIncidental>> GetAllIncidenteExcesoIncidentalAsync(
          int id);

        Task<List<ResponseAmparoIndirectoAbogadoBandeja>> GetAllAbogado(
            int pageSize,
            int page,
            string orderByColumn,
            bool orderDesc);

        Task<int?> GetAllAbogadoCount();

        Task<List<ResponseHistoricoAbogadoByFilters>> GetHistoricoAbogadoByFilters(
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
            // string juicioAmparo, //este se modifico 
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
            int? idEstadoProcesalIncidental);

        Task<int?> GetHistoricoAbogadoByFiltersCount(
            DateTime? fechaRecepcionInicial,
            DateTime? fechaRecepcionFinal,
            DateTime? fechaInicialVencimiento,
            DateTime? fechaFinalVencimiento,
            string numeroExpediete,
            string numeroAsunto,
            // string juicioAmparo, //este se modifico 
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
            int? idEstadoProcesalIncidental);

        Task<List<ResponseAutoridadesResponsables>> GetAutoridadesResponsablesAsync(
        int idNumeroAsunto);
        //Task<List<ResponseAutoridadesResponsables>> GetAutoridadesResponsablesAsync(
        //int idJuicioAmparo); //este se modifico
        Task<ResponseAmparoIndirectoByID> GetByIdAmparoAsync(
           int idNumeroAsunto);
        //Task<ResponseAmparoIndirectoByID> GetByIdAmparoAsync(
        // int idJuicioAmparo); //este se modifico
        Task<List<ResponseNotaLitigioById>> GetByIdNotaLitigioAsync(int id);

        Task<ResultTransaction> CreateSuspensionProvisionalAbogadoAsync(
        SuspensionProvisional entity);

        Task<ResultTransaction> CreateIncidenteExcesoIncidentalAsync(
        IncidenteExcesoIncidental entity);

        Task<ResultTransaction> CreateInformeJustificadoAsync(InformeJustificadoConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);

        Task<List<ResponseInformacionAdicional>> GetAllInformacionAdicionalAsync(int id);
        Task<ResponseRecursoIncondormidad> GetAllRecursoInconformidadAsync(int id);
        Task<List<ResponseRecursoReclamacion>> GetAllRecursoReclamacionAsync(int id);
        Task<List<ResponseInformeJustificadoConstitucional>> GetAllInformeJustificadoConstitucionalAsync(
            int id);
        Task<List<ResponseRecursoQuejaPrincipalConstitucional>> GetAllRecursoQuejaPrincipalConstitucionalAsync(
            int id);

        Task<List<ResponseRecursoRevisionPrincipal>> GetAllRecursoRevisionConstitucionalAsync(
            int id);
        Task<List<ResponseSentenciaConstitucional>> GetAllSentenciaConstitucionalAsync(int id);
        Task<List<IncidenteExcesoIncidentalDisconnected>> GetAllIncidentePorExcesoIncidentalAsync(int id);
        Task<List<ResponseCumplimientoFalloProtector>> GetAllCumplimientoFalloConstitucionalAsync(
            int id);

        Task<ResultTransaction> CreateNotaLitigioAsync(
        NotaLitigio entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);

        Task<ResultTransaction> UpdateEliminarNotaLitigio(int idNotaLitigio);

        Task<ResultTransaction> CreateRecursoQuejaConstitucionalResponsableAsync(
        RecursoQuejaPrincipalConstitucional entity);

        Task<ResultTransaction> CreateRecursoQuejaConstitucionalOtrosAsync(
        RecursoQuejaPrincipalConstitucional entity);

        Task<ResultTransaction> CreateRecursoQuejaConstitucionalQuejosoAsync(
        RecursoQuejaPrincipalConstitucional entity);

        #region RECURSO REVISION INCIDENTAL

        Task<ResultTransaction> CreateRecursoRevisionIncidentalResponsableAsync(
        RecursoRevisionIncidental entity);

        Task<ResultTransaction> CreateRecursoRevisionIncidentalOtrosAsync(
        RecursoRevisionIncidental entity);

        Task<ResultTransaction> CreateRecursoRevisionIncidentalQuejosoAsync(
        RecursoRevisionIncidental entity);

        #endregion

        Task<ResultTransaction> CreateRecursoRevisionConstitucionalResponsableAsync(
        RecursoRevisionPrincipalConstitucional entity);

        Task<ResultTransaction> CreateRecursoRevisionConstitucionalOtrosAsync(
        RecursoRevisionPrincipalConstitucional entity);

        Task<ResultTransaction> CreateRecursoRevisionConstitucionalQuejosoAsync(
        RecursoRevisionPrincipalConstitucional entity);

        Task<ResultTransaction> CreateRecursoInconformidadConstitucionalAsync(
       RecursoInconformidadConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);

        Task<ResultTransaction> CreateRecursoReclamacionConstitucionalAsync(
      RecursoReclamacionConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);


    }
}
