using Example.WebApi.Context;
using Example.WebApi.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Services
{
    public interface ISatelliteService
    {
        Task<IEnumerable<Satellite>> GetAll(CancellationToken cancellationToken);

        Task<IEnumerable<SatelliteObsolete>> GetAllObsolete(CancellationToken cancellationToken);
    }

    internal class SatellitesService : ISatelliteService
    {
        private readonly IExampleDbContext _dbContext;
        private readonly IMapper _mapper;

        public SatellitesService(IExampleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Satellite>> GetAll(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Satellite>>(await _dbContext.Satellites.Select(x => x)
                .ToListAsync(cancellationToken));
        }

        public async Task<IEnumerable<SatelliteObsolete>> GetAllObsolete(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<SatelliteObsolete>>(await _dbContext.Satellites.Select(x => x)
                .ToListAsync(cancellationToken));
        }
    }
}