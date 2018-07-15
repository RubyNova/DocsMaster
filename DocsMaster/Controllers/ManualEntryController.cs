using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DocsMaster.Models;
using DocsMaster.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocsMaster.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManualEntryController : ControllerBase
    {
        private readonly IManualEntryRetriverService _docsDb;

        public ManualEntryController(IManualEntryRetriverService docsDb)
        {
            _docsDb = docsDb;
        }

        [HttpGet("{manualName}")]
        public ActionResult<List<string>> GetAllManualVersions(string manualName)
        {
            return _docsDb.GetAllManualVersions(manualName);
        }

        [HttpGet("{manualName}/{entryName}")]
        public ActionResult<ManualEntryModel> GetEntryForLatestVersion(string manualName, string entryName)
        {
            var result = _docsDb.GetManualEntryForLatestVersion(manualName, entryName);
            
            if (result == null)return NotFound("The manual or entry does not exist.");
            
            return new ManualEntryModel
            {
                Description = result.Description,
                EntryName = result.EntryName,
                FullReferenceLink = result.FullReferenceLink,
                ManualVersion = _docsDb.GetLatestVersionForManual(manualName)
            };
        }
        
        [HttpGet("{manualName}/{entryName}/{version}")]
        public ActionResult<ManualEntryModel> GetEntry(string manualName, string entryName, string version)
        {
            var result = _docsDb.GetManualEntry(manualName, entryName, version);
            
            if (result == null)return NotFound("The manual or entry does not exist.");
            
            return new ManualEntryModel
            {
                Description = result.Description,
                EntryName = result.EntryName,
                FullReferenceLink = result.FullReferenceLink,
                ManualVersion = version
            };
        }
        

    }
}