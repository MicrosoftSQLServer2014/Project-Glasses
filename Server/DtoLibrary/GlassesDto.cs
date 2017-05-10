using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibrary
{
    public class GlassesDto
    {
        public string Id { get; set; }
        public string ModeId { get; set; }

        [Required]
        public string UserName { get; set; }
        public ModeDto Mode { get; set; }

        public GlassesDto(string userName)
        {
            UserName = userName;
        }
    }
}
