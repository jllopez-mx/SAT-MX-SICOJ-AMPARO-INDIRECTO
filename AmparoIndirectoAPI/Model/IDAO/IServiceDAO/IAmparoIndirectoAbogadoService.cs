using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.IDAO.IServiceDAO
{
    public interface IAmparoIndirectoAbogadoService
    {
        Task<ResultOperation> GetCuadernoConstitucionalRecurrente(int id, int idEstadoProcesal);
        Task<ResultOperation> UpdateCumplimientoFalloProtector(int id);
        Task<ResultOperation> CreateCumplimientoFalloProtector(CumplimientoFalloProtectorConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation> UpdateSentenciaConstitucional(int id, string usuario);
        Task<ResultOperation> UpdateIncidenteExcesoIncidental(int id, string usuario);
        Task<ResultOperation> CreateSentenciaConstitucional(SentenciaConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation> CreateSentenciaIncidental(SentenciaIncidental entity);
        Task<ResultOperation> CreateRecursoQuejaIncidental(RecursoQuejaIncidental entity);
        Task<ResultOperation> CreateInformePrevioIncidental(InformePrevioIncidental entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation> CreateSuspensionProvisionalAbogado(SuspensionProvisional entity);
        Task<ResultOperation> CreateInformacionAdicional(InformacionAdicional request);
        Task<ResultOperation> CreateAutoridadResponsableAbogado(RequestCreateAutoridadResponsableAbogado request);
        Task<ResultOperation> GetAllSuspensionProvisionalIncidental(int id);
        Task<ResultOperation> GetAllInformePrevioIncidental(int id);
        Task<ResultOperation> GetAllSentenciaIncidental(int id);
        Task<ResultOperation> GetAllRecursoQuejaIncidental(int id/*, int recurrente*/);
        Task<ResultOperation> GetAllRecursoRevisionIncidental(int id);
        Task<ResultOperation> GetAllIncidenteExcesoIncidental(int id);
        Task<ResultOperation> GetAllInformeJustificadoConstitucional(int id);
        Task<ResultOperation> GetAllInformacionAdicional(int id);
        Task<ResultOperation> GetAllRecursoInconformidad(int id);
        Task<ResultOperation> GetAllRecursoReclamacion(int id);
        Task<ResultOperation> GetAllSentenciaConstitucional(int id);
        Task<ResultOperation> GetAllCumplimientoFalloConstitucional(int id);
        Task<ResultOperation> GetAllRecursoQuejaPrincipalConstitucional(int id);
        Task<ResultOperation> GetAllRecursoRevisionConstitucional(int id);
        Task<List<ResponseSentenciaConstitucional>> GetAllSentenciaConstitucionalDisconnected(int id);
        Task<List<IncidenteExcesoIncidentalDisconnected>> GetAllIncidentePorExcesoIncidentalDisconnected(int id);
        Task<List<ResponseSentenciaIncidental>> GetAllSentenciaIncidentalDisconnected(int id);
        Task<List<RecursoQuejaIncidentalDisconected>> GetAllRecursoQuejaIncidentalDisconnected(int id);
        Task<List<RecursoRevisionIncidentalDisconnected>> GetAllRecursoRevisionIncidentalDisconnected(int id);
        Task<List<ResponseInformePrevioIncidental>> GetAllInformePrevioIncidentalDisconnected(int id);
        Task<ResultOperation> GetAllBandejaPendientesAbogado(int Fetch, int Page, string orderByColumn, bool orderDesc);

        Task<ResultOperation> GetHistoricoAbogadoByFilters(
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
            //string juicioAmparo, //este se modifico
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
        Task<AmparoIndirecto>GetAmparoIndirectoByIdDisconnected(int id);

        Task<List<AutoridadesResponsables>>GetAutoridadesResponsablesIdDisconnected(int id);
        Task<List<AutoridadesResponsables>>GetAutoridadesResponsablesIdRealDisconnected(int id);
        Task<List<RequestCuadernoConstitucional>> GetCuadernoConstitucionalIdDisconnected(int id);
        Task<List<RecursoRevisionPrincipalConstitucionalDisconnected>> GetRecursoRevisionPrincipalIdDisconnected(int id);
        Task<List<ResponseInformeJustificadoConstitucional>> GetIndormeJustificadoIdDisconnected(int id);
        Task<ResultOperation> GetByIdAmparo(int idNumeroAsunto);
        //Task<ResultOperation> GetByIdAmparo(int idJuicioAmparo); //este se modifico
        Task<ResultOperation> CreateInformeJustificado(InformeJustificadoConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation> UpdateInformeJustificadoConstitucional(int id, string usuario);
        Task<ResultOperation> UpdateInformePrevioIncidental(int id, string usuario);
        Task<ResultOperation> UpdateSentenciaIncidental(int id, string usuario);
        Task<ResultOperation> UpdateRecursoQuejaIncidental(int id, string usuario);
        Task<ResultOperation> UpdateRecursoRevisionIncidental(int id, bool recursoRevisionIncidental /*string usuario*/);
        Task<ResultOperation> UpdateRecursoQuejaConstitucional(int id, bool recursoQuejaPrincipal);
        Task<ResultOperation> UpdateRecursoRevisionConstitucional(int id, bool recursoQuejaPrincipal);
        Task<ResultOperation> CreateRecursoQuejaConstitucional(RecursoQuejaPrincipalConstitucional entity);
        Task<ResultOperation> CreateRecursoRevisionIncidental(RecursoRevisionIncidental entity);
        Task<ResultOperation> CreateRecursoRevisionConstitucional(RecursoRevisionPrincipalConstitucional entity);
        Task<ResultOperation> CreateRecursoInconformidadConstitucional(RecursoInconformidadConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation> CreateRecursoReclamacionConstitucional(RecursoReclamacionConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation> CreateIncidenteExcesoIncidental(IncidenteExcesoIncidental entity);
        Task<ResultOperation> CreateNotaLitigio(NotaLitigio requestNotaLitigio, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultOperation<bool>> UpdateDeleteNotaLitigio(int idNotaLitigio);
        Task<ResultOperation> GetByIdNotaLitigio(int id);
    }
}
