using System;
using System.Configuration;
using AmparoIndirectoAPI.Model.DAO.ServicesDAO;
using AmparoIndirectoAPI.Model.DTO;
using AmparoIndirectoAPI.Model.DTO.ContractsValidations;
using AmparoIndirectoAPI.Model.Entities;
using AmparoIndirectoAPI.Model.Entities.Events;
using AmparoIndirectoAPI.Model.Entities.Events.OficialPartes;
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
    [Route("sicoj/amparo-indirecto/api/v1/abogado/amparo-indirecto")]
    public class AbogadoController : ControllerBase
    {
        #region Variables / Constructor
        private readonly ILogger<AbogadoController> _logger;


        #region Abogado
        private readonly IValidator<RequestCreateInformePrevioIncidental> _validatorCreateInformePrevioIncidental;
        private readonly IValidator<RequestCreateAutoridadResponsableAbogado> _validatorCreateAutoridadResponsableAbogado;
        private readonly IValidator<RequestCreateNotaLitigio> _validatorCreateNotaLitigio;
        private readonly IValidator<RequestCreateInformacionAdicional> _validatorCreateInformacionAdicional;

        private readonly IAmparoIndirectoAbogadoService _amparoindirectoAbogadoService;
        private readonly IValidator<RequestCreateSuspensionProvisionalAbogado> _validatorSuspensionProvisionalAbogado;
        private readonly IValidator<RequestCreateSentenciaIncidental> _validatorSentenciaIncidental;
        private readonly IValidator<RequestRecursoQuejaIncidental> _validatorRecursoQuejaIncidental;
        private readonly IValidator<RequestCreateSentenciaConstitucional> _validatorSentenciaConstitucional;
        private readonly IValidator<RequestCreateCumplimientoFalloProtectorConstitucional> _validatorCreateCumplimientoFalloProtector;


        private readonly IValidator<RequestCreateInformeJustificadoConstitucional> _validatorCreateInformeJustificado;
        private readonly IValidator<RequestCreateRecursoQuejaConstitucional> _validatorCreateRecursoQuejaConstitucional;
        private readonly IValidator<RequestCreateRecursoRevisionConstitucional> _validatorCreateRecursoRevisionConstitucional;
        private readonly IValidator<RequestCreateRecursoInconformidadConstitucional> _validatorCreateRecursoInconformidadConstitucional;
        private readonly IValidator<RequestCreateRecursoReclamacionConstitucional> _validatorCreateRecursoReclamacionConstitucional;
        private readonly IValidator<RequestCreateIncidenteExcesoIncidental> _validatorCreateIncidenteExcesoIncidental;
        private readonly IValidator<RequestCreateRecursoRevisionIncidental> _validatorCreateRecursoRevisionIncidental;



        #endregion 

        private readonly IRedisClient _redisClient;
        private static object _lock = new object();
        private readonly IFileSystemService _fileSystemService;

        public AbogadoController(
            ILogger<AbogadoController> logger,
            IRedisClient redisClient,
            IFileSystemService fileSystemService,
            IAmparoIndirectoAbogadoService amparoindirectoAbogadoService,
            IValidator<RequestCreateInformePrevioIncidental> validatorCreateInformePrevioIncidental,
            IValidator<RequestCreateSuspensionProvisionalAbogado> validatorSuspensionProvisionalAbogado,
            IValidator<RequestCreateSentenciaIncidental> validatorSentenciaIncidental,
            IValidator<RequestCreateAutoridadResponsableAbogado> validatorCreateAutoridadResponsableAbogado,
            IValidator<RequestCreateNotaLitigio> validatorCreateNotaLitigio,
            IValidator<RequestCreateSentenciaConstitucional> validatorSentenciaConstitucional,
            IValidator<RequestCreateCumplimientoFalloProtectorConstitucional> validatorCreateCumplimientoFalloProtector,
            IValidator<RequestCreateInformeJustificadoConstitucional> validatorCreateInformeJustificado,
            IValidator<RequestCreateRecursoQuejaConstitucional> validatorCreateRecursoQuejaConstitucional,
            IValidator<RequestCreateRecursoRevisionConstitucional> validatorCreateRecursoRevisionConstitucional,
            IValidator<RequestCreateRecursoInconformidadConstitucional> validatorCreateRecursoInconformidadConstitucional,
            IValidator<RequestCreateRecursoReclamacionConstitucional> validatorCreateRecursoReclamacionConstitucional,
            IValidator<RequestRecursoQuejaIncidental> validatorRecursoQuejaIncidental,
            IValidator<RequestCreateInformacionAdicional> validatorCreateInformacionAdicional,
            IValidator<RequestCreateIncidenteExcesoIncidental> validatorCreateIncidenteExcesoIncidental,
            IValidator<RequestCreateRecursoRevisionIncidental> validatorCreateRecursoRevisionIncidental

            )
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _amparoindirectoAbogadoService = amparoindirectoAbogadoService ?? throw new ArgumentNullException(nameof(amparoindirectoAbogadoService));
            _validatorCreateInformePrevioIncidental = validatorCreateInformePrevioIncidental ?? throw new ArgumentNullException(nameof(validatorCreateInformePrevioIncidental));
            _validatorSuspensionProvisionalAbogado = validatorSuspensionProvisionalAbogado ?? throw new ArgumentNullException(nameof(validatorSuspensionProvisionalAbogado));
            _validatorSentenciaIncidental = validatorSentenciaIncidental ?? throw new ArgumentNullException(nameof(validatorSentenciaIncidental));
            _validatorCreateAutoridadResponsableAbogado = validatorCreateAutoridadResponsableAbogado ?? throw new ArgumentNullException(nameof(validatorCreateAutoridadResponsableAbogado));
            _validatorCreateNotaLitigio = validatorCreateNotaLitigio ?? throw new ArgumentNullException(nameof(validatorCreateNotaLitigio));
            _validatorSentenciaConstitucional = validatorSentenciaConstitucional ?? throw new ArgumentNullException(nameof(validatorSentenciaConstitucional));
            _validatorCreateCumplimientoFalloProtector = validatorCreateCumplimientoFalloProtector ?? throw new ArgumentNullException(nameof(validatorCreateCumplimientoFalloProtector));
            _validatorCreateInformeJustificado = validatorCreateInformeJustificado ?? throw new ArgumentNullException(nameof(validatorCreateInformeJustificado));
            _validatorCreateRecursoQuejaConstitucional = validatorCreateRecursoQuejaConstitucional ?? throw new ArgumentNullException(nameof(validatorCreateRecursoQuejaConstitucional));
            _validatorCreateRecursoRevisionConstitucional = validatorCreateRecursoRevisionConstitucional ?? throw new ArgumentNullException(nameof(validatorCreateRecursoRevisionConstitucional));
            _validatorCreateRecursoInconformidadConstitucional = validatorCreateRecursoInconformidadConstitucional ?? throw new ArgumentNullException(nameof(validatorCreateRecursoInconformidadConstitucional));
            _validatorCreateRecursoReclamacionConstitucional = validatorCreateRecursoReclamacionConstitucional ?? throw new ArgumentNullException(nameof(validatorCreateRecursoReclamacionConstitucional));
            _validatorRecursoQuejaIncidental = validatorRecursoQuejaIncidental ?? throw new ArgumentNullException(nameof(validatorRecursoQuejaIncidental));
            _validatorCreateInformacionAdicional = validatorCreateInformacionAdicional ?? throw new ArgumentNullException(nameof(validatorCreateInformacionAdicional));
            _validatorCreateIncidenteExcesoIncidental = validatorCreateIncidenteExcesoIncidental ?? throw new ArgumentNullException(nameof(validatorCreateIncidenteExcesoIncidental));
            _validatorCreateRecursoRevisionIncidental = validatorCreateRecursoRevisionIncidental ?? throw new ArgumentNullException(nameof(validatorCreateRecursoRevisionIncidental));
        }
        #endregion

        #region PENDIENTES
        /// <summary>
        ///  GetBandeja Obtiene todos los registros
        ///  para la Bandeja de Pendientes para el Abogado con estatus 
        ///  tarea = "Pendiente de Turnar" y estado_procesal ="Activo"
        ///  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<ResponseAbogadoByFilters>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("pendientes")]
        public async Task<IActionResult> GetBandejaAbogado([FromQuery] PagerQuery request
        )
        {
            try
            {
                if (!Filters.MapSort<EnumOrderColumnAmparoIndirectoByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseAbogadoByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }

                ////var result = await _amparoindirectoAdministradorService.GetAllBandejaPendientesAdministrador(
                var result = await _amparoindirectoAbogadoService.GetAllBandejaPendientesAbogado(
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
        ///  para el Rol de Abogado
        /// </summary>
        /// <param name = "request" ></ param >
        /// < returns ></ returns >
        [ProducesResponseType(typeof(ResultOperation<ResponseAbogadoByFilters>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("historico")]
        public async Task<IActionResult> GetHistoricoAbogado(
            [FromQuery] PagerQueryFilters request
        )
        {
            try
            {
                RequestAbogadoFilters filters = new();
                Filters.MapFilters(request, filters);

                if (!Filters.MapSort<EnumOrderColumnAbogadoByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    //ResponseAbogadoByFilters
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseHistoricoAbogadoByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }

                //string orderByColumn = null!;
                //bool orderDesc = false;
                if (request.sort is not null && request.sort.Any())
                {
                    orderByColumn = request.sort.FirstOrDefault().Key;
                    if (!Enum.TryParse<EnumOrderColumnAbogadoByFiltros>(orderByColumn, out _))
                    {
                        return Ok(
                        ResultOperation<
                            List<ResponseHistoricoAbogadoByFilters>>.FailureWarningResponse("La columna de ordenamiento no es válida.")
                        );
                    }
                    orderDesc = request.sort.FirstOrDefault().Value;
                }

                //var result = await _amparoindirectoOficialPartesService.GetAmparoIndirectoDisconnectedByFilters(
                var result = await _amparoindirectoAbogadoService.GetHistoricoAbogadoByFilters(
                    request.fetch,
                    request.page,
                    orderByColumn,
                    orderDesc,
                    Filters.GetDateTimeValue(filters!.ByFechaRecepcionInicial.FirstOrDefault()),
                    Filters.GetDateTimeValue(filters!.ByFechaRecepcionFinal.FirstOrDefault()),
                    Filters.GetDateTimeValue(filters!.ByFechaInicialVencimiento.FirstOrDefault()),
                    Filters.GetDateTimeValue(filters!.ByFechaFinalVencimiento.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByNumeroExpediente.FirstOrDefault())!,
                    Filters.GetStringValue(filters!.ByNumeroAsunto.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdJuzgado.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByNombreQuejoso.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdMateria.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdSubmateria.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdTipoActo.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByDespacho.FirstOrDefault())!,
                    Filters.GetIntValue(filters!.ByIdAdministracion.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdSubAdministracion.FirstOrDefault()),
                    Filters.GetIntValue(filters!.ByIdAutoridadResponsable.FirstOrDefault()),
                    Filters.GetStringValue(filters!.ByRrfcQuejoso.FirstOrDefault())!,
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

        #region CREATE NOTAS DE LITIGIO 

        /// <summary>
        /// Metodo para registrar una nota de litigo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("nota-litigio")]

        public async Task<IActionResult> PostNotasLitigio([FromForm]
            RequestCreateNotaLitigio request
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

                var validationResult = await _validatorCreateNotaLitigio.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.idNumeroAsunto);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de Amparo Indirecto no existe."
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
                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.idTipoDocumento > 0 ||
                    request.idSeccion > 0)
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
                            Path.Combine("AMPARO INDIRECTO", request.idNumeroAsunto.ToString()),
                            out dataFile
                        );
                    }
                }
                NotaLitigio entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateNotaLitigio(
                        request.idNumeroAsunto,
                        Convert.ToDateTime(request.fechaRegistroNota),
                        request.idTipoDocumento,
                        request.idSeccion,
                        sessionInformation.UserInformation.Rfc
                    );

                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.idNumeroAsunto,
                            request.idTipoDocumento,
                            request.idSeccion,
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoAbogadoService.CreateNotaLitigio(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoAbogadoService.CreateNotaLitigio(entity, null!, null!);
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

        #region DELETE NOTA DE LITIGIO

        /// <summary>
        /// Metodo para eliminar el registro de Nota de Litigio a Eliminado
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpDelete("nota-litigio")]

        public async Task<IActionResult> PathDeleteNotaLitigio(
            int idAmparo, int idNotaLitigio)
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
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(idAmparo);
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

                var entityNotaLitigio = await _amparoindirectoAbogadoService.GetByIdNotaLitigio(idNotaLitigio);
                if (entityNotaLitigio is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "La nota litigio no existe."
                        )
                    );
                }

                var result = await _amparoindirectoAbogadoService.UpdateDeleteNotaLitigio(idNotaLitigio);
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

        #region GET NOTA DE LITIGIO

        [ProducesResponseType(typeof(ResultOperation<ResponseNotaLitigioById>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("nota-litigio-id")]

        public async Task<IActionResult> GetByIdNotaLitigio([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetByIdNotaLitigio(id);

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

        #region CREATE AUTORIDAD RESPONSABLE

        /// <summary>
        /// Metodo para registrar una autoridad responsable
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("autoridad-responsable")]

        public async Task<IActionResult> PostAutoridadResponsable(
            RequestCreateAutoridadResponsableAbogado request
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



                var validationResult = await _validatorCreateAutoridadResponsableAbogado.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }

                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe.")
                    );
                }

                var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(request.id);
                if (entityAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
                    );
                }

                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{request.id}");
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

                RequestCreateAutoridadResponsableAbogado entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateAutoridadesResponsables(
                        ref request,
                        ref entityExists,
                        ref entityAutoridadesExists
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }

                var result = await _amparoindirectoAbogadoService.CreateAutoridadResponsableAbogado(request);

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

        #region GET ALL INFORMACIÓN ADICIONAL

        [ProducesResponseType(typeof(ResultOperation<ResponseInformacionAdicional>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("informacion-adicional")]
        public async Task<IActionResult> GetAllInformacionAdicional([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllInformacionAdicional(id);

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

        #region CREATE INFORMACIÓN ADICIONAL

        /// <summary>
        /// Metodo para registrar información adicional a un juicio
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("informacion-adicional")]

        public async Task<IActionResult> PostInformacionAdicional(
            RequestCreateInformacionAdicional request
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



                var validationResult = await _validatorCreateInformacionAdicional.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }

                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe.")
                    );
                }

                var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{request.id}");
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

                InformacionAdicional entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateInformacionAdicional(
                        request.id,
                        string.IsNullOrEmpty(request.fecha_resolucion_oficio) ? null! : Convert.ToDateTime(request.fecha_resolucion_oficio),
                        request.cuantia,
                        request.oficio_resolucion_reclamado,
                        request.id_autoridad_emisora_resolucion_oficio,
                        request.concepto_violacion,
                        sessionInformation.UserInformation.Rfc

                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }

                var result = await _amparoindirectoAbogadoService.CreateInformacionAdicional(entity);

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
                var result = await _amparoindirectoAbogadoService.GetByIdAmparo(id);

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

        #region GET ALL CUADERNO CONSTITUCIONAL

        [ProducesResponseType(typeof(ResultOperation<RequestCuadernoConstitucional>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("cuaderno-constitucional")]
        public async Task<IActionResult> GetAllCuadernoConstitucional([FromQuery] int id, int idEstadoProcesal)
        {
            try
            {
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe."
                        )
                    );
                }
                var result = await _amparoindirectoAbogadoService.GetCuadernoConstitucionalRecurrente(id, idEstadoProcesal);

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

        #region GET ALL INFORME JUSTIFICADO

        [ProducesResponseType(typeof(ResultOperation<ResponseInformeJustificadoConstitucional>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("informe-justificado-constitucional")]
        public async Task<IActionResult> GetAllInformeJustificadoConstitucional([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllInformeJustificadoConstitucional(id);

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

        #region CREATE INFORME JUSTIFICADO (Constitucional)

        /// <summary>
        /// Metodo para crear Informe Justificado Constitucional
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("informe-justificado-constitucional")]
        public async Task<IActionResult> PostInformeJustificado([FromForm] RequestCreateInformeJustificadoConstitucional request
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

                var validationResult = await _validatorCreateInformeJustificado.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }

                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.idNumeroAsunto);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de Amparo Indirecto no existe o fue eliminada."
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

                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.idTipoArchivo.GetValueOrDefault() > 0 ||
                    request.idSeccion.GetValueOrDefault() > 0)
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
                            Path.Combine("AMPARO INDIRECTO", request.idNumeroAsunto.ToString()),
                            out dataFile
                        );
                    }
                }

                InformeJustificadoConstitucional entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateInformeJustificado(
                        request.id,
                        request.idNumeroAsunto,
                        request.idAutoridadResponsable,
                        request.solicitudOpinionTecnica,
                        Convert.ToDateTime(request.fechaRecepcionDemanda),
                        Convert.ToDateTime(request.fechaVencimientoJustificado),
                        request.numeroOficioInformeJustificado,
                        string.IsNullOrEmpty(request.fechaOficioInformeJustificado) ? null! : Convert.ToDateTime(request.fechaOficioInformeJustificado),
                        Convert.ToDateTime(request.fechaPresentacionInformeJustificado),
                        request.observacionesJustificado,
                        sessionInformation.UserInformation.Rfc
                    );

                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.idNumeroAsunto,
                            request.idTipoArchivo.GetValueOrDefault(),
                            request.idSeccion.GetValueOrDefault(),
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoAbogadoService.CreateInformeJustificado(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoAbogadoService.CreateInformeJustificado(entity, null!, null!);
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

        #region  CREATE INFORME JUSTIFICADO CONSTITUCIONAL VALIDACION

        /// <summary>
        /// Metodo para cambiar el estado procesal a Pendiente de Sentencia Constitucional
        /// y para validar que todos los campos esten llenos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("informe-justificado-constitucional-validacion")]
        public async Task<IActionResult> UpdateInformeJustificadoConstitucional(int id)
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

                //var validationResult = await _validatorSentenciaConstitucional.ValidateAsync(request);
                //if (!validationResult.IsValid)
                //{
                //    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                //}
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe o fue eliminado."
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
                var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(id);
                if (entityAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
                    );
                }
                //Cambiar este metodo porque ya no va a existir esta tabla ahora debo apuntar a la tabla de informe justificado
                //var entityCuadernoAutoridadesExists = await _amparoindirectoAbogadoService.GetCuadernoConstitucionalIdDisconnected(id);
                var entityInformeJustificadoExists = await _amparoindirectoAbogadoService.GetIndormeJustificadoIdDisconnected(id);
                if (entityInformeJustificadoExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no ha llenado el cuaderno constitucional responsable registrada.")
                    );
                }

                bool entity = false!;
                try
                {
                    entity = AbogadoEvents.UpdateInformeJustificadoConstitucional(
                        ref entityAutoridadesExists,
                        ref entityInformeJustificadoExists
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.UpdateInformeJustificadoConstitucional(id, sessionInformation.UserInformation.Rfc);

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

        #region GET ALL SENTENCIA CONSTITUCIONAL

        [ProducesResponseType(typeof(ResultOperation<ResponseSentenciaConstitucional>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("sentencia-constitucional")]
        public async Task<IActionResult> GetAllSentenciaConstitucional([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllSentenciaConstitucional(id);

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

        #region  CREATE SENTENCIA CONSTITUCIONAL

        /// <summary>
        /// Metodo para generar un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("sentencia-constitucional")]
        public async Task<IActionResult> CreateSentenciaConstitucional([FromForm] RequestCreateSentenciaConstitucional request)
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

                var validationResult = await _validatorSentenciaConstitucional.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe."
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
                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.idTipoArchivo.GetValueOrDefault() > 0 ||
                    request.idSeccion.GetValueOrDefault() > 0)
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

                SentenciaConstitucional entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateSentenciaConstitucional(
                        request.id,
                        request.idAutoridadResponsable,
                        string.IsNullOrEmpty(request.fechaNotificacionSentencia) ? null! : Convert.ToDateTime(request.fechaNotificacionSentencia),
                        request.idSentidoSentencia,
                        request.idTipoSentidoSentencia,
                        request.idDictamenNoRevision,
                        request.idSentidoGeneralAsunto,
                        request.idTipoSentidoGeneral,
                        request.oficioComunicacionAutoridad,
                        string.IsNullOrEmpty(request.fechaOficioComunicacionSentencia) ? null! : Convert.ToDateTime(request.fechaOficioComunicacionSentencia),
                        string.IsNullOrEmpty(request.fechaPresentacionOficioComunicacion) ? null! : Convert.ToDateTime(request.fechaPresentacionOficioComunicacion),
                        string.IsNullOrEmpty(request.fechaRecepcionAutoSentenciaEjecutoria) ? null! : Convert.ToDateTime(request.fechaRecepcionAutoSentenciaEjecutoria),
                        request.comunicadoAcuerdoFirmeza,
                        string.IsNullOrEmpty(request.fechaComunicacionAcuerdoFirmeza) ? null! : Convert.ToDateTime(request.fechaComunicacionAcuerdoFirmeza),
                        string.IsNullOrEmpty(request.fechaConclusionExpediente) ? null! : Convert.ToDateTime(request.fechaConclusionExpediente),
                        sessionInformation.UserInformation.Rfc
                    );
                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.id,
                            request.idTipoArchivo.GetValueOrDefault(),
                            request.idSeccion.GetValueOrDefault(),
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoAbogadoService.CreateSentenciaConstitucional(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoAbogadoService.CreateSentenciaConstitucional(entity, null!, null!);
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

        #region  CREATE SENTENCIA CONSTITUCIONAL VALIDACION

        /// <summary>
        /// Metodo para generar un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("sentencia-constitucional-validacion")]
        public async Task<IActionResult> UpdateSentenciaConstitucional(int id)
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

                //var validationResult = await _validatorSentenciaConstitucional.ValidateAsync(request);
                //if (!validationResult.IsValid)
                //{
                //    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                //}
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe o fue eliminado."
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
                var entityCuadernoAutoridadesExists = await _amparoindirectoAbogadoService.GetAllSentenciaConstitucionalDisconnected(id);
                //var entityCuadernoAutoridadesExists = await _amparoindirectoAbogadoService.GetCuadernoConstitucionalIdDisconnected(id);
                if (entityCuadernoAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no ha llenado el cuaderno constitucional responsable registrada.")
                    );
                }

                bool entity = false!;
                try
                {
                    entity = AbogadoEvents.UpdateSentenciaConstitucional(
                        ref entityCuadernoAutoridadesExists
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.UpdateSentenciaConstitucional(id, sessionInformation.UserInformation.Rfc);

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

        #region GET ALL CUMPLIMIENTO FALLO PROTECTOR CONSTITUCIONAL

        [ProducesResponseType(typeof(ResultOperation<ResponseCumplimientoFalloProtector>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("cumplimiento-fallo-protector-constitucional")]
        public async Task<IActionResult> GetAllCumplimientoFalloConstitucional([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllCumplimientoFalloConstitucional(id);

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

        #region  CREATE CUMPLIMIENTO FALLO PROTECTOR CONSTITUCIONAL VALIDACION

        ///// <summary>
        ///// Metodo para generar un registro de amparo indirecto
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[ProducesResponseType(typeof(ResultOperation<int>), 200)]
        //[ProducesResponseType(typeof(ResultOperation), 400)]
        ////[Authorize(EnumRoles.JAI_OP)]
        //[Authorize(EnumRoles.RR_OP)]
        //[HttpPatch("cumplimiento-fallo-protector-constitucional-validacion")]
        //public async Task<IActionResult> UpdateCumplimientoFalloProtectorConstitucional(int id)
        //{
        //    try
        //    {
        //        //             UserSession sessionInformation = new()
        //        //             {
        //        //                 UserInformation = new()
        //        //                 {
        //        //                     Rfc = "JUAN"
        //        //                 }
        //        //,
        //        //                 TokenInfomation = new()
        //        //                 {
        //        //                     workforceID = "00000001032"
        //        //                 }
        //        //             };
        //        var sessionInformation = UserSession.GetValue(HttpContext);
        //        if (sessionInformation is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse(
        //                    "No se pudo obtener la información del usuario."
        //                )
        //            );
        //        }

        //        //var validationResult = await _validatorSentenciaConstitucional.ValidateAsync(request);
        //        //if (!validationResult.IsValid)
        //        //{
        //        //    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
        //        //}
        //        var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
        //        if (entityExists is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse<bool>(
        //                    "El juicio de amparo indirecto no existe o fue eliminado."
        //                )
        //            );
        //        }
        //        var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
        //        if (keyExists is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse<int>("El registro no se puede modificar ya que el usuario no lo ha tomado.")
        //            );
        //        }

        //        if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
        //        {
        //            return Ok(
        //                ResultOperation.FailureInformationResponse(
        //                    "El registro ya se encuentra en uso por otro usuario."
        //                )
        //            );
        //        }
        //        var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(id);
        //        if (entityAutoridadesExists is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse<bool>(
        //                    "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
        //            );
        //        }

        //        var entityCuadernoAutoridadesExists = await _amparoindirectoAbogadoService.GetCuadernoConstitucionalIdDisconnected(id);
        //        if (entityCuadernoAutoridadesExists is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse<bool>(
        //                    "El juicio de amparo indirecto no ha llenado el cuaderno constitucional responsable registrada.")
        //            );
        //        }

        //        bool entity = false!;
        //        try
        //        {
        //            entity = AbogadoEvents.UpdateCumplimientoFalloProtector(
        //                ref entityAutoridadesExists,
        //                ref entityCuadernoAutoridadesExists
        //            );
        //        }
        //        catch (Exception _ex)
        //        {
        //            return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
        //        }
        //        var result = await _amparoindirectoAbogadoService.UpdateCumplimientoFalloProtector(id);

        //        return Ok(result);

        //    }
        //    catch (Exception _e)
        //    {
        //        _logger.LogError(_e, "Ocurrió un error al generar el cumplimiento de fallo protector del libro constitucional.");
        //        return BadRequest(
        //            ResultOperation.FailureErrorResponse(
        //                "Ha ocurrido un error inesperado al intentar crear el cumplimiento de fallo protector del libro constitucional."
        //            )
        //        );
        //    }
        //}
        #endregion

        #region  CREATE CUMPLIMIENTO FALLO PROTECTOR CONSTITUCIONAL

        /// <summary>
        /// Metodo para generar un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("cumplimiento-fallo-protector-constitucional")]
        public async Task<IActionResult> CreateSentenciaConstitucional([FromForm]
            RequestCreateCumplimientoFalloProtectorConstitucional request
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

                var validationResult = await _validatorCreateCumplimientoFalloProtector.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.idNumeroAsunto);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe."
                        )
                    );
                }
                var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(request.idNumeroAsunto);
                if (entityAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
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
                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.idTipoArchivo.GetValueOrDefault() > 0 ||
                    request.idSeccion.GetValueOrDefault() > 0)
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
                            Path.Combine("AMPARO INDIRECTO", request.idNumeroAsunto.ToString()),
                            out dataFile
                        );
                    }
                }

                CumplimientoFalloProtectorConstitucional entity = null!;
                try
                {
                    entity = AbogadoEvents.UpdateCumplimientoFalloProtectorAutoridadOp(
                        request.idNumeroAsunto,
                        request.idAutoridadResponsable,
                         //request.cumplimiento_fallo,
                         Convert.ToDateTime(request.fechaNotificacionRequerimiento),
                        //string.IsNullOrEmpty(request.fecha_notificacion_requerimiento) ? null! : Convert.ToDateTime(request.fecha_notificacion_requerimiento),
                        request.plazoFallo,
                        //string.IsNullOrEmpty(request.fecha_vencimiento_requerimiento) ? null! : Convert.ToDateTime(request.fecha_vencimiento_requerimiento),
                        string.IsNullOrEmpty(request.fechaPresentacionFallo) ? null! : Convert.ToDateTime(request.fechaPresentacionFallo),
                        request.numeroOficioAtencion,
                        string.IsNullOrEmpty(request.fechaNotificacionAcuerdoFallo) ? null! : Convert.ToDateTime(request.fechaNotificacionAcuerdoFallo),
                        string.IsNullOrEmpty(request.fechaOficioComunicacionFallo) ? null! : Convert.ToDateTime(request.fechaOficioComunicacionFallo),
                        request.numeroOficioComunicacionAutoridad,
                        sessionInformation.UserInformation.Rfc
                    );
                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.idNumeroAsunto,
                            request.idTipoArchivo.GetValueOrDefault(),
                            request.idSeccion.GetValueOrDefault(),
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoAbogadoService.CreateCumplimientoFalloProtector(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoAbogadoService.CreateCumplimientoFalloProtector(entity, null!, null!);
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

        #region GET ALL RECURSO DE QUEJA PRINCIPAL (CONSTITUCIONAL)

        [ProducesResponseType(typeof(ResultOperation<ResponseRecursoQuejaPrincipalConstitucional>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("recurso-queja-constitucional")]
        public async Task<IActionResult> GetAllRecursoQuejaPrincipalConstitucional([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllRecursoQuejaPrincipalConstitucional(id);

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

        #region CREATE RECURSO DE QUEJA PRINCIPAL(Constitucional)

        /// <summary>
        /// Metodo para crear recurso de queja principal libro Constitucional
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("recurso-queja-constitucional")]

        public async Task<IActionResult> PostRecursoQuejaConstitucional(
            RequestCreateRecursoQuejaConstitucional request
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
                var validationResult = await _validatorCreateRecursoQuejaConstitucional.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }

                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id_numero_asunto);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de Amparo Indirecto no existe."
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

                RecursoQuejaPrincipalConstitucional entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateRecursoQuejaPrincipalConstitucional(
                        request.id,
                        request.id_numero_asunto,//SE AGREGA CAMPO PARA ACTUALIZAR
                        request.id_recurrente,
                        request.recurso_queja_principal,
                        request.id_autoridad_responsable,
                        Convert.ToDateTime(request.fecha_recepcion_acuerdo_queja),
                        //string.IsNullOrEmpty(request.fecha_recepcion_acuerdo_queja) ? null! : Convert.ToDateTime(request.fecha_recepcion_acuerdo_queja),
                        //string.IsNullOrEmpty(request.fecha_vencimiento_recurso_queja) ? null! : Convert.ToDateTime(request.fecha_vencimiento_recurso_queja),
                        request.oficio_recurso_queja,
                        string.IsNullOrEmpty(request.fecha_presentacion_queja) ? null! : Convert.ToDateTime(request.fecha_presentacion_queja),
                        request.id_motivo_recurso_queja,
                        string.IsNullOrEmpty(request.fecha_admision_queja) ? null! : Convert.ToDateTime(request.fecha_admision_queja),
                        request.id_organo_radicacion_queja,
                        request.toca_queja,
                        string.IsNullOrEmpty(request.fecha_notificacion_ejecutoria) ? null! : Convert.ToDateTime(request.fecha_notificacion_ejecutoria),
                        request.id_sentido_resolucion_queja,
                        request.oficio_comunicacion_area_correspondiente,
                        string.IsNullOrEmpty(request.fecha_oficio_comunicacion) ? null! : Convert.ToDateTime(request.fecha_oficio_comunicacion),
                        sessionInformation.UserInformation.Rfc
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }

                var result = await _amparoindirectoAbogadoService.CreateRecursoQuejaConstitucional(entity);

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

        #region  CREATE RECURSO QUEJA PRINCIPAL CONSTITUCIONAL VALIDACION

        ///// <summary>
        ///// COMENTADO POR SI SE REQUIERE VALIDAR EL CAMBIO DE UN ESTADO PROCESAL
        ///// Metodo para generar un registro de amparo indirecto
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[ProducesResponseType(typeof(ResultOperation<int>), 200)]
        //[ProducesResponseType(typeof(ResultOperation), 400)]
        ////[Authorize(EnumRoles.JAI_OP)]
        //[Authorize(EnumRoles.RR_OP)]
        //[HttpPatch("recurso-queja-constitucional-validacion")]
        //public async Task<IActionResult> UpdateRecursoQuejaValidacionConstitucional(int id, bool recursoQuejaPrincipal)
        //{
        //    try
        //    {
        //        var sessionInformation = UserSession.GetValue(HttpContext);
        //        if (sessionInformation is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse(
        //                    "No se pudo obtener la información del usuario."
        //                )
        //            );
        //        }
        //        var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
        //        if (entityExists is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse<bool>(
        //                    "El juicio de amparo indirecto no existe o fue eliminado."
        //                )
        //            );
        //        }
        //        var keyExists = await _redisClient.GetValueAsync<UserBlockingRegistration>($"{EnumModulosRedis.AMPARO_INDIRECTO.ToStringValue()}{entityExists.id}");
        //        if (keyExists is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse<int>("El registro no se puede modificar ya que el usuario no lo ha tomado.")
        //            );
        //        }

        //        if (!keyExists.rfc.Equals(sessionInformation.UserInformation.Rfc))
        //        {
        //            return Ok(
        //                ResultOperation.FailureInformationResponse(
        //                    "El registro ya se encuentra en uso por otro usuario."
        //                )
        //            );
        //        }
        //        var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(id);
        //        if (entityAutoridadesExists is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse<bool>(
        //                    "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
        //            );
        //        }

        //        var entityCuadernoAutoridadesExists = await _amparoindirectoAbogadoService.GetCuadernoConstitucionalIdDisconnected(id);
        //        if (entityCuadernoAutoridadesExists is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse<bool>(
        //                    "El juicio de amparo indirecto no ha llenado el cuaderno constitucional responsable registrada.")
        //            );
        //        }
        //        bool entity = false!;
        //        try
        //        {
        //            entity = AbogadoEvents.UpdateRecursoQuejaConstitucional(
        //                ref entityAutoridadesExists,
        //                ref entityCuadernoAutoridadesExists,
        //                entityExists.recurso_queja_principal,
        //                recursoQuejaPrincipal
        //            );
        //        }
        //        catch (Exception _ex)
        //        {
        //            return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
        //        }
        //        var result = await _amparoindirectoAbogadoService.UpdateRecursoQuejaConstitucional(id, recursoQuejaPrincipal);

        //        return Ok(result);

        //    }
        //    catch (Exception _e)
        //    {
        //        _logger.LogError(_e, "Ocurrió un error al generar el informe justificado.");
        //        return BadRequest(
        //            ResultOperation.FailureErrorResponse(
        //                "Ha ocurrido un error inesperado al intentar crear el informe justificado."
        //            )
        //        );
        //    }
        //}
        #endregion

        #region GET ALL RECURSO REVISION PRINCIPAL (CONSTITUCIONAL)

        [ProducesResponseType(typeof(ResultOperation<ResponseRecursoRevisionPrincipal>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("recurso-revision-constitucional")]
        public async Task<IActionResult> GetAllRecursoRevisionConstitucional([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllRecursoRevisionConstitucional(id);

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

        #region CREATE RECURSO DE REVISION PRINCIPAL(Constitucional)

        /// <summary>
        /// Metodo para crear recurso de revision principal libro Constitucional
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("recurso-revision-constitucional")]
        public async Task<IActionResult> PostRecursoRevisionConstitucional(
            RequestCreateRecursoRevisionConstitucional request
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

                var validationResult = await _validatorCreateRecursoRevisionConstitucional.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }


                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id);
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

                RecursoRevisionPrincipalConstitucional entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateRecursoRevisionPrincipalConstitucional(
                        request.id,
                        request.id_recurrente,
                        request.recurso_revision_principal,
                        request.id_autoridad_responsable,
                        Convert.ToDateTime(request.fecha_recepcion_sentencia),
                        //string.IsNullOrEmpty(request.fecha_recepcion_sentencia) ? null! : Convert.ToDateTime(request.fecha_recepcion_sentencia),
                        //string.IsNullOrEmpty(request.fecha_vencimiento_recurso_revision) ? null! : Convert.ToDateTime(request.fecha_vencimiento_recurso_revision),
                        request.oficio_recurso,
                        string.IsNullOrEmpty(request.fecha_presentacion_revision) ? null! : Convert.ToDateTime(request.fecha_presentacion_revision),
                        Convert.ToDateTime(request.fecha_admision),
                        //string.IsNullOrEmpty(request.fecha_admision) ? null! : Convert.ToDateTime(request.fecha_admision),
                        request.id_organo_radicacion_revision,
                        request.toca_revision,
                        request.revision_adhesiva,
                        request.oficio_revision_adhesiva,
                        //string.IsNullOrEmpty(request.fecha_vencimiento_adhesion) ? null! : Convert.ToDateTime(request.fecha_vencimiento_adhesion),
                        string.IsNullOrEmpty(request.fecha_notificacion_ejecutoria_revision) ? null! : Convert.ToDateTime(request.fecha_notificacion_ejecutoria_revision),
                        request.id_sentido_resolucion_revision,
                        request.id_tipo_sentido_revision,
                        request.id_sentido_general,
                        request.id_tipo_sentido_general_revision,
                        request.oficio_comunicacion_autoridad_revision,
                        string.IsNullOrEmpty(request.fecha_oficio_comunicacion_revision) ? null! : Convert.ToDateTime(request.fecha_oficio_comunicacion_revision),
                        sessionInformation.UserInformation.Rfc
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.CreateRecursoRevisionConstitucional(entity);

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

        #region  CREATE RECURSO REVISION PRINCIPAL CONSTITUCIONAL VALIDACION

        /// <summary>
        /// COMENTADO POR SI SE CAMBIA A QUE SE TENGA QUE VALIDAR EL CAMBIO DE UN ESTADO PROCESAL
        /// Metodo para generar un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("recurso-revision-constitucional-validacion")]
        public async Task<IActionResult> UpdateRecursoRevisionValidacionConstitucional(int id, bool recursoRevisionPrincipal)
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
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe o fue eliminado."
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
                var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdRealDisconnected(id);
                if (entityAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
                    );
                }

                var entityCuadernoAutoridadesExists = await _amparoindirectoAbogadoService.GetRecursoRevisionPrincipalIdDisconnected(id);
                if (entityCuadernoAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no ha llenado el cuaderno constitucional responsable registrada.")
                    );
                }
                bool entity = false!;
                try
                {
                    entity = AbogadoEvents.UpdateRecursoRevisionConstitucional(
                        ref entityAutoridadesExists,
                        ref entityCuadernoAutoridadesExists,
                        entityExists.recurso_revision_principal,
                        recursoRevisionPrincipal
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.UpdateRecursoRevisionConstitucional(id, recursoRevisionPrincipal);

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

        #region GET ALL RECURSO DE INCONFORMIDAD (CONSTITUCIONAL)

        [ProducesResponseType(typeof(ResultOperation<ResponseRecursoIncondormidad>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("recurso-inconformidad-constitucional")]
        public async Task<IActionResult> GetAllRecursoInconformidad([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllRecursoInconformidad(id);

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

        #region CREATE RECURSO DE INCONFORMIDAD (Constitucional)

        /// <summary>
        /// Metodo para crear Recurso de Inconformidad (Cuaderno Constitucional)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("recurso-inconformidad-constitucional")]
        public async Task<IActionResult> PostRecursoInconformidadConstitucional([FromForm]
            RequestCreateRecursoInconformidadConstitucional request
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
                var validationResult = await _validatorCreateRecursoInconformidadConstitucional.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id);
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

                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.idTipoArchivo.GetValueOrDefault() > 0 ||
                    request.idSeccion.GetValueOrDefault() > 0)
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
                RecursoInconformidadConstitucional entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateRecursoInconformidadConstitucional(
                        request.id,
                        string.IsNullOrEmpty(request.notificacionAdmisionRecursInconformidad) ? null! : Convert.ToDateTime(request.notificacionAdmisionRecursInconformidad),
                        request.numeroRecursoInconformidad,
                        request.idOrganoRadicacioInconformidad,
                        string.IsNullOrEmpty(request.fechaNotificacionResolucionInconformidad) ? null! : Convert.ToDateTime(request.fechaNotificacionResolucionInconformidad),
                        request.idSentidoResolucionInconformidad,
                        sessionInformation.UserInformation.Rfc
                    );
                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.id,
                            request.idTipoArchivo.GetValueOrDefault(),
                            request.idSeccion.GetValueOrDefault(),
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoAbogadoService.CreateRecursoInconformidadConstitucional(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoAbogadoService.CreateRecursoInconformidadConstitucional(entity, null!, null!);
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

        #region GET ALL RECURSO RECLAMACION (CONSTITUCIONAL)

        [ProducesResponseType(typeof(ResultOperation<ResponseRecursoReclamacion>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("recurso-reclamacion-constitucional")]
        public async Task<IActionResult> GetAllRecursoReclamacion([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllRecursoReclamacion(id);

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

        #region CREATE RECURSO DE RECLAMACION (Constitucional)

        /// <summary>
        /// Metodo para crear Recurso de Reclamación (Cuaderno Constitucional)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("recurso-reclamacion-constitucional")]
        public async Task<IActionResult> PostRecursoReclamacionConstitucional([FromForm]
            RequestCreateRecursoReclamacionConstitucional request
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
                var validationResult = await _validatorCreateRecursoReclamacionConstitucional.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de Amparo no existe."
                        )
                    );
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
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
                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.idTipoArchivo.GetValueOrDefault() > 0 ||
                    request.idSeccion.GetValueOrDefault() > 0)
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

                RecursoReclamacionConstitucional entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateRecursoReclamacionConstitucional(
                        request.id,
                        request.recursoReclamacion,
                         Convert.ToDateTime(request.fechaNotificacionAcuerdo),
                        //string.IsNullOrEmpty(request.fecha_notificacion_acuerdo) ? null! : Convert.ToDateTime(request.fecha_notificacion_acuerdo),
                        //string.IsNullOrEmpty(request.fecha_vencimiento_recurso_reclamacion) ? null! : Convert.ToDateTime(request.fecha_vencimiento_recurso_reclamacion), 
                        string.IsNullOrEmpty(request.fechaPresentacion) ? null! : Convert.ToDateTime(request.fechaPresentacion),
                        request.oficioReclamacion,
                        string.IsNullOrEmpty(request.notificacionAdmisionRecursoReclamacion) ? null! : Convert.ToDateTime(request.notificacionAdmisionRecursoReclamacion),
                        request.numeroRecurso,
                        request.idOrganoRadicacion,
                        string.IsNullOrEmpty(request.fechaNotificacionResolucion) ? null! : Convert.ToDateTime(request.fechaNotificacionResolucion),
                        request.idSentidoResolucionReclamacion,
                        sessionInformation.UserInformation.Rfc
                    );
                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.id,
                            request.idTipoArchivo.GetValueOrDefault(),
                            request.idSeccion.GetValueOrDefault(),
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoAbogadoService.CreateRecursoReclamacionConstitucional(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoAbogadoService.CreateRecursoReclamacionConstitucional(entity, null!, null!);
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

        //INICIA CUADERNO INCIDENTAL

        #region CREATE SUSPENSION PROVISIONAL  ----------------------

        /// <summary>
        /// Metodo para crear Suspensión provisional Constitucional
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("suspension-provisional-incidental")]
        public async Task<IActionResult> PostSuspensionProvisional(
            RequestCreateSuspensionProvisionalAbogado request
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
                var validationResult = await _validatorSuspensionProvisionalAbogado.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id);
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

                SuspensionProvisional entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateSuspensionProvisional(
                        request.id,
                        request.suspension_provisional,
                        request.otorgamiento_garantia,
                        request.oficio_comunicacion,
                        string.IsNullOrEmpty(request.fecha_comunicacion) ? null! : Convert.ToDateTime(request.fecha_comunicacion),
                        sessionInformation.UserInformation.Rfc
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.CreateSuspensionProvisionalAbogado(entity);

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

        #region GET SUSPENSION PROVISIONAL

        [ProducesResponseType(typeof(ResultOperation<ResponseSuspensionProvisionalIncidental>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("suspension-provisional-incidental")]
        public async Task<IActionResult> GetAllSuspensionProvisionalIncidental([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllSuspensionProvisionalIncidental(id);


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

        #region GET ALL INFORME PREVIO INCIDENTAL

        [ProducesResponseType(typeof(ResultOperation<ResponseInformePrevioIncidental>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("informe-previo-incidental")]
        public async Task<IActionResult> GetAllInformePrevioIncidental([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllInformePrevioIncidental(id);

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

        #region  CREATE INFORME PREVIO INCIDENTAL

        /// <summary>
        /// Metodo para Informe Previo Incidental
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("informe-previo-incidental")]
        public async Task<IActionResult> CreateInformePrevioIncidental([FromForm]
            RequestCreateInformePrevioIncidental request
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
                var validationResult = await _validatorCreateInformePrevioIncidental.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio no existe."
                        )
                    );
                }
                EnumFileType[] requiredExtentions = { EnumFileType.JPG, EnumFileType.JPEG, EnumFileType.PDF };
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
                DataFile dataFile = null!;
                if (request.documento != null ||
                    request.idTipoArchivo.GetValueOrDefault() > 0 ||
                    request.idSeccion.GetValueOrDefault() > 0)
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
                InformePrevioIncidental entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateInformePrevioIncidental(

                        request.id,
                        request.idAutoridadResponsable,
                        Convert.ToDateTime(request.fechaAperturaIncidente),
                        //string.IsNullOrEmpty(request.fecha_vencimiento) ? null! : Convert.ToDateTime(request.fecha_vencimiento),
                        request.numeroOficioInformePrevio,
                        string.IsNullOrEmpty(request.fechaPresentacionInformPrevio) ? null! : Convert.ToDateTime(request.fechaPresentacionInformPrevio),
                        sessionInformation.UserInformation.Rfc
                    );
                    if (dataFile is not null)
                    {
                        var entityDocumentoUpdated = ArchivosAmparoIndirectoEvents.CreateDocumentoAmparoIndirecto(
                            request.id,
                            request.idTipoArchivo.GetValueOrDefault(),
                            request.idSeccion.GetValueOrDefault(),
                            sessionInformation.UserInformation.IdAdministracionCentral.GetValueOrDefault(),
                            dataFile.File.FileName,
                            dataFile.FilePath,
                            dataFile.File!.ContentType,
                            _fileSystemService.ConvertBytesToMegaBytesString(dataFile.File.Length),
                            EnumRolesSicoj.ABOGADO.GetHashCode(),
                            sessionInformation.UserInformation.Rfc!
                        );
                        var resultWithDocument = await _amparoindirectoAbogadoService.CreateInformePrevioIncidental(entity, entityDocumentoUpdated, dataFile);
                        return Ok(resultWithDocument);
                    }
                    else
                    {
                        var resultWithoutDocument = await _amparoindirectoAbogadoService.CreateInformePrevioIncidental(entity, null!, null!);
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

        #region  CREATE INFORME PREVIO INCIDENTAL VALIDACION

        /// <summary>
        /// Metodo para generar un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("informe-previo-incidental-validacion")]
        public async Task<IActionResult> UpdateInformePrevioIncidentalValidacion(int id)
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

                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe o fue eliminado."
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
                var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(id);
                if (entityAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
                    );
                }

                var entityInformePrevio = await _amparoindirectoAbogadoService.GetAllInformePrevioIncidentalDisconnected(id);

                if (entityInformePrevio is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "No se ha capturado el informe previo del cuaderno incidental.")
                    );
                }
                bool entity = false!;
                try
                {
                    entity = AbogadoEvents.UpdateInformePrevio(
                        ref entityAutoridadesExists,
                        ref entityInformePrevio
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.UpdateInformePrevioIncidental(id, sessionInformation.UserInformation.Rfc);

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

        #region GET ALL SENTENCIA INCIDENTAL

        [ProducesResponseType(typeof(ResultOperation<ResponseSentenciaIncidental>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("sentencia-incidental")]
        public async Task<IActionResult> GetAllSentenciaIncidental([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllSentenciaIncidental(id);


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

        #region  CREATE SENTENCIA INCIDENTAL

        /// <summary>
        /// Metodo para generar la sentencia Incidental
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("sentencia-incidental")]
        public async Task<IActionResult> CreateSentenciaIncidental(
            RequestCreateSentenciaIncidental request
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
                var validationResult = await _validatorSentenciaIncidental.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id_numero_asunto);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio no existe."
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
                SentenciaIncidental entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateSentenciaIncidental(
                        request.id_numero_asunto,
                        request.id_autoridad_responsable,
                        string.IsNullOrEmpty(request.fecha_notificacion_sentencia) ? null! : Convert.ToDateTime(request.fecha_notificacion_sentencia),
                        request.id_sentido_suspencion_definitiva,
                        request.id_otorgamiento_garatia,
                        request.id_sentido_general_asunto,
                        request.id_tipo_sentido_general_asunto,
                        request.oficio_comunicacion_area_correspondiente,
                        string.IsNullOrEmpty(request.fecha_comunicacion_suspencion) ? null! : Convert.ToDateTime(request.fecha_comunicacion_suspencion),
                        request.id_dictamen_no_revision_incidental,
                        sessionInformation.UserInformation.Rfc,
                        entityExists.id_estado_procesal
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.CreateSentenciaIncidental(entity);

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

        #region  CREATE SENTENCIA INCIDENTAL VALIDACION

        /// <summary>
        /// Metodo para generar un registro de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("sentencia-incidental-validacion")]
        public async Task<IActionResult> UpdateSentenciaIncidentalValidacion(int id)
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

                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe o fue eliminado."
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
                #region FUNCIONA PARA VALIDAR QUE TODA LA TABLA DE AUTORIDADES ESTE LLENA
                //var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(id);
                //if (entityAutoridadesExists is null)
                //{
                //    return Ok(
                //        ResultOperation.FailureErrorResponse<bool>(
                //            "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
                //    );
                //}
                //var entitySentencia = await _amparoindirectoAbogadoService.GetAllSentenciaIncidentalDisconnected(id);

                //if (entitySentencia is null)
                //{
                //    return Ok(
                //        ResultOperation.FailureErrorResponse<bool>(
                //            "No se ha capturado la sentencia del cuaderno incidental.")
                //    );
                //}
                //bool entity = false!;
                //try
                //{
                //    entity = AbogadoEvents.UpdateSentenciaIncidental(
                //        ref entityAutoridadesExists,
                //        ref entitySentencia
                //    );
                //}
                //catch (Exception _ex)
                //{
                //    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                //}
                #endregion
                var result = await _amparoindirectoAbogadoService.UpdateSentenciaIncidental(id, sessionInformation.UserInformation.Rfc);

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

        #region GET ALL RECURSO QUEJA INCIDENTAL

        [ProducesResponseType(typeof(ResultOperation<ResponseRecursoQuejaIncidental>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("recurso-queja-incidental")]
        public async Task<IActionResult> GetAllRecursoQuejaIncidental([FromQuery] int id/*, int recurrente*/)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllRecursoQuejaIncidental(id/*, recurrente*/);


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

        #region  CREATE RECURSO QUEJA INCIDENTAL

        /// <summary>
        /// Metodo para generar el recurso de queja Incidental
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("recuso-queja-incidental")]
        public async Task<IActionResult> CreateSentenciaConstitucional(
            RequestRecursoQuejaIncidental request
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
                var validationResult = await _validatorRecursoQuejaIncidental.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id_numero_asunto);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio no existe."
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
                RecursoQuejaIncidental entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateRecursoQuejaIncidental(

                        request.id,
                        request.id_numero_asunto,
                        request.id_autoridad_responsable,
                        request.recurso_queja,
                        request.recurrente,
                        Convert.ToDateTime(request.fecha_recepcion_apertura),
                        //string.IsNullOrEmpty(request.fecha_vencimiento_recurso_queja) ? null! : Convert.ToDateTime(request.fecha_vencimiento_recurso_queja),
                        request.oficio_queja,
                        string.IsNullOrEmpty(request.fecha_presentacion_recurso_queja) ? null! : Convert.ToDateTime(request.fecha_presentacion_recurso_queja),
                        string.IsNullOrEmpty(request.fecha_admision) ? null! : Convert.ToDateTime(request.fecha_admision),
                        request.organo_radicacion,
                        request.toca,
                        string.IsNullOrEmpty(request.fecha_notificacion_ejecutoria) ? null! : Convert.ToDateTime(request.fecha_notificacion_ejecutoria),
                        request.sentido_resolucion,
                        request.oficio_comunicacion_area_correspondiente,
                        string.IsNullOrEmpty(request.fecha_oficio_comunicacion) ? null! : Convert.ToDateTime(request.fecha_oficio_comunicacion),
                        sessionInformation.UserInformation.Rfc
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.CreateRecursoQuejaIncidental(entity);

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

        #region  CREATE RECURSO QUEJA INCIDENTAL VALIDACION

        /// <summary>
        /// Metodo validar el recurso queja incidental y cambiarlo de estado procesal
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("recurso-queja-incidental-validacion")]
        public async Task<IActionResult> UpdateRecursoQuejaIncidentalValidacion(int id, bool recursoQuejaIncidental)
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

                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe o fue eliminado."
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
                var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(id);
                if (entityAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
                    );
                }
                var entityRecursoQueja = await _amparoindirectoAbogadoService.GetAllRecursoQuejaIncidentalDisconnected(id);

                if (entityRecursoQueja is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "No se ha capturado el recurso queja del cuaderno incidental.")
                    );
                }
                bool entity = false!;
                try
                {
                    entity = AbogadoEvents.UpdateRecursoQuejaIncidental(
                        ref entityAutoridadesExists,
                        ref entityRecursoQueja,
                        entityExists.recurso_queja_incidental,
                        recursoQuejaIncidental
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.UpdateRecursoQuejaIncidental(id, sessionInformation.UserInformation.Rfc);

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

        #region GET ALL RECURSO REVISION INCIDENTAL

        [ProducesResponseType(typeof(ResultOperation<ResponseRecursoQuejaIncidental>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("recurso-revision-incidental")]
        public async Task<IActionResult> GetAllRecursoRevisionIncidental([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllRecursoRevisionIncidental(id);


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

        #region CREATE RECURSO RECURSO REVISION (Incidental)

        /// <summary>
        /// Metodo para crear recurso de queja principal libro Constitucional
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("recurso-revision-incidental")]

        public async Task<IActionResult> PostRecursoRevisionIncidental(
            RequestCreateRecursoRevisionIncidental request
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
                var validationResult = await _validatorCreateRecursoRevisionIncidental.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                //valida ek juicio de amparo indirecto
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id_numero_asunto);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de Amparo Indirecto no existe."
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

                RecursoRevisionIncidental entity = null!;
                try
                {
                    entity = AbogadoEvents.CreateRecursoRevisionIncidental(

                        request.id,
                        request.id_numero_asunto,
                        request.id_autoridad_responsable,
                        request.recurso_revision_incidental,
                        request.id_recurrente,
                        Convert.ToDateTime(request.fecha_recepcion_sentencia),
                        //string.IsNullOrEmpty(request.fecha_recepcion_sentencia) ? null! : Convert.ToDateTime(request.fecha_recepcion_sentencia),
                        //string.IsNullOrEmpty(request.fecha_vencimiento_recurso_revision) ? null! : Convert.ToDateTime(request.fecha_vencimiento_recurso_revision),
                        request.oficio_recurso,
                        string.IsNullOrEmpty(request.fecha_presentacion_recurso_revision) ? null! : Convert.ToDateTime(request.fecha_presentacion_recurso_revision),
                        //string.IsNullOrEmpty(request.fecha_admision) ? null! : Convert.ToDateTime(request.fecha_admision),
                        Convert.ToDateTime(request.fecha_admision),
                        request.id_organo_radicacion,
                        request.toca_recurso_revision_incidental,
                        request.revision_adhesiva,
                        request.oficio_revision_adhesiva,
                        //string.IsNullOrEmpty(request.fecha_vencimiento_adhesion) ? null! : Convert.ToDateTime(request.fecha_vencimiento_adhesion),
                        string.IsNullOrEmpty(request.fecha_presentacion_adhesion) ? null! : Convert.ToDateTime(request.fecha_presentacion_adhesion),
                        string.IsNullOrEmpty(request.fecha_notificacion_resolucion) ? null! : Convert.ToDateTime(request.fecha_notificacion_resolucion),
                        request.id_sentido_resolucion,
                        request.id_tipo_sentido,
                        request.id_sentido_general_asunto,
                        request.id_tipo_sentido_general,
                        request.oficio_comunicacion_autoridad,
                        string.IsNullOrEmpty(request.fecha_oficio_comunicacion) ? null! : Convert.ToDateTime(request.fecha_oficio_comunicacion),
                        sessionInformation.UserInformation.Rfc
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }

                var result = await _amparoindirectoAbogadoService.CreateRecursoRevisionIncidental(entity);

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

        #region  CREATE RECURSO REVISION INCIDENTAL VALIDACION

        /// <summary>
        /// Metodo validar el recurso queja incidental y cambiarlo de estado procesal
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("recurso-revision-incidental-validacion")]
        public async Task<IActionResult> UpdateRecursoRevisionIncidentalValidacion(int id, bool recursoRevisionIncidental)
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

                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe o fue eliminado."
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
                var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(id);
                if (entityAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
                    );
                }
                var entityRecursoRevision = await _amparoindirectoAbogadoService.GetAllRecursoRevisionIncidentalDisconnected(id);

                if (entityRecursoRevision is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "No se ha capturado el recurso queja del cuaderno incidental.")
                    );
                }
                bool entity = false!;
                try
                {
                    entity = AbogadoEvents.UpdateRecursoRevisionIncidental(
                        ref entityAutoridadesExists,
                        ref entityRecursoRevision,
                        entityExists.recurso_revision_incidental,
                        recursoRevisionIncidental
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.UpdateRecursoRevisionIncidental(id, recursoRevisionIncidental /*sessionInformation.UserInformation.Rfc*/);

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


        #region CREATE INCIDENTE POR EXCESO

        /// <summary>
        /// Metodo para crear Incidente por Exceso Incidental
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPost("incidente-exceso-incidental")]
        public async Task<IActionResult> PostIncidenteExcesoIncidental(
            RequestCreateIncidenteExcesoIncidental request
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
                var validationResult = await _validatorCreateIncidenteExcesoIncidental.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    return Ok(ResultOperation.FailureWarningResponse(validationResult.ToString(" - ")));
                }
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(request.id_numero_asunto);
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

                IncidenteExcesoIncidental entity = null!;
                try
                {

                    entity = AbogadoEvents.CreateIncidenteExceso(
                        request.id,
                        request.id_numero_asunto,
                        request.id_autoridad_responsable,
                        request.interposicion_incidente,
                        string.IsNullOrEmpty(request.fecha_notificacion_acuerdo) ? null! : Convert.ToDateTime(request.fecha_notificacion_acuerdo),
                        request.oficio_desahogo,
                        string.IsNullOrEmpty(request.fecha_oficio_desahogo) ? null! : Convert.ToDateTime(request.fecha_oficio_desahogo),
                        request.id_sentido,
                        string.IsNullOrEmpty(request.fecha_notificacion_resolucion) ? null! : Convert.ToDateTime(request.fecha_notificacion_resolucion),
                        request.oficio_comunicacion_autoridad,
                        string.IsNullOrEmpty(request.fecha_oficio_comunicacion) ? null! : Convert.ToDateTime(request.fecha_oficio_comunicacion),
                        sessionInformation.UserInformation.Rfc
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }

                var result = await _amparoindirectoAbogadoService.CreateIncidenteExcesoIncidental(entity);

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
        #region  INCIDENTE POR EXCESO INCIDENTAL VALIDACION

        /// <summary>
        /// Metodo para generar un registro de incidente por exceso de amparo indirecto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("incidente-exceso-incidental-validacion")]
        public async Task<IActionResult> UpdateIncidenteExceso(int id)
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

                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
                if (entityExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no existe o fue eliminado."
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

                var entityAutoridadesExists = await _amparoindirectoAbogadoService.GetAutoridadesResponsablesIdDisconnected(id);
                if (entityAutoridadesExists is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no tiene una autoridad responsable registrada.")
                    );
                }

                var entityIncidenteExcesoIncidental = await _amparoindirectoAbogadoService.GetAllIncidentePorExcesoIncidentalDisconnected(id);
                if (entityIncidenteExcesoIncidental is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse<bool>(
                            "El juicio de amparo indirecto no ha llenado el cuaderno constitucional responsable registrada.")
                    );
                }

                bool entity = false!;
                try
                {
                    entity = AbogadoEvents.UpdateIncidenteExcesoIncidental(
                        ref entityIncidenteExcesoIncidental,
                        ref entityAutoridadesExists
                    );
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                var result = await _amparoindirectoAbogadoService.UpdateIncidenteExcesoIncidental(id, sessionInformation.UserInformation.Rfc);

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
        #region GET INCIDENTE POR EXCESO

        [ProducesResponseType(typeof(ResultOperation<ResponseIncidenteExcesoIncidental>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("incidente-exceso-incidental")]
        public async Task<IActionResult> GetAllIncidenteExcesoIncidental([FromQuery] int id)
        {
            try
            {
                var result = await _amparoindirectoAbogadoService.GetAllIncidenteExcesoIncidental(id);


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
                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
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

                var entityExists = await _amparoindirectoAbogadoService.GetAmparoIndirectoByIdDisconnected(id);
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
