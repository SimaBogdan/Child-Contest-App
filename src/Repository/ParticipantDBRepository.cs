using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using src.Repository;
using src.Model;
using log4net;

namespace src.Repository
{
    public class ParticipantDBRepository : IParticipantRepository<Participant>
    {
        private DBUtils dbUtils;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ParticipantDBRepository));

        private List<Participant> participanti = new List<Participant>();

        public ParticipantDBRepository(DBUtils dbUtils)
        {
            this.dbUtils = dbUtils;
        }

        
        public void add(Participant participant)
        {
            Logger.TraceEntry("saving participant {0}", participant);
            using (IDbConnection con = DBUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("insert into Participant (id, nume, prenume, varsta, proba) values (@id, @nume, @prenume, @varsta, @proba)", con))
                    {
                        cmd.Parameters.AddWithValue("@id", participant.Id);
                        cmd.Parameters.AddWithValue("@nume", participant.Nume);
                        cmd.Parameters.AddWithValue("@prenume", participant.Prenume);
                        cmd.Parameters.AddWithValue("@varsta", participant.Varsta);
                        cmd.Parameters.AddWithValue("@proba", participant.Proba);

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

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public Participant getById(int id)
        {
            foreach (Participant participant in participanti)
            {
                if (participant.Id == id)
                    return participant;
            }
            return null;
        }

        public int id { get; }

        public int size()
        {
            return participanti.Count;
        }

        public void update(Participant participant)
        {
            Logger.TraceEntry("updating participant {0}", participant);
            using (IDbConnection con = dbUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("update Participant set nume = @nume, prenume = @prenume, varsta = @varsta, proba = @proba where id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@nume", participant.Nume);
                        cmd.Parameters.AddWithValue("@prenume", participant.Prenume);
                        cmd.Parameters.AddWithValue("@varsta", participant.Varsta);
                        cmd.Parameters.AddWithValue("@proba", participant.Proba);
                        cmd.Parameters.AddWithValue("@id", participant.Id);

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

        public List<Participant> getAll()
        {
            Logger.TraceEntry();
            List<Participant> participanti = new List<Participant>();
            using (IDbConnection con = dbUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select * from participanti", con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("id"));
                                string nume = reader.GetString(reader.GetOrdinal("nume"));
                                string prenume = reader.GetString(reader.GetOrdinal("prenume"));
                                int varsta = reader.GetInt32(reader.GetOrdinal("varsta"));
                                string proba = reader.GetString(reader.GetOrdinal("proba"));

                                Participant participant = new Participant(id, nume, prenume, varsta, proba);
                                participanti.Add(participant);
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
            return participanti;
        }

        public Participant findOne(int id)
        {
            Logger.TraceEntry();
            using (IDbConnection con = dbUtils.getConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select * from Participant where id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string nume = reader.GetString(reader.GetOrdinal("nume"));
                                string prenume = reader.GetString(reader.GetOrdinal("prenume"));
                                int varsta = reader.GetInt32(reader.GetOrdinal("varsta"));
                                string proba = reader.GetString(reader.GetOrdinal("proba"));

                                Participant participant = new Participant(id, nume, prenume, varsta, proba);
                                participant.Id = id;

                                Logger.TraceExit();
                                return participant;
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