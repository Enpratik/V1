
using System.Data.Entity;

namespace Enpratik.Data
{
    public class Configuration : DbConfiguration
    {
        public Configuration()
        {
            //var transactionHandler = new CacheTransactionHandler(new InMemoryCache());

            //AddInterceptor(transactionHandler);

            //Loaded +=
            //  (sender, args) => args.ReplaceService<DbProviderServices>(
            //    (s, _) => new CachingProviderServices(s, transactionHandler));
        }
    }
}
