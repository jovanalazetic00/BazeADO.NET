using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.DAO
{
    public interface ICRUDDao<T, ID>
    {
        int Count();

        int Delete(T entity);

        int DeleteAll();

        int DeleteById(ID id);

        bool ExistsById(ID id);

        IEnumerable<T> FindAll();

        IEnumerable<T> FindAllById(string idl);

        T FindById(ID id);

        int Save(T entity);

        int SaveAll(IEnumerable<T> entities);

    }
}
