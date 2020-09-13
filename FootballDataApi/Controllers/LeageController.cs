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
    public class LeageController : ControllerBase
    {
        private readonly ILogger<LeageController> logger;
        private readonly IDataImportManager dataImportManager;

        public LeageController(ILogger<LeageController> logger, IDataImportManager dataImportManager)
        {
            this.logger = logger;
            this.dataImportManager = dataImportManager;
        }

        [HttpGet]
        [Route("import-leage/{leageCode}")]
        public async Task<IActionResult> Get(string leageCode)
        {
            try
            {
                await dataImportManager.ImportLeage(leageCode);

                return StatusCode(201, ResponseBuilder.GetApiSuccessResponse());
            }
            catch (AlreadyImportedLeageException ex){
                return StatusCode(409, ResponseBuilder.GetErrorApiResponse(ex));
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
