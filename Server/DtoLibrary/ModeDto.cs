using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibrary
{
    public class ModeDto
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double LeftLenseOpticPower { get; set; }
        [Required]
        public double RightLenseOpticPower { get; set; }
        public int NecessaryIllumination { get; set; }

        [Required]
        public string UserName { get; set; }

        ModeDto(string userName)
        {
            UserName = userName;
        }
    }
}
