using Jovana.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.DAO
{
    public interface ILiceDAO<T>
    {
        List<Objekat> ObjByType(string type);
        int CountByType(string type);
        int SumDug(string type);
        int Save(T entity);
        int SaveAll(IEnumerable<T> entities);
        int Save(T entity, IDbConnection connection);
        bool ExistsById(string id);
        T FindById(string id);
        List<T> ByType(string type);
    }
}
