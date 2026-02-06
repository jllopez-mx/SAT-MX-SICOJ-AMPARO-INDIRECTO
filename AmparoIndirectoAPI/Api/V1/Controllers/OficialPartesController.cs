using System.Configuration;
using AmparoIndirectoAPI.Model.DAO.ServicesDAO;
using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.DTO.ContractsValidations;

using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.Entities.Events;
using AmparoIndirectoAPI.Model.Entities.Events.OficialPartes;
using AmparoIndirectoAPI.Model.IDAO.IRepository;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;
using AmparoIndirectoAPI.Model.ViewModels.Enums;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Sicoj.Utils.Enums;
using Sicoj.Utils.Extentions;
using Sicoj.Utils.Files;
using Sicoj.Utils.Middleware;
using Sicoj.Utils.Models;
using Sicoj.Utils.Redis;
using Sicoj.Utils.ViewModels;

namespace AmparoIndirectoAPI.Api.V1.Controllers
{
    [ApiController]
    [Route("sicoj/amparo-indirecto/api/v1/oficial-partes/amparo-indirecto")]
    public class OficialPartesController : ControllerBase
    {
        #region Variables / Constructor
        private readonly ILogger<OficialPartesController> _logger;

        #region BEGIN Amparo Indirecto
        private readonly IValidator<RequestUpdateAmparoIndirectoOficialPartes> _updateAmparoIndirectoValidator;
        private readonly IValidator<RequestCreateArchivoAmparoIndirecto> _validatorCreateDocumentoAmparoIndirecto;
        private readonly IValidator<RequestCreateAmparoIndirecto> _validatorCreateAmparoIndirecto;
        private readonly IAmparoIndirectoOficialPartesService _amparoindirectoOficialPartesService;
        private readonly IValidator<RequestCreateSolicitudTransparencia> _validatorCreateSolicitudTransparencia;
        private readonly IValidator<RequestCreateAcumularJuicio> _validatorCreateAcumularJuicio;
        private readonly IValidator<RequestCreateDeclinacionCompetencia> _validatorCreateDeclinacionCompetencia;
        private readonly IValidator<RequestCreateCanalizarAsunto> _validatorRemitiAsuntoAmparoIndirecto;
        private readonly IValidator<RequestAsignarAbogadoOficialPartes> _validatorAsignarAmparoIndirecto;
        private readonly IValidator<RequestCreateDocumentoList> _createDocumentoListValidator;
        private readonly IValidator<RequestCreateAsuntoControlDocumental> _validatorCreateAsuntoControlDocumental;
        private readonly IValidator<RequestUpdateDocumento> _updateDocumentoValidator;

        #endregion END Amparo Indirecto

        private readonly IRedisClient _redisClient;
        private static object _lock = new object();
        private readonly IFileSystemService _fileSystemService;

        public OficialPartesController(
            ILogger<OficialPartesController> logger,
            IRedisClient redisClient,
            IAmparoIndirectoOficialPartesService amparoindirectoOficialPartesService,
            IValidator<RequestCreateAmparoIndirecto> validatorCreateAmparoIndirecto,
            IValidator<RequestCreateArchivoAmparoIndirecto> validatorCreateDocumentoAmparoIndirecto,
            IValidator<RequestUpdateAmparoIndirectoOficialPartes> updateAmparoIndirectoValidator,
            IValidator<RequestCreateSolicitudTransparencia> validatorCreateSolicitudTransparencia,
            IValidator<RequestCreateAcumularJuicio> validatorCreateAcumularJuicio,
            IValidator<RequestCreateDeclinacionCompetencia> validatorCreateDeclinacionCompetencia,
            IValidator<RequestCreateCanalizarAsunto> validatorRemitiAsuntoAmparoIndirecto,
            IValidator<RequestAsignarAbogadoOficialPartes> validatorAsignarAmparoIndirecto,
            IValidator<RequestCreateDocumentoList> createDocumentoListValidator,
            IValidator<RequestUpdateDocumento> updateDocumentoValidator,
            IValidator<RequestCreateAsuntoControlDocumental> validatorCreateAsuntoControlDocumental,
            IFileSystemService fileSystemService
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
            _validatorCreateAmparoIndirecto = validatorCreateAmparoIndirecto ?? throw new ArgumentNullException(nameof(validatorCreateAmparoIndirecto));
            _amparoindirectoOficialPartesService = amparoindirectoOficialPartesService ?? throw new ArgumentNullException(nameof(amparoindirectoOficialPartesService));
            _validatorCreateDocumentoAmparoIndirecto = validatorCreateDocumentoAmparoIndirecto ?? throw new ArgumentNullException(nameof(validatorCreateDocumentoAmparoIndirecto));
            _updateAmparoIndirectoValidator = updateAmparoIndirectoValidator ?? throw new ArgumentNullException(nameof(updateAmparoIndirectoValidator));
            _validatorCreateSolicitudTransparencia = validatorCreateSolicitudTransparencia ?? throw new ArgumentNullException(nameof(validatorCreateSolicitudTransparencia));
            _validatorCreateAcumularJuicio = validatorCreateAcumularJuicio ?? throw new ArgumentNullException(nameof(validatorCreateAcumularJuicio));
            _validatorCreateDeclinacionCompetencia = validatorCreateDeclinacionCompetencia ?? throw new ArgumentNullException(nameof(validatorCreateDeclinacionCompetencia));
            _validatorRemitiAsuntoAmparoIndirecto = validatorRemitiAsuntoAmparoIndirecto ?? throw new ArgumentNullException(nameof(validatorRemitiAsuntoAmparoIndirecto));
            _validatorAsignarAmparoIndirecto = validatorAsignarAmparoIndirecto ?? throw new ArgumentNullException(nameof(validatorAsignarAmparoIndirecto));
            _createDocumentoListValidator = createDocumentoListValidator ?? throw new ArgumentNullException(nameof(createDocumentoListValidator));
            _updateDocumentoValidator = updateDocumentoValidator ?? throw new ArgumentNullException(nameof(updateDocumentoValidator));
            _validatorCreateAsuntoControlDocumental = validatorCreateAsuntoControlDocumental ?? throw new ArgumentNullException(nameof(validatorCreateAsuntoControlDocumental));
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        }
        #endregion

        #region GETBYID PARA FRONT C0NTROL DOCUMENTAL

        [ProducesResponseType(typeof(ResultOperation<ResponseAsuntoControlDocumental>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("ControlDocumental/Buscar")]
        public async Task<IActionResult> GetAsuntoControlDocumental([FromQuery] string NumeroAsunto)
        {
            try
            {
                var result = await _amparoindirectoOficialPartesService.GetAsuntoControlDocumental(NumeroAsunto);

                if (result is null)
                {
                    return Ok(
                        ResultOperation.FailureWarningResponse("No se encontraron resultados")
                    );
                }
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region  CREATE REGISTRO JUICIO AMPARO CONTROL DOCUMENTAL

        /// <summary>
        /// Metodo para generar un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("ControlDocumental")]
        public async Task<IActionResult> PatchCreateAsuntoControlDocumental(
            RequestCreateAsuntoControlDocumental request
        )
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                var validationResult = await _validatorCreateAsuntoControlDocumental.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }

                AmparoIndirecto entity = null!;
                try
                {
                    entity = AmparoIndirectoOficialPartesEvents.CreateAsuntoControlDocumental(
                        Convert.ToDateTime(request.FechaRecepcionDemanda),
                        request.NumeroAsunto,
                        request.Juzgado,
                        request.NombreQuejoso,
                        request.RfcQuejoso,
                        sessionInformation.UserInformation.Rfc,
                        sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault()
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoOficialPartesService.CreateAsuntoControlDocumental(entity);

                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }
        #endregion

        #region  ASIGNA ABOGADO A UN REGISTRO JUICIO AMPARO

        /// <summary>
        /// Metodo para asignar un abogado un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("asignar")]
        public async Task<IActionResult> PatchCreateAmparoIndirecto(
            RequestAsignarAbogadoOficialPartes request
        )
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                var validationResult = await _validatorAsignarAmparoIndirecto.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(request.id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "El juicio Amparo Indirecto no existe."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede modificar ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }
                var result = await _amparoindirectoOficialPartesService.CreateAsignar(request);

                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }


        #endregion

        #region GET CANALIZAR ASUNTO
        /// <summary>
        ///  GetAll Obtiene todos los registros
        ///  para que se han canalizado  
        ///  a otra unidad Administrativa
        ///  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<ResponseAmparoIndirectoByFilters>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("canalizar-asunto")]
        public async Task<IActionResult> GetAllCanalizarAsunto([FromQuery] PagerQuery request
        )
        {
            try
            {
                if (!Filters.MapSort<EnumOrderColumnAmparoIndirectoByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseAmparoIndirectoRemitir>>("La columna de ordenamiento no es válida.")
                        );
                }

                var result = await _amparoindirectoOficialPartesService.GetAmparoIndirectoCanalizarAsuntoGetAll(
                    request.fetch,
                    request.page,
                    orderByColumn,
                    orderDesc

                    );

                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region CREATE CANALIZAR ASUNTO

        /// <summary>
        /// Metodo para Canalizar un asunto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("canalizar-asunto")]
        public async Task<IActionResult> PostCanalizarAsunto([FromForm]
            RequestCreateCanalizarAsunto request
        )
        {
            try
            {
                var validationResult = await _validatorRemitiAsuntoAmparoIndirecto.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.id_tipo_documento > 0 ||
                    request.id_seccion > 0)
                {

                    if (request.documento != null)
                    {
                        //var validationResultFile = await _validatorCreateSolicitudOpinionInformacion.ValidateAsync(request);
                        //if (!validationResultFile.IsValid)
                        //{
                        //    return Ok(ResultOperation.FailureWarningResponse(validationResultFile.ToString(" -- ")));
                        //}

                        _fileSystemService.FileTryOut(
                            request.documento!,
                            Path.Combine("AMPARO INDIRECTO", request.id_tipo_documento.ToString()),
                            out dataFile
                        );
                    }
                }

                CanalizarAsunto entity = null!;
                try
                {

                    entity = AmparoIndirectoOficialPartesEvents.CreateCanalizarAsunto(
                    Convert.ToDateTime(request.fecha_canalizacion),
                    request.numero_oficio_canalizacion,
                    request.unidad_administrativa_canaliza,
                    request.unidad_administrativa_recibe,
                    request.motivo_canalizacion,
                    sessionInformation.UserInformation.Rfc!

                );
                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.id_tipo_documento,
                            request.id_tipo_documento,
                            request.id_seccion,
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoOficialPartesService.CreateCanalizarAsunto(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoOficialPartesService.CreateCanalizarAsunto(entity, null!, null!);
                        return Ok(resultWithoutDocument);
                    }
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region  HISTORICO
        /// <summary>
        ///  Amparo Indirecto  GetAll Obtiene todos los registros
        ///  para el Histórico de Asuntos  
        ///  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<ResponseAmparoIndirectoByFilters>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("historico")]
        public async Task<IActionResult> GetAllByFiltersAI(
            [FromQuery] PagerQueryFilters request
        )
        {
            try
            {
                RequestAmparoIndirectoFilters filters = new();
                Filters.MapFilters(request, filters);

                if (!Filters.MapSort<EnumOrderColumnAmparoIndirectoByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseAmparoIndirectoByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }

                //string orderByColumn = null!;
                //bool orderDesc = false;
                if (request.sort is not null && request.sort.Any())
                {
                    orderByColumn = request.sort.FirstOrDefault().Key;
                    if (!Enum.TryParse<EnumOrderColumnAmparoIndirectoByFiltros>(orderByColumn, out _))
                    {
                        return Ok(
                        ResultOperation<
                            List<ResponseAmparoIndirectoByFilters>>.FailureWarningResponse("La columna de ordenamiento no es válida.")
                        );
                    }
                    orderDesc = request.sort.FirstOrDefault().Value;
                }

                var result = await _amparoindirectoOficialPartesService.GetAmparoIndirectoDisconnectedByFilters(
                    request.fetch,
                    request.page,
                    orderByColumn,
                    orderDesc,
                    Filters.GetDateTimeValue(filters!.ByFechaRecepcionInicial.FirstOrDefault()),
                    Filters.GetDateTimeValue(filters!.ByFechaRecepcionFinal.FirstOrDefault()),
                    Filters.GetDateTimeValue(filters!.ByFechaVencimientoDemanda.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByNumeroExpediente.FirstOrDefault())!,
                    Filters.GetStringValue(filters!.ByNumeroAsunto.FirstOrDefault())!, //se modifico
                    Filters.GetIntValue(filters!.ByIdJuzgado.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByNombreQuejoso.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdMateria.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdSubmateria.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdTipoActo.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByDespacho.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdAdministracion.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdSubAdministracion.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByRfcQuejoso.FirstOrDefault())!,
                    Filters.GetBoolValue(filters!.ByTransparencia.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdEstadoTarea.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdEstadoProcesal.FirstOrDefault())

                    );

                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion GETALL BY FILTERS

        #region PENDIENTES
        /// <summary>
        ///  Amparo Indirecto  GetAll Obtiene todos los registros
        ///  para la Bandeja de Pendientes con estatus 
        ///  tarea = "Pendiente de Turnar" y estado_procesal ="Activo"
        ///  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<ResponseAmparoIndirectoByFilters>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("pendientes")]
        public async Task<IActionResult> GetAllAI([FromQuery] PagerQuery request
        )
        {
            try
            {
                if (!Filters.MapSort<EnumOrderColumnAmparoIndirectoByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseAmparoIndirectoByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }

                var result = await _amparoindirectoOficialPartesService.GetAmparoIndirectoDisconnectedGetAll(
                    request.fetch,
                    request.page,
                    orderByColumn,
                    orderDesc

                    );

                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region DELETE REGISTRO JUICIO AMPARO INDIRECTO

        /// <summary>
        /// Metodo para eliminar el registro de Amparo Indirecto a Eliminado
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpDelete]
        public async Task<IActionResult> PatchDeleteAmparoIndirecto(
            int id
        )
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "El amparo indirecto no existe."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede eliminar ya que el usuario no lo ha tomado")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }

                var result = await _amparoindirectoOficialPartesService.UpdateDeleteAmparoIndirecto(id);
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }
        #endregion

        #region  CREATE REGISTRO JUICIO AMPARO

        /// <summary>
        /// Metodo para generar un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost]
        public async Task<IActionResult> PatchCreateAmparoIndirecto(
            RequestCreateAmparoIndirecto request
        )
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }

                var validationResult = await _validatorCreateAmparoIndirecto.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }

                AmparoIndirecto entity = null!;
                try
                {
                    entity = AmparoIndirectoOficialPartesEvents.CreateAmparoIndirecto(
                        request.numero_asunto,
                        request.numero_expediente,
                        Convert.ToDateTime(request.fecha_recepcion_demanda),
                        request.id_juzgado,
                        request.contribuyente,
                        request.rfc_quejoso,
                        request.nombre_quejoso,
                        request.id_materia,
                        request.id_submateria,
                        request.id_tipo_acto,
                        request.despacho,
                        request.id_autoridad_responsable,
                        request.id_administracion_central,
                        sessionInformation.UserInformation.Rfc
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoOficialPartesService.CreateAmparoIndirecto(entity);

                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }


        #endregion

        #region TURNAR

        /// <summary>
        /// Metodo para actualizar el registro de Amparo Indirecto cuando se turna
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("turnar")]
        public async Task<IActionResult> PatchUpdateAmparoIndirecto(
            int id)
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "El amparo indirecto no existe."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede turnar ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }

                if ((entityExists.id_administracion != 0 && entityExists.id_subadministracion == 0) || (entityExists.id_administracion == 0 && entityExists.id_subadministracion != 0) || (entityExists.id_administracion == 0 && entityExists.id_subadministracion == 0))
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede turnar ya que no se ha seleccionado una administración o subadministración.")
                    );
                }

                var result = await _amparoindirectoOficialPartesService.UpdateTurnarAmparoIndirecto(id, entityExists.id_administracion, entityExists.id_administracion, entityExists.id_subadministracion, "numeroEmpleado");
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region MODIFICAR JUICIO

        /// <summary>
        /// Metodo para actualizar juicios de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch]
        public async Task<IActionResult> PatchRegistroAI(
            RequestUpdateAmparoIndirectoOficialPartes request
        )
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                var validationResult = await _updateAmparoIndirectoValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(request.id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "El juicio Amparo Indirecto no existe."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede modificar ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }
                AmparoIndirecto entity = null!;
                try
                {
                    entity = AmparoIndirectoOficialPartesEvents.UpdateRegistroAI(
                        request.id,
                        request.numero_asunto,
                        request.numero_expediente,
                        Convert.ToDateTime(request.fecha_recepcion_demanda),
                        request.id_juzgado,
                        //request.numero_oficio_segunda_pieza,
                        request.rfc_quejoso,
                        request.nombre_quejoso,
                        request.id_materia,
                        request.id_submateria,
                        request.id_tipo_acto,
                        request.id_administracion,
                        request.id_subadministracion
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoOficialPartesService.UpdateAmparoIndirecto(entity);
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region  CREATE SOLICITUD TRANSPARENCIA

        /// <summary>
        /// Metodo para generar un registro de Solicitud de Transparencia
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("solicitud-transparencia")]
        public async Task<IActionResult> PatchCreateSolicitudTransparencia([FromForm] RequestCreateSolicitudTransparencia request)
        {
            try
            {
                var validationResult = await _validatorCreateSolicitudTransparencia.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };

                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(request.id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "El amparo indirecto no existe."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede turnar ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }
                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.id_tipo_documento > 0 ||
                    request.id_seccion > 0)
                {

                    if (request.documento != null)
                    {
                        //var validationResultFile = await _validatorCreateSolicitudOpinionInformacion.ValidateAsync(request);
                        //if (!validationResultFile.IsValid)
                        //{
                        //    return Ok(ResultOperation.FailureWarningResponse(validationResultFile.ToString(" -- ")));
                        //}

                        _fileSystemService.FileTryOut(
                            request.documento!,
                            Path.Combine("AMPARO INDIRECTO", request.id.ToString()),
                            out dataFile
                        );
                    }
                }

                SolicitudTransparencia entity = null!;
                try
                {

                    entity = SolicitudTransparenciaOficialPartesEvents.CreateSolicitudTransparencia(
                    request.id,
                    request.numero_solicitud,
                    Convert.ToDateTime(request.fecha_solicitud),
                    sessionInformation.UserInformation.Rfc!

                );
                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.id,
                            request.id_tipo_documento,
                            request.id_seccion,
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoOficialPartesService.CreateSolicitudTransparencia(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoOficialPartesService.CreateSolicitudTransparencia(entity, null!, null!);
                        return Ok(resultWithoutDocument);
                    }
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region GET ALL SOLICITUD DE TRANSPARENCIA
        /// <summary>
        ///  Obtiene todos los registros de la tabla
        ///  Solicitud de Transparencia 
        ///  con campo activo ="True"
        ///  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<ResponseSolicitudTransparenciaByFilter>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("solicitud-transparencia")]
        public async Task<IActionResult> GetSolicitudesT([FromQuery] PagerQueryFilters request)
        {
            try
            {
                RequestSolicitudTransparenciaFilters filters = new();
                Filters.MapFilters(request, filters);

                if (!Filters.MapSort<EnumOrderColumnAmparoIndirectoByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseAmparoIndirectoByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }


                ////string orderByColumn = null!;
                ////bool orderDesc = false;
                ////if (request.sort is not null && request.sort.Any())
                ////{
                ////    orderByColumn = request.sort.FirstOrDefault().Key;
                ////    if (!Enum.TryParse<EnumOrderColumnSolicitudTransparenciaByFiltros>(orderByColumn, out _))
                ////    {
                ////        return Ok(
                ////        ResultOperation<
                ////            List<ResponseSolicitudTransparenciaByFilter>>.FailureWarningResponse("La columna de ordenamiento no es válida.")
                ////        );
                ////    }
                ////    orderDesc = request.sort.FirstOrDefault().Value;
                ////}


                ///Original
                var result = await _amparoindirectoOficialPartesService.GetSolicitudTransparenciaGetAll(
                 request.fetch,
                 request.page,
                 Filters.GetIntValue(filters!.idNumeroAsunto.FirstOrDefault())
                 //orderByColumn,
                 //orderDesc,

                 );

                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region  DELETE SOLICITUD DE TRANSPARENCIA

        /// <summary>
        /// Metodo para actualizar el registro de Solicitud de Transparencia a Eliminado
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpDelete("solicitud-transparencia")]
        public async Task<IActionResult> PatchDeleteSolicitudTransparencia(int idAmparo, int id)
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                string key = null!;
                var entityExists = await _amparoindirectoOficialPartesService.GetSolicitudTransparenciaById(idAmparo);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "La Solicitud de Transparencia no existe."
                        )
                    );
                }
                key = $"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{idAmparo}";
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>(key);
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede usar ya que el usuario no ha apartado la Solicitud de Transparencia.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }
                var result = await _amparoindirectoOficialPartesService.DeleteSolicitudTransparencia(id);
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }
        #endregion

        #region  CONSULTA EXPEDIENTE ANTES DE ACUMULAR
        /// <summary>
        ///  Obtiene todos los registros con los filtros indicados
        ///  para la consulta de la primera parte de acumular  
        ///  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<ResponseAcumularByFilters>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("consulta-expediente-antes-acumular")]
        public async Task<IActionResult> Get([FromQuery] PagerQueryFilters request)
        {
            try
            {
                RequestAmparoIndirectoFilters filters = new();
                Filters.MapFilters(request, filters);

                if (!Filters.MapSort<EnumOrderColumnAmparoIndirectoByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseAcumularByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }

                //string orderByColumn = null!;
                //bool orderDesc = false;
                //if (request.sort is not null && request.sort.Any())
                //{
                //    orderByColumn = request.sort.FirstOrDefault().Key;
                //    if (!Enum.TryParse<EnumOrderColumnAmparoIndirectoByFiltros>(orderByColumn, out _))
                //    {
                //        return Ok(
                //        ResultOperation<
                //            List<ResponseAmparoIndirectoByFilters>>.FailureWarningResponse("La columna de ordenamiento no es válida.")
                //        );
                //    }
                //    orderDesc = request.sort.FirstOrDefault().Value;
                //}

                var result = await _amparoindirectoOficialPartesService.GetExpedienteAntesAcumularByFilters(
                    request.fetch,
                    request.page,
                    orderByColumn,
                    orderDesc,
                    Filters.GetStringValue(filters!.ByNumeroExpediente.FirstOrDefault())!,
                    Filters.GetStringValue(filters!.ByNumeroAsunto.FirstOrDefault())!,
                    Filters.GetStringValue(filters!.ByRfcQuejoso.FirstOrDefault())!,
                    Filters.GetStringValue(filters!.ByNombreQuejoso.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdJuzgado.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdTipoActo.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdEstadoTarea.FirstOrDefault())

                    );

                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }


        #endregion

        #region ACUMULAR GET CABEZA DE SERIE


        //    /// <summary>
        //    ///Método para consultar la Cabeza de Serie por Id
        //    /// </summary>
        //    /// <param name="id">Id Amparo Indirecto</param>
        //    /// <returns></returns>
        //    [ProducesResponseType(
        //        typeof(ResultOperation<ResponseAmparoIndirectoByID>),
        //        200
        //    )]
        //    [ProducesResponseType(typeof(ResultOperation), 400)]
        //    [Authorize(EnumRoles.JAI_OP)]
        //    [HttpGet("c")]
        //    public async Task<IActionResult> GetCabezaSerie([FromQuery] int id)
        //    {
        //        try
        //        {
        //            UserSession sessionInformation = new()
        //            {
        //                UserInformation = new()
        //                {
        //                    Rfc = "JUAN"
        //                }
        //,
        //                TokenInfomation = new()
        //                {
        //                    workforceID = "00000001032"
        //                }
        //            };
        //            //var sessionInformation = UserSession.GetValue(HttpContext);
        //            //if (sessionInformation is null)
        //            //{
        //            //    return Ok(
        //            //        ResultOperation.FailureErrorResponse(
        //            //            "No se pudo obtener la información del usuario."
        //            //        )
        //            //    );
        //            //}
        //            var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{id}");
        //            if (keyExists is null)
        //            {
        //                return Ok(
        //                    ResultOperation.FailureErrorResponse<int>("El registro no se puede turnar ya que el usuario no lo ha tomado.")
        //                );
        //            }

        //            if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
        //            {
        //                return Ok(
        //                    ResultOperation.FailureInformationResponse(
        //                        "El registro ya se encuentra en uso por otro usuario."
        //                    )
        //                );
        //            }
        //            var result = await _amparoindirectoOficialPartesService.GetCabezaSerieById(id);
        //            return Ok(result);
        //        }
        //        catch (Exception _e)
        //        {
        //            return BadRequest(
        //                ResultOperation.FailureErrorResponse(
        //                    $"Ha ocurrido un error inesperado => {_e.Message}"
        //                )
        //            );
        //        }
        //    }

        #endregion

        #region ACUMULAR GET CABEZA DE SERIE


        /// <summary>
        ///Método para consultar la Cabeza de Serie por Id
        /// </summary>
        /// <param name="id">Id Amparo Indirecto</param>
        /// <returns></returns>
        [ProducesResponseType(
            typeof(ResultOperation<ResponseAmparoIndirectoByID>),
            200
        )]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("cabeza-serie-id")]
        public async Task<IActionResult> GetCabezaSerieFront([FromQuery] int id)
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede consultar ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }
                var result = await _amparoindirectoOficialPartesService.GetCabezaSerieFrontById(id);
                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region ACUMULAR-DESACUMULAR JUICIOS

