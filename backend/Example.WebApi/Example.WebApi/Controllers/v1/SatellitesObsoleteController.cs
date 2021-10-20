using Example.WebApi.Contract;
using Example.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/satellites")]
    [Authorize]
    public class SatellitesObsoleteController : ControllerBase
    {
        private readonly ISatelliteService _satelliteService;

        public SatellitesObsoleteController(ISatelliteService satelliteService)
        {
            _satelliteService = satelliteService;
        }

        public async Task<IEnumerable<SatelliteObsolete>> Get(CancellationToken cancellationToken)
        {
            return await _satelliteService.GetAllObsolete(cancellationToken);
        }
    }
}