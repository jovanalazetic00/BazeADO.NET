using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jovana.Model;
using Jovana.DTO;

namespace Jovana.DAO
{
    public interface IObjekatDAO : ICRUDDao<Objekat, int>
    {
        ObjekatIDL All(string idl);

        int CenaObjekata(string idl);
        List<Objekat> GetByType(int objectType);
    }
}
