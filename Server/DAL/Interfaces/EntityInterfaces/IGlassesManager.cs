using System;
using DAL.Entities;

namespace DAL.Interfaces.EntityInterfaces
{
    public interface IGlassesManager:ICrud<GlassesEntity>, IDisposable
    {
    }
}
