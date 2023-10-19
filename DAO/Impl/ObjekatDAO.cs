using Jovana.Connection;
using Jovana.DTO;
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
    public class ObjekatDAO : IObjekatDAO
    {

        public int Count()
        {
            string query = "select count(*) from objekat";
            int rowsAffected = 0;
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
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

        public int Delete(Objekat entity)
        {
            string query = String.Format("delete from objekat where ido = {0}", entity.Ido);
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected;
                }
            }
        }

        public int DeleteAll()
        {
            string query = "delete from objekat";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected;
                }
            }
        }

        public int DeleteById(int id)
        {
            if (!ExistsById(id))
                return -1;

            string query = String.Format("delete from objekat where ido = {0}", id);

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected;
                }
            }
        }

        public bool ExistsById(int id)
        {
            if (FindById(id) != null)
                return true;

            return false;
        }

        public IEnumerable<Objekat> FindAll()
        {
            string query = "select * from objekat";
            List<Objekat> objekti = new List<Objekat>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;

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

        public IEnumerable<Objekat> FindAllById(string ido)
        {
            List<Objekat> objekti = new List<Objekat>();

            string query = String.Format("select * from objekat where ido = :ido");

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "ido", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "ido", ido);
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

        public Objekat FindById(int id)
        {
            string query = "select objekat.Ido, objekat.Idl, objekat.Idvo, objekat.Povrsina, objekat.Adresa, objekat.Vrednost from OBJEKAT where ido = :ido";
            Objekat objekat = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "ido", DbType.Int32);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "ido", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            objekat = new Objekat(reader.GetInt32(0), reader.GetString(1),
                                  reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5));
                        }
                    }
                }

            }

            return objekat;
        }

        public int Save(Objekat entity)
        {
            string query;
            if (ExistsById(entity.Ido))
                query = "update objekat set Ido =:Ido, Idl=:Idl, Idvo=:Idvo, " +
                "Povrsina=:Povrsina, Adresa=:Adresa, Vrednost=:Vrednost where Ido=:Ido";
            else
                query = "insert into objekat ( Ido, Idl, Idvo, Povrsina, Adresa, Vrednost) " +
                                "values (:Ido, :Idl , :Idvo, :Povrsina, :Adresa, :Vrednost)";
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection, query);
            }
        }

        public int Save(Objekat entity, IDbConnection connection, string query)
        {

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                ParameterUtil.AddParameter(command, "Ido", DbType.Int32);
                ParameterUtil.AddParameter(command, "Idl", DbType.String);
                ParameterUtil.AddParameter(command, "Idvo", DbType.Int32);
                ParameterUtil.AddParameter(command, "Povrsina", DbType.Int32);
                ParameterUtil.AddParameter(command, "Adresa", DbType.String);
                ParameterUtil.AddParameter(command, "Vrednost", DbType.Int32);


                command.Prepare();

                ParameterUtil.SetParameterValue(command, "Ido", entity.Ido);
                ParameterUtil.SetParameterValue(command, "Idl", entity.Idl);
                ParameterUtil.SetParameterValue(command, "Idvo", entity.Idvo);
                ParameterUtil.SetParameterValue(command, "Povrsina", entity.Povrsina);
                ParameterUtil.SetParameterValue(command, "Adresa", entity.Adresa);
                ParameterUtil.SetParameterValue(command, "Vrednost", entity.Vrednost);

                return command.ExecuteNonQuery();

            }
        }

        public int SaveAll(IEnumerable<Objekat> entities)
        {
            List<String> qs = new List<string>();
            foreach (var entity in entities)
            {
                if (ExistsById(entity.Ido))
                {
                    qs.Add("update objekat set Ido =:Ido, Idl=:Idl, Idvo=:Idvo, " +
                "Povrsina=:Povrsina, Adresa=:Adresa, Vrednost=:Vrednost where Ido=:Ido");
                }

                else
                {
                    qs.Add("insert into objekat ( Ido, Idl, Idvo, Povrsina, Adresa, Vrednost) " +
                                "values (:Ido, :Idl , :Idvo, :Povrsina, :Adresa, :Vrednost)");
                }

            }
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                int numSaved = 0;

                int i = 0;
                foreach (Objekat entity in entities)
                {
                    numSaved += Save(entity, connection, qs[i]);
                    i++;
                }

                transaction.Commit();

                return numSaved;
            }
        }
        //Ova metoda ce da izvuce sve objekte cije polje idl == idl-u koji je prosledjen.
        public ObjekatIDL All(string idl)
        {
            ObjekatIDL objekatLice = new ObjekatIDL() { Idl = idl, Objekti = new List<Objekat>() };

            string query = String.Format("select IDO, IDL, IDVO, POVRSINA, ADRESA, VREDNOST from objekat where idl = :idl");

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
                        while (reader.Read())
                        {
                            Objekat o = new Objekat(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5));

                            objekatLice.Objekti.Add(o);
                        }
                    }
                }

            }

            return objekatLice;
        }

        public int CenaObjekata(string idl)
        {

            string query = String.Format("select sum(vrednost) from objekat where idl = :idl");
            int ukupnaCena = 0;

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
                            ukupnaCena = reader.GetInt32(0);

                        }
                    }
                }

            }

            return ukupnaCena;
        }

        public List<Objekat> GetByType(int objectType)
        {

            string query = String.Format("select * from objekat where idvo = :idvo");
            List<Objekat> objekats = new List<Objekat>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idvo", DbType.Int32);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idvo", objectType);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objekats.Add(new Objekat(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5)));

                        }
                    }
                }

            }

            return objekats;
        }
    }
}
