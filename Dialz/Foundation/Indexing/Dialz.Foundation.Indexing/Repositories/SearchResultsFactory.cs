using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dialz.Foundation.Indexing.Repositories
{
    public class SearchResultsFactory
    {
        public static ISearchResults Create(SearchResults<SearchResultItem> results, IQuery query)
        {
            var searchResults = CreateSearchResults(results);
            return new SearchResults
            {
                Results = searchResults,
                TotalNumberOfResults = results.TotalSearchResults,
                Query = query
            };
        }

        private static IEnumerable<ISearchResult> CreateSearchResults(SearchResults<SearchResultItem> results)
        {
            return results.Hits.Select(h => SearchResultFactory.Create(h.Document)).ToArray();
        }
    }
}
