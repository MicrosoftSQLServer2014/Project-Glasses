using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class StatisticEntity
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public int Illumination { get; set; }


        #region ForeignKeys

        [ForeignKey("ClientProfile")]
        public string ClientProfileId { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }

        [ForeignKey("Mode")]
        public string ModeId { get; set; }
        public virtual ModeEntity Mode { get; set; }

        #endregion
    }
}
