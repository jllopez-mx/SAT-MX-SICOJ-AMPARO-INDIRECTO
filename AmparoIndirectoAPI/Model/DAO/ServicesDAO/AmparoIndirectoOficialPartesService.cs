using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.DTO.ContractsValidations;
using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.Entities.Events;
using AmparoIndirectoAPI.Model.Entities.Events.OficialPartes;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;
using Mapster;
using Sicoj.Utils.Enums;
using Sicoj.Utils.Extentions;
using Sicoj.Utils.Models;
using Sicoj.Utils.Redis;
using Sicoj.Utils.ViewModels;
using Microsoft.Extensions.Options;
using Sicoj.Utils.Middleware;

namespace AmparoIndirectoAPI.Model.DAO.ServicesDAO
{
    public class AmparoIndirectoOficialPartesService : IAmparoIndirectoOficialPartesService
    {

        private readonly IAmparoIndirectoRepository _repositoryAmparoIndirectoDisconnected;
        private readonly IApiService _apiService;
        private readonly IRedisClient _redisClient;
        private readonly CatalogosEnpoints _catologosEnpoints;
        private readonly ProxyEnpoints _proxyEnpoints;
        private readonly IAmparoIndirectoOficialPartesRepository _repositoryAmparoIndirectoOficialPartes;
        private readonly IArchivosAmparoIndirectoRepository _repositoryArchivosAmparoIndirectoDisconnected;
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

        public AmparoIndirectoOficialPartesService(
            IConfiguration configuration,
            IAmparoIndirectoRepository repositoryAmparoIndirectoDisconnected,
            IAmparoIndirectoOficialPartesRepository repositoryAmparoIndirectoOficialPartes,
            IArchivosAmparoIndirectoRepository repositoryArchivosAmparoIndirectoDisconnected,
            IApiService apiService,
            IRedisClient redisClient,
            IOptions<CatalogosEnpoints> catologosEnpoints,
            IOptions<ProxyEnpoints> proxyEnpoints
            )
        {
            _repositoryAmparoIndirectoDisconnected = repositoryAmparoIndirectoDisconnected ?? throw new ArgumentNullException(nameof(repositoryAmparoIndirectoDisconnected));
            _repositoryAmparoIndirectoOficialPartes = repositoryAmparoIndirectoOficialPartes ?? throw new ArgumentNullException(nameof(repositoryAmparoIndirectoOficialPartes));
            _repositoryArchivosAmparoIndirectoDisconnected = repositoryArchivosAmparoIndirectoDisconnected ?? throw new ArgumentNullException(nameof(repositoryArchivosAmparoIndirectoDisconnected));
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
            _proxyEnpoints = proxyEnpoints.Value ?? throw new ArgumentNullException(nameof(proxyEnpoints));
            //SE agrega para fecha de vencimiento
        }



        #region GetById

        public async Task<AmparoIndirecto> GetAmparoIndirectoByIdDisconnected(int id) =>
            await _repositoryAmparoIndirectoDisconnected.GetByIdAsyncAI(id);
        #endregion

        #region Get byId Cabeza de Serie
        public async Task<Acumular> GetCabezaSerieById(int id) =>
           await _repositoryAmparoIndirectoDisconnected.GetCabezaSerieId(id);
        #endregion

