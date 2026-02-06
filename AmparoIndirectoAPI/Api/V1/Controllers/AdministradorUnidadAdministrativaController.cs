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
    [Route("sicoj/amparo-indirecto/api/v1/administrador-unidad-administrativa/amparo-indirecto")]
    public class AdministradorUnidadAdministrativaController : ControllerBase
    {
        #region Variables / Constructor
        private readonly ILogger<AdministradorUnidadAdministrativaController> _logger;


        #region AdministradorUnidadAdministrativa      
        private readonly IAmparoIndirectoAdministradorUnidadAdministrativaService _amparoindirectoAdministradorUnidadAdministrativaService;
       
        #endregion

        private readonly IRedisClient _redisClient;
        private static object _lock = new object();

        public AdministradorUnidadAdministrativaController(
            ILogger<AdministradorUnidadAdministrativaController> logger,
            IRedisClient redisClient,
            IAmparoIndirectoAdministradorUnidadAdministrativaService amparoindirectoAdministradorUnidadAdministrativaService          

            )
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
            _amparoindirectoAdministradorUnidadAdministrativaService = amparoindirectoAdministradorUnidadAdministrativaService ?? throw new ArgumentNullException(nameof(amparoindirectoAdministradorUnidadAdministrativaService));
            
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
        [ProducesResponseType(typeof(ResultOperation<ResponseAdministradorUnidadAdministrativaByFilters>), 200)]
        [ProducesResponseType(typeof(ResultOperation), 400)]
        [Authorize(EnumRoles.RR_OP)]
        [HttpGet("pendientes")]
        public async Task<IActionResult> GetBandejaAdministradorUnidadAdministrativa([FromQuery] PagerQuery request
        )
        {
            try
            {
                if (!Filters.MapSort<EnumOrderColumnAdministradorGlobalByFiltros>(request, null!, false, out string orderByColumn, out bool orderDesc))
                {
                    return Ok(ResultOperation.FailureWarningResponse<List<ResponseAdministradorUnidadAdministrativaByFilters>>("La columna de ordenamiento no es válida.")
                        );
                }

                var result = await _amparoindirectoAdministradorUnidadAdministrativaService.GetAllBandejaPendientesAdministradorUnidadAdministrativa(
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

    }
}
