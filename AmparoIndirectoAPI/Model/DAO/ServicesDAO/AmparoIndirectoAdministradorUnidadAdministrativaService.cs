using System.Configuration;
using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.DTO.ContractsValidations;
using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;
using Mapster;
using Sicoj.Utils.Enums;
using Sicoj.Utils.Extentions;
using Sicoj.Utils.Models;
using Sicoj.Utils.Redis;
using Sicoj.Utils.ViewModels;
using Microsoft.Extensions.Options;

namespace AmparoIndirectoAPI.Model.DAO.ServicesDAO
{
    public class AmparoIndirectoAdministradorUnidadAdministrativaService : IAmparoIndirectoAdministradorUnidadAdministrativaService
    {
        private readonly IAmparoIndirectoRepository _repositoryAmparoIndirectoDisconnected;
        private readonly IAmparoIndirectoAdministradorUnidadAdministrativaRepository _repositoryAmparoIndirectoAdministradorUnidadAdministrativa;
        private readonly ApiService _apiService;
        private readonly string _routedInfoUsuario = null!;
        private readonly string _routeAutoridadesResponsable = null!;
        private readonly string _routeTipoActo = null!;
        private readonly string _routeEstadoTarea = null!;
        private readonly string _routeEstadoProcesal = null!;
        private readonly string _routeJuzgado = null!;
        private readonly string _routeMateria = null!;
        private readonly string _routeSubmateria = null!;
        private readonly string _routeAdministracion = null!;
        private readonly string _routeSubadministracion = null!;
        private readonly IRedisClient _redisClient;
        private readonly ProxyEnpoints _proxyEnpoints;

        public AmparoIndirectoAdministradorUnidadAdministrativaService(
            IConfiguration configuration,
            IAmparoIndirectoRepository repositoryAmparoIndirectoDisconnected,
            IAmparoIndirectoAdministradorUnidadAdministrativaRepository repositoryAmparoIndirectoAdministradorUnidadAdministrativa,
            IArchivosAmparoIndirectoRepository repositoryArchivosAmparoIndirectoDisconnected,
            ApiService apiService,
            IRedisClient redisClient,
            IOptions<ProxyEnpoints> proxyEnpoints
            )
        {
            _repositoryAmparoIndirectoDisconnected = repositoryAmparoIndirectoDisconnected ?? throw new ArgumentNullException(nameof(repositoryAmparoIndirectoDisconnected));
            _repositoryAmparoIndirectoAdministradorUnidadAdministrativa = repositoryAmparoIndirectoAdministradorUnidadAdministrativa ?? throw new ArgumentNullException(nameof(repositoryAmparoIndirectoAdministradorUnidadAdministrativa));
            
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
            _routedInfoUsuario = configuration.GetValue<string>("CatalogsEndpoints:RoutedInfoUsuario")!;
            _routeAutoridadesResponsable = configuration.GetValue<string>("CatalogsEndpoints:RouteAutoridadesResponsable")!;
            _routeTipoActo = configuration.GetValue<string>("CatalogsEndpoints:RouteTipoActo")!;
            _routeEstadoTarea = configuration.GetValue<string>("CatalogsEndpoints:RouteEstadoTarea")!;
            _routeEstadoProcesal = configuration.GetValue<string>("CatalogsEndpoints:RouteEstadoProcesal")!;
            _routeJuzgado = configuration.GetValue<string>("CatalogsEndpoints:RouteJuzgado")!;
            _routeMateria = configuration.GetValue<string>("CatalogsEndpoints:RouteMateria")!;
            _routeSubmateria = configuration.GetValue<string>("CatalogsEndpoints:RouteSubmateria")!;
            _routeAdministracion = configuration.GetValue<string>("CatalogsEndpoints:RouteAdministracion")!;
            _routeSubadministracion = configuration.GetValue<string>("CatalogsEndpoints:RouteSubadministracion")!;
            _proxyEnpoints = proxyEnpoints.Value?? throw new ArgumentNullException(nameof(proxyEnpoints));
        }

        ////#region GetById

        ////public async Task<AmparoIndirecto> GetAmparoIndirectoByIdDisconnected(int id) =>
        ////    await _repositoryAmparoIndirectoDisconnected.GetByIdAsyncAI(id);
        ////#endregion

        //#region Historico
        //public async Task<ResultOperation> GetHistoricoAbogadoByFilters(
        //    int Fetch,
        //    int Page,
        //    string OrderByColumn,
        //    bool OrderDesc,
        //    DateTime? fechaRecepcionInicial,
        //    DateTime? fechaRecepcionFinal,
        //    DateTime? fechaInicialVencimiento,
        //    DateTime? fechaFinalVencimiento,
        //    string numeroExpediete,
        //    string numeroAsunto,
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
        //    int? idEstadoProcesalIncidental)
        //{
        //    try
        //    {

        //        var countResult = await _repositoryAmparoIndirectoAbogado.GetHistoricoAbogadoByFiltersCount(
        //        fechaRecepcionInicial,
        //        fechaRecepcionFinal,
        //        fechaInicialVencimiento,
        //        fechaFinalVencimiento,
        //        numeroExpediete,
        //        numeroAsunto,
        //        idJuzgado,
        //        nombreQuejoso,
        //        idMateria,
        //        idSubmateria,
        //        idTipoActo,
        //        despacho,
        //        idAdministracion,
        //        idSubadministracion,
        //        idAutoridadResponsable,
        //        rfcQuejoso,
        //        idEstadoTarea,
        //        idEstadoProcesal,
        //        idEstadoProcesalIncidental
        //            );

        //        if (countResult is null || countResult <= 0)
        //        {
        //            return ResultOperation.SuccessResponseNoMessage(
        //                new DataTableView<ResponseHistoricoAbogadoByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
        //        }

        //        var result = await _repositoryAmparoIndirectoAbogado.GetHistoricoAbogadoByFilters(
        //            Fetch,
        //            Page,
        //            OrderByColumn,
        //            OrderDesc,
        //            fechaRecepcionInicial,
        //            fechaRecepcionFinal,
        //            fechaInicialVencimiento,
        //            fechaFinalVencimiento,
        //            numeroExpediete,
        //            numeroAsunto,
        //            idJuzgado,
        //            nombreQuejoso,
        //            idMateria,
        //            idSubmateria,
        //            idTipoActo,
        //            despacho,
        //            idAdministracion,
        //            idSubadministracion,
        //            idAutoridadResponsable,
        //            rfcQuejoso,
        //            idEstadoTarea,
        //            idEstadoProcesal,
        //            idEstadoProcesalIncidental);

        //        if (result is null)
        //        {
        //            //ResponseAmparoIndirectoByFilters
        //            return ResultOperation.FailureWarningResponse<List<ResponseHistoricoAbogadoByFilters>>("No se encontraron resultados");
        //        }
        //        return ResultOperation.SuccessResponseNoMessage(
        //            new DataTableView<ResponseHistoricoAbogadoByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()), result)
        //        );
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //#endregion