        #region GetByFiltersAi
        public async Task<ResultOperation> GetAmparoIndirectoDisconnectedByFilters(int Fetch, int Page, string OrderByColumn, bool OrderDesc, DateTime? fechaRecepcionInicial, DateTime? fechaRecepcionFinal, DateTime? fechaVencimientoDemanda, string numeroExpediente, string numeroAsunto, int? idJuzgado, string nombreQuejoso, int? idMateria, int? idSubmateria, int? idTipoActo, string despacho, int? idAdministracion, int? idSubadministracion, string rfcQuejoso, bool? transperencia, int? idEstadoTarea, int? idEstadoProcesal)
        {
            try
            {
                var countResult = await _repositoryAmparoIndirectoOficialPartes.GetAllByFiltersCountAsyncAI(

                        fechaRecepcionInicial,
                        fechaRecepcionFinal,
                        fechaVencimientoDemanda,
                        numeroExpediente,
                        numeroAsunto,
                        idJuzgado,
                        nombreQuejoso,
                        idMateria,
                        idSubmateria,
                        idTipoActo,
                        despacho,
                        idAdministracion,
                        idSubadministracion,
                        rfcQuejoso,
                        idEstadoTarea,
                        idEstadoProcesal
                    );

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation.SuccessResponseNoMessage(
                        new DataTableView<ResponseAmparoIndirectoByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
                }

                var result = await _repositoryAmparoIndirectoOficialPartes.GetAllByFiltersAsyncAI(
                    Fetch,
                    Page,
                    OrderByColumn,
                    OrderDesc,
                    fechaRecepcionInicial,
                    fechaRecepcionFinal,
                    fechaVencimientoDemanda,
                    numeroExpediente,
                    numeroAsunto,
                    idJuzgado,
                    nombreQuejoso,
                    idMateria,
                    idSubmateria,
                    idTipoActo,
                    despacho,
                    idAdministracion,
                    idSubadministracion,
                    rfcQuejoso,
                    idEstadoTarea,
                    idEstadoProcesal

                );
                if (result is null)
                {
                    return ResultOperation.FailureWarningResponse<List<ResponseAmparoIndirectoByFilters>>("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseAmparoIndirectoByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()),
                    result)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetAllAI
        public async Task<ResultOperation> GetAmparoIndirectoDisconnectedGetAll(int Fetch, int Page, string orderByColumn, bool orderDesc)
        {
            try
            {
                var countResult = await _repositoryAmparoIndirectoOficialPartes.GetAllCountAsyncAI();

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation.SuccessResponseNoMessage(
                        new DataTableView<ResponseAmparoIndirectoBandeja>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
                }

                var result = await _repositoryAmparoIndirectoOficialPartes.GetAllAsyncAI(
                    Fetch,
                    Page,
                    orderByColumn,
                    orderDesc
                );

                if (result is null)
                {
                    return ResultOperation.FailureErrorResponse<List<ResponseAmparoIndirectoBandeja>>("Ha ocurrido un error al recuperar los datos");
                }

                _redisClient.ValidateTakeList(ref result, EnumModulosRedis.AMPARO_INDIRECTO);

                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseAmparoIndirectoBandeja>(new(Page, Fetch, countResult.GetValueOrDefault()),
                    result)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region Eliminar registro

        public async Task<ResultOperation<bool>> UpdateDeleteAmparoIndirecto(int idNumeroAsunto)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoDisconnected.UpdateEliminarRegistroAI(idNumeroAsunto);
                if (!result.Success)
                    return ResultOperation.FailureErrorResponse<bool>($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Asignar registro

        public async Task<ResultOperation> CreateAsignar(RequestAsignarAbogadoOficialPartes entity)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoDisconnected.CreateAsignarAsync(entity);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Crea registro

        public async Task<ResultOperation> CreateAsuntoControlDocumental(AmparoIndirecto entity)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                if (Convert.ToDateTime(entity.fecha_recepcion_demanda) > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de recepción de demanda no puede ser mayor a la fecha actual.");
                }

                var result = await _repositoryAmparoIndirectoDisconnected.CreateAsuntoControlDocumentalAsync(entity);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(result.Result.GetValueOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Crea registro

        public async Task<ResultOperation> CreateAmparoIndirecto(AmparoIndirecto entity)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                if (Convert.ToDateTime(entity.fecha_recepcion_demanda) > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de recepción de demanda no puede ser mayor a la fecha actual.");
                }
                //var resposeFechaVencimiento = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_recepcion_demanda.ToString("yyyy-MM-dd")}&addDays={15}&nextDay={true}");
                /*if (resposeFechaVencimiento is null || !resposeFechaVencimiento.Success || string.IsNullOrEmpty(resposeFechaVencimiento.Result))
                {
                    return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento.");
                }
                if (!DateTime.TryParse(resposeFechaVencimiento.Result, out DateTime fechaVencimiento))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento no tiene el formato correcto.");
                }
               // entity.fecha_vecimiento_demanda = fechaVencimiento;
                */
                var result = await _repositoryAmparoIndirectoDisconnected.CreateAmparoIndirectoAsync(entity);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(result.Result.GetValueOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Turnar

        public async Task<ResultOperation> UpdateTurnarAmparoIndirecto(int id, int idAdministracionCentral, int idAdministracion, int idSubadministracion, string numeroEmpleado)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoDisconnected.UpdateTurnarRegistroAI(id, idAdministracionCentral, idAdministracion, idSubadministracion, numeroEmpleado);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Update registro
        public async Task<ResultOperation> UpdateAmparoIndirecto(AmparoIndirecto entity)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoDisconnected.UpdateAmparoIndirectoAsync(entity);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Get All antes de Acumular

        public async Task<ResultOperation> GetExpedienteAntesAcumularByFilters(int Fetch, int Page, string OrderByColumn, bool OrderDesc, string numeroExpediente, string numeroAsunto, string rfcQuejoso, string nombreQuejoso, int? idJuzgado, int? idTipoActo, int? idEstadoTarea)
        {
            try
            {
                var countResult = await _repositoryAmparoIndirectoOficialPartes.GetExpedientesAntesAcumularByFiltersCount(
                        numeroExpediente,
                        numeroAsunto,
                        rfcQuejoso,
                        nombreQuejoso,
                        idJuzgado,
                        idTipoActo,
                        idEstadoTarea

                    );

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation.SuccessResponseNoMessage(
                        new DataTableView<ResponseAcumularByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
                }

                var result = await _repositoryAmparoIndirectoOficialPartes.GetExpedientesAntesAcumularByFilters(
                    Fetch,
                    Page,
                    OrderByColumn,
                    OrderDesc,
                    numeroExpediente,
                    numeroAsunto,
                    rfcQuejoso,
                    nombreQuejoso,
                    idJuzgado,
                    idTipoActo,
                    idEstadoTarea

                );
                if (result is null)
                {
                    return ResultOperation.FailureWarningResponse<List<ResponseAcumularByFilters>>("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseAcumularByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()),
                    result)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region CREATE ACUMULAR JUICIO

        public async Task<ResultOperation> CreateAcumularJuicio(AcumularDesacumular entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {
                var entityListExists = await _repositoryAmparoIndirectoDisconnected.GetByIdsJuicio(entity.id_juicio_acumulado.ToArray());
                if (entityListExists is null || !entityListExists.Any())
                {
                    return (
                        ResultOperation.FailureErrorResponse(
                            "Los asuntos no existen."
                        )
                    );
                }

                List<int> listValidos = new List<int>();
                List<int> listInvalidos = new List<int>();

                var resultOperation = ResultOperation.SuccessResponseNoMessage(new ResponseReasignar());

                var validIds = entityListExists.Select(e => e.id).ToHashSet();

                foreach (var ids in entity.id_juicio_acumulado)
                {
                    var obj = entityListExists.FirstOrDefault(e => e.id == ids);
                    if (obj is null)
                    {
                        resultOperation.AddWarningMessage($"El asunto con el identificador {ids} no existe o se eliminó.");
                        listInvalidos.Add(ids);
                        continue;
                    }
                    try
                    {
                        obj = AmparoIndirectoOficialPartesEvents.CreateAcumular(ref obj);

                        var responseAdministrador = await _apiService.GetResultOperationAsync<UserInformationView>($"{_catologosEnpoints.RouteInfoUsuario}/{entity.usuario}");
                        if (responseAdministrador is null || !responseAdministrador.Success || responseAdministrador.Result is null)
                        {
                            return ResultOperation.FailureErrorResponse<ResponseReasignar>($"No se pudo realizar la validación para verificar que el abogado seleccionado pertenezca a la administracion/subadministración.");
                        }
                        //if (!UserSession.ValidateUser(responseAdministrador!.Result!, EnumRolesSicoj.ABOGADO, EnumModulosSicoj.RECURSO_DE_REVOCACIÓN, out string message, false, null!, true, entity))
                        //{
                        //    resultOperation.AddWarningMessage($"El juicio de Amparo Indirecto el número de asunto {obj.juicio_amparo}");
                        //}

                        //if (!UserSession.ValidateUser(responseAbogado!.Result!, EnumRolesSicoj.ABOGADO, EnumModulosSicoj.AUTORIZACIONES, out string message, false, null!, true, entity.id_unidad_administrativa_central, true, entity.id_unidad_administrativa, true, entityReasignar.id_subadministracion_reasignado))
                        if (!UserSession.ValidateUser(responseAdministrador!.Result!, EnumRolesSicoj.ABOGADO, EnumModulosSicoj.RECURSO_DE_REVOCACIÓN, out string message, false, null!, true, entity.id_administracion))
                        {
                            resultOperation.AddWarningMessage($"El Asunto con número  {obj.numero_asunto}");
                        }

                        listValidos.Add(ids);
                    }
                    catch (Exception _e)
                    {
                        resultOperation.AddWarningMessage($"El Asunto con el número {obj.numero_asunto}: {_e.Message}");
                        listInvalidos.Add(ids);
                        continue;
                    }
                }
                resultOperation.Result.ReasignacionesExitosas = listValidos.Count;
                resultOperation.Result.ReasignacionesIncorrectas = listInvalidos.Count;
                //resultOperation.Result.abogado = request.rfc_abogado!;

                if (!listValidos.Any())
                {
                    return (resultOperation);
                }
                var result = await _repositoryAmparoIndirectoDisconnected.CreateAcumularJuicioAsync(listValidos.ToArray(), entity, entityDocumento, dataFile);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return resultOperation;

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region CREATE DECLINAR COMPETENCIA

        public async Task<ResultOperation> CreateDeclinarCompetencia(DeclinarCompetencia entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                if (entity.fecha_recepcion_declinacion.Date > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de recepción de demanda no puede ser mayor a la fecha actual.");
                }
                var result = await _repositoryAmparoIndirectoDisconnected.CreateDeclinarCompetenciaAsync(entity, entityDocumento, dataFile);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(result.Result.GetValueOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Obtener todos los registros de Solicitud de Transparencia GetAll  
        public async Task<ResultOperation> GetSolicitudTransparenciaGetAll(int Fetch, int Page, int? id_numero_asunto)
        {
            try
            {

                var countResult = await _repositoryAmparoIndirectoOficialPartes.GetAllCountSolicitudTransparencia(id_numero_asunto);

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation<List<ResponseSolicitudTransparenciaLista>>.FailureWarningResponse("No se encontraron resultados");
                }


                var result = await _repositoryAmparoIndirectoOficialPartes.GetAllSolicitudTransparencia(Fetch, Page, id_numero_asunto);

                if (result is null)
                {
                    return ResultOperation<List<ResponseSolicitudTransparenciaLista>>.FailureWarningResponse("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseSolicitudTransparenciaLista>
                    (new(Page, Fetch, countResult.GetValueOrDefault()), result)
                );
            }
            catch (Exception)

            {
                throw;
            }
        }
        #endregion

        #region
        public async Task<ResultOperation> GetSolicitudTransparenciaById(int id)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoDisconnected.GetSolicitudById(id);
                if (result is null)
                {
                    return ResultOperation.FailureWarningResponse("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region Crea registro de Solicitud de Transparencia

        public async Task<ResultOperation> CreateSolicitudTransparencia(SolicitudTransparencia entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoDisconnected.CreateSolicitudTransparenciaAsync(entity, entityDocumento, dataFile);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(result.Result.GetValueOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Borrar registro de la Solicitud de Transparencia

        public async Task<ResultOperation<bool>> DeleteSolicitudTransparencia(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoDisconnected.EliminarSolicitudTransparencia(id);
                if (!result.Success)
                    return ResultOperation.FailureErrorResponse<bool>($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region GET ASUNTO CONTROL DOCUMENTAL
        public async Task<ResultOperation> GetAsuntoControlDocumental(string NumeroAsunto)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoOficialPartes.GetAsuntoControlDocumentalAsync(NumeroAsunto);

                if (result is null)
                {
                    return ResultOperation<ResponseAsuntoControlDocumental>.FailureWarningResponse("No se encontraron resultados");
                }
                //_redisClient.ValidateTake(result, EnumModulosRedis.AMPARO_INDIRECTO);
                var resultOperation = ResultOperation.SuccessResponseNoMessage(result);

                if (result.id_administracion > 0)
                {
                    //var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeAdministracion}/{result.administracion.Value}");
                    var catalogResponse = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeAdministracion}/{result.id_administracion}");
                    if (catalogResponse is not null && catalogResponse.Success && catalogResponse.Result is not null)
                    {
                        resultOperation.Result.administracion = catalogResponse.Result.nombre;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la administración.");
                }
                return ResultOperation.SuccessResponseNoMessage(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get BY ID AI
        public async Task<ResultOperation> GetByIdAmparo(int idNumeroAsunto)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoOficialPartes.GetByIdAmparoAsync(idNumeroAsunto);

                if (result is null)
                {
                    return ResultOperation<ResponseAmparoIndirectoByID>.FailureWarningResponse("No se encontraron resultados");
                }
                _redisClient.ValidateTake(result, EnumModulosRedis.AMPARO_INDIRECTO);
                var resultOperation = ResultOperation.SuccessResponseNoMessage(result);

                if (result.id_juzgado > 0)
                {
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.JuzgadoAmparo, result.id_juzgado.ToString());
                    if (!string.IsNullOrEmpty(catalogResponse))
                    {
                        resultOperation.Result.juzgado = catalogResponse;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del juzgado.");
                }
                if (result.id_materia > 0)
                {
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.Materia, result.id_materia.ToString());
                    //var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeMateria}/{result.materia.Value}");
                    if (!string.IsNullOrEmpty(catalogResponse))
                    {
                        resultOperation.Result.materia = catalogResponse;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la materia.");
                }
                if (result.id_submateria > 0)
                {
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.Submateria, result.id_submateria.ToString());
                    //var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeSubmateria}/{result.submateria.Value}");
                    if (!string.IsNullOrEmpty(catalogResponse))
                    {
                        resultOperation.Result.submateria = catalogResponse;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la submateria.");
                }
                //if (result.tipo_acto.Value > 0)
                //{
                //    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.TipoActo, result.tipo_acto.Value.ToString());
                //    if (!string.IsNullOrEmpty(catalogResponse))
                //    {
                //        resultOperation.Result.tipo_acto.Label = catalogResponse;
                //    }
                //    else
                //        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del tipo de acto.");
                //}
                // Nancy
                //if (result.tipo_acto.Value > 0)
                //{
                //    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.TipoActo, result.tipo_acto.Value.ToString());
                //    if (!string.IsNullOrEmpty(catalogResponse))
                //    {
                //        resultOperation.Result.tipo_acto.Label = catalogResponse;
                //    }
                //    else
                //        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del tipo de acto.");
                //}


                if (result.id_tipo_acto > 0)
                {
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.TipoActo, result.id_tipo_acto.ToString());
                    //var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeSubmateria}/{result.submateria.Value}");
                    if (!string.IsNullOrEmpty(catalogResponse))
                    {
                        resultOperation.Result.tipo_acto = catalogResponse;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la submateria.");
                }

                // Fin Nancy

                //if (result.autoridad_responsable.Value > 0)
                //{
                //    var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeAutoridadesResponsable}/{result.autoridad_responsable.Value}");
                //    if (responseTipoAsunto is not null && responseTipoAsunto.Success && responseTipoAsunto.Result is not null)
                //    {
                //        resultOperation.Result.autoridad_responsable.Label = responseTipoAsunto.Result.nombre;
                //    }
                //    else
                //        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del juzgado.");
                //}
                if (result.id_administracion > 0)
                {
                    //var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeAdministracion}/{result.administracion.Value}");
                    var catalogResponse = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeAdministracion}/{result.id_administracion}");
                    if (catalogResponse is not null && catalogResponse.Success && catalogResponse.Result is not null)
                    {
                        resultOperation.Result.administracion = catalogResponse.Result.nombre;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la administración.");
                }
                if (result.id_subadministracion > 0)
                {
                    var catalogResponse = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeSubadministracion}/{result.id_subadministracion}");
                    if (catalogResponse is not null && catalogResponse.Success && catalogResponse.Result is not null)
                    {
                        resultOperation.Result.subadministracion = catalogResponse.Result.nombre;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la subadminstración.");
                }
                //if (result.estado_tarea.Value > 0)
                //{
                //    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.EstadoTareaAmparo, result.estado_tarea.Value.ToString());
                //    if (!string.IsNullOrEmpty(catalogResponse))
                //    {
                //        resultOperation.Result.estado_tarea.Label = catalogResponse;
                //    }
                //    else
                //        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del estado tarea.");
                //}
                ////if (result.estado_tarea.Value > 0)
                ////{
                ////    var catalogResponse = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeEstadoTarea}/{result.estado_tarea.Value}");
                ////    if (catalogResponse is not null && catalogResponse.Success && catalogResponse.Result is not null)
                ////    {
                ////        resultOperation.Result.estado_tarea.Label = catalogResponse.Result.nombre;
                ////    }
                ////    else
                ////        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del estado tarea.");
                ////}
                ///

                if (result.id_estado_tarea > 0)
                {
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.EstadoTarea, result.id_estado_tarea.ToString());
                    if (!string.IsNullOrEmpty(catalogResponse))
                    {
                        resultOperation.Result.estado_tarea = catalogResponse;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del estado tarea.");
                }


                if (result.id_estado_procesal > 0)
                {
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.EstadoProcesalAmparo, result.id_estado_procesal.ToString());
                    if (!string.IsNullOrEmpty(catalogResponse))
                    {
                        resultOperation.Result.estado_procesal = catalogResponse;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del estado procesal.");
                }

                var resultAutoridades = await _repositoryAmparoIndirectoOficialPartes.GetAutoridadesResponsablesAsync(idNumeroAsunto);
                if (resultAutoridades is not null && resultAutoridades.Any())
                    resultOperation.Result.list_autoridad_responsable.AddRange(resultAutoridades);

                return ResultOperation.SuccessResponseNoMessage(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get Transparencia ById
        public async Task<ResultOperation> GetByIdTransparencia(int id)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoOficialPartes.GetByIdTransparenciaAsync(id);
                if (result is null)
                {
                    return ResultOperation.FailureWarningResponse("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public async Task<ResponseDocumentoAmparoIndirecto> GetArchivoAmparoIndirectoById(int id) =>
        await _repositoryArchivosAmparoIndirectoDisconnected.GetArchivosByIdAsyncAI(id);



        public async Task<ResponseDocumentoAmparoIndirecto> GetArchivoDiscconected(int id) =>
        await _repositoryArchivosAmparoIndirectoDisconnected.GetArchivoAsync(id);

        #region Get BY ID AI
        public async Task<ResultOperation> GetCabezaSerieFrontById(int idNumeroAsunto)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoOficialPartes.GetCabezaSerieFrontById(idNumeroAsunto);

                if (result is null)
                {
                    return ResultOperation<ResponseAcumularByFilters>.FailureWarningResponse("No se encontraron resultados");
                }
                var resultOperation = ResultOperation.SuccessResponseNoMessage(result);
                if (result.id_juzgado > 0)
                {
                    var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeAdministracion}/{result.id_administracion}");
                    if (responseTipoAsunto is not null && responseTipoAsunto.Success && responseTipoAsunto.Result is not null)
                    {
                        resultOperation.Result.administracion = responseTipoAsunto.Result.nombre;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del juzgado.");
                }

                if (result.id_juzgado > 0)
                {
                    var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeJuzgado}/{result.id_juzgado}");
                    if (responseTipoAsunto is not null && responseTipoAsunto.Success && responseTipoAsunto.Result is not null)
                    {
                        resultOperation.Result.juzgado = responseTipoAsunto.Result.nombre;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del juzgado.");
                }
                return ResultOperation.SuccessResponseNoMessage(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Crea registro
        public async Task<ResultOperation> CreateCanalizarAsunto(CanalizarAsunto entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                if (entity.fecha_canalizacion.Date > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de canalización de asunto no puede ser mayor a la fecha actual.");
                }
                var result = await _repositoryAmparoIndirectoDisconnected.CreateCanalizarAsuntoAsync(entity, entityDocumento, dataFile);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(result.Result.GetValueOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetAll Canalizar Asunto
        public async Task<ResultOperation> GetAmparoIndirectoCanalizarAsuntoGetAll(int Fetch, int Page, string orderByColumn, bool orderDesc)
        {
            try
            {
                var countResult = await _repositoryAmparoIndirectoOficialPartes.GetAllCanalizarCountAsyncAI();

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation.SuccessResponseNoMessage(
                        new DataTableView<ResponseAmparoIndirectoRemitir>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
                }

                var result = await _repositoryAmparoIndirectoOficialPartes.GetAllCanalizarAsyncAI(
                    Fetch,
                    Page,
                    orderByColumn,
                    orderDesc
                );
                if (result is null)
                {
                    return ResultOperation.FailureErrorResponse<List<ResponseAmparoIndirectoRemitir>>("Ha ocurrido un error al recuperar los datos");
                }
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseAmparoIndirectoRemitir>(new(Page, Fetch, countResult.GetValueOrDefault()),
                    result)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Documentos AI
        public async Task<ResultOperation> GetAllArchivoAmparoIndirecto(int idNumeroAsunto)
        {
            try
            {
                var result = await _repositoryArchivosAmparoIndirectoDisconnected.GetAllArchivoAmparoIndirectoAsync(idNumeroAsunto);

                if (result is null)
                {
                    return ResultOperation<List<ResponseDocumentoAmparoIndirecto>>.FailureWarningResponse("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseDocumentoAmparoIndirecto>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Documentos Seccion
        public async Task<ResultOperation> GetAllArchivoSeccion(int idNumeroAsunto, int idSeccion)
        {
            try
            {
                var result = await _repositoryArchivosAmparoIndirectoDisconnected.GetAllArchivoSeccionAsync(idNumeroAsunto, idSeccion);

                if (result is null)
                {
                    return ResultOperation<List<ResponseDocumentoAmparoIndirecto>>.FailureWarningResponse("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseDocumentoAmparoIndirecto>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get Documento by id
        public async Task<ResultOperation> GetArchivo(int id)
        {
            try
            {
                var result = await _repositoryArchivosAmparoIndirectoDisconnected.GetArchivoAsync(id);

                if (result is null)
                {
                    return ResultOperation<ResponseDocumentoAmparoIndirecto>.FailureWarningResponse("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(result.Adapt<ResponseDocumentoAmparoIndirecto>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public async Task<ResultOperation> GetDocumentosHistoricoAsync(int Fetch, int Page, string? OrderByColumn, bool OrderDesc, int id)
        {
            //List<int> listSecciones = new()
            //{
            //    EnumSecciones.AVISOS_Y_COMUNICADOS.GetHashCode(),
            //    EnumSecciones.AVISO_SIN_RESPUESTA.GetHashCode(),
            //    EnumSecciones.DATOS_GENERALES.GetHashCode(),
            //    EnumSecciones.EMISION_RESOLUCION.GetHashCode(),
            //    EnumSecciones.MEDIOS_DE_DEFENSA.GetHashCode(),
            //    EnumSecciones.REQUERIMIENTO.GetHashCode(),
            //    EnumSecciones.REQUERIMIENTO_PRODECON.GetHashCode(),
            //    EnumSecciones.SOLICITUD_DE_OPINION.GetHashCode(),
            //    EnumSecciones.SOLICITUD_DE_TRANSPARENCIA.GetHashCode(),
            //};

            var countResult = await _repositoryArchivosAmparoIndirectoDisconnected.GetDocumentosDisconnectedCount(id /*listSecciones,*/ /*null!, true*/);

            if (countResult is null || countResult <= 0)
            {
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseDocumentoAmparoIndirecto>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
            }

            Filters.GetDefaultPage(countResult.GetValueOrDefault(), ref Page, Fetch);

            var result = await _repositoryArchivosAmparoIndirectoDisconnected.GetDocumentosHistoricoDisconnected(
                //true,
                id,
                //listSecciones,
                //null!,
                //true,
                Fetch,
                Page,
                OrderByColumn,
                OrderDesc
                );

            if (result is null)
            {
                return ResultOperation.FailureWarningResponse<List<ResponseDocumentoAmparoIndirecto>>("No se encontraron resultados");
            }

            return ResultOperation.SuccessResponseNoMessage(
                new DataTableView<ResponseDocumentoAmparoIndirecto>(new(Page, Fetch, countResult.GetValueOrDefault()),
                result)
            );
        }

        #region

        public async Task<ResultOperation> GetDocumentosHistoricoSeccionAsync(int Fetch, int Page, string? OrderByColumn, bool OrderDesc, int id, int idSeccion)
        {
            //List<int> listSecciones = new()
            //{
            //    EnumSecciones.AVISOS_Y_COMUNICADOS.GetHashCode(),
            //    EnumSecciones.AVISO_SIN_RESPUESTA.GetHashCode(),
            //    EnumSecciones.DATOS_GENERALES.GetHashCode(),
            //    EnumSecciones.EMISION_RESOLUCION.GetHashCode(),
            //    EnumSecciones.MEDIOS_DE_DEFENSA.GetHashCode(),
            //    EnumSecciones.REQUERIMIENTO.GetHashCode(),
            //    EnumSecciones.REQUERIMIENTO_PRODECON.GetHashCode(),
            //    EnumSecciones.SOLICITUD_DE_OPINION.GetHashCode(),
            //    EnumSecciones.SOLICITUD_DE_TRANSPARENCIA.GetHashCode(),
            //};

            var countResult = await _repositoryArchivosAmparoIndirectoDisconnected.GetDocumentosSeccionDisconnectedCount(id, idSeccion/*listSecciones,*/ /*null!, true*/);

            if (countResult is null || countResult <= 0)
            {
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseDocumentoAmparoIndirecto>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
            }

            Filters.GetDefaultPage(countResult.GetValueOrDefault(), ref Page, Fetch);

            var result = await _repositoryArchivosAmparoIndirectoDisconnected.GetDocumentosHistoricoSeccionDisconnected(
                //true,
                id,
                idSeccion,
                //listSecciones,
                //null!,
                //true,
                Fetch,
                Page,
                OrderByColumn,
                OrderDesc
                );

            if (result is null)
            {
                return ResultOperation.FailureWarningResponse<List<ResponseDocumentoAmparoIndirecto>>("No se encontraron resultados");
            }

            return ResultOperation.SuccessResponseNoMessage(
                new DataTableView<ResponseDocumentoAmparoIndirecto>(new(Page, Fetch, countResult.GetValueOrDefault()),
                result)
            );
        }

        #endregion

        public async Task<List<ArchivosAmparoIndirecto>> GetArchivobyIds(int[] ids) =>
           await _repositoryArchivosAmparoIndirectoDisconnected.GetArchivosByIdsAsync(ids);

        #region Crea Documento

        public async Task<ResultOperation> CreateDocumentoAmparoIndirecto(ArchivosAmparoIndirecto entity, DataFile dataFile)
        {
            try
            {
                //dataFile.Path = entity.ruta;
                var result = await _repositoryArchivosAmparoIndirectoDisconnected.CreateDocumentoAmparoIndirectoAsync(entity, dataFile);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(result.Result.GetValueOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        public async Task<ResultOperation<bool>> CreateDocumentoList(List<ArchivosAmparoIndirecto> entityList, List<DataFile> dataFileList)
        {
            try
            {
                List<Message> messages = new List<Message>();
                bool error = false;
                for (int i = 0; i < entityList.Count; i++)
                {
                    dataFileList[i].Path = entityList[i].file_path;
                    //dataFileList[i].Path = entityList[i].file_path;
                    var result = await _repositoryArchivosAmparoIndirectoDisconnected.CreateDocumentoAmparoIndirectoAsync(entityList[i], dataFileList[i]);
                    if (!result.Success)
                    {
                        error = true;
                        messages.Add(new(TypeMessage.Error, $"{result.MsgError!}:{result.DetailError}"));
                    }
                }

                if (!error)
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                var resultOperation = ResultOperation.FailureErrorResponse<bool>("No se pudienton agregar correctamente los documentos");
                resultOperation.AddMessages(messages);
                return resultOperation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Update Documento

        public async Task<ResultOperation> UpdateDocumentoAmparoIndirecto(ArchivosAmparoIndirecto entity, DataFile dataFile)
        {
            try
            {
                //dataFile.Path = entity.ruta;
                var result = await _repositoryArchivosAmparoIndirectoDisconnected.UpdateDocumentoAmparoIndirectoAsync(entity, dataFile);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(result.Result.GetValueOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Eliminar documento

        public async Task<ResultOperation<bool>> DeleteArchivo(int[] idDocumento, string usuarioModificacion)
        {
            try
            {

                var result = await _repositoryArchivosAmparoIndirectoDisconnected.EliminarDocumentoAsyncAI(idDocumento, usuarioModificacion);
                if (!result.Success)
                    return ResultOperation.FailureErrorResponse<bool>($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Envio email
        public async Task<ResultOperation> EnvioEmail(Email entity)

        {
            try
            {
                using var httpClient = new HttpClient();

                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(string.Join(",", entity.To)), "To");
                formData.Add(new StringContent(string.Join(",", entity.Cc)), "Cc");
                formData.Add(new StringContent(entity.Subject), "Subject");
                formData.Add(new StringContent(entity.Body), "Body");
                formData.Add(new StringContent(entity.IsHtml.ToString()), "IsHtml");
                formData.Add(new StringContent(entity.Priority?.ToString() ?? string.Empty), "Priority");


                var response = await httpClient.PostAsync($"{_proxyEnpoints.RouteEmail}", formData);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Correo enviado exitosamente.");
                }
                else
                {
                    Console.WriteLine($"Error al enviar correo: {response.StatusCode}");
                }


                return ResultOperation.SuccessResponseNoMessage(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResultOperation> UpdateFechaVencimiento(string fechaVencimiento, int idAsunto, int idModulo, int idSeccion,  int idSeccionRenglon)
        {
            if (idModulo == 0 || idAsunto == 0 || idSeccionRenglon == 0)
            {
                var respuesta = ResultOperation.FailureErrorResponse<ResponseFechaVencimiento>("El modulo es incorrecto");
            }
            try
            {
                var result = await _repositoryArchivosAmparoIndirectoDisconnected.UpdateFechaVencimientoAsync(fechaVencimiento, idAsunto, idModulo, idSeccion, idSeccionRenglon);
                if (!result.Success)
                    return ResultOperation<int?>.FailureErrorResponse($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get fecha de vencimiento seccion
        public async Task<ResultOperation> GetAllFechaVencimientoSeccion(string fechaInicio, string fechaFinal, List<int> idSecciones)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoOficialPartes.GetAllFechaVencimientoSeccionAsync(fechaInicio, fechaFinal, idSecciones);
                //if (result is null)
                //{
                //    return ResultOperation < List < ResponseFechaVencimiento >>>.FailureWarningResponse("No se encontraron resultados");
                //}
                //_redisClient.ValidateTake(result, EnumModulosRedis.AMPARO_INDIRECTO);
                var resultOperation = ResultOperation.SuccessResponseNoMessage(result);
                return ResultOperation.SuccessResponseNoMessage(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
