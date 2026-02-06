using AmparoIndirectoAPI.Model.DAO.Repository;
using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.IDAO.IRepository
{
    public interface IAmparoIndirectoRepository
    {
        #region Update Asignar
        Task<ResultTransaction> UpdateAsignarAbogadoAdministradorAsync(
           AmparoIndirecto entity
       );
        #endregion

        #region Create remitir asunto administrador
        Task<ResultTransaction> CreateCanalizarAsuntoAdministradorAsync(
           RequestCreateCanalizarAsuntoAdministrador entity
       );
        #endregion
        #region Update AI
        Task<ResultTransaction> UpdateAmparoIndirectoAdministradorAsync(
           AmparoIndirecto entity
       );
        #endregion
        Task<AmparoIndirecto> GetByIdAsyncAI(
           int idNumeroAsunto
       );
        Task<List<AutoridadesResponsables>> GetAutoridadesResponsablesIdAsync(
           int idNumeroAsunto
       );
        Task<List<AutoridadesResponsables>> GetAutoridadesResponsablesIdDisconnectedAsync(
           int idNumeroAsunto
       );

        Task<Acumular> GetCabezaSerieId(int idNumeroAsunto);

        #region Asignar AI
        Task<ResultTransaction> CreateAsignarAsync(
           RequestAsignarAbogadoOficialPartes entity
       );
        #endregion
        #region DeteleAI
        Task<ResultTransaction> UpdateEliminarRegistroAI(
           int idNumeroAsunto
       );
        #endregion

        #region Create Asunto Control documental
        Task<ResultTransaction> CreateAsuntoControlDocumentalAsync(
           AmparoIndirecto entity
       );
        #endregion

        #region CreateAI
        Task<ResultTransaction> CreateAmparoIndirectoAsync(
           AmparoIndirecto entity
       );
        #endregion

        #region Turnar
        Task<ResultTransaction> UpdateTurnarRegistroAI(
            int id,
            int idAdministracionCentral,
            int idAdministracion,
            int idSubadministracion,
            string numeroEmpleado
       );
        #endregion

        #region Update AI
        Task<ResultTransaction> UpdateAmparoIndirectoAsync(
           AmparoIndirecto entity
       );
        #endregion

        #region Create Acumular Juicio
        Task<ResultTransaction> CreateAcumularJuicioAsync(
          int [] ids,  AcumularDesacumular entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile
       );
        #endregion

        Task<ResponseSolicitudTransparenciaLista> GetSolicitudById(int id);


        #region Create Solicitud Transparencia
        Task<ResultTransaction> CreateSolicitudTransparenciaAsync(SolicitudTransparencia entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);

        #endregion


        #region Delete Solicitud Transparencia
        Task<ResultTransaction> EliminarSolicitudTransparencia(int id);
        #endregion

        #region CREATE DECLINAR COMPETENCIA
        Task<ResultTransaction> CreateDeclinarCompetenciaAsync(
           DeclinarCompetencia entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile
       );
        #endregion
        #region Create remitir asunto
        Task<ResultTransaction> CreateCanalizarAsuntoAsync(
           CanalizarAsunto entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile
       );
        #endregion

        #region Create remitir asunto
        Task<ResultTransaction> CreateReasignarJuicioAsuntoAsync(
           int[] ids , RequestCreateReasignarJuicioAdministrador request
       );
        #endregion


        #region ABOGADO
        Task<ResultTransaction> UpdateInformePrevioIncidentalAsync(int id, string usuario);
        Task<ResultTransaction> UpdateSentenciaIncidentalAsync(int id, string usuario);
        Task<ResultTransaction> UpdateRecursoQuejaIncidentalAsync(int id, string usuario);
        Task<ResultTransaction> UpdateRecursoRevisionIncidentalAsync(int id, bool recursoRevisionIncidental /*string usuario*/);
        Task<ResultTransaction> UpdateInformeJustificadoConstitucionalAsync(int id, string usuario);
        Task<ResultTransaction> UpdateRecursoQuejaConstitucionalAsync(int id, bool recursoQuejaPrincipal);
        Task<ResultTransaction> UpdateRecursoRevisionConstitucionalAsync(int id, bool recursoQuejaPrincipal);
        Task<ResultTransaction> UpdateCumplimientoFalloProtectorAsync(int id);
        Task<ResultTransaction> CreateCumplimientoFalloProtectorAsync(CumplimientoFalloProtectorConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultTransaction> UpdateSentenciaConstitucionalAsync(int id, string usuario);
        Task<ResultTransaction> UpdateIncidenteExcesoAsync(int id, string usuario);
        Task<ResultTransaction> CreateSentenciaConstitucionalAsync(SentenciaConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultTransaction> CreateSentenciaIncidentalAsync(SentenciaIncidental entity);
        Task<ResultTransaction> CreateRecursoQuejaIncidentalAsync(RecursoQuejaIncidental entity);
        Task<ResultTransaction> CreateRecursoQuejaIncidentalQuejosoAsync(RecursoQuejaIncidental entity);
        Task<ResultTransaction> CreateRecursoQuejaIncidentalOtrasAutoridadesAsync(RecursoQuejaIncidental entity);
        Task<ResultTransaction> CreateInformePrevioIncidentalAsync(InformePrevioIncidental entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile);
        Task<ResultTransaction> CreateAutoridadesResponsablesAsync(RequestCreateAutoridadResponsableAbogado request);
        Task<ResultTransaction> CreateInformacionAdicionalAsync(InformacionAdicional request);


        #endregion

        Task<List<AmparoIndirecto>> GetByIdsJuicio(int[] id);
    }
}
