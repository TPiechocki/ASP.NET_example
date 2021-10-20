using AutoMapper;
using Example.WebApi.Contract;
using SatelliteContract = Example.WebApi.Contract.Satellite;
using SatelliteModel = Example.WebApi.Model.Satellite;

namespace Example.WebApi.Mappings
{
    public class SatelliteMappings : Profile
    {
        public SatelliteMappings()
        {
            CreateMap<SatelliteModel, SatelliteContract>().DisableCtorValidation();
            CreateMap<SatelliteModel, SatelliteObsolete>().DisableCtorValidation();
        }
    }
}