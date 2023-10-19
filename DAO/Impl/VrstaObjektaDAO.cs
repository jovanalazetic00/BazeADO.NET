using Jovana.Connection;
using Jovana.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.DAO.Impl
{
    public class VrstaObjektaDAO : IVrstaObjektaDAO
    {
        public int GetByName(string objectType)
        {
            int idvo = -1;

            string query = "select * from vrstaobjekta where NAZIVVO =:objectType";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "objectType", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "objectType", objectType);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idvo = reader.GetInt32(0);

                            return idvo;
                        }
                    }
                }

            }

            return -1;
        }

        public string GetVrstaByID(int idvo)
        {
            string vrstaObj = "";

            string query = "select nazivvo from vrstaobjekta where idvo =:idvo";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idvo", DbType.Int32);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idvo", idvo);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            vrstaObj = reader.GetString(0);
                        }
                    }
                }

            }

            return vrstaObj;
        }
    }
}
