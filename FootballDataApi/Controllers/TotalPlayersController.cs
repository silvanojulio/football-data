using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FootballDataCommon.Contracts.Api;
using FootballDataCommon.Contracts.Exceptions;
using FootballDataCommon.Contracts.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FootballDataApi.Controllers
{
    [ApiController]
    public class TotalPlayersController : ControllerBase
    {
        private readonly ILogger<LeageController> logger;
        private readonly IPlayersManager playersManager;

        public TotalPlayersController(ILogger<LeageController> logger, IPlayersManager playersManager)
        {
            this.logger = logger;
            this.playersManager = playersManager;
        }

        [HttpGet]
        [Route("total-players/{leageCode}")]
        public async Task<IActionResult> Get(string leageCode)
        {
            try
            {
                var playersCount = await playersManager.GetTotalPlayersByLeage(leageCode);

                return StatusCode(200, ResponseBuilder.GetApiSuccessResponseWithData<int>(playersCount));
            }
            catch (ItemNotFoundException ex){
                return StatusCode(404, ResponseBuilder.GetErrorApiResponse(ex));
            }
            catch (System.Exception ex)
            {
                return StatusCode(504, ResponseBuilder.GetErrorApiResponse(ex));
            }
        }
    }
}
