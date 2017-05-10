using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Entities
{
    public class ClientProfile
    {
        #region RelativeObjects

        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        #endregion

        public string Name { get; set; }
        
        public int Age { get; set; }


        #region RelativeCollections   

        public virtual ICollection<GlassesEntity> Glasses { get; set; }

        public virtual ICollection<ModeEntity> Modes { get; set; }

        public virtual ICollection<StatisticEntity> Statistic { get; set; }

        #endregion
    }
}