using System;
using System.Collections.Generic;
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
            //var filter = Builders<DocsManual>.Filter.Eq(x => x.ManualName, manualName);
            var manual = _context.Docs.AsQueryable().Where(x => x.ManualName.ToLower() == manualName.ToLower()).OrderByDescending(x => x.ManualVersion).FirstOrDefault();

            return manual?.Entries.FirstOrDefault(x =>
                string.Equals(entryName, x.EntryName, StringComparison.OrdinalIgnoreCase));
        }

        public DocsManualEntry GetManualEntry(string manualName, string entryName, string manualVersion)
        {
            var manual = _context.Docs.AsQueryable().FirstOrDefault(x =>
                x.ManualName.ToLower() == manualName.ToLower() && x.ManualVersion.ToLower() == manualVersion.ToLower());

            return manual?.Entries.FirstOrDefault(x =>
                string.Equals(entryName, x.EntryName, StringComparison.OrdinalIgnoreCase));
        }

        public string GetLatestVersionForManual(string manualName)
        {
            var manual = _context.Docs.Find(_ => true).SortByDescending(x => x.ManualVersion).First();
            return manual.ManualVersion;
        }

        public List<string> GetAllManualVersions(string manualName)
        {
            return _context.Docs.AsQueryable().Where(x => x.ManualName.ToLower() == manualName.ToLower())
                .Select(x => x.ManualVersion).OrderBy(x => x).ToList();
        }
    }
}