using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocsMaster.Services
{
    public class DocsManual
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        [BsonElement("id")]
        public int Id { get; set; }
        [BsonElement("manualName")]
        public string ManualName { get; set; }
        [BsonElement("entries")]
        public List<DocsManualEntry> Entries { get; set; }
        [BsonElement("manualVersion")]
        public string ManualVersion { get; set; }
    }
}