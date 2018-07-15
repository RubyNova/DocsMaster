using System.Collections.Generic;

namespace DocsMaster.Services
{
    public interface IManualEntryRetriverService
    {
        DocsManualEntry GetManualEntryForLatestVersion(string manualName, string entryName);
        DocsManualEntry GetManualEntry(string manualName, string entryName, string manualVersion);
        string GetLatestVersionForManual(string manualName);
        List<string> GetAllManualVersions(string manualName);
    }
}