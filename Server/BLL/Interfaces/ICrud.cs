using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Infrastructure;

namespace BLL.Interfaces
{
    public interface ICrud<T>
    {
        void Insert(T item);
        void Remove(T item);
    }
}
