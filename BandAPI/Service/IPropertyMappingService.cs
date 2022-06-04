using System.Collections.Generic;

namespace BandAPI.Service
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMapingValue> GetPropertyMapping<Tsource, TDestination>();
        bool ValidMappingExists<Tsource, TDestination>(string fields);
    }
}