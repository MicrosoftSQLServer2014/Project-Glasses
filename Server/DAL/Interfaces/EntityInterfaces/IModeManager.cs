using System;
using DAL.Entities;

namespace DAL.Interfaces.EntityInterfaces
{
    public interface IModeManager: ICrud<ModeEntity>, IDisposable
    {

    }
}
