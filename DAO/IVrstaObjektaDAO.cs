using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.DAO
{
    public interface IVrstaObjektaDAO
    {
        string GetVrstaByID(int idvo);
        int GetByName(string objectType);
    }
}
