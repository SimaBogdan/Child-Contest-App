using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using src.Repository;
using src.Model;
using log4net;

namespace src.Repository
{
    public class ConcursDBRepository : IRepo<Concurs>
    {
        private DBUtils dbUtils;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ConcursDBRepository));

        private List<Concurs> concursuri = new List<Concurs>();

        public ConcursDBRepository(DBUtils dbUtils)
        {
            this.dbUtils = dbUtils;
        }
        public void add(Concurs concurs)
        {
            Logger.TraceEntry("saving concurs {0}", concurs);
            using (IDbConnection con = DBUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("insert into Concurs (id, proba, participant) values (@id, @proba, @participant)", con))
                    {
                        cmd.Parameters.AddWithValue("@id", concurs.id);
                        cmd.Parameters.AddWithValue("@proba", concurs.Proba);
                        cmd.Parameters.AddWithValue("@participant", concurs.Participant);

                        int result = cmd.ExecuteNonQuery();
                        Logger.Trace("saved {0} instances", result);
                    }
                }
                catch (SqlException e)
                {
                    Logger.Error(e);
                    Console.Error.WriteLine("db error " + e);
                }
            }
            Logger.TraceExit();
        }

        public void update(Concurs concurs)
        {
            Logger.TraceEntry("updating concurs {0}", concurs);
            using (IDbConnection con = dbUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("update Concurs set proba = @proba, participant = @participant where id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@proba", concurs.Proba);
                        cmd.Parameters.AddWithValue("@participant", concurs.Participant);
                        cmd.Parameters.AddWithValue("@id", concurs.id);

                        int result = cmd.ExecuteNonQuery();
                        Logger.Trace("updated {0} instances", result);
                    }
                }
                catch (SqlException e)
                {
                    Logger.Error(e);
                    Console.Error.WriteLine("db error " + e);
                }
            }
            Logger.TraceExit();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public Concurs getById(int id)
        {
            foreach (Concurs concurs in concursuri)
            {
                if (concurs.id == id)
                    return concurs;
            }
            return null;
        }

        public int id { get; }
        public int size()
        {
            return concursuri.Capacity;
        }

        public Proba getProbaById(int id)
        {
            Concurs concurs = getById(id);
            if (concurs != null)
            {
                return concurs.Proba;
            }

            return default;
        }
        
        public Participant getParticipantById(int id)
        {
            Concurs concurs = getById(id);
            if (concurs != null)
            {
                return concurs.Participant;
            }

            return default;
        }

        public List<Concurs> getAll()
        {
            Logger.TraceEntry();
            List<Concurs> concursuri = new List<Concurs>();
            using (IDbConnection con = dbUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select * from concursuri", con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("id"));
                                int id_proba = reader.GetInt32(reader.GetOrdinal("id_proba"));
                                int id_participant = reader.GetInt32(reader.GetOrdinal("id_participant"));



                                Concurs concurs = new Concurs(id, getProbaById(id_proba),
                                    getParticipantById(id_participant));
                                concursuri.Add(concurs);
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Logger.Error(e);
                    Console.Error.WriteLine("db error " + e);
                }
            }
            Logger.TraceExit();
            return concursuri;
        }
        
        public Concurs findOne(int id)
        {
            Logger.TraceEntry();
            using (IDbConnection con = dbUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select * from Concurs where id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id_proba = reader.GetInt32(reader.GetOrdinal("id_proba"));
                                int id_participant = reader.GetInt32(reader.GetOrdinal("id_participant"));

                                Concurs concurs = new Concurs(id, getProbaById(id_proba),
                                    getParticipantById(id_participant));
                                concurs.id = id;

                                Logger.TraceExit();
                                return concurs;
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Logger.Error(e);
                    Console.Error.WriteLine("db error " + e);
                    Logger.TraceExit();
                }
            }
            return null;
        }
    }
}