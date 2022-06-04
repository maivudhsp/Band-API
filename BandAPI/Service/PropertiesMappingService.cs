using System;
using System.Collections.Generic;
using System.Linq;
using BandAPI.Entities;
using BandAPI.Models;

namespace BandAPI.Service
{
    public class PropertiesMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMapingValue> _bandPropertyMapping =
            new Dictionary<string, PropertyMapingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Id", new PropertyMapingValue(new  List<string>() {"Id"})},
                {"Name", new PropertyMapingValue(new  List<string>() {"Name"})},
                {"MainGenre", new PropertyMapingValue(new  List<string>() {"MainGenre"})},
                {"FoundedYearsAgo", new PropertyMapingValue(new  List<string>() {"Founded"}, true)},
            };

        private IList<IPropertyMappingMarker> _propertyMappings = new List<IPropertyMappingMarker>();
        public PropertiesMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<BandDto, Band>(_bandPropertyMapping));
        }

        public Dictionary<string, PropertyMapingValue> GetPropertyMapping<Tsource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<Tsource, TDestination>>();
            if(matchingMapping.Count() ==  1)
                return matchingMapping.FirstOrDefault().MappingDictionary;
            throw new Exception("No mapping was found");
        }
        public bool ValidMappingExists<Tsource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<Tsource, TDestination>();
            if(string.IsNullOrWhiteSpace(fields))
                return true;

            var fieldsAfterSplit = fields.Split(",");
            foreach(var field in fieldsAfterSplit)
            {
                var trimmeField = field.Trim();
                var indexOfSpace = trimmeField.IndexOf(" ");
                var propertyName = indexOfSpace ==  -1 ? trimmeField : trimmeField.Remove(indexOfSpace);

                if(!propertyMapping.ContainsKey(propertyName))
                    return false;
            }
            return true;
        }
    }
}