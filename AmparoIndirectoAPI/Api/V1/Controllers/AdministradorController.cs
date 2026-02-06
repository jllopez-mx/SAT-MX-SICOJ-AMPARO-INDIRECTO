using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.DTO.ContractsValidations;

using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.Entities.Events;
using AmparoIndirectoAPI.Model.IDAO.IServiceDAO;
using AmparoIndirectoAPI.Model.ViewModels.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sicoj.Utils.Enums;
using Sicoj.Utils.Extentions;
using Sicoj.Utils.Files;
using Sicoj.Utils.Middleware;
using Sicoj.Utils.Models;
using Sicoj.Utils.Redis;

namespace AmparoIndirectoAPI.Api.V1.Controllers
{
    [ApiController]
    [Route("sicoj/amparo-indirecto/api/v1/administrador/amparo-indirecto")]
    public class AdministradorController : ControllerBase
    {
        #region Variables / Constructor
        private readonly ILogger<AdministradorController> _logger;


        #region Administrador
        private readonly RequestUpdateAmparoIndirectoAdministradorValidator _updateAmparoIndirectoAdministradorValidator;
        private readonly IValidator<RequestCreateReasignarJuicioAdministrador> _validatorCreateReasignarAmparoIndirecto;
        private readonly IValidator<RequestAsignarAbogadoAdministrador> _validatorUpdateAsignarAmparoIndirecto;

        private readonly IAmparoIndirectoAdministradorService _amparoindirectoAdministradorService;
        private readonly IValidator<RequestCreateCanalizarAsuntoAdministrador> _validatorRemitiAsuntoAdministrador;

        #endregion END Amparo Indirecto

        private readonly IRedisClient _redisClient;
        private static object _lock = new object();
        //private readonly IFileSystemService _fileSystemService;

        public AdministradorController(
            ILogger<AdministradorController> logger,
            IRedisClient redisClient,
            IAmparoIndirectoAdministradorService amparoindirectoAdministradorService,
            RequestUpdateAmparoIndirectoAdministradorValidator updateAmparoIndirectoAdministradorValidator,
            IValidator<RequestCreateReasignarJuicioAdministrador> validatorCreateReasignarAmparoIndirecto,
            IValidator<RequestCreateCanalizarAsuntoAdministrador> validatorRemitiAsuntoAdministrador,
            IValidator<RequestAsignarAbogadoAdministrador> validatorUpdateAsignarAmparoIndirecto
            )
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
            _updateAmparoIndirectoAdministradorValidator = updateAmparoIndirectoAdministradorValidator ?? throw new ArgumentNullException(nameof(updateAmparoIndirectoAdministradorValidator));
            _amparoindirectoAdministradorService = amparoindirectoAdministradorService ?? throw new ArgumentNullException(nameof(amparoindirectoAdministradorService));
            _validatorCreateReasignarAmparoIndirecto = validatorCreateReasignarAmparoIndirecto ?? throw new ArgumentNullException(nameof(validatorCreateReasignarAmparoIndirecto));
            _validatorRemitiAsuntoAdministrador = validatorRemitiAsuntoAdministrador ?? throw new ArgumentNullException(nameof(validatorRemitiAsuntoAdministrador));
            _validatorUpdateAsignarAmparoIndirecto = validatorUpdateAsignarAmparoIndirecto ?? throw new ArgumentNullException(nameof(validatorUpdateAsignarAmparoIndirecto));
        }
        #endregion
        #region UPDATE REASIGNAR JUICIO

        /// <summary>
        /// Metodo para reasignar un asunto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("reasignar-juicio")]
        public async Task<IActionResult> PostReasignar(
            RequestCreateReasignarJuicioAdministrador request
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

                var validationResult = await _validatorCreateReasignarAmparoIndirecto.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                

                var result = await _amparoindirectoAdministradorService.CreateReasignarJuicio(request);

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
        #region UPDATE ASIGNAR JUICIO

        /// <summary>
        /// Metodo para reasignar un asunto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("asignar")]
        public async Task<IActionResult> PostReasignar(
            RequestAsignarAbogadoAdministrador request
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

                var validationResult = await _validatorUpdateAsignarAmparoIndirecto.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoAdministradorService.GetAmparoIndirectoByIdDisconnected(request.id);
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
                    entity = AmparoIndirectoAdministradorEvents.UpdateAsignar(
                        ref entityExists,
                        request.id_abogado
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAdministradorService.UpdateAsignarAdministrador(entity);
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
        /// Metodo para remitir un asunto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("canalizar-asunto")]
        public async Task<IActionResult> PostRemitirAsunto(
            RequestCreateCanalizarAsuntoAdministrador request
        )
        {
            try
            {
                var validationResult = await _validatorRemitiAsuntoAdministrador.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }

                var result = await _amparoindirectoAdministradorService.CreateCanalizarAsuntoAdministrador(request);

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
            RequestUpdateAmparoIndirectoAdministrador request
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

                var validationResult = await _updateAmparoIndirectoAdministradorValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoAdministradorService.GetAmparoIndirectoByIdDisconnected(request.id);
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
                    entity = AmparoIndirectoAdministradorEvents.UpdateRegistroAdministradorAI(
                        request.id,
                        request.numero_asunto,
                        request.numero_expediente,
                        /*string.IsNullOrEmpty(request.fecha_recepcion_demanda) ? null! :*/ Convert.ToDateTime(request.fecha_recepcion_demanda),
                        //Convert.ToDateTime(request.fecha_recepcion),
                        request.id_juzgado,
                        request.rfc_quejoso,
                        request.nombre_quejoso,
                        request.id_materia,
                        request.id_submateria,
                        request.id_tipo_acto,
                        request.despacho
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAdministradorService.UpdateAmparoIndirectoAdministrador(entity);
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
        #region PENDIENTES
        /// <summary>
        ///  GetBandeja Obtiene todos los registros
        ///  para la Bandeja de Pendientes para el Administrador con estatus 
        ///  tarea = "Pendiente de Turnar" y estado_procesal ="Activo"
        ///  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<ResponseAmparoIndirectoByFilters>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("pendientes")]
        public async Task<IActionResult> GetBandeja([FromQuery] PagerQuery request
        )
        {
            try
            {
                if (!Filters.MapSort<EnumOrderColumnAmparoIndirectoByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseAmparoIndirectoByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }

                var result = await _amparoindirectoAdministradorService.GetAllBandejaPendientesAdministrador(                                                              
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
        #region  HISTORICO
        /// <summary>
        ///  GetHistorico Obtiene todos los registros
        ///  para el Histórico de Asuntos
        ///  para el Rol de Administrador
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
                RequestHistoricoAdministradorFilters filters = new();
                Filters.MapFilters(request, filters);

                if (!Filters.MapSort<EnumOrderColumnAdministradorByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseHistoricoAdministradorByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }

                //string orderByColumn = null!;
                //bool orderDesc = false;
                if (request.sort is not null && request.sort.Any())
                {
                    orderByColumn = request.sort.FirstOrDefault().Key;
                    if (!Enum.TryParse<EnumOrderColumnAdministradorByFiltros>(orderByColumn, out _))
                    {
                        return Ok(
                        ResultOperation<
                            List<ResponseHistoricoAdministradorByFilters>>.FailureWarningResponse("La columna de ordenamiento no es válida.")
                        );
                    }
                    orderDesc = request.sort.FirstOrDefault().Value;
                }
                //var result = await _amparoindirectoOficialPartesService.GetAmparoIndirectoDisconnectedByFilters(
                var result = await _amparoindirectoAdministradorService.GetHistoricoByFilters(
                    request.fetch,
                    request.page,
                    orderByColumn,
                    orderDesc,
                    Filters.GetDateTimeValue(filters!.ByFechaRecepcionInicial.FirstOrDefault()),
                    Filters.GetDateTimeValue(filters!.ByFechaRecepcionFinal.FirstOrDefault()),
                    Filters.GetDateTimeValue(filters!.ByFechaVencimientoDemandaIncial.FirstOrDefault()),
                    Filters.GetDateTimeValue(filters!.ByFechVencimientoDemandaFinal.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByNumeroExpediente.FirstOrDefault())!,
                    Filters.GetStringValue(filters!.ByNumeroAsunto.FirstOrDefault())!,
                    //Filters.GetStringValue(filters!.ByJuicioAmparo.FirstOrDefault())!, se modifico
                    Filters.GetIntValue(filters!.ByIdJuzgado.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByNombreQuejoso.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdMateria.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdSubmateria.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdTipoActo.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByDespacho.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdAdministracion.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdSubAdministracion.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdAutoridadResponsable.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByRfcQuejoso.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdEstadoTarea.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdEstadoProcesal.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdEstadoProcesalIncidental.FirstOrDefault())

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
        #region GETBYID PARA FRONT

        [ProducesResponseType(typeof(ResultOperation<ResponseAmparoIndirectoByID>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAmparoIndirecto([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAdministradorService.GetByIdAmparo(id);

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
                var entityExists = await _amparoindirectoAdministradorService.GetAmparoIndirectoByIdDisconnected(id);
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

                var entityExists = await _amparoindirectoAdministradorService.GetAmparoIndirectoByIdDisconnected(id);
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

    }
}
