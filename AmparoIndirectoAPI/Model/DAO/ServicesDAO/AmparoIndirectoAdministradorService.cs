using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.DTO.ContractsValidations;
using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.Entities.Events;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;
using Mapster;
using Microsoft.Extensions.Options;
using Sicoj.Utils.Enums;
using Sicoj.Utils.Extentions;
using Sicoj.Utils.Models;
using Sicoj.Utils.Redis;
using Sicoj.Utils.Middleware;
using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DAO.ServicesDAO
{
    public class AmparoIndirectoAdministradorService : IAmparoIndirectoAdministradorService
    {
        private readonly IApiService _apiService;
        private readonly IRedisClient _redisClient;
        private readonly CatalogosEnpoints _catologosEnpoints;
        private readonly ProxyEnpoints _proxyEnpoints;
        private readonly IAmparoIndirectoRepository _repositoryAmparoIndirectoDisconnected;
        private readonly IAmparoIndirectoAdministradorRepository _repositoryAmparoIndirectoAdministrador;
        //private readonly IArchivosAmparoIndirectoRepository _repositoryArchivosAmparoIndirectoDisconnected;
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

        public AmparoIndirectoAdministradorService(
            IConfiguration configuration,
            IAmparoIndirectoRepository repositoryAmparoIndirectoDisconnected,
            IAmparoIndirectoAdministradorRepository repositoryAmparoIndirectoAdministrador,
            IApiService apiService,
            IRedisClient redisClient,
            IOptions<CatalogosEnpoints> catologosEnpoints,
            IOptions<ProxyEnpoints> proxyEnpoints
            //IArchivosAmparoIndirectoRepository repositoryArchivosAmparoIndirectoDisconnected
            )
        {
            _repositoryAmparoIndirectoDisconnected = repositoryAmparoIndirectoDisconnected ?? throw new ArgumentNullException(nameof(repositoryAmparoIndirectoDisconnected));
            _repositoryAmparoIndirectoAdministrador = repositoryAmparoIndirectoAdministrador ?? throw new ArgumentNullException(nameof(repositoryAmparoIndirectoAdministrador));
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
            _catologosEnpoints = catologosEnpoints.Value ?? throw new ArgumentNullException(nameof(catologosEnpoints));
            _proxyEnpoints = proxyEnpoints.Value ?? throw new ArgumentNullException(nameof(proxyEnpoints));
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
            //_repositoryArchivosAmparoIndirectoDisconnected = repositoryArchivosAmparoIndirectoDisconnected ?? throw new ArgumentNullException(nameof(repositoryArchivosAmparoIndirectoDisconnected));
        }

        #region Update asignar
        public async Task<ResultOperation> UpdateAsignarAdministrador(AmparoIndirecto entity)
        {
            try
            {
                var responseAdministrador = await _apiService.GetResultOperationAsync<UserInformationView>($"{_catologosEnpoints.RouteInfoUsuario}/{entity.id_abogado}");
                if (responseAdministrador is null || !responseAdministrador.Success || responseAdministrador.Result is null)
                {
                    return ResultOperation.FailureErrorResponse<ResponseReasignar>($"No se pudo realizar la validación para verificar que el abogado seleccionado pertenezca a la administracion/subadministración.");
                }

                //if (result is null)
                //{
                //    return ResultOperation<ResponseAmparoIndirectoByID>.FailureWarningResponse("No se encontraron resultados");
                //}
                //var resultOperation = ResultOperation.SuccessResponseNoMessage(result);
                //if (!UserSession.ValidateUser(responseAdministrador!.Result!, EnumRolesSicoj.ABOGADO, EnumModulosSicoj.RECURSO_DE_REVOCACIÓN, out string message, false, null!, true, entity.id_administracion, true, entity.id_administracion, true, entity.id_subadministracion))
                //{
                //    return ResultOperation.FailureErrorResponse<ResponseAsignar>(message);
                //    //resultOperation.AddWarningMessage($"El Asunto con número {entity.juicio_amparo}");
                //}

                var result = await _repositoryAmparoIndirectoDisconnected.UpdateAsignarAbogadoAdministradorAsync(entity);
                if (!result.Success)
                    return ResultOperation.FailureErrorResponse<ResponseAsignar>($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(new ResponseAsignar()
                {
                    numero_asunto = entity.numero_asunto!,
                    abogado = responseAdministrador.Result.Rfc!
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Canalizar administrador
        public async Task<ResultOperation> CreateCanalizarAsuntoAdministrador(RequestCreateCanalizarAsuntoAdministrador entity)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                if (DateTime.Parse(entity.fecha_canalizacion).Date > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de canalización no puede ser mayor a la fecha actual.");
                }
                var result = await _repositoryAmparoIndirectoDisconnected.CreateCanalizarAsuntoAdministradorAsync(entity);
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

        #region Crea reasignar abogado

        public async Task<ResultOperation> CreateReasignarJuicio(RequestCreateReasignarJuicioAdministrador request)
        {
            try
            {
                var entityListExists = await _repositoryAmparoIndirectoAdministrador.GetByIdsJuicio(request.ids.ToArray());
                if (entityListExists is null || !entityListExists.Any())
                {
                    return (
                        ResultOperation.FailureErrorResponse(
                            "Los asuntos no existen."
                        )
                    );
                }

                //if (!request.ids.All(value => entityListExists.Select(x => x.id).Contains(value)))
                //{
                //    return Ok(
                //        ResultOperation.FailureErrorResponse(
                //            "Algunos juicios de amparo indirecto no existen."
                //        )
                //    );
                //}
                List<int> listValidos = new List<int>();
                List<int> listInvalidos = new List<int>();

                var resultOperation = ResultOperation.SuccessResponseNoMessage(new ResponseReasignar());

                var validIds = entityListExists.Select(e => e.id).ToHashSet();
                var entity = entityListExists.Select(r => r.id_administracion).FirstOrDefault();

                foreach (var ids in request.ids)
                {
                    var obj = entityListExists.FirstOrDefault(e => e.id == ids);
                    if (obj is null)
                    {
                        resultOperation.AddWarningMessage($"El Asunto con el identificador {ids} no existe o se eliminó.");
                        listInvalidos.Add(ids);
                        continue;
                    }
                    try
                    {
                        obj = AmparoIndirectoAdministradorEvents.UpdateReasignar(
                                ref obj,
                                request.id_abogado,
                                request.motivo_reasignacion
                            );

                        var responseAdministrador = await _apiService.GetResultOperationAsync<UserInformationView>($"{_catologosEnpoints.RouteInfoUsuario}/{request.id_abogado}");
                        if (responseAdministrador is null || !responseAdministrador.Success || responseAdministrador.Result is null)
                        {
                            return ResultOperation.FailureErrorResponse<ResponseReasignar>($"No se pudo realizar la validación para verificar que el abogado seleccionado pertenezca a la administracion/subadministración.");
                        }
                        if (!UserSession.ValidateUser(responseAdministrador!.Result!, EnumRolesSicoj.ABOGADO, EnumModulosSicoj.RECURSO_DE_REVOCACIÓN, out string message, false, null!, true, entity))
                        {
                            resultOperation.AddWarningMessage($"El Asunto con número {obj.numero_asunto}");
                        }

                        listValidos.Add(ids);
                    }
                    catch (Exception _e)
                    {
                        resultOperation.AddWarningMessage($"El Asunto con número {obj.numero_asunto}: {_e.Message}");
                        listInvalidos.Add(ids);
                        continue;
                    }
                }
                resultOperation.Result.ReasignacionesExitosas = listValidos.Count;
                resultOperation.Result.ReasignacionesIncorrectas = listInvalidos.Count;
                resultOperation.Result.abogado = request.id_abogado!;

                if (!listValidos.Any())
                {
                    return (resultOperation);
                }
                var result = await _repositoryAmparoIndirectoDisconnected.CreateReasignarJuicioAsuntoAsync(listValidos.ToArray(), request);
                if (!result.Success)
                    return ResultOperation.FailureErrorResponse<ResponseReasignar>($"{result.MsgError!}:{result.DetailError}");

                return resultOperation;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        public async Task<List<AmparoIndirecto>> GetAmparoIndirectoByIds(int[] ids) =>
        await _repositoryAmparoIndirectoAdministrador.GetByIdsJuicio(ids);


        #region GetById

        public async Task<AmparoIndirecto> GetAmparoIndirectoByIdDisconnected(int id) =>
            await _repositoryAmparoIndirectoDisconnected.GetByIdAsyncAI(id);
        #endregion

        #region Update registro
        public async Task<ResultOperation> UpdateAmparoIndirectoAdministrador(AmparoIndirecto entity)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoDisconnected.UpdateAmparoIndirectoAdministradorAsync(entity);
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

        #region GetAll Bandeja de Pendientes Administrador
        public async Task<ResultOperation> GetAllBandejaPendientesAdministrador(int Fetch, int Page, string orderByColumn, bool orderDesc)
        {
            try
            {
                var countResult = await _repositoryAmparoIndirectoAdministrador.GetAllAdmCount();

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation.SuccessResponseNoMessage(
                        new DataTableView<ResponseAmparoIndirectoAdministradorBandeja>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
                }

                var result = await _repositoryAmparoIndirectoAdministrador.GetAllAdm(
                    Fetch,
                    Page,
                    orderByColumn,
                    orderDesc
                );
                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }
                _redisClient.ValidateTakeList(ref result, EnumModulosRedis.AMPARO_INDIRECTO);
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseAmparoIndirectoAdministradorBandeja>(new(Page, Fetch, countResult.GetValueOrDefault()), result)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get Histórico 
        public async Task<ResultOperation> GetHistoricoByFilters(
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
            int? idEstadoProcesalIncidental)
        {
            try
            {
                var countResult = await _repositoryAmparoIndirectoAdministrador.GetHistoricoByFiltersCount(

                    fechaRecepcionInicial,
                    fechaRecepcionFinal,
                    fechaVencimientoDemandaInicial,
                    fechaVencimientoDemandaFinal,
                    numeroExpediete,
                    NumeroAsunto,
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
                    idEstadoProcesal,
                    idEstadoProcesalIncidental
                );

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation.SuccessResponseNoMessage(
                        new DataTableView<ResponseAmparoIndirectoByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
                }

                var result = await _repositoryAmparoIndirectoAdministrador.GetHistoricoByFilters(
                    Fetch,
                    Page,
                    OrderByColumn,
                    OrderDesc,
                    fechaRecepcionInicial,
                    fechaRecepcionFinal,
                    fechaVencimientoDemandaInicial,
                    fechaVencimientoDemandaFinal,
                    numeroExpediete,
                    NumeroAsunto,
                    idJuzgado,
                    nombreQuejoso,
                    idMateria,
                    idSubmateria,
                    idTipoActo,
                    despacho,
                    idAdministracion,
                    idSubadministracion,
                    idAutoridadResponsable,
                    rfcQuejoso,
                    idEstadoTarea,
                    idEstadoProcesal,
                    idEstadoProcesalIncidental

                );
                if (result is null)
                {
                    return ResultOperation.FailureWarningResponse<List<ResponseHistoricoAdministradorByFilters>>("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseHistoricoAdministradorByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()),
                    result)
                );
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
                var result = await _repositoryAmparoIndirectoAdministrador.GetByIdAmparoAsync(idNumeroAsunto);

                if (result is null)
                {
                    return ResultOperation<ResponseAmparoIndirectoByID>.FailureWarningResponse("No se encontraron resultados");
                }
                _redisClient.ValidateTake(result, EnumModulosRedis.AMPARO_INDIRECTO);
                var resultOperation = ResultOperation.SuccessResponseNoMessage(result);

                if (result.id_juzgado > 0)
                {
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.Juzgado, result.id_juzgado.ToString());
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
                if (result.id_tipo_acto > 0)
                {
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.TipoActo, result.id_tipo_acto.ToString());
                    if (!string.IsNullOrEmpty(catalogResponse))
                    {
                        resultOperation.Result.tipo_acto = catalogResponse;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del tipo de acto.");
                }
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
                    var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeAdministracion}/{result.id_administracion}");
                    if (responseTipoAsunto is not null && responseTipoAsunto.Success && responseTipoAsunto.Result is not null)
                    {
                        resultOperation.Result.administracion = responseTipoAsunto.Result.nombre;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la administración.");
                }
                if (result.id_subadministracion > 0)
                {
                    var responseTipoAsunto = await _apiService.GetResultOperationAsync<CatalogSicoj>($"{_routeSubadministracion}/{result.id_subadministracion}");
                    if (responseTipoAsunto is not null && responseTipoAsunto.Success && responseTipoAsunto.Result is not null)
                    {
                        resultOperation.Result.subadministracion = responseTipoAsunto.Result.nombre;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre de la subadminstración.");
                }
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
                if (result.id_estado_procesal_incidental > 0)
                {
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.EstadoProcesalAmparo, result.id_estado_procesal_incidental.ToString());
                    if (!string.IsNullOrEmpty(catalogResponse))
                    {
                        resultOperation.Result.estado_procesal_incidental = catalogResponse;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del estado procesal.");
                }
                var resultAutoridades = await _repositoryAmparoIndirectoAdministrador.GetAutoridadesResponsablesAsync(idNumeroAsunto);
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
    }
}
