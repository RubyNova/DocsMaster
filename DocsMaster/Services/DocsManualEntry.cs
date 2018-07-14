using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DocsMaster.Services
{
    public class DocsManualEntry
    {
        [BsonElement("entryName")]
        public string EntryName { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("fullReferenceLink")]
        public Uri FullReferenceLink { get; set; }
    }
}