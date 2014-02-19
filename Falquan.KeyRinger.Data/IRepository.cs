using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falquan.KeyRinger.Data
{
    public interface IRepository<T>
    {
        T Create(T entity);
        IQueryable<T> Retrieve();
        T Retrieve(Guid id);
        T Update(T entity);
        T Delete(Guid id);
    }
}
