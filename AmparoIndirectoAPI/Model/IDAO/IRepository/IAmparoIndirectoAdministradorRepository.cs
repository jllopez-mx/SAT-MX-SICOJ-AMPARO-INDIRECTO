using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.IDAO.IRepository
{
    public interface IAmparoIndirectoAdministradorRepository
    {
        Task<List<ResponseAmparoIndirectoAdministradorBandeja>> GetAllAdm(
            int pageSize,
            int page,
            string orderByColumn,
            bool orderDesc);

        Task<int?> GetAllAdmCount();
        Task<List<AmparoIndirecto>> GetByIdsJuicio(int[] id);

        Task<List<ResponseHistoricoAdministradorByFilters>> GetHistoricoByFilters(
          int Fetch,
          int Page,
          string OrderByColumn,
          bool OrderDesc,
          DateTime? fechaRecepcionInicial,
          DateTime? fechaRecepcionFinal,
          DateTime? fechaVencimientoDemandaInicial,
          DateTime? fechaVencimientoDemandaFinal,
          string numeroExpediete,
          string numeroAsunto,
          //string juicioAmparo, se modifcio
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
          int? idEstadoProcesalIncidental
      );

        Task<int?> GetHistoricoByFiltersCount(
        DateTime? fechaRecepcionInicial,
        DateTime? fechaRecepcionFinal,
        DateTime? fechaVencimientoDemandaInicial,
        DateTime? fechaVencimientoDemandaFinal,
        string numeroExpediete,
        string numeroAsunto,
        //string juicioAmparo, se modifico
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
        int? idEstadoProcesal,
        int? idEstadoProcesalIncidental
        );

        Task<ResponseAmparoIndirectoByID> GetByIdAmparoAsync(
           int idNumeroAsunto);
        //int idJuicioAmparo); se modifico
        Task<List<ResponseAutoridadesResponsables>> GetAutoridadesResponsablesAsync(
           int idNumeroAmparo);
        //int idJuicioAmparo); se modifico
    }
}
