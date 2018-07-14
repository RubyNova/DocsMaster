using System;
using System.Linq;
using DocsMaster.DataAccess;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DocsMaster.Services
{
    public class ManualEntryRetrieverService : IManualEntryRetriverService
    {
        private readonly DocsMasterContext _context;

        public ManualEntryRetrieverService(IOptions<ApplicationOptions> options)
        {
            _context = new DocsMasterContext(options);
        }
        
        public DocsManualEntry GetManualEntryForLatestVersion(string manualName, string entryName)
        {
            var filter = Builders<DocsManual>.Filter.Eq(x => x.ManualName, manualName);
            var manual = _context.Docs.Find(filter).SortByDescending(x => x.ManualVersion).First();

            return manual.Entries.FirstOrDefault(x =>
                string.Equals(entryName, x.EntryName, StringComparison.OrdinalIgnoreCase));
        }

        public DocsManualEntry GetManualEntry(string manualName, string entryName, string manualVersion)
        {
            var filter = Builders<DocsManual>.Filter;
            var versionFilter = filter.Eq(x => x.ManualName, manualName) & filter.Eq(x => x.ManualVersion, manualVersion);
            var manual = _context.Docs.Find(versionFilter).SortByDescending(x => x.ManualVersion).First();
            
            return manual.Entries.FirstOrDefault(x =>
                string.Equals(entryName, x.EntryName, StringComparison.OrdinalIgnoreCase));
        }

        public string GetLatestVersionForManual(string manualName)
        {
            var manual = _context.Docs.Find(_ => true).SortByDescending(x => x.ManualVersion).First();
            return manual.ManualVersion;
        }
    }
}