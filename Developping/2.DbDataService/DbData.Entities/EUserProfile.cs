using Mic.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("UserProfile")]
    public class EUserProfile : EntityBase<EUserProfile>
    {
        // UserId
        public Guid Id { get; set; }

        [StringLength(200)]
        public string BannerUrl { get; set; }

        [StringLength(500)]
        public string Intro { get; set; }

        public int TotalExperiences { get; set; }
        public int TotalPlans { get; set; }
        public long TotalComments { get; set; }
        public long TotalRates { get; set; }
        public double AvgRates { get; set; }
        public int TotalFollowers { get; set; }
        public int TotalFollowings { get; set; }
    }
}
