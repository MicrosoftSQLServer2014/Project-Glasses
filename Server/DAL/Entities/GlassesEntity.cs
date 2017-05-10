using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class GlassesEntity
    {
        [Key]
        public string Id { get; set; }


        #region ForeignKeys

        [ForeignKey("Mode")]
        public string ModeId { get; set; }
        public virtual ModeEntity Mode { get; set; }

        [ForeignKey("ClientProfile")]
        public string ClientProfileId { get; set; }
        public virtual  ClientProfile ClientProfile { get; set; }

        #endregion
    }
}
