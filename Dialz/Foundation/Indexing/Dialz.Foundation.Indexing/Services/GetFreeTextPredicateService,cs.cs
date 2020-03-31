using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dialz.Foundation.Indexing.Services
{
    public static class GetFreeTextPredicateService
    {
        public static Expression<Func<SearchResultItem, bool>> GetFreeTextPredicate(string[] fieldNames, IQuery query)
        {
            var predicate = PredicateBuilder.False<SearchResultItem>();
            if (string.IsNullOrWhiteSpace(query.QueryText))
            {
                return predicate;
            }
            return fieldNames.Aggregate(predicate,
              (current, fieldName) => current.Or(
                i => i[string.Format("{0}_t", fieldName.ToLower().Replace(" ", "_"))].Contains(query.QueryText)
              )
            );
        }
    }
}
