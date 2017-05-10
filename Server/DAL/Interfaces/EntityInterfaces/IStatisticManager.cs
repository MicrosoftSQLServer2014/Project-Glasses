using System;
using DAL.Entities;

namespace DAL.Interfaces.EntityInterfaces
{
    public interface IStatisticManager: ICrud<StatisticEntity>, IDisposable
    {
    }
}
