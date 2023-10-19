using Jovana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.DAO
{
    public interface IBilansStanjaDAO
    {
        int Save(BilansStanja entity);
        BilansStanja FindById(int idbs);
        int GetIdbs(string idl);
        BilansStanja GetByIdl(string idl);
    }
}
