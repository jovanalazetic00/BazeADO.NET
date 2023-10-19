using Jovana.Connection;
using Jovana.Model;
using Jovana.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.DAO.Impl
{
    public class BilansStanjaDAO : IBilansStanjaDAO
    {
        public int Save(BilansStanja entity)
        {
            string query;
            if (ExistsById(entity.Idbs))
                query = "update BILANSSTANJA set Idbs =:Idbs, Idl=:Idl, Saldo=:Saldo, " +
                "Dug=:Dug, Kamata=:Kamata where Idbs =:Idbs";
            else
                query = "insert into BILANSSTANJA ( Idbs, Idl, Saldo, Dug, Kamata) " +
                                "values (:Idbs, :Idl , :Saldo, :Dug, :Kamata)";
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection, query);
            }
        }

        public int Save(BilansStanja entity, IDbConnection connection, string query)
        {

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                ParameterUtil.AddParameter(command, "Idbs", DbType.Int32);
                ParameterUtil.AddParameter(command, "Idl", DbType.String);
                ParameterUtil.AddParameter(command, "Saldo", DbType.Decimal);
                ParameterUtil.AddParameter(command, "Dug", DbType.Decimal);
                ParameterUtil.AddParameter(command, "Kamata", DbType.Decimal);


                command.Prepare();

                ParameterUtil.SetParameterValue(command, "Idbs", entity.Idbs);
                ParameterUtil.SetParameterValue(command, "Idl", entity.Idl);
                ParameterUtil.SetParameterValue(command, "Saldo", entity.Saldo);
                ParameterUtil.SetParameterValue(command, "Dug", entity.Dug);
                ParameterUtil.SetParameterValue(command, "Kamata", entity.Kamata);

                return command.ExecuteNonQuery();

            }
        }

        public int SaveAll(IEnumerable<BilansStanja> entities)
        {
            List<String> qs = new List<string>();
            foreach (var entity in entities)
            {
                if (ExistsById(entity.Idbs))
                {
                    qs.Add("update BILANSSTANJA set Idbs =:Idbs, Idl=:Idl, Saldo=:Saldo, " +
                "Dug=:Dug, Kamata=:Kamata");
                }

                else
                {
                    qs.Add("insert into BILANSSTANJA ( Idbs, Idl, Saldo, Dug, Kamata) " +
                                "values (:Idbs, :Idl , :Saldo, :Dug, :Kamata)");
                }

            }
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                int numSaved = 0;

                int i = 0;
                foreach (BilansStanja entity in entities)
                {
                    numSaved += Save(entity, connection, qs[i]);
                    i++;
                }

                transaction.Commit();

                return numSaved;
            }
        }

        public bool ExistsById(int idbs)
        {
            if (FindById(idbs) != null)
                return true;

            return false;
        }

        public BilansStanja FindById(int idbs)
        {
            string query = "select * from BILANSSTANJA where idbs = :idbs";
            BilansStanja bilans = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idbs", DbType.Int32);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idbs", idbs);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bilans = new BilansStanja(reader.GetInt32(0), reader.GetString(1),
                                  reader.GetDecimal(2), reader.GetDecimal(3), reader.GetDecimal(4));
                        }
                    }
                }

            }

            return bilans;
        }

        public int GetIdbs(string idl)
        {
            int idbs = -1;

            string query = "select bilansstanja.idbs from bilansstanja where idl =:idl";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idl", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idl", idl);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idbs = reader.GetInt32(0);
                        }
                    }
                }

            }

            return idbs;
        }

        public BilansStanja GetByIdl(string idl)
        {
            BilansStanja idbs = null;

            string query = "select * from bilansstanja where idl =:idl";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idl", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idl", idl);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idbs = new BilansStanja(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetDecimal(3), reader.GetDecimal(4));
                        }
                    }
                }

            }

            return idbs;
        }
    }
}