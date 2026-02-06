using System.Configuration;
using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.DTO.ContractsValidations;
using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;
using Mapster;
using Microsoft.Extensions.Options;
using Sicoj.Utils.Enums;
using Sicoj.Utils.Extentions;
using Sicoj.Utils.Models;
using Sicoj.Utils.Redis;
using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Model.DAO.ServicesDAO
{
    public class AmparoIndirectoAbogadoService : IAmparoIndirectoAbogadoService
    {
        private readonly IAmparoIndirectoRepository _repositoryAmparoIndirectoDisconnected;
        private readonly IAmparoIndirectoAbogadoRepository _repositoryAmparoIndirectoAbogado;
        //private readonly IArchivosAmparoIndirectoRepository _repositoryArchivosAmparoIndirectoDisconnected;
        private readonly IApiService _apiService;
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

        public AmparoIndirectoAbogadoService(
            IConfiguration configuration,
            IAmparoIndirectoRepository repositoryAmparoIndirectoDisconnected,
            IAmparoIndirectoAbogadoRepository repositoryAmparoIndirectoAbogado,
            IArchivosAmparoIndirectoRepository repositoryArchivosAmparoIndirectoDisconnected,
            IApiService apiService,
            IRedisClient redisClient,
            IOptions<ProxyEnpoints> proxyEnpoints
            )
        {
            _repositoryAmparoIndirectoDisconnected = repositoryAmparoIndirectoDisconnected ?? throw new ArgumentNullException(nameof(repositoryAmparoIndirectoDisconnected));
            _repositoryAmparoIndirectoAbogado = repositoryAmparoIndirectoAbogado ?? throw new ArgumentNullException(nameof(repositoryAmparoIndirectoAbogado));
            //_repositoryArchivosAmparoIndirectoDisconnected = repositoryArchivosAmparoIndirectoDisconnected ?? throw new ArgumentNullException(nameof(repositoryArchivosAmparoIndirectoDisconnected));
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
        }

        #region GET Cuaderno Incidental
        public async Task<ResultOperation> GetCuadernoConstitucionalRecurrente(int idNumeroAsunto, int idEstadoProcesal)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoAbogado.GetCuadernoConstitucionalRecurrenteAsync(idNumeroAsunto, idEstadoProcesal);

                if (result is null)
                {
                    return ResultOperation<List<RequestCuadernoConstitucional>>.FailureWarningResponse("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<RequestCuadernoConstitucional>>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<ResultOperation> GetCuadernoConstitucional(int idNumeroAsunto)
        //{
        //    try
        //    {
        //        var result = await _repositoryAmparoIndirectoAbogado.GetCuadernoConstitucionalAsync(idNumeroAsunto);

        //        if (result is null)
        //        {
        //            return ResultOperation<List<RequestCuadernoConstitucional>>.FailureWarningResponse("No se encontraron resultados");
        //        }
        //        return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<RequestCuadernoConstitucional>>());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion
        public async Task<List<ResponseSentenciaConstitucional>> GetAllSentenciaConstitucionalDisconnected(int id) =>
            await _repositoryAmparoIndirectoAbogado.GetAllSentenciaConstitucionalAsync(id);

        public async Task<List<IncidenteExcesoIncidentalDisconnected>> GetAllIncidentePorExcesoIncidentalDisconnected(int id) =>
            await _repositoryAmparoIndirectoAbogado.GetAllIncidentePorExcesoIncidentalAsync(id);

        public async Task<List<ResponseSentenciaIncidental>> GetAllSentenciaIncidentalDisconnected(int id) =>
            await _repositoryAmparoIndirectoAbogado.GetAllSentenciaIncidentalAsync(id);

        public async Task<List<RecursoQuejaIncidentalDisconected>> GetAllRecursoQuejaIncidentalDisconnected(int id) =>
            await _repositoryAmparoIndirectoAbogado.GetAllRecursoQuejaIncidentalDisconnectedAsync(id);
        public async Task<List<RecursoRevisionIncidentalDisconnected>> GetAllRecursoRevisionIncidentalDisconnected(int id) =>
            await _repositoryAmparoIndirectoAbogado.GetAllRecursoRevisionIncidentalDisconnectedAsync(id);

        public async Task<List<ResponseInformePrevioIncidental>> GetAllInformePrevioIncidentalDisconnected(int id) =>
            await _repositoryAmparoIndirectoAbogado.GetAllInformePrevioIncidentalAsync(id);

        #region Get All Informe previo incidental
        public async Task<ResultOperation> GetAllInformePrevioIncidental(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllInformePrevioIncidentalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseInformePrevioIncidental>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Sentencia Incidental
        public async Task<ResultOperation> GetAllSentenciaIncidental(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllSentenciaIncidentalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseSentenciaIncidental>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Recurso Queja Incidental
        public async Task<ResultOperation> GetAllRecursoQuejaIncidental(int id/*, int recurrente*/)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoAbogado.GetAllRecursoQuejaIncidentalAsync(id/*, recurrente*/);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseRecursoQuejaIncidental>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Recurso Revision Incidental
        public async Task<ResultOperation> GetAllRecursoRevisionIncidental(int id)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoAbogado.GetAllRecursoRevisionIncidentalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseRecursoQuejaRevisionIncidental>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Update Cumplimiento fallo protector constitucional

        public async Task<ResultOperation> UpdateCumplimientoFalloProtector(int id)
        {
            try
            {
                //DateTime dateTime = DateTime.Now;
                //if (Convert.ToDateTime(entity.fecha_vencimiento) > dateTime.Date)
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimento de demanda no puede ser mayor a la fecha actual.");
                //}
                var result = await _repositoryAmparoIndirectoDisconnected.UpdateCumplimientoFalloProtectorAsync(id);
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

        #region Crea Cumplimiento fallo protector constitucional

        public async Task<ResultOperation> CreateCumplimientoFalloProtector(CumplimientoFalloProtectorConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                //if (Convert.ToDateTime(entity.fecha_notificacion_requerimiento) <= Convert.ToDateTime(entity.fecha_recepcion_demanda))
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de notificación de requerimiento no debe ser menor o igual a la fecha de recepción de la demanda.");
                //}
                if (Convert.ToDateTime(entity.fecha_notificacion_requerimiento) > dateTime)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de notificación de requerimiento no debe ser mayor a la fecha actual del sistema.");
                }

                if (Convert.ToDateTime(entity.fecha_presentacion_fallo) <= Convert.ToDateTime(entity.fecha_notificacion_requerimiento))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de presentación fallo no puede ser menor o igual a la fecha de notifiación de requerimiento.");
                }

                if (Convert.ToDateTime(entity.fecha_presentacion_fallo) > dateTime)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de presentación fallo no debe ser mayor a la fecha actual del sistema.");
                }

                if (Convert.ToDateTime(entity.fecha_oficio_comunicacion_fallo) <= Convert.ToDateTime(entity.fecha_vencimiento_requerimiento))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de oficio comunicación fallo no debe ser menor a la fecha de vencimiento.");
                }
                if (Convert.ToDateTime(entity.fecha_oficio_comunicacion_fallo) > dateTime)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de oficio comunicación fallo no debe ser mayor a la fecha actual del sistema.");
                }


                switch (entity.plazo_fallo)
                {
                    case 1:

                        var resposeFechaVencimientoFallo1 = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_notificacion_requerimiento.ToString("yyyy-MM-dd")}&addDays={3}&nextDay={false}");
                        if (resposeFechaVencimientoFallo1 is null || !resposeFechaVencimientoFallo1.Success || string.IsNullOrEmpty(resposeFechaVencimientoFallo1.Result))
                        {
                            return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento del requerimiento.");
                        }
                        if (!DateTime.TryParse(resposeFechaVencimientoFallo1.Result, out DateTime fechaVencimientoFallo1))
                        {
                            return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento del requerimiento no tiene el formato correcto.");
                        }
                        entity.fecha_vencimiento_requerimiento = fechaVencimientoFallo1;

                        break;

                    case 2:

                        var resposeFechaVencimientoFallo2 = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_notificacion_requerimiento.ToString("yyyy-MM-dd")}&addDays={5}&nextDay={true}");
                        if (resposeFechaVencimientoFallo2 is null || !resposeFechaVencimientoFallo2.Success || string.IsNullOrEmpty(resposeFechaVencimientoFallo2.Result))
                        {
                            return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento del requerimiento.");
                        }
                        if (!DateTime.TryParse(resposeFechaVencimientoFallo2.Result, out DateTime fechaVencimientoFallo2))
                        {
                            return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento del requerimiento no tiene el formato correcto.");
                        }
                        entity.fecha_vencimiento_requerimiento = fechaVencimientoFallo2;

                        break;

                    case 3:

                        var resposeFechaVencimientoFallo3 = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_notificacion_requerimiento.ToString("yyyy-MM-dd")}&addDays={10}&nextDay={true}");
                        if (resposeFechaVencimientoFallo3 is null || !resposeFechaVencimientoFallo3.Success || string.IsNullOrEmpty(resposeFechaVencimientoFallo3.Result))
                        {
                            return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento del requerimiento.");
                        }
                        if (!DateTime.TryParse(resposeFechaVencimientoFallo3.Result, out DateTime fechaVencimientoFallo3))
                        {
                            return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento del requerimiento no tiene el formato correcto.");
                        }
                        entity.fecha_vencimiento_requerimiento = fechaVencimientoFallo3;

                        break;

                    case 4:

                        var resposeFechaVencimientoFallo4 = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_notificacion_requerimiento.ToString("yyyy-MM-dd")}&addDays={15}&nextDay={true}");
                        if (resposeFechaVencimientoFallo4 is null || !resposeFechaVencimientoFallo4.Success || string.IsNullOrEmpty(resposeFechaVencimientoFallo4.Result))
                        {
                            return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento del requerimiento.");
                        }
                        if (!DateTime.TryParse(resposeFechaVencimientoFallo4.Result, out DateTime fechaVencimientoFallo4))
                        {
                            return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento del requerimiento no tiene el formato correcto.");
                        }
                        entity.fecha_vencimiento_requerimiento = fechaVencimientoFallo4;

                        break;
                }

                var result = await _repositoryAmparoIndirectoDisconnected.CreateCumplimientoFalloProtectorAsync(entity, entityDocumento, dataFile);
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

        #region Update estado sentencia constitucional

        public async Task<ResultOperation> UpdateSentenciaConstitucional(int id, string usuario)
        {
            try
            {
                //DateTime dateTime = DateTime.Now;
                //if (Convert.ToDateTime(entity.fecha_vencimiento) > dateTime.Date)
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimento de demanda no puede ser mayor a la fecha actual.");
                //}
                var result = await _repositoryAmparoIndirectoDisconnected.UpdateSentenciaConstitucionalAsync(id, usuario);
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

        #region Update estado incidente exceso incidental

        public async Task<ResultOperation> UpdateIncidenteExcesoIncidental(int id, string usuario)
        {
            try
            {
                //DateTime dateTime = DateTime.Now;
                //if (Convert.ToDateTime(entity.fecha_vencimiento) > dateTime.Date)
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimento de demanda no puede ser mayor a la fecha actual.");
                //}
                var result = await _repositoryAmparoIndirectoDisconnected.UpdateIncidenteExcesoAsync(id, usuario);
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

        #region Crea sentencia constitucional

        public async Task<ResultOperation> CreateSentenciaConstitucional(SentenciaConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                if (Convert.ToDateTime(entity.fecha_notificacion_sentencia) > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de notificación del sentencia no puede ser mayor a la fecha actual.");
                }

                //if (Convert.ToDateTime(entity.fecha_notificacion_sentencia) <= Convert.ToDateTime(entity.fecha_recepcion_auto_sentencia_ejecutoria))
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de notificación del sentencia no puede ser menor o iguial a la fecha de recepción.");
                //}

                //if (Convert.ToDateTime(entity.fecha_conclusion_expediente) <= Convert.ToDateTime(entity.fecha_recepcion_auto_sentencia_ejecutoria))
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de concliusión del expediente no puede ser menor o iguial a la fecha de recepción.");
                //}
                //if (Convert.ToDateTime(entity.fecha_conclusion_expediente) < Convert.ToDateTime(entity.fecha_notificacion_sentencia))
                //{   
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de concliusión del expediente no puede ser menor a la fecha de notificación de sentencia.");
                //}

                //if (Convert.ToDateTime(entity.fecha_conclusion_expediente) < Convert.ToDateTime(entity.fecha_oficio_comunicacion_sentencia))
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de concliusión del expediente no puede ser menor a la fecha de oficio de comuniación de sentencia.");
                //}

                //if (Convert.ToDateTime(entity.fecha_conclusion_expediente) < Convert.ToDateTime(entity.fecha_recepcion_auto_sentencia_ejecutoria))
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de concliusión del expediente no puede ser menor a la fecha de recepción.");
                //}


                var result = await _repositoryAmparoIndirectoDisconnected.CreateSentenciaConstitucionalAsync(entity, entityDocumento, dataFile);
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

        #region Crea sentencia incidental

        public async Task<ResultOperation> CreateSentenciaIncidental(SentenciaIncidental entity)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoDisconnected.CreateSentenciaIncidentalAsync(entity);
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

        #region Crea recurso queja incidental

        public async Task<ResultOperation> CreateRecursoQuejaIncidental(RecursoQuejaIncidental entity)
        {
            try
            {

                var resposeFechaVencimientoRecu = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_recepcion_apertura.ToString("yyyy-MM-dd")}&addDays={2}&nextDay={true}");
                if (resposeFechaVencimientoRecu is null || !resposeFechaVencimientoRecu.Success || string.IsNullOrEmpty(resposeFechaVencimientoRecu.Result))
                {
                    return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento de recurso queja.");
                }
                if (!DateTime.TryParse(resposeFechaVencimientoRecu.Result, out DateTime fechaVencimientoRecu))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento de recurso queja no tiene el formato correcto.");
                }
                entity.fecha_vencimiento_recurso_queja = fechaVencimientoRecu;

                var result = new ResultTransaction();
                if (entity.recurso_queja == true)
                {
                    switch (entity.id_recurrente)
                    {
                        case 1:
                            result = await _repositoryAmparoIndirectoDisconnected.CreateRecursoQuejaIncidentalAsync(entity);
                            break;

                        case 2:
                            result = await _repositoryAmparoIndirectoDisconnected.CreateRecursoQuejaIncidentalQuejosoAsync(entity);
                            break;

                        case 3:
                            result = await _repositoryAmparoIndirectoDisconnected.CreateRecursoQuejaIncidentalOtrasAutoridadesAsync(entity);
                            break;
                    }
                }

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

        #region Crea informe previo incidental

        public async Task<ResultOperation> CreateInformePrevioIncidental(InformePrevioIncidental entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                if (Convert.ToDateTime(entity.fecha_vencimiento) > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimento de informe precio no puede ser mayor a la fecha actual.");
                }

                var resposeFechaVencimientoInf = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_apertura_incidente.ToString("yyyy-MM-dd")}&addDays={2}&nextDay={true}");
                if (resposeFechaVencimientoInf is null || !resposeFechaVencimientoInf.Success || string.IsNullOrEmpty(resposeFechaVencimientoInf.Result))
                {
                    return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento de informe previo.");
                }
                if (!DateTime.TryParse(resposeFechaVencimientoInf.Result, out DateTime fechaVencimientoInf))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento de informe previo no tiene el formato correcto.");
                }
                entity.fecha_vencimiento = fechaVencimientoInf;

                var result = await _repositoryAmparoIndirectoDisconnected.CreateInformePrevioIncidentalAsync(entity, entityDocumento, dataFile);
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

        #region Get All Información adicional
        public async Task<ResultOperation> GetAllInformacionAdicional(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllInformacionAdicionalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseInformacionAdicional>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region Get All Recurso Inconformidad
        public async Task<ResultOperation> GetAllRecursoInconformidad(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllRecursoInconformidadAsync(id);

                if (result is null)
                {
                    return ResultOperation<ResponseRecursoIncondormidad>.SuccessResponseNoMessage(true);
                    //return ResultOperation<ResponseRecursoIncondormidad>.FailureWarningResponse("No se encontraron resultados");
                }

                return ResultOperation.SuccessResponseNoMessage(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Recurso Reclamación
        public async Task<ResultOperation> GetAllRecursoReclamacion(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllRecursoReclamacionAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseRecursoReclamacion>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Informe Justificado Constitucional
        public async Task<ResultOperation> GetAllInformeJustificadoConstitucional(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllInformeJustificadoConstitucionalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseInformeJustificadoConstitucional>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Recurso Queja Principal Constitucional
        public async Task<ResultOperation> GetAllRecursoQuejaPrincipalConstitucional(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllRecursoQuejaPrincipalConstitucionalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseRecursoQuejaPrincipalConstitucional>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Recurso Revision Constitucional
        public async Task<ResultOperation> GetAllRecursoRevisionConstitucional(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllRecursoRevisionConstitucionalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseRecursoRevisionPrincipal>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Sentencia Constitucional
        public async Task<ResultOperation> GetAllSentenciaConstitucional(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllSentenciaConstitucionalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseSentenciaConstitucional>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Cumplimiento Fallo Protector Constitucional
        public async Task<ResultOperation> GetAllCumplimientoFalloConstitucional(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllCumplimientoFalloConstitucionalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseCumplimientoFalloProtector>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Crear Autoridad Responsable

        public async Task<ResultOperation> CreateAutoridadResponsableAbogado(RequestCreateAutoridadResponsableAbogado request)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoDisconnected.CreateAutoridadesResponsablesAsync(request);
                if (!result.Success)
                    return ResultOperation.FailureErrorResponse<ResponseReasignar>($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(true);


            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Crear Información adicional

        public async Task<ResultOperation> CreateInformacionAdicional(InformacionAdicional request)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoDisconnected.CreateInformacionAdicionalAsync(request);
                if (!result.Success)
                    return ResultOperation.FailureErrorResponse<ResponseReasignar>($"{result.MsgError!}:{result.DetailError}");

                return ResultOperation.SuccessResponseNoMessage(true);


            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region GetById

        public async Task<AmparoIndirecto> GetAmparoIndirectoByIdDisconnected(int id) =>
            await _repositoryAmparoIndirectoDisconnected.GetByIdAsyncAI(id);
        #endregion

        #region GetByIdAutoridadesResponsables

        public async Task<List<AutoridadesResponsables>> GetAutoridadesResponsablesIdDisconnected(int id) =>
            await _repositoryAmparoIndirectoDisconnected.GetAutoridadesResponsablesIdAsync(id);

        public async Task<List<AutoridadesResponsables>> GetAutoridadesResponsablesIdRealDisconnected(int id) =>
            await _repositoryAmparoIndirectoDisconnected.GetAutoridadesResponsablesIdDisconnectedAsync(id);
        #endregion

        #region GetById Cuaderno Constitucional

        public async Task<List<RequestCuadernoConstitucional>> GetCuadernoConstitucionalIdDisconnected(int id) =>
            await _repositoryAmparoIndirectoAbogado.GetCuadernoConstitucionalAsync(id);
        #endregion

        public async Task<List<RecursoRevisionPrincipalConstitucionalDisconnected>> GetRecursoRevisionPrincipalIdDisconnected(int id) =>
            await _repositoryAmparoIndirectoAbogado.GetRecursoRevisionPrincipalIdDisconnectedAsync(id);

        public async Task<List<ResponseInformeJustificadoConstitucional>> GetIndormeJustificadoIdDisconnected(int id) =>
           await _repositoryAmparoIndirectoAbogado.GetAllInformeJustificadoConstitucionalAsync(id);

        #region Historico
        public async Task<ResultOperation> GetHistoricoAbogadoByFilters(
            int Fetch,
            int Page,
            string OrderByColumn,
            bool OrderDesc,
            DateTime? fechaRecepcionInicial,
            DateTime? fechaRecepcionFinal,
            DateTime? fechaInicialVencimiento,
            DateTime? fechaFinalVencimiento,
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

                var countResult = await _repositoryAmparoIndirectoAbogado.GetHistoricoAbogadoByFiltersCount(
                fechaRecepcionInicial,
                fechaRecepcionFinal,
                fechaInicialVencimiento,
                fechaFinalVencimiento,
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

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation.SuccessResponseNoMessage(
                        new DataTableView<ResponseHistoricoAbogadoByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
                }

                var result = await _repositoryAmparoIndirectoAbogado.GetHistoricoAbogadoByFilters(
                    Fetch,
                    Page,
                    OrderByColumn,
                    OrderDesc,
                    fechaRecepcionInicial,
                    fechaRecepcionFinal,
                    fechaInicialVencimiento,
                    fechaFinalVencimiento,
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
                    idEstadoProcesalIncidental);

                if (result is null)
                {
                    //ResponseAmparoIndirectoByFilters
                    return ResultOperation.FailureWarningResponse<List<ResponseHistoricoAbogadoByFilters>>("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseHistoricoAbogadoByFilters>(new(Page, Fetch, countResult.GetValueOrDefault()), result)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetAll Bandeja de Pendientes Abogado
        public async Task<ResultOperation> GetAllBandejaPendientesAbogado(int Fetch, int Page, string orderByColumn, bool orderDesc)
        {
            try
            {
                var countResult = await _repositoryAmparoIndirectoAbogado.GetAllAbogadoCount();

                if (countResult is null || countResult <= 0)
                {
                    return ResultOperation.SuccessResponseNoMessage(
                        new DataTableView<ResponseAmparoIndirectoAbogadoBandeja>(new(Page, Fetch, countResult.GetValueOrDefault()), new()));
                }

                var result = await _repositoryAmparoIndirectoAbogado.GetAllAbogado(
                    Fetch,
                    Page,
                    orderByColumn,
                    orderDesc
                );
                if (result is null)
                {
                    return ResultOperation.FailureErrorResponse<List<ResponseAmparoIndirectoAbogadoBandeja>>("Ha ocurrido un error al recuperar los datos");
                }
                _redisClient.ValidateTakeList(ref result, EnumModulosRedis.AMPARO_INDIRECTO);
                return ResultOperation.SuccessResponseNoMessage(
                    new DataTableView<ResponseAmparoIndirectoAbogadoBandeja>(new(Page, Fetch, countResult.GetValueOrDefault()), result)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Crear nota de litigio
        public async Task<ResultOperation> CreateNotaLitigio(NotaLitigio entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                if (entity.fecha_registro_nota.Date > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de registro de nota de litigio no puede ser mayor a la fecha actual.");
                }

                var result = await _repositoryAmparoIndirectoAbogado.CreateNotaLitigioAsync(entity, entityDocumento, dataFile);

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

        #region Eliminar Nota de Litigo

        public async Task<ResultOperation<bool>> UpdateDeleteNotaLitigio(int idNotaLitigio)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoAbogado.UpdateEliminarNotaLitigio(idNotaLitigio);
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

        #region GetByID Nota de Litigio

        public async Task<ResultOperation> GetByIdNotaLitigio(int id)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoAbogado.GetByIdNotaLitigioAsync(id);

                if (result is null)
                {
                    return ResultOperation.FailureErrorResponse<List<ResponseNotaLitigioById>>("No se encontraron resultados");
                }
                return ResultOperation.SuccessResponseNoMessage(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region Crea registro de Informe Justificado

        public async Task<ResultOperation> CreateInformeJustificado(InformeJustificadoConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {

                //DateTime dateTime = DateTime.Now;


                //if (Convert.ToDateTime(entity.fecha_recepcion_demanda) > dateTime.Date)
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de recepción de demanda no puede ser mayor a la fecha actual.");
                //}

                if (Convert.ToDateTime(entity.fecha_presentacion_informe_justificado) < Convert.ToDateTime(entity.fecha_recepcion_demanda))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de presentación del Informe Justificado no puede ser menor a la fecha de recepcion de la demanda.");
                }

                if (Convert.ToDateTime(entity.fecha_presentacion_informe_justificado) > Convert.ToDateTime(entity.fecha_vencimiento_justificado))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de presentación del Informe Justificado no puede ser mayor a la fecha de vencimiento.");
                }

                if (Convert.ToDateTime(entity.fecha_presentacion_informe_justificado) <= Convert.ToDateTime(entity.fecha_oficio_informe_justificado))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de presentación del Informe Justificado no puede ser menor o igual a la fecha de oficio del informe justificado.");
                }

                var resposeFechaVencimientoJust = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_recepcion_demanda.ToString("yyyy-MM-dd")}&addDays={15}&nextDay={true}");
                if (resposeFechaVencimientoJust is null || !resposeFechaVencimientoJust.Success || string.IsNullOrEmpty(resposeFechaVencimientoJust.Result))
                {
                    return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento justificado.");
                }
                if (!DateTime.TryParse(resposeFechaVencimientoJust.Result, out DateTime fechaVencimientoJust))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento justificado no tiene el formato correcto.");
                }
                entity.fecha_vencimiento_justificado = fechaVencimientoJust;

                var result = await _repositoryAmparoIndirectoAbogado.CreateInformeJustificadoAsync(entity, entityDocumento, dataFile);

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

        #region Update Informe Previo Incidental

        public async Task<ResultOperation> UpdateInformePrevioIncidental(int id, string usuario)
        {
            try
            {
                //DateTime dateTime = DateTime.Now;
                //if (Convert.ToDateTime(entity.fecha_vencimiento) > dateTime.Date)
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimento de demanda no puede ser mayor a la fecha actual.");
                //}
                var result = await _repositoryAmparoIndirectoDisconnected.UpdateInformePrevioIncidentalAsync(id, usuario);
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

        #region Update Informe Previo Incidental

        public async Task<ResultOperation> UpdateSentenciaIncidental(int id, string usuario)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoDisconnected.UpdateSentenciaIncidentalAsync(id, usuario);
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

        #region

        public async Task<ResultOperation> UpdateRecursoQuejaIncidental(int id, string usuario)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoDisconnected.UpdateRecursoQuejaIncidentalAsync(id, usuario);
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

        #region

        public async Task<ResultOperation> UpdateRecursoRevisionIncidental(int id, bool recursoRevisionIncidental /*string usuario*/)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoDisconnected.UpdateRecursoRevisionIncidentalAsync(id, recursoRevisionIncidental);
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

        #region Update informe justificado constitucional

        public async Task<ResultOperation> UpdateInformeJustificadoConstitucional(int id, string usuario)
        {
            try
            {
                //DateTime dateTime = DateTime.Now;
                //if (Convert.ToDateTime(entity.fecha_vencimiento) > dateTime.Date)
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimento de demanda no puede ser mayor a la fecha actual.");
                //}
                var result = await _repositoryAmparoIndirectoDisconnected.UpdateInformeJustificadoConstitucionalAsync(id, usuario);
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

        #region Crea registro de Recurso de Queja Constitucional

        public async Task<ResultOperation> CreateRecursoQuejaConstitucional(RecursoQuejaPrincipalConstitucional entity)
        {
            try
            {


                var resposeFechaVencimientoRecu = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_recepcion_acuerdo_queja.ToString("yyyy-MM-dd")}&addDays={5}&nextDay={true}");
                if (resposeFechaVencimientoRecu is null || !resposeFechaVencimientoRecu.Success || string.IsNullOrEmpty(resposeFechaVencimientoRecu.Result))
                {
                    return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento de recurso de queja.");
                }
                if (!DateTime.TryParse(resposeFechaVencimientoRecu.Result, out DateTime fechaVencimientoRecu))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento recurso queja no tiene el formato correcto.");
                }
                entity.fecha_vencimiento_recurso_queja = fechaVencimientoRecu;

                var result = new ResultTransaction();
                if (entity.recurso_queja_principal == true)
                {
                    switch (entity.id_recurrente)
                    {
                        case 1:
                            result = await _repositoryAmparoIndirectoAbogado.CreateRecursoQuejaConstitucionalResponsableAsync(entity);
                            break;

                        case 2:
                            result = await _repositoryAmparoIndirectoAbogado.CreateRecursoQuejaConstitucionalQuejosoAsync(entity);
                            break;

                        case 3:
                            result = await _repositoryAmparoIndirectoAbogado.CreateRecursoQuejaConstitucionalOtrosAsync(entity);
                            break;
                    }
                }


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
        #region Crea registro de Recurso de revision incidental

        public async Task<ResultOperation> CreateRecursoRevisionIncidental(RecursoRevisionIncidental entity)
        {
            try
            {
                //FECHA VENCIMIENTO DE RECURSO DE REVISION
                var resposeFechaVencimientoRevi = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_recepcion_sentencia.ToString("yyyy-MM-dd")}&addDays={10}&nextDay={true}");
                if (resposeFechaVencimientoRevi is null || !resposeFechaVencimientoRevi.Success || string.IsNullOrEmpty(resposeFechaVencimientoRevi.Result))
                {
                    return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento de recurso de revision.");
                }
                if (!DateTime.TryParse(resposeFechaVencimientoRevi.Result, out DateTime fechaVencimientoRevi))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento de recurso de revision no tiene el formato correcto.");
                }
                entity.fecha_vencimiento_recurso_revision = fechaVencimientoRevi;


                //FECHA VENCIMIENTO DE LA ADHESION
                var resposeFechaVencimientoAdhe = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_admision.ToString("yyyy-MM-dd")}&addDays={5}&nextDay={true}");
                if (resposeFechaVencimientoAdhe is null || !resposeFechaVencimientoAdhe.Success || string.IsNullOrEmpty(resposeFechaVencimientoAdhe.Result))
                {
                    return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento de adhesion.");
                }
                if (!DateTime.TryParse(resposeFechaVencimientoAdhe.Result, out DateTime fechaVencimientoAdhe))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento de adhesion no tiene el formato correcto.");
                }
                entity.fecha_vencimiento_adhesion = fechaVencimientoAdhe;


                var result = new ResultTransaction();
                if (entity.recurso_revision_incidental == true)
                {
                    switch (entity.id_recurrente)
                    {
                        case 1:
                            result = await _repositoryAmparoIndirectoAbogado.CreateRecursoRevisionIncidentalResponsableAsync(entity);
                            break;

                        case 2:
                            result = await _repositoryAmparoIndirectoAbogado.CreateRecursoRevisionIncidentalQuejosoAsync(entity);
                            break;

                        case 3:
                            result = await _repositoryAmparoIndirectoAbogado.CreateRecursoRevisionIncidentalOtrosAsync(entity);
                            break;
                    }
                }


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

        #region Update recurso queja constitucional

        public async Task<ResultOperation> UpdateRecursoQuejaConstitucional(int id, bool recursoQuejaPrincipal)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoDisconnected.UpdateRecursoQuejaConstitucionalAsync(id, recursoQuejaPrincipal);
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

        #region Update Recurso Revision constitucional

        public async Task<ResultOperation> UpdateRecursoRevisionConstitucional(int id, bool recursoRevisionPrincipal)
        {
            try
            {
                //DateTime dateTime = DateTime.Now;
                //if (Convert.ToDateTime(entity.fecha_vencimiento) > dateTime.Date)
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimento de demanda no puede ser mayor a la fecha actual.");
                //}
                var result = await _repositoryAmparoIndirectoDisconnected.UpdateRecursoRevisionConstitucionalAsync(id, recursoRevisionPrincipal);
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


        #region Crea registro de Recurso de Revision Constitucional
        public async Task<ResultOperation> CreateRecursoRevisionConstitucional(RecursoRevisionPrincipalConstitucional entity)
        {

            //FECHA VENCIMIENTO RECURSO DE REVISION
            var resposeFechaVencimientoRevi = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_recepcion_sentencia.ToString("yyyy-MM-dd")}&addDays={10}&nextDay={true}");
            if (resposeFechaVencimientoRevi is null || !resposeFechaVencimientoRevi.Success || string.IsNullOrEmpty(resposeFechaVencimientoRevi.Result))
            {
                return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento recurso revision.");
            }
            if (!DateTime.TryParse(resposeFechaVencimientoRevi.Result, out DateTime fechaVencimientoRevi))
            {
                return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento recurso revision no tiene el formato correcto.");
            }
            entity.fecha_vencimiento_recurso_revision = fechaVencimientoRevi;


            //FECHA VENCIMIENTO DE ADHESION
            var resposeFechaVencimientoAdhe = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_admision.ToString("yyyy-MM-dd")}&addDays={5}&nextDay={true}");
            if (resposeFechaVencimientoAdhe is null || !resposeFechaVencimientoAdhe.Success || string.IsNullOrEmpty(resposeFechaVencimientoAdhe.Result))
            {
                return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento de adhesion.");
            }
            if (!DateTime.TryParse(resposeFechaVencimientoAdhe.Result, out DateTime fechaVencimientoAdhe))
            {
                return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento de adhesion no tiene el formato correcto.");
            }
            entity.fecha_vencimiento_adhesion = fechaVencimientoAdhe;

            try
            {
                var result = new ResultTransaction();
                if (entity.recurso_revision_principal == true)
                {
                    switch (entity.id_recurrente)
                    {
                        case 1:
                            result = await _repositoryAmparoIndirectoAbogado.CreateRecursoRevisionConstitucionalResponsableAsync(entity);
                            break;

                        case 2:
                            result = await _repositoryAmparoIndirectoAbogado.CreateRecursoRevisionConstitucionalQuejosoAsync(entity);
                            break;

                        case 3:
                            result = await _repositoryAmparoIndirectoAbogado.CreateRecursoRevisionConstitucionalOtrosAsync(entity);
                            break;
                    }
                }
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

        #region Crea registro de Recurso de Inconformidad Constitucional
        public async Task<ResultOperation> CreateRecursoInconformidadConstitucional(RecursoInconformidadConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                //fecha_notificacion_resolucion_inconformidad
                if (entity.notificacion_admision_recurso_inconformidad > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de notificación resolución incondormidad no puede ser mayor a la fecha actual del sistema.");
                }

                //if (entity.notificacion_admision_recurso_inconformidad <= entity.fecha_recepcion_demanda)
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de notificación resolución incondormidad no puede ser menor a la fecha de recepción de demanda.");
                //}

                if (entity.fecha_notificacion_resolucion_inconformidad > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de notificación resolución incondormidad no puede ser mayor a la fecha actual del sistema.");
                }
                if (entity.fecha_notificacion_resolucion_inconformidad <= entity.notificacion_admision_recurso_inconformidad)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de notificación resolución incondormidad no puede ser menor a la notificación de admision de recurso de inconformidad.");
                }

                var result = await _repositoryAmparoIndirectoAbogado.CreateRecursoInconformidadConstitucionalAsync(entity, entityDocumento, dataFile);

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

        #region Crea registro de Recurso de Reclamación Constitucional
        public async Task<ResultOperation> CreateRecursoReclamacionConstitucional(RecursoReclamacionConstitucional entity, ArchivosAmparoIndirecto entityDocumento, DataFile dataFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                if (entity.notificacion_admision_recurso_reclamacion > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de Notificación de admision de recurso de reclamación no puede ser mayor a la fecha actual.");
                }

                //if (entity.fecha_notificacion_acuerdo <= entity.fecha_recepcion_demanda)
                //{
                //    return ResultOperation.FailureWarningResponse<int>("La fecha de fecha notificacion acuerdo no puede ser mayor a la fecha actual.");
                //}

                if (entity.fecha_presentacion <= entity.notificacion_admision_recurso_reclamacion)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de fecha prsentación no puede ser menor o igual a la fecha de admisión recurso reclamación.");
                }
                if (entity.fecha_notificacion_resolucion <= entity.notificacion_admision_recurso_reclamacion)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de fecha notificación resolución no puede ser menor o igual a la notificacion admisión recurso reclamación.");
                }
                if (entity.fecha_notificacion_resolucion <= entity.fecha_vencimiento_recurso_reclamacion)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de fecha notificación resolución no puede ser menor o igual a la fecha de vencimiento de recurso de reclamación.");
                }
                if (entity.fecha_notificacion_resolucion <= entity.fecha_presentacion)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de fecha notificación resolución no puede ser menor o igual a la fecha de presentación.");
                }



                //FECHA VENCIMIENTO RECURSO RECLAMACION
                var resposeFechaVencimientoRec = await _apiService.GetResultOperationAsync<string>($"{_proxyEnpoints.RouteFechasAutoCalculadas}?idModule={EnumModulosSicoj.AMPARO_INDIRECTO.GetHashCode()}&baseDate={entity.fecha_notificacion_acuerdo.ToString("yyyy-MM-dd")}&addDays={3}&nextDay={true}");
                if (resposeFechaVencimientoRec is null || !resposeFechaVencimientoRec.Success || string.IsNullOrEmpty(resposeFechaVencimientoRec.Result))
                {
                    return ResultOperation.FailureWarningResponse<int>("No se pudo calcular la fecha de vencimiento de recurso de reclamacion.");
                }
                if (!DateTime.TryParse(resposeFechaVencimientoRec.Result, out DateTime fechaVencimientoReclamacion))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de vencimiento de recurso de reclamacion no tiene el formato correcto.");
                }
                entity.fecha_vencimiento_recurso_reclamacion = fechaVencimientoReclamacion;

                var result = await _repositoryAmparoIndirectoAbogado.CreateRecursoReclamacionConstitucionalAsync(entity,entityDocumento, dataFile);

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


        #region Get BY ID AI
        public async Task<ResultOperation> GetByIdAmparo(int idNumeroAsunto)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoAbogado.GetByIdAmparoAsync(idNumeroAsunto);

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
                if (result.id_submateria> 0)
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
                    var catalogResponse = await _redisClient.GetCatalogValue(EnumCatalogos.EstadoProcesalAmparo, result.id_estado_procesal.ToString());
                    if (!string.IsNullOrEmpty(catalogResponse))
                    {
                        resultOperation.Result.estado_procesal_incidental = catalogResponse;
                    }
                    else
                        resultOperation.AddWarningMessage("No se pudo recuperar el nombre del estado procesal.");
                }
                var resultAutoridades = await _repositoryAmparoIndirectoAbogado.GetAutoridadesResponsablesAsync(idNumeroAsunto);
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

        #region Crea registro de Suspension Provisional

        public async Task<ResultOperation> CreateSuspensionProvisionalAbogado(SuspensionProvisional entity)
        {
            try
            {
                var result = await _repositoryAmparoIndirectoAbogado.CreateSuspensionProvisionalAbogadoAsync(entity);
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

        #region Crea registro de Incidente por Exceso o Defecto de Cumplimiento

        public async Task<ResultOperation> CreateIncidenteExcesoIncidental(IncidenteExcesoIncidental entity)
        {
            try
            {

                DateTime dateTime = DateTime.Now;
                if (Convert.ToDateTime(entity.fecha_notificacion_acuerdo) > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de notificación del acuerdo no puede ser mayor a la fecha actual.");
                }
                if (Convert.ToDateTime(entity.fecha_oficio_desahogo) > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de oficio de desahogo no puede ser mayor a la fecha actual.");
                }
                if (Convert.ToDateTime(entity.fecha_oficio_desahogo) < Convert.ToDateTime(entity.fecha_notificacion_acuerdo))
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de oficio de desahogo no puede ser menor que la fecha de notificación del acuerdo.");
                }
                if (Convert.ToDateTime(entity.fecha_oficio_comunicacion) > dateTime.Date)
                {
                    return ResultOperation.FailureWarningResponse<int>("La fecha de oficio de comunicación no puede ser mayor a la fecha actual.");
                }
                var result = await _repositoryAmparoIndirectoAbogado.CreateIncidenteExcesoIncidentalAsync(entity);
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

        #region Get All Incidente por Exceso Incidental
        public async Task<ResultOperation> GetAllIncidenteExcesoIncidental(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllIncidenteExcesoIncidentalAsync(id);

                if (result is null || !result.Any())
                {
                    return ResultOperation.SuccessResponseNoMessage(true);
                }

                return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseIncidenteExcesoIncidental>>());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        //#region Get All Suspension Provisional Incidental  ---------------AQUI
        //public async Task<ResultOperation> GetAllSuspensionProvisionalIncidental(int id)
        //{
        //    try
        //    {

        //        var result = await _repositoryAmparoIndirectoAbogado.GetAllSuspensionProvisionalIncidentalAsync(id);

        //        if (result is null || !result.Any())
        //        {
        //            return ResultOperation.SuccessResponseNoMessage(true);
        //        }

        //        return ResultOperation.SuccessResponseNoMessage(result.Adapt<List<ResponseSuspensionProvisionalIncidental>>());
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //#endregion

        #region Get All Suspension Provisional
        public async Task<ResultOperation> GetAllSuspensionProvisionalIncidental(int id)
        {
            try
            {

                var result = await _repositoryAmparoIndirectoAbogado.GetAllSuspensionProvisionalIncidentalAsync(id);

                if (result is null)
                {
                    return ResultOperation.SuccessResponseNoMessage(true);

                }

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
