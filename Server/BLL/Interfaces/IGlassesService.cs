using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Infrastructure;
using DAL.Entities;
using DtoLibrary;

namespace BLL.Interfaces
{
    public interface IGlassesService: ICrud<GlassesDto>
    {
        void ChangeMode(GlassesDto glassesDto, ModeDto modeDto);
    }
}
