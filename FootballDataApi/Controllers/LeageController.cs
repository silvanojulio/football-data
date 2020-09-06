using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballDataCommon.Contracts.Managers;
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
        public void Get(string leageCode)
        {
            dataImportManager.ImportLeage(leageCode);
        }
    }
}
