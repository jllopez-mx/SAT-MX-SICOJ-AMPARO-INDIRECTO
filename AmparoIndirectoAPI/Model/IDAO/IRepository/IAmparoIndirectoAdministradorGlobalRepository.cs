using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;
using System.Threading.Tasks;

namespace AmparoIndirectoAPI.Model.IDAO.IRepository
{
    public interface IAmparoIndirectoAdministradorGlobalRepository
    {
        
        //Task<ResponseSuspensionProvisionalIncidental> GetAllSuspensionProvisionalIncidentalAsync(int id);
       

        Task<List<ResponseAmparoIndirectoAdministradorGlobalBandeja>> GetAllAdministradorGlobal(
            int pageSize,
            int page,
            string orderByColumn,
            bool orderDesc);

        Task<int?> GetAllAdministradorGlobalCount();

        //Task<List<ResponseHistoricoAbogadoByFilters>> GetHistoricoAbogadoByFilters(
        //    int Fetch,
        //    int Page,
        //    string OrderByColumn,
        //    bool OrderDesc,
        //    DateTime? fechaRecepcionInicial,
        //    DateTime? fechaRecepcionFinal,
        //    DateTime? fechaInicialVencimiento,
        //    DateTime? fechaFinalVencimiento,
        //    string numeroExpediete,
        //    string juicioAmparo,
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
        //    int? idEstadoProcesalIncidental);

        //Task<int?> GetHistoricoAbogadoByFiltersCount(
        //    DateTime? fechaRecepcionInicial,
        //    DateTime? fechaRecepcionFinal,
        //    DateTime? fechaInicialVencimiento,
        //    DateTime? fechaFinalVencimiento,
        //    string numeroExpediete,
        //    string juicioAmparo,
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
        //    int? idEstadoProcesalIncidental);

        // Task<List<ResponseAutoridadesResponsables>> GetAutoridadesResponsablesAsync(
        // int idJuicioAmparo);
        // Task<ResponseAmparoIndirectoByID> GetByIdAmparoAsync(
        //    int idJuicioAmparo);
        // Task<List<ResponseNotaLitigioById>> GetByIdNotaLitigioAsync(int id);

        // Task<ResultTransaction> CreateSuspensionProvisionalAbogadoAsync(
        // SuspensionProvisional entity);

        // Task<ResultTransaction> CreateIncidenteExcesoIncidentalAsync(
        // IncidenteExcesoIncidental entity);

        // Task<ResultTransaction> CreateInformeJustificadoAsync(
        //InformeJustificadoConstitucional entity);

        // Task<List<ResponseInformacionAdicional>> GetAllInformacionAdicionalAsync(int id);
        // Task<ResponseRecursoIncondormidad> GetAllRecursoInconformidadAsync(int id);
        // Task<List<ResponseRecursoReclamacion>> GetAllRecursoReclamacionAsync(int id);
        // Task<List<ResponseInformeJustificadoConstitucional>> GetAllInformeJustificadoConstitucionalAsync(
        //     int id);
        // Task<List<ResponseRecursoQuejaPrincipalConstitucional>> GetAllRecursoQuejaPrincipalConstitucionalAsync(
        //     int id);

        // Task<List<ResponseRecursoRevisionPrincipal>> GetAllRecursoRevisionConstitucionalAsync(
        //     int id);
        // Task<List<ResponseSentenciaConstitucional>> GetAllSentenciaConstitucionalAsync(int id);
        // Task<List<IncidenteExcesoIncidentalDisconnected>> GetAllIncidentePorExcesoIncidentalAsync(int id);
        // Task<List<ResponseCumplimientoFalloProtector>> GetAllCumplimientoFalloConstitucionalAsync(
        //     int id);

        //Task<ResultTransaction> CreateNotaLitigioAsync(
        //RequestCreateNotaLitigio entity);

        //Task<ResultTransaction> UpdateEliminarNotaLitigio(int idNotaLitigio);

        Task<List<ResponseInformeJustificadoConstitucional>> GetByIdInformeJustificadoConstitucionalAsync(
            int id);
        Task<List<ResponseDescartarInformeJustificadoConstitucional>> GetAllInformeJustificadoDescartarAsync(int id);
        Task<ResultTransaction> UpdateDescartarInformeJustificado(int idInformeJustificado);
    }
}
