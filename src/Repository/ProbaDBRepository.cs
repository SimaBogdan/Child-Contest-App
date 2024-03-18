using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using src.Repository;
using src.Model;
using log4net;

namespace src.Repository
{
    public class ProbaDBRepository : IProbaRepository<Proba>
    {
        
        private DBUtils dbUtils;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProbaDBRepository));
        private List<Proba> probe = new List<Proba>();
        public ProbaDBRepository(DBUtils dbUtils)
        {
            this.dbUtils = dbUtils;
        }
        public void add(Proba proba)
        {
            Logger.TraceEntry("saving proba {0}", proba);
            using (IDbConnection con = DBUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("insert into Proba (id, tip_proba, varsta_min, varsta_max, nr_loc, locatie) values (@id, @tip_proba, @varsta_min, @varsta_max, @nr_loc, @locatie)", con))
                    {
                        cmd.Parameters.AddWithValue("@id", proba.IdProba);
                        cmd.Parameters.AddWithValue("@tip_proba", proba.TipProba);
                        cmd.Parameters.AddWithValue("@varsta_min", proba.VarstaMin);
                        cmd.Parameters.AddWithValue("@varsta_max", proba.VarstaMax);
                        cmd.Parameters.AddWithValue("@nr_loc", proba.NrLoc);
                        cmd.Parameters.AddWithValue("@locatie", proba.Locatie);

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

        public void update(Proba proba)
        {
            Logger.TraceEntry("updating proba {0}", proba);
            using (IDbConnection con = dbUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("update Proba set tip_proba = @tip_proba, varsta_min = @varsta_min, varsta_max = @varsta_max, nr_loc = @nr_loc, locatie = @locatie where id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@tip_proba", proba.TipProba);
                        cmd.Parameters.AddWithValue("@varsta_min", proba.VarstaMin);
                        cmd.Parameters.AddWithValue("@varsta_max", proba.VarstaMax);
                        cmd.Parameters.AddWithValue("@nr_loc", proba.NrLoc);
                        cmd.Parameters.AddWithValue("@locatie", proba.Locatie);
                        cmd.Parameters.AddWithValue("@id", proba.IdProba);

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

        public Proba getById(int id)
        {
            foreach (Proba proba in probe)
            {
                if (proba.IdProba == id)
                    return proba;
            }
            return null;
        }

        public int id { get; }
        public int size()
        {
            return probe.Capacity;
        }

        public List<Proba> getAll()
        {
            Logger.TraceEntry();
            List<Proba> probe = new List<Proba>();
            using (IDbConnection con = dbUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select * from probe", con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("id"));
                                string tip_proba = reader.GetString(reader.GetOrdinal("tip_proba"));
                                int varsta_min = reader.GetInt32(reader.GetOrdinal("varsta_min"));
                                int varsta_max = reader.GetInt32(reader.GetOrdinal("varsta_max"));
                                int nr_loc = reader.GetInt32(reader.GetOrdinal("nr_loc"));
                                string locatie = reader.GetString(reader.GetOrdinal("locatie"));

                                Proba proba = new Proba(id, tip_proba, varsta_min, varsta_max, nr_loc, locatie);
                                probe.Add(proba);
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
            return probe;
        }
        
        public Proba findOne(int id)
        {
            Logger.TraceEntry();
            using (IDbConnection con = dbUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select * from Proba where id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string tip_proba = reader.GetString(reader.GetOrdinal("tip_proba"));
                                int varsta_min = reader.GetInt32(reader.GetOrdinal("varsta_min"));
                                int varsta_max = reader.GetInt32(reader.GetOrdinal("varsta_max"));
                                int nr_loc = reader.GetInt32(reader.GetOrdinal("nr_loc"));
                                string locatie = reader.GetString(reader.GetOrdinal("locatie"));

                                Proba proba = new Proba(id, tip_proba, varsta_min, varsta_max, nr_loc, locatie);
                                proba.IdProba = id;

                                Logger.TraceExit();
                                return proba;
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