using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using src.Repository;
using src.Model;
using log4net;
using System.Linq;
using log4net.Config;

namespace src.Repository
{
    public class ParticipantDBRepository : IParticipantRepository<Participant>
    {
        private DBUtils dbUtils;
        IDictionary<string, string> props;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ParticipantDBRepository));

        private List<Participant> participanti = new List<Participant>();

        public ParticipantDBRepository(IDictionary<string, string> props)
        {
            Logger.Info("creating ParticipantDbRepository ");
            this.props = props;
        }
        
        public Participant findOneByNumeSiVarsta(string nume, string prenume, int varsta)
        {
            Logger.InfoFormat("entering findOneByNumeSiVarsta with values: nume={0}, prenume={1}, varsta={2}", nume, prenume, varsta);
            var con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id_participant from Participant where nume = @nume and prenume = @prenume and varsta = @varsta";

                var paramNume = comm.CreateParameter();
                paramNume.ParameterName = "@nume";
                paramNume.Value = nume;
                comm.Parameters.Add(paramNume);

                var paramPrenume = comm.CreateParameter();
                paramPrenume.ParameterName = "@prenume";
                paramPrenume.Value = prenume;
                comm.Parameters.Add(paramPrenume);

                var paramVarsta = comm.CreateParameter();
                paramVarsta.ParameterName = "@varsta";
                paramVarsta.Value = varsta;
                comm.Parameters.Add(paramVarsta);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var participant = new Participant(id, nume, prenume, varsta);

                        Logger.InfoFormat("exiting FindOneByNameAndAge with value: {0}", participant);
                        return participant;
                    }
                }
            }

            Logger.InfoFormat("exiting FindOneByNameAndAge with value: null");
            return null;
        }

        
        public void add(Participant participant)
        {
            Logger.InfoFormat("saving participant {0}", participant);
            var con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Participant values (@id_participant, @nume, @prenume, @varsta)";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_participant";
                paramId.Value = participant.id;
                comm.Parameters.Add(paramId);

                var paramNume = comm.CreateParameter();
                paramNume.ParameterName = "@nume";
                paramNume.Value = participant.Nume;
                comm.Parameters.Add(paramNume);

                var paramPrenume = comm.CreateParameter();
                paramPrenume.ParameterName = "@prenume";
                paramPrenume.Value = participant.Prenume;
                comm.Parameters.Add(paramPrenume);

                var paramVarsta = comm.CreateParameter();
                paramVarsta.ParameterName = "@varsta";
                paramVarsta.Value = participant.Varsta;
                comm.Parameters.Add(paramVarsta);

                // var paramProba = comm.CreateParameter();
                // paramProba.ParameterName = "@proba";
                // paramProba.Value = participant.Proba;
                // comm.Parameters.Add(paramProba);

                var result = comm.ExecuteNonQuery();
                Logger.InfoFormat("exiting add with value {0}", result);
            }
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
            // Logger.TraceEntry("updating participant {0}", participant);
            // using (IDbConnection con = dbUtils.getConnection())
            // {
            //     try
            //     {
            //         using (SqlCommand cmd = new SqlCommand("update Participant set nume = @nume, prenume = @prenume, varsta = @varsta, proba = @proba where id = @id", con))
            //         {
            //             cmd.Parameters.AddWithValue("@nume", participant.Nume);
            //             cmd.Parameters.AddWithValue("@prenume", participant.Prenume);
            //             cmd.Parameters.AddWithValue("@varsta", participant.Varsta);
            //             cmd.Parameters.AddWithValue("@proba", participant.Proba);
            //             cmd.Parameters.AddWithValue("@id", participant.Id);
            //
            //             int result = cmd.ExecuteNonQuery();
            //             Logger.Trace("updated {0} instances", result);
            //         }
            //     }
            //     catch (SqlException e)
            //     {
            //         Logger.Error(e);
            //         Console.Error.WriteLine("db error " + e);
            //     }
            // }
            // Logger.TraceExit();
        }

        public List<Participant> getAll()
        {
            // Logger.TraceEntry();
            // List<Participant> participanti = new List<Participant>();
            // using (IDbConnection con = dbUtils.getConnection())
            // {
            //     try
            //     {
            //         using (SqlCommand cmd = new SqlCommand("select * from participanti", con))
            //         {
            //             using (SqlDataReader reader = cmd.ExecuteReader())
            //             {
            //                 while (reader.Read())
            //                 {
            //                     int id = reader.GetInt32(reader.GetOrdinal("id"));
            //                     string nume = reader.GetString(reader.GetOrdinal("nume"));
            //                     string prenume = reader.GetString(reader.GetOrdinal("prenume"));
            //                     int varsta = reader.GetInt32(reader.GetOrdinal("varsta"));
            //                     string proba = reader.GetString(reader.GetOrdinal("proba"));
            //
            //                     Participant participant = new Participant(id, nume, prenume, varsta, proba);
            //                     participanti.Add(participant);
            //                 }
            //             }
            //         }
            //     }
            //     catch (SqlException e)
            //     {
            //         Logger.Error(e);
            //         Console.Error.WriteLine("db error " + e);
            //     }
            // }
            // Logger.TraceExit();
            // return participanti;
            return null;
        }

        public Participant findOne(int id)
        {
            Logger.InfoFormat("entering findOne with value {0}", id);
            var con = DBUtils.getConnection(props);
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select nume, prenume, varsta from Participant where id_participant = @id_participant";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_participant";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var nume = dataR.GetString(0);
                        var prenume = dataR.GetString(1);
                        var varsta = dataR.GetInt32(2);
                        //var proba = dataR.GetString(3);
                        
                        var participant = new Participant(id, nume, prenume, varsta);
                        
                        Logger.InfoFormat("exiting findOne with value {0}", participant);
                        return participant;
                    }
                }
            }
                Logger.InfoFormat("exiting FindOne with value {0}", null);
                return null;
        }

        public IEnumerable<Participant> findAll()
        {
            Logger.InfoFormat("entering findAll");
            var con = DBUtils.getConnection(props);
            IList<Participant> participants = new List<Participant>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Participant";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id_participant = dataR.GetInt32(0);
                        var nume = dataR.GetString(1);
                        var prenume = dataR.GetString(2);
                        var varsta = dataR.GetInt32(3);
                        //var proba = dataR.GetString(4);

                        var participant = new Participant(id_participant, nume, prenume, varsta);
                        participants.Add(participant);
                    }
                }
            }
            Logger.InfoFormat("exiting FindAll");
            return participants;
        }
    }
}