using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Service
{
    public class PropertyMapping<Tsource, TDestination> : IPropertyMappingMarker
    {
        public Dictionary<string, PropertyMapingValue> MappingDictionary { get; set; }
        public PropertyMapping(Dictionary<string, PropertyMapingValue> mappingDicionary)
        {
            MappingDictionary = mappingDicionary ?? throw new ArgumentNullException(nameof(mappingDicionary));
        }
    }
}