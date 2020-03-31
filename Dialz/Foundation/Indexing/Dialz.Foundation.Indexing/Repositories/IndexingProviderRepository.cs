using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dialz.Foundation.Indexing.Repositories
{
    internal static class IndexingProviderRepository
    {
        private static IEnumerable<ProviderBase> _all;
        private static ISearchResultFormatter _defaultSearchResultFormatter;

        private static void Initialize()
        {
            ProviderBase defaultProvider;
            var providers = Factory.GetProviders<ProviderBase, ProviderCollectionBase<ProviderBase>>("solutionFramework/indexing", out defaultProvider);
            var defaultSearchResultFormatter = defaultProvider as ISearchResultFormatter;
            if (defaultSearchResultFormatter == null)
                throw new ConfigurationErrorsException("The default solutionFramework/indexing provider must derive from ISearchResultFormatter");

            _all = providers;
            _defaultSearchResultFormatter = defaultSearchResultFormatter;
        }

        public static IEnumerable<ProviderBase> All
        {
            get
            {
                if (_all == null)
                {
                    Initialize();
                }
                return _all;
            }
            private set
            {
                _all = value;
            }
        }

        public static IEnumerable<IQueryPredicateProvider> QueryPredicateProviders
        {
            get
            {
                return All.Where(p => p is IQueryPredicateProvider).Cast<IQueryPredicateProvider>();
            }
        }

        public static ISearchResultFormatter DefaultSearchResultFormatter
        {
            get
            {
                if (_defaultSearchResultFormatter == null)
                {
                    Initialize();
                }
                return _defaultSearchResultFormatter;
            }
            private set
            {
                _defaultSearchResultFormatter = value;
            }
        }

        public static IEnumerable<ISearchResultFormatter> SearchResultFormatters
        {
            get
            {
                return All.Where(p => p is ISearchResultFormatter).Cast<ISearchResultFormatter>();
            }
        }
    }
}