        #region GetAll Bandeja de Pendientes Administrador Unidad Administrativa
        public async Task<ResultOperation> GetAllBandejaPendientesAdministradorUnidadAdministrativa(int Fetch, int Page, string orderByColumn, bool orderDesc)
        {
            try
            {
                var countResult = await _repositoryAmparoIndirectoAdministradorUnidadAdministrativa.GetAllAdministradorUnidadAdministrativaCount();

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation.SuccessResponseNoMessage(
                        new DataTableView<ResponseAmparoIndirectoAdministradorUnidadAdministrativaBandeja>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
                }

                var result = await _repositoryAmparoIndirectoAdministradorUnidadAdministrativa.GetAllAdministradorUnidadAdministrativa(
                    Fetch,
                    Page,
                    orderByColumn,
                    orderDesc
                );
                if (result is null)
                {
                    return ResultOperation.FailureErrorResponse<List<ResponseAmparoIndirectoAdministradorUnidadAdministrativaBandeja>>("Ha ocurrido un error al recuperar los datos");
                }
                _redisClient.ValidateTakeList(ref result, EnumModulosRedis.AMPARO_INDIRECTO);
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseAmparoIndirectoAdministradorUnidadAdministrativaBandeja>(new(Page, Fetch, countResult.GetValueOrDefault()), result)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        ////#region Get BY ID AI
        ////public async Task<ResultOperation> GetByIdAmparo(int idNumeroAsunto)
        ////{
        ////    try
        ////    {
        ////        var result = await _repositoryAmparoIndirectoAbogado.GetByIdAmparoAsync(idNumeroAsunto);

        ////        if (result is null)
        ////        {
        ////            return ResultOperation<ResponseAmparoIndirectoByID>.FailureWarningResponse("No se encontraron resultados");
        ////        }
        ////        var resultOperation = ResultOperation.SuccessResponseNoMessage(result);

        ////        if (result.juzgado.Value > 0)
        ////        {
        ////            var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.JuzgadoAmparo, result.juzgado.Value.ToString());
        ////            if (!string.IsNullOrEmpty(catalogResponse))
        ////            {
        ////                resultOperation.Result.juzgado.Label = catalogResponse;
        ////            }
        ////            else
        ////                resultOperation.AddWarningMessage("No se pudo recuperar el nombre del juzgado.");
        ////        }
        ////        if (result.materia.Value > 0)
        ////        {
        ////            var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.Materia, result.materia.Value.ToString());
        ////            //var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeMateria}/{result.materia.Value}");
        ////            if (!string.IsNullOrEmpty(catalogResponse))
        ////            {
        ////                resultOperation.Result.juzgado.Label = catalogResponse;
        ////            }
        ////            else
        ////                resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la materia.");
        ////        }
        ////        if (result.submateria.Value > 0)
        ////        {
        ////            var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.Submateria, result.submateria.Value.ToString());
        ////            //var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeSubmateria}/{result.submateria.Value}");
        ////            if (!string.IsNullOrEmpty(catalogResponse))
        ////            {
        ////                resultOperation.Result.juzgado.Label = catalogResponse;
        ////            }
        ////            else
        ////                resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la submateria.");
        ////        }
        ////        if (result.tipo_acto.Value > 0)
        ////        {
        ////            var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.TipoActo, result.tipo_acto.Value.ToString());
        ////            if (!string.IsNullOrEmpty(catalogResponse))
        ////            {
        ////                resultOperation.Result.juzgado.Label = catalogResponse;
        ////            }
        ////            else
        ////                resultOperation.AddWarningMessage("No se pudo recuperar el nombre del tipo de acto.");
        ////        }
        ////        //if (result.autoridad_responsable.Value > 0)
        ////        //{
        ////        //    var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeAutoridadesResponsable}/{result.autoridad_responsable.Value}");
        ////        //    if (responseTipoAsunto is not null && responseTipoAsunto.Success && responseTipoAsunto.Result is not null)
        ////        //    {
        ////        //        resultOperation.Result.autoridad_responsable.Label = responseTipoAsunto.Result.nombre;
        ////        //    }
        ////        //    else
        ////        //        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del juzgado.");
        ////        //}
        ////        if (result.administracion.Value > 0)
        ////        {
        ////            var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeAdministracion}/{result.administracion.Value}");
        ////            if (responseTipoAsunto is not null && responseTipoAsunto.Success && responseTipoAsunto.Result is not null)
        ////            {
        ////                resultOperation.Result.administracion.Label = responseTipoAsunto.Result.nombre;
        ////            }
        ////            else
        ////                resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la administración.");
        ////        }
        ////        if (result.subadministracion.Value > 0)
        ////        {
        ////            var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeSubadministracion}/{result.subadministracion.Value}");
        ////            if (responseTipoAsunto is not null && responseTipoAsunto.Success && responseTipoAsunto.Result is not null)
        ////            {
        ////                resultOperation.Result.subadministracion.Label = responseTipoAsunto.Result.nombre;
        ////            }
        ////            else
        ////                resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la subadminstración.");
        ////        }
        ////        if (result.estado_tarea.Value > 0)
        ////        {
        ////            var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.EstadoTarea, result.estado_tarea.Value.ToString());
        ////            if (!string.IsNullOrEmpty(catalogResponse))
        ////            {
        ////                resultOperation.Result.juzgado.Label = catalogResponse;
        ////            }
        ////            else
        ////                resultOperation.AddWarningMessage("No se pudo recuperar el nombre del estado tarea.");
        ////        }
        ////        if (result.estado_procesal.Value > 0)
        ////        {
        ////            var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.EstadoProcesal, result.estado_procesal.Value.ToString());
        ////            if (!string.IsNullOrEmpty(catalogResponse))
        ////            {
        ////                resultOperation.Result.juzgado.Label = catalogResponse;
        ////            }
        ////            else
        ////                resultOperation.AddWarningMessage("No se pudo recuperar el nombre del estado procesal.");
        ////        }
        ////        var resultAutoridades = await _repositoryAmparoIndirectoAbogado.GetAutoridadesResponsablesAsync(idNumeroAsunto);
        ////        if (resultAutoridades is not null && resultAutoridades.Any())
        ////            resultOperation.Result.list_autoridad_responsable.AddRange(resultAutoridades);


        ////        return ResultOperation.SuccessResponseNoMessage(result);
        ////    }
        ////    catch (Exception)
        ////    {
        ////        throw;
        ////    }
        ////}
        ////#endregion

    }
}
