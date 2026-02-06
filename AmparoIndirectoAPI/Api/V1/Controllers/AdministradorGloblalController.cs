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
    [Route("sicoj/amparo-indirecto/api/v1/administrador-global/amparo-indirecto")]
    public class AdministradorGlobalController : ControllerBase
    {
        #region Variables / Constructor
        private readonly ILogger<AdministradorGlobalController> _logger;


        #region AdministradorGlobal       
        private readonly IAmparoIndirectoAdministradorGlobalService _amparoindirectoAdministradorGlobalService;
        //private readonly IValidator<RequestCreateInformePrevioIncidental> _validatorCreateInformePrevioIncidental;
       
        #endregion

        private readonly IRedisClient _redisClient;
        private static object _lock = new object();
        //private readonly IFileSystemService _fileSystemService;

        public AdministradorGlobalController(
            ILogger<AdministradorGlobalController> logger,
            IRedisClient redisClient,
            IAmparoIndirectoAdministradorGlobalService amparoindirectoAdministradorGlobalService
            //IValidator<RequestCreateInformePrevioIncidental> validatorCreateInformePrevioIncidental,
            //IValidator<RequestCreateSuspensionProvisionalAbogado> validatorSuspensionProvisionalAbogado,
            

            )
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
            _amparoindirectoAdministradorGlobalService = amparoindirectoAdministradorGlobalService ?? throw new ArgumentNullException(nameof(amparoindirectoAdministradorGlobalService));
            
        }
        #endregion




        #region PENDIENTES
        /// <summary>
        ///  GetBandeja Obtiene todos los registros
        ///  para la Bandeja de Pendientes para el Administrador Global  
        ///  con estatus id_estado_tarea = "En Reparación(5)"   
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<ResponseAdministradorGlobalByFilters>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("pendientes")]
        public async Task<IActionResult> GetBandejaAdministradorGlobal([FromQuery] PagerQuery request
        )
        {
            try
            {
                if (!Filters.MapSort<EnumOrderColumnAdministradorGlobalByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseAdministradorGlobalByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }

                var result = await _amparoindirectoAdministradorGlobalService.GetAllBandejaPendientesAdministradorGlobal(
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
        //////[ProducesResponseType(typeof(ResultOperation<ResponseAbogadoByFilters>), 200)]
        //////[ProducesResponseType(typeof(ResultOperation), 400)]
        //////[Authorize(EnumRoles.RR_OP)]
        //////[HttpGet("historico")]
        //////public async Task<IActionResult> GetHistoricoAbogado(
        //////    [FromQuery] PagerQueryFilters request
        //////)
        //////{
        //////    try
        //////    {
        //////        RequestAbogadoFilters filters = new();
        //////        Filters.MapFilters(request, filters);

        //////        if (!Filters.MapSort<EnumOrderColumnAbogadoByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
        //////        {
        //////            //ResponseAbogadoByFilters
        //////            return Ok(ResultOperation.FailureWarningResponse<List<ResponseHistoricoAbogadoByFilters>>("La columna de ordenamiento no es válida.")
        //////                );
        //////        }

        //////        //string orderByColumn = null!;
        //////        //bool orderDesc = false;
        //////        if (request.sort is not null && request.sort.Any())
        //////        {
        //////            orderByColumn = request.sort.FirstOrDefault().Key;
        //////            if (!Enum.TryParse<EnumOrderColumnAbogadoByFiltros>(orderByColumn, out _))
        //////            {
        //////                return Ok(
        //////                ResultOperation<
        //////                    List<ResponseHistoricoAbogadoByFilters>>.FailureWarningResponse("La columna de ordenamiento no es válida.")
        //////                );
        //////            }
        //////            orderDesc = request.sort.FirstOrDefault().Value;
        //////        }

        //////        //var result = await _amparoindirectoOficialPartesService.GetAmparoIndirectoDisconnectedByFilters(
        //////        var result = await _amparoindirectoAbogadoService.GetHistoricoAbogadoByFilters(
        //////            request.fetch,
        //////            request.page,
        //////            orderByColumn,
        //////            orderDesc,
        //////            Filters.GetDateTimeValue(filters!.ByFechaRecepcionInicial.FirstOrDefault()),
        //////            Filters.GetDateTimeValue(filters!.ByFechaRecepcionFinal.FirstOrDefault()),
        //////            Filters.GetDateTimeValue(filters!.ByFechaInicialVencimiento.FirstOrDefault()),
        //////            Filters.GetDateTimeValue(filters!.ByFechaFinalVencimiento.FirstOrDefault()),
        //////            Filters.GetStringValue(filters!.ByNumeroExpediente.FirstOrDefault())!,
        //////            Filters.GetStringValue(filters!.ByNumeroAsunto.FirstOrDefault())!,
        //////            Filters.GetIntValue(filters!.ByIdJuzgado.FirstOrDefault()),
        //////            Filters.GetStringValue(filters!.ByNombreQuejoso.FirstOrDefault())!,
        //////            Filters.GetIntValue(filters!.ByIdMateria.FirstOrDefault()),
        //////            Filters.GetIntValue(filters!.ByIdSubmateria.FirstOrDefault()),
        //////            Filters.GetIntValue(filters!.ByIdTipoActo.FirstOrDefault()),
        //////            Filters.GetStringValue(filters!.ByDespacho.FirstOrDefault())!,
        //////            Filters.GetIntValue(filters!.ByIdAdministracion.FirstOrDefault()),
        //////            Filters.GetIntValue(filters!.ByIdSubAdministracion.FirstOrDefault()),
        //////            Filters.GetIntValue(filters!.ByIdAutoridadResponsable.FirstOrDefault()),
        //////            Filters.GetStringValue(filters!.ByRrfcQuejoso.FirstOrDefault())!,
        //////            Filters.GetIntValue(filters!.ByIdEstadoTarea.FirstOrDefault()),
        //////            Filters.GetIntValue(filters!.ByIdEstadoProcesal.FirstOrDefault()),
        //////            Filters.GetIntValue(filters!.ByIdEstadoProcesalIncidental.FirstOrDefault())

        //////            );

        //////        return Ok(result);
        //////    }
        //////    catch (Exception _e)
        //////    {
        //////        _logger.LogError(_e, "Ha ocurrrido un error.");
        //////        return BadRequest(
        //////            ResultOperation.FailureErrorResponse(
        //////                _e.ManageException()
        //////            )
        //////        );
        //////    }
        //////}
        #endregion


        #region GETBYID PARA FRONT

        //[ProducesResponseType(typeof(ResultOperation<ResponseAmparoIndirectoByID>), 200)]
        //[ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.RR_OP)]
        //[HttpGet("id")]
        //public async Task<IActionResult> GetByIdAmparoIndirecto([FromQuery] int id)
        //{
        //    try
        //    {
        //        var result = await _amparoindirectoAbogadoService.GetByIdAmparo(id);

        //        if (result is null)
        //        {
        //            return Ok(
        //                ResultOperation.FailureWarningResponse("No se encontraron resultados")
        //            );
        //        }
        //        return Ok(result);

        //    }
        //    catch (Exception _e)
        //    {
        //        _logger.LogError(_e, "Ha ocurrrido un error.");
        //        return BadRequest(
        //            ResultOperation.FailureErrorResponse(
        //                _e.ManageException()
        //            )
        //        );
        //    }
        //}

        #endregion


        // TOMAR Y SOLTAR

        //#region TOMAR

        //[ProducesResponseType(typeof(ResultOperation<bool>), 200)]
        //[ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.RR_OP)]
        //[HttpPatch("tomar")]
        //public async Task<IActionResult> PatchTomar([FromQuery] int id)
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
        //        if (entityExists is null || !entityExists.activo)
        //        {
        //            return Ok(
        //                ResultOperation.FailureErrorResponse<bool>(
        //                    "El asunto no existe o se eliminó."
        //                )
        //            );
        //        }
        //        var response = await _redisClient.Take(EnumModulosRedis.AMPARO_INDIRECTO, entityExists.id, sessionInformation.UserInformation.Rfc!, sessionInformation.UserInformation.Nombre!);
        //        return Ok(response);
        //    }
        //    catch (Exception _e)
        //    {
        //        _logger.LogError(_e, "Ha ocurrrido un error.");
        //        return BadRequest(
        //            ResultOperation.FailureErrorResponse(
        //                _e.ManageException()
        //            )
        //        );
        //    }
        //}

        //#endregion

        //#region SOLTAR

        //[ProducesResponseType(typeof(ResultOperation<bool>), 200)]
        //[ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.RR_OP)]
        //[HttpPatch("soltar")]
        //public async Task<IActionResult> PatchSoltar([FromQuery] int id)
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
        //                    "El asunto no existe."
        //                )
        //            );
        //        }

        //        var result = await _redisClient.Drop(EnumModulosRedis.AMPARO_INDIRECTO, id, sessionInformation.UserInformation.Rfc!);
        //        return Ok(result);
        //    }
        //    catch (Exception _e)
        //    {
        //        _logger.LogError(_e, "Ha ocurrrido un error.");
        //        return BadRequest(
        //            ResultOperation.FailureErrorResponse(
        //                _e.ManageException()
        //            )
        //        );
        //    }
        //}

        //#endregion

        #region GET DESCARTAR INFORME JUSTIFICADO

        /// <summary>
        /// Metodo para traer un registro de informe justificado
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<ResponseDescartarInformeJustificadoConstitucional>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        //[Authorize(EnumRoles.JAI_OP)]
        [HttpGet("descartar-informe-justificado-constitucional")]

        public async Task<IActionResult> GetAllInformeJustificadoConstitucionalDescartar([FromQuery]int id)
        {
            try
            {
                var result = await _amparoindirectoAdministradorGlobalService.GetAllInformeJustificadoDescartar(id);

                if(result is null)
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

        #region DESCARTAR INFORME JUSTIFICADO

        /// <summary>
        /// Metodo para descartar un registro de informe justificado
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ResultOperation<int>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpPatch("descartar-informe-justificado-constitucional")]

        public async Task<IActionResult> DescartarInformeJustificado(
            int idAmparo, int idInformeJustificado)
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
                var entityExists = await _amparoindirectoAdministradorGlobalService.GetAmparoIndirectoByIdDisconnected(idAmparo);
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
                        ResultOperation.FailureErrorResponse<int>("El registro no se puede descartar ya que el usuario no lo ha tomado")
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

                var entityInformeJustificado = await _amparoindirectoAdministradorGlobalService.GetByIdInformeJustificadoDescartar(idInformeJustificado);
                if (entityInformeJustificado is null)
                {
                    return Ok(
                        ResultOperation.FailureErrorResponse(
                            "El informe justificado constitucional no existe."
                        )
                    );
                }

                AdministradorGlobalDescartar entity = null!;
                try
                {
                    entity = AdministradorGlobalEvents.CreateDescartar(
                    entityExists,
                    sessionInformation.UserInformation.Rfc!);
                }
                catch (Exception _ex)
                {
                    return Ok(ResultOperation.FailureWarningResponse(_ex.Message));
                }
                

                var result = await _amparoindirectoAdministradorGlobalService.UpdateDescartarInformeJustificado(idInformeJustificado);
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
