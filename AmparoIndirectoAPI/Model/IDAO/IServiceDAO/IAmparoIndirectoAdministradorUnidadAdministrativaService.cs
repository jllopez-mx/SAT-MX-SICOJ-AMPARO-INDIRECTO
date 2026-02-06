using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.IDAO.IServiceDAO
{
    public interface IAmparoIndirectoAdministradorUnidadAdministrativaService
    {
       
        Task<ResultOperation> GetAllBandejaPendientesAdministradorUnidadAdministrativa(int Fetch, int Page, string orderByColumn, bool orderDesc);

        //Task<ResultOperation> GetHistoricoAbogadoByFilters(
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
        //Task<AmparoIndirecto> GetAmparoIndirectoByIdDisconnected(int id);
        //Task<ResultOperation> GetByIdAmparo(int idJuicioAmparo);
        
    }
}
