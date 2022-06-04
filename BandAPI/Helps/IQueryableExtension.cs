using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BandAPI.Service;
using System.Linq.Dynamic.Core;

namespace BandAPI.Helps
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, 
            string orderBy, Dictionary<string, PropertyMapingValue> mappingDictionary)
            {
                var orderByString = "";
                if(source == null)
                    throw new ArgumentNullException(nameof(source));
                if(mappingDictionary == null )
                    throw new ArgumentNullException(nameof(mappingDictionary));

                if (string.IsNullOrWhiteSpace(orderBy))
                    return source; 

                var orderBySplit = orderBy.Split(",");
                foreach( var orderByClause in orderBySplit.Reverse())
                {
                    var trimmeOrderBy = orderByClause.Trim();
                    var orderByDesc = trimmeOrderBy.EndsWith(" desc");
                    var indexOfSpace = trimmeOrderBy.IndexOf(" ");
                    var propertyName = indexOfSpace == -1 ? trimmeOrderBy :  trimmeOrderBy.Remove(indexOfSpace);

                    if(!mappingDictionary.ContainsKey(propertyName))
                        throw new ArgumentNullException("Mapping doesn't exists for " + propertyName);

                    var propertyMapingValue = mappingDictionary[propertyName];
                    if(propertyMapingValue == null)
                        throw new ArgumentNullException(nameof(propertyMapingValue));
                    foreach(var destination in propertyMapingValue.DestinationProperties.Reverse())
                    {
                        if(propertyMapingValue.Revert)
                            orderByDesc = !orderByDesc;
                        orderByString = orderByString + (!string.IsNullOrWhiteSpace(orderByString) ? "," : "") + destination + 
                        (orderByDesc ? " descending" : " ascending");
                    }
                }
                return source.OrderBy(orderByString);
            }
    }
}