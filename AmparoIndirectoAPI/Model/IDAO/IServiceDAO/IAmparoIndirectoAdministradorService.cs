using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.Entities;
using Sicoj.Utils.Models;

namespace AmparoIndirectoAPI.Model.IDAO.IServiceDAO
{
    public interface IAmparoIndirectoAdministradorService
    {
        Task<ResultOperation> UpdateAsignarAdministrador(AmparoIndirecto entity);
        Task<ResultOperation> CreateCanalizarAsuntoAdministrador(RequestCreateCanalizarAsuntoAdministrador requestCanalizarAsunto);
        //Task<ResultOperation> CreateReasignarJuicio(AmparoIndirecto requestRemitirAsunto);
        Task<ResultOperation> CreateReasignarJuicio(RequestCreateReasignarJuicioAdministrador request);

        Task<ResultOperation> UpdateAmparoIndirectoAdministrador(AmparoIndirecto entity);
        Task<ResultOperation> GetAllBandejaPendientesAdministrador(int Fetch, int Page, string orderByColumn, bool orderDesc);
        Task<ResultOperation> GetHistoricoByFilters(
            int Fetch, 
            int Page, 
            string OrderByColumn, 
            bool OrderDesc, 
            DateTime? fechaRecepcionInicial, 
            DateTime? fechaRecepcionFinal,
            DateTime? fechaVencimientoDemandaInicial,
            DateTime? fechaVencimientoDemandaFinal,
            string numeroExpediete, 
            string NumeroAsunto,
            //string juicioAmparo, se modifico
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
        Task<AmparoIndirecto> GetAmparoIndirectoByIdDisconnected(int id);
        Task<List<AmparoIndirecto>> GetAmparoIndirectoByIds(int[] ids);
        Task<ResultOperation> GetByIdAmparo(int idNumeroAsunto);
    }
}
