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
    public class LiceDAO : ILiceDAO<Lice>
    {
        public int CountByType(string type)
        {
            if (!(type.ToUpper().Trim().Equals("FIZICKO") || type.ToUpper().Trim().Equals("FIZICKO")))
            {
                return -1;
            }

            int rowsAffected = 0;

            string query = String.Format("select count(ido) from objekat where idl in (select idl from lice where vrstal = :type)");

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "type", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "type", type);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rowsAffected = reader.GetInt32(0);
                        }
                    }
                }

            }

            return rowsAffected;
        }

        public bool ExistsById(string idl)
        {
            if (FindById(idl) != null)
                return true;

            return false;
        }
        //Izvlaci lica na osnovu tipa 
        public List<Lice> ByType(string type)
        {
            List<Lice> lica = new List<Lice>();

            type.ToUpper();

            string query = String.Format("select * from lice where vrstal = :type");

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "type", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "type", type);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lica.Add(new Lice(reader.GetString(0), reader.GetString(1),
                                reader.GetString(2), reader.GetString(3), reader.GetInt32(4)));
                        }
                    }
                }

            }

            return lica;
        }

        public Lice FindById(string idl)
        {
            string query = "select lice.Idl, lice.Imel, lice.Przl, lice.Vrstal, lice.Mes_prihodIl from LICE where idl = :idl";
            Lice lice = null;

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
                            lice = new Lice(reader.GetString(0), reader.GetString(1),
                                  reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                        }
                    }
                }

            }

            return lice;
        }

        public int Save(Lice entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        public int Save(Lice entity, IDbConnection connection)
        {
            string insertSql = "insert into lice (imel, przl, vrstal, mes_prihodl, idl) " +
                               "values (:imel , :przl, :vrstal, :mes_prihodl, :idl)";

            string updateSql = "update lice set imel=:imel, przl=:przl, " +
                "vrstal=:vrstal, mes_prihodl=:mes_prihodl where Idl=:Idl";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Idl) ? updateSql : insertSql;

                ParameterUtil.AddParameter(command, "Imel", DbType.String, 50);
                ParameterUtil.AddParameter(command, "Prezl", DbType.String, 50);
                ParameterUtil.AddParameter(command, "Vrstal", DbType.String, 50);
                ParameterUtil.AddParameter(command, "Mes_prihodil", DbType.Int32);
                ParameterUtil.AddParameter(command, "Idl", DbType.String, 50);

                command.Prepare();

                ParameterUtil.SetParameterValue(command, "Idl", entity.Idl);
                ParameterUtil.SetParameterValue(command, "Imel", entity.Imel);
                ParameterUtil.SetParameterValue(command, "Prezl", entity.Przl);
                ParameterUtil.SetParameterValue(command, "Vrstal", entity.Vrstal);
                ParameterUtil.SetParameterValue(command, "Mes_prihodil", entity.Mes_PrihodL);

                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<Lice> entities)
        {
            throw new NotImplementedException();
        }
        //Prvo se vrsi provera ulaza, ako je nije fizicko ili pravno necemo ni pristupati bazi
        // Ako je tip adekvatan onda trazimo sve objekte lica koji odgovaraju odredjenom tipu, za to pristupamo i tabeli lice i tabeli objekat 
        // Na osnovu tabele lice izvlaci odgovarajuca lica i za ta lica trazi objekte
        public List<Objekat> ObjByType(string type)
        {
            if (!(type.ToUpper().Trim().Equals("FIZICKO") || type.ToUpper().Trim().Equals("PRAVNO")))
            {
                return null;
            }

            List<Objekat> objekti = new List<Objekat>();

            string query = String.Format("select * from objekat where idl in (select idl from lice where vrstal = :type)");

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "type", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "type", type);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objekti.Add(new Objekat(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5)));
                        }
                    }
                }

            }

            return objekti;
        }
        //Vraca sumu dugova za odredjen tip lica
        //Prvo pristupa tabeli lica kako bi izvukao prema odgovarajucem tipu
        //Onda za ta lica izvlaci dugove iz tabele bilansstanja
        public int SumDug(string type)
        {
            if (!(type.ToUpper().Trim().Equals("FIZICKO") || type.ToUpper().Trim().Equals("PRAVNO")))
            {
                return -1;
            }

            int dug = 0;

            string query = String.Format("select sum(dug) from bilansstanja where idl in (select idl from lice where vrstal = :type)");

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "type", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "type", type);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dug = reader.GetInt32(0);

                        }
                    }
                }

            }


            return dug;
        }
    }
}
