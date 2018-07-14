using DocsMaster.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DocsMaster.DataAccess
{
    public class DocsMasterContext
    {
        private readonly IMongoDatabase _db;
        private IOptions<ApplicationOptions> _options;

        public IMongoCollection<DocsManual> Docs => _db.GetCollection<DocsManual>(_options.Value.DocsCollection);

        public DocsMasterContext(IOptions<ApplicationOptions> options)
        {
            _options = options;
            var client = new MongoClient(options.Value.DbConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
    }
}