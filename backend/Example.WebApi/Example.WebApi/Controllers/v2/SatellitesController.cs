using Example.WebApi.Contract;
using Example.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example.WebApi.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class SatellitesController : ControllerBase
    {
        private readonly ISatelliteService _satelliteService;

        public SatellitesController(ISatelliteService satelliteService)
        {
            _satelliteService = satelliteService;
        }


        public async Task<IEnumerable<Satellite>> Get(CancellationToken cancellationToken)
        {
            return await _satelliteService.GetAll(cancellationToken);
        }
    }
}
