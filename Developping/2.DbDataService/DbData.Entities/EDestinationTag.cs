using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("DestinationTag")]
    public class EDestinationTag
    {
        public Guid DestinationId { get; set; }
        public Guid TagId { get; set; }

        public virtual EDestination Destination { get; set; }
        public virtual ETag Tag { get; set; }
    }
}
