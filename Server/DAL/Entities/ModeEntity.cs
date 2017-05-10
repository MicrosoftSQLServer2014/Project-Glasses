using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ModeEntity
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double LeftLenseOpticPower { get; set; }
        [Required]
        public double RightLenseOpticPower { get; set; }
        public int NecessaryIllumination { get; set; }


        #region ForeignKeys

        [ForeignKey("ClientProfile")]
        public string ClientProfileId { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }

        #endregion

        #region RelativeCollections

        public virtual ICollection<GlassesEntity> Glasses { get; set; }

        public virtual ICollection<StatisticEntity> Statistic { get; set; }

        #endregion

    }
}