        /// <summary>
        /// Metodo para generar un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("acumular-juicio")]
        public async Task<IActionResult> CreateAcumularJuicio(
            RequestCreateAcumularJuicio request
        )
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                var validationResult = await _validatorCreateAcumularJuicio.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(request.id_juicio_padre);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "El juicio Amparo Indirecto no existe."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede acumular ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }
                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.id_tipo_documento > 0 ||
                    request.id_seccion > 0)
                {

                    if (request.documento != null)
                    {
                        //var validationResultFile = await _validatorCreateSolicitudOpinionInformacion.ValidateAsync(request);
                        //if (!validationResultFile.IsValid)
                        //{
                        //    return Ok(ResultOperation.FailureWarningResponse(validationResultFile.ToString(" -- ")));
                        //}

                        _fileSystemService.FileTryOut(
                            request.documento!,
                            Path.Combine("AMPARO INDIRECTO", request.id_juicio_padre.ToString()),
                            out dataFile
                        );
                    }
                }
                AcumularDesacumular entity = null!;
                try
                {

                    entity = AmparoIndirectoOficialPartesEvents.CreateAcumularDesacumular(
                    request.id_juicio_padre,
                    request.id_juicio_acumulado,
                    request.numero_oficio,
                    entityExists.id_administracion,
                    sessionInformation.UserInformation.Rfc!

                );
                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.id_juicio_padre,
                            request.id_tipo_documento,
                            request.id_seccion,
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoOficialPartesService.CreateAcumularJuicio(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoOficialPartesService.CreateAcumularJuicio(entity, null!, null!);
                        return Ok(resultWithoutDocument);
                    }
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ocurrió un error al generar un amparo indirecto.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region GETBYID PARA FRONT

        [ProducesResponseType(typeof(ResultOperation<ResponseAmparoIndirectoByID>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAmparoIndirecto([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoOficialPartesService.GetByIdAmparo(id);

                if (result is null)
                {
                    return Ok(
                        ResultOperation.FailureWarningResponse("No se encontraron resultados")
                    );
                }
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region GETBYID SOLICITUD DE TRANSPARENCIA PARA FRONT

        [ProducesResponseType(typeof(ResultOperation<ResponseAmparoIndirectoByID>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("id-transparencia")]
        public async Task<IActionResult> GetByIdSolicitudTransparencia([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoOficialPartesService.GetByIdTransparencia(id);

                if (result is null)
                {
                    return Ok(
                        ResultOperation.FailureWarningResponse("No se encontraron resultados")
                    );
                }
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region DESCARGAR ARCHIVO

        [ProducesResponseType(typeof(ResultOperation<ResponseDocumentoAmparoIndirecto>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("descargar-archivo")]
        public async Task<IActionResult> GetDescargarArchivo(int id, int idNumeroAsunto)
        {
            try
            {
                var entityExistsJuicio = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(idNumeroAsunto);
                if (entityExistsJuicio is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "El documento no existe."
                        )
                    );
                }

                //var entityExists = await _amparoindirectoOficialPartesService.GetArchivoAmparoIndirectoById(id);
                var entityExists = await _amparoindirectoOficialPartesService.GetArchivoDiscconected(id);
                if (entityExists is null)
                {
                    return BadRequest("No existe el registro.");
                }

                var response = await _fileSystemService.GetFileAsync(entityExists.file_path);
                if (response is null)
                {
                    return BadRequest("No existe el documento.");
                }

                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    Inline = true,
                    FileName = Path.GetFileName(entityExists.file_path)
                };

                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                return File(response, entityExists.content_type);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region GET ALL DOCUMENTOS

        [ProducesResponseType(typeof(ResultOperation<ResponseDocumentoAmparoIndirecto>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("archivos")]
        public async Task<IActionResult> GetAllDocumentos([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoOficialPartesService.GetAllArchivoAmparoIndirecto(id);

                if (result is null)
                {
                    return Ok(
                        ResultOperation.FailureWarningResponse("No se encontraron resultados")
                    );
                }
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region GET ALL DOCUMENTOS SECCIÓN

        [ProducesResponseType(typeof(ResultOperation<ResponseDocumentoAmparoIndirecto>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("archivos/seccion")]
        public async Task<IActionResult> GetAllDocumentosSeccion([FromQuery] int id, int idSeccion)
        {
            try
            {
                var result = await _amparoindirectoOficialPartesService.GetAllArchivoSeccion(id, idSeccion);

                if (result is null)
                {
                    return Ok(
                        ResultOperation.FailureWarningResponse("No se encontraron resultados")
                    );
                }
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region GET HISTORICO DOCUMENTOS

        [ProducesResponseType(typeof(ResultOperation<ResponseDocumentoAmparoIndirecto>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("archivo/historico")]
        public async Task<IActionResult> GetAHistoricoDocumentos([FromQuery] PagerQuery request, int id)
        {
            try
            {
                if (!Filters.MapSort<EnumOrderColumnDocumentosByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseDocumentoAmparoIndirecto>>("La columna de ordenamiento no es válida."));
                }

                var result = await _amparoindirectoOficialPartesService.GetDocumentosHistoricoAsync(
                    request.fetch,
                    request.page,
                    orderByColumn,
                    orderDesc,
                    id);
                return Ok(result);



                //var result = await _amparoindirectoOficialPartesService.GetAllArchivoAmparoIndirecto(id);

                if (result is null)
                {
                    return Ok(
                        ResultOperation.FailureWarningResponse("No se encontraron resultados")
                    );
                }
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region GET HISTORICO DOCUMENTOS SECCION

        [ProducesResponseType(typeof(ResultOperation<ResponseDocumentoAmparoIndirecto>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("archivo/historico/seccion")]
        public async Task<IActionResult> GetAHistoricoDocumentosSeccion([FromQuery] PagerQuery request, int id, int idSeccion)
        {
            try
            {
                if (!Filters.MapSort<EnumOrderColumnDocumentosByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseDocumentoAmparoIndirecto>>("La columna de ordenamiento no es válida."));
                }

                var result = await _amparoindirectoOficialPartesService.GetDocumentosHistoricoSeccionAsync(
                    request.fetch,
                    request.page,
                    orderByColumn,
                    orderDesc,
                    id,
                    idSeccion);
                return Ok(result);



                //var result = await _amparoindirectoOficialPartesService.GetAllArchivoAmparoIndirecto(id);

                if (result is null)
                {
                    return Ok(
                        ResultOperation.FailureWarningResponse("No se encontraron resultados")
                    );
                }
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region GET BY ID DOCUMENTO

        [ProducesResponseType(typeof(ResultOperation<ResponseDocumentoAmparoIndirecto>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("archivo")]
        public async Task<IActionResult> GetDocumento([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoOficialPartesService.GetArchivo(id);

                if (result is null)
                {
                    return Ok(
                        ResultOperation.FailureWarningResponse("No se encontraron resultados")
                    );
                }
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region CREATE DOCUMENTO

        /// <summary>
        ///Método para Agregar un archivo al registro de amparo indirecto
        /// </summary>
        /// <param name="request">Datos del archivo y del registro de amparo indirecto</param>
        /// <returns>Id del registro de archivo agregado</returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("archivo")]
        public async Task<IActionResult> PostFileAmparoIndirecto(
        [FromForm] RequestCreateArchivoAmparoIndirecto request
)
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                Filters.ValidateContractValues(request);
                var validationResult = await _validatorCreateDocumentoAmparoIndirecto.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }

                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(request.id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de Amparo no existe."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede modificar ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };

                if (!_fileSystemService.FileTryOut(
                            request.documento,
                            Path.Combine("AMPARO INDIRECTO", request.id.ToString()),
                            out var dataFile, out string message, requiredExtentions))
                {
                    return Ok(
                            ResultOperation.FailureErrorResponse<int>(
                                message
                            )
                        );
                }

                ArchivosAmparoIndirecto entity = null!;
                try
                {
                    entity = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                    request.id,
                    request.id_tipo_documento,
                    request.id_seccion,
                    sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                    dataFile!.File!.FileName, //nombre documento                
                    dataFile.FilePath,
                    dataFile.File.ContentType,
                    _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                    EnumRolesSicoj.OFICIAL_DE_PARTES.GetHashCode(),
                    sessionInformation.UserInformation.Rfc
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }


                var result = await _amparoindirectoOficialPartesService.CreateDocumentoAmparoIndirecto(entity, dataFile);

                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region UPDATE DOCUMENTO

        /// <summary>
        ///Método para modificar un archivo al registro de amparo indirecto
        /// </summary>
        /// <param name="request">Datos del archivo y del registro de amparo indirecto</param>
        /// <returns>Id del registro de archivo agregado</returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("archivo")]
        public async Task<IActionResult> UpdateFileAmparoIndirecto(
        [FromForm] RequestUpdateDocumento request
)
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                Filters.ValidateContractValues(request);
                var validationResult = await _updateDocumentoValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }

                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(request.id_numero_asunto);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de Amparo no existe."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede modificar ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }

                var entityDocumentoList = await _amparoindirectoOficialPartesService.GetArchivobyIds(new int[] { request.id });
                if (entityDocumentoList is null || !entityDocumentoList.Any(c => c.activo))
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>(
                            "No existe el documento o fue eliminado."
                        )
                    );
                }
                var entityDocumento = entityDocumentoList.FirstOrDefault(c => c.activo);

                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };

                if (!_fileSystemService.FileTryOut(
                            request.documento,
                            Path.Combine("AMPARO INDIRECTO", request.id.ToString()),
                            out var dataFile, out string message, requiredExtentions, entityDocumento!.file_path))
                {
                    return Ok(
                            ResultOperation.FailureErrorResponse<int>(
                                message
                            )
                        );
                }

                //if (dataFile is not null)
                //{

                //}

                ArchivosAmparoIndirecto entity = null!;
                try
                {
                    entity = ArchivosAmparoIndirectoEvents.UpdateDocumento(
                    request.id,
                    request.id_numero_asunto,
                    request.id_tipo_documento,
                    request.id_seccion,
                    sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                    dataFile!.File!.FileName, //nombre documento                
                    dataFile.FilePath,
                    dataFile.File.ContentType,
                    _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                    EnumRolesSicoj.OFICIAL_DE_PARTES.GetHashCode(),
                    sessionInformation.UserInformation.Rfc,
                    sessionInformation.UserInformation.Rfc
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }


                var result = await _amparoindirectoOficialPartesService.UpdateDocumentoAmparoIndirecto(entity, dataFile);

                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region CREATE LIST DOCUMENTO

        //        /// <summary>
        //        ///Método para Agregar un archivo al registro de amparo indirecto
        //        /// </summary>
        //        /// <param name="request">Datos del archivo y del registro de amparo indirecto</param>
        //        /// <returns>Id del registro de archivo agregado</returns>
        //        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        //        [ProducesResponseType(typeof(ResultOperation), 400)]
        //        [Authorize(EnumRoles.RR_OP)]
        //        [HttpPost("archivo-list")]
        //        public async Task<IActionResult> PostArchivoList(
        //        [FromForm] RequestCreateDocumentoList request
        //)
        //        {
        //            try
        //            {
        //                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(request.id);
        //                if (entityExists is null)
        //                {
        //                    return Ok(
        //                        ResultOperation.FailureErrorResponse(
        //                            "El amparo indirecto no existe."
        //                        )
        //                    );
        //                }
        //                //var validationResult = await _validatorCreateDocumentoAmparoIndirecto.ValidateAsync(request);
        //                //if (!validationResult.IsValid)
        //                //{
        //                //    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
        //                //}

        //                var validationResult = await _createDocumentoListValidator.ValidateAsync(request);
        //                if (!validationResult.IsValid)
        //                {
        //                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
        //                }

        //                bool error = false;
        //                List<Message> listMessage = new();
        //                foreach (var item in request.documentoList)
        //                {
        //                    var validationResultDocumento = await _validatorCreateDocumentoAmparoIndirecto.ValidateAsync(item);
        //                    if (!validationResultDocumento.IsValid)
        //                    {
        //                        error = true;
        //                        listMessage.Add(new(TypeMessage.Error, validationResultDocumento.ToString(" - ")));
        //                    }
        //                }
        //                if (error)
        //                {
        //                    ResultOperation resultOperation = ResultOperation<bool>.FailureWarningResponse("Algunos documentos no son válidos");
        //                    resultOperation.AddMessages(listMessage);
        //                    return Ok(resultOperation);
        //                }

        //                List<ArchivosAmparoIndirecto> entityList = new();
        //                List<DataFile> dataFileList = new();
        //                foreach (var item in request.documentoList)
        //                {
        //                    try
        //                    {
        //                        entityList.Add(ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
        //                        item.id,
        //                        item.id_tipo_documento,
        //                        item.documento.FileName, //nombre documento                
        //                        Path.Combine("AMPARO INDIRECTO", request.id.ToString()),
        //                        _fileSystemService.ConvertBytesToMegaBytesString(item.documento.Length),
        //                        item.documento.ContentType
        //                        ));
        //                    }
        //                    catch (Exception _ex)
        //                    {
        //                        return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
        //                    }

        //                    dataFileList.Add(new()
        //                    {
        //                        File = item.documento
        //                    });
        //                }

        //                var result = await _amparoindirectoOficialPartesService.CreateDocumentoList(entityList, dataFileList);

        //                return Ok(result);
        //            }
        //            catch (Exception _e)
        //            {
        //                _logger.LogError(_e, "Ocurrió un error al actualizar una autorización de impuestos internos.");
        //                return BadRequest(
        //                    ResultOperation.FailureErrorResponse(
        //                        "Ha ocurrido un error inesperado al intentar crear el registro."
        //                    )
        //                );
        //            }
        //        }

        #endregion

        #region DELETE DOCUMENTO JUICIO AMPARO

        /// <summary>
        /// Metodo para eliminar los documentos relacionados con el Juicio de Amparo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpDelete("archivo")]
        public async Task<IActionResult> DeleteDocumentoAmparoIndirecto(
            RequestDeleteDocumento request
        )
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                string key = null!;
                var entityListExists = await _amparoindirectoOficialPartesService.GetArchivobyIds(request.Ids.ToArray());
                if (entityListExists is null || !entityListExists.Any())
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "Los documentos no existen."
                        )
                    );
                }
                key = $"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{request.IdAmparo}";
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>(key);
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede modificar ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }
                if (!request.Ids.All(value => entityListExists.Select(x => x.id).Contains(value)))
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "Algunos documentos no existen."
                        )
                    );
                }
                try
                {
                    for (int i = 0; i < entityListExists.Count; i++)
                    {
                        var entity = entityListExists[i];
                        ArchivosAmparoIndirectoEvents.Delete(ref entity, sessionInformation.UserInformation.Rfc);
                    }
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoOficialPartesService.DeleteArchivo(request.Ids.ToArray(), sessionInformation.UserInformation.Rfc);
                return Ok(result);

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region CREATE DECLINAR COMPETENCIA

        /// <summary>
        /// Metodo para generar una declinación de competencia de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("declinar-competencia")]
        public async Task<IActionResult> PosthCreateDeclinarCompetencia(
            RequestCreateDeclinacionCompetencia request
        )
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(request.id_numero_asunto);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "El amparo indirecto que desea declinar no existe."
                        )
                    );
                }
                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
                if (keyExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede acumular ya que el usuario no lo ha tomado.")
                    );
                }

                if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                {
                    return Ok(
                        ResultOperation.FailureInformationResponse(
                            "El registro ya se encuentra en uso por otro usuario."
                        )
                    );
                }
                var validationResult = await _validatorCreateDeclinacionCompetencia.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.id_tipo_documento > 0 ||
                    request.id_seccion > 0)
                {

                    if (request.documento != null)
                    {
                        //var validationResultFile = await _validatorCreateSolicitudOpinionInformacion.ValidateAsync(request);
                        //if (!validationResultFile.IsValid)
                        //{
                        //    return Ok(ResultOperation.FailureWarningResponse(validationResultFile.ToString(" -- ")));
                        //}

                        _fileSystemService.FileTryOut(
                            request.documento!,
                            Path.Combine("AMPARO INDIRECTO", request.id_numero_asunto.ToString()),
                            out dataFile
                        );
                    }
                }
                DeclinarCompetencia entity = null!;
                try
                {
                    entity = AmparoIndirectoOficialPartesEvents.CreateDeclinarCompetencia(
                        request.id_numero_asunto,
                        request.numero_oficio,
                        DateTime.Parse(request.fecha_recepcion_declinacion),
                        entityExists.numero_asunto,
                        request.numero_asunto_destino,
                        entityExists.id_juzgado,
                        request.id_juzgado_destino,
                        sessionInformation.UserInformation.Rfc!
                    );
                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.id_numero_asunto,
                            request.id_tipo_documento,
                            request.id_seccion,
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoOficialPartesService.CreateDeclinarCompetencia(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoOficialPartesService.CreateDeclinarCompetencia(entity, null!, null!);
                        return Ok(resultWithoutDocument);
                    }
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }

            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region  Envio correo
        /// <summary>
        /// Envió de correos : Oficial de Partes
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("Email")]
        public async Task<IActionResult> Email(
         [FromForm] RequestEmailMessage request
        )
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }


                Email entity = null!;
                try
                {
                    entity = AmparoIndirectoOficialPartesEvents.CreaCorreo(
                        request.To,
                        request.Cc,
                        request.Subject,
                        request.IsHtml,
                        request.Body

                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }

                var result = await _amparoindirectoOficialPartesService.EnvioEmail(entity);
                if (result.Success == false)
                {
                    return Ok(result.Messages);
                }

                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }
        #endregion

        #region TOMAR

        [ProducesResponseType(typeof(ResultOperation<bool>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("tomar")]
        public async Task<IActionResult> PatchTomar([FromQuery] int id)
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }
                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null || !entityExists.activo)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio no existe o se eliminó."
                        )
                    );
                }
                var response = await _redisClient.Take(EnumModulosRedis.AMPARO_INDIRECTO, entityExists.id, sessionInformation.UserInformation.Rfc!, sessionInformation.UserInformation.Nombre!);
                return Ok(response);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region SOLTAR

        [ProducesResponseType(typeof(ResultOperation<bool>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("soltar")]
        public async Task<IActionResult> PatchSoltar([FromQuery] int id)
        {
            try
            {
                var sessionInformation = UserSession.GetValue(HttpContext);
                if (sessionInformation is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "No se pudo obtener la información del usuario."
                        )
                    );
                }

                var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio no existe."
                        )
                    );
                }
                var result = await _redisClient.Drop(EnumModulosRedis.AMPARO_INDIRECTO, id, sessionInformation.UserInformation.Rfc!);
                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion

        #region GET FECHA VENCIMIENTO

        [ProducesResponseType(typeof(ResultOperation<bool>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("fecha_vencimiento_seccion")]//modificado
        public async Task<IActionResult> GetAllFechaVencimientoSeccion([FromQuery] string fechaInicial, string fechaFinal, List<int> idSecciones) //modificado
        {
            try
            {
                var result = await _amparoindirectoOficialPartesService.GetAllFechaVencimientoSeccion(fechaInicial, fechaFinal, idSecciones); //se modifico
                if (result is null)
                {
                    return Ok(
                        ResultOperation.FailureWarningResponse("No se encontraron resultados")
                    );
                }
                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }

        #endregion
            
        #region FECHA VENCIMIENTO

        [ProducesResponseType(typeof(ResultOperation<bool>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("fecha_vencimiento_seccion")]//modificado
        public async Task<IActionResult> UpdateFechaVencimiento([FromQuery] string fechaVencimiento, int idAsunto, int idModulo, int idSeccion,  int idSeccionRenglon) //modificado
        {
            try
            {
                //var sessionInformation = UserSession.GetValue(HttpContext);
                //if (sessionInformation is null)
                //{
                //    return Ok(
                //        ResultOperation.FailureErrorResponse(
                //            "No se pudo obtener la información del usuario."
                //        )
                //    );
                //}
                //string key = null!;
                //var entityExists = await _amparoindirectoOficialPartesService.GetAmparoIndirectoByIdDisconnected(idAsunto);
                //if (entityExists is null)
                //{
                //    return Ok(
                //        ResultOperation.FailureErrorResponse<bool>(
                //            "El juicio de amparo indirecto no existe."
                //        )
                //    );
                //}
                //var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>(key);
                //if (keyExists is null)
                //{
                //    return Ok(
                //        ResultOperation.FailureErrorResponse<int>("El registro no se puede modificar ya que el usuario no lo ha tomado.")
                //    );
                //}

                //if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
                //{
                //    return Ok(
                //        ResultOperation.FailureInformationResponse(
                //            "El registro ya se encuentra en uso por otro usuario."
                //        )
                //    );
                //}
                var result = await _amparoindirectoOficialPartesService.UpdateFechaVencimiento(fechaVencimiento, idAsunto, idModulo, idSeccion, idSeccionRenglon); //se modifico
                return Ok(result);
            }
            catch (Exception _e)
            {
                _logger.LogError(_e, "Ha ocurrrido un error.");
                return BadRequest(
                    ResultOperation.FailureErrorResponse(
                        _e.ManageException()
                    )
                );
            }
        }
        #endregion


    }
}
