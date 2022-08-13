using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities
{
    [Table("ItemView")]
    public class EItemView : EntityBase<EItemView>
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public Guid DetailId { get; set; }
        [StringLength(500)]
        public string ImgThumb { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Author { get; set; }
        public int SavedInStopBy { get; set; }
        public int Saved { get; set; }
        public int Comment { get; set; }
        public int RankingByAdmin { get; set; }
        public int RankingByUser { get; set; }
        public int Like { get; set; }
        public int NumberItemInCollection { get; set; }
        public DateTime CreatedTime { get; set; }
        public EnumPostStatus Status { get; set; }
        public int TopView { get; set; }
        public string RouteUri { get; set; }
    }
}
