using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibrary
{
    public class StatisticDto
    {
        public string Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public int Illumination { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public ModeDto Mode { get; set; }

        public StatisticDto(string userName)
        {
            UserName = userName;
        }
    }
}
