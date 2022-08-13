using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("AttractionTag")]
    public class EAttractionTag
    {
        public Guid AttractionId { get; set; }
        public Guid TagId { get; set; }

        public virtual EAttraction Attraction { get; set; }
        public virtual ETag Tag { get; set; }
    }
}
