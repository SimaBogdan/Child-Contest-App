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
    public class ConcursDBRepository : IConcursRepository<Concurs>
    {
        private DBUtils dbUtils;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ConcursDBRepository));
        IDictionary<string, string> props;
        private List<Concurs> concursuri = new List<Concurs>();

        public ConcursDBRepository(IDictionary<string, string> props)
        {
            Logger.Info("creating ConcursDBRepository ");
            this.props = props;
        }
        
        public IEnumerable<Concurs> findByProba(int id_proba)
        {
            Logger.InfoFormat("entering findByProba");
            var con = DBUtils.getConnection(props);
            IList<Concurs> concursuri = new List<Concurs>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Concurs where id_proba = @id_proba";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_proba";
                paramId.Value = id_proba;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var idParticipant = dataR.GetInt32(1);
                        var idOrganizator = dataR.GetInt32(3);

                        var concurs = new Concurs(id, idParticipant, id_proba, idOrganizator);

                        concursuri.Add(concurs);
                    }
                }
            }

            Logger.InfoFormat("exiting findByProba");
            return concursuri;
        }
        
        public IEnumerable<Concurs> findByParticipant(int id_participant)
        {
            Logger.InfoFormat("entering FindByParticipant");
            var con = DBUtils.getConnection(props);
            IList<Concurs> concursList = new List<Concurs>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Concurs where id_participant = @id_participant";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_participant";
                paramId.Value = id_participant;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var idProba = dataR.GetInt32(2);
                        var idOrganizator = dataR.GetInt32(3);
                        
                        var concurs = new Concurs(id, id_participant, idProba, idOrganizator);

                        concursList.Add(concurs);
                    }
                }
            }

            Logger.InfoFormat("exiting FindByParticipant");
            return concursList;
        }

        public void add(Concurs concurs)
        {
            Logger.InfoFormat("saving concurs", concurs);
            var con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Concurs values (@id_concurs, @id_participant, @id_proba, @id_organizator)";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_concurs";
                paramId.Value = concurs.id;
                comm.Parameters.Add(paramId);

                var paramIdParticipant = comm.CreateParameter();
                paramIdParticipant.ParameterName = "@id_participant";
                paramIdParticipant.Value = concurs.IdParticipant;
                comm.Parameters.Add(paramIdParticipant);

                var paramIdProba = comm.CreateParameter();
                paramIdProba.ParameterName = "@id_proba";
                paramIdProba.Value = concurs.IdProba;
                comm.Parameters.Add(paramIdProba);

                var paramIdOrg = comm.CreateParameter();
                paramIdOrg.ParameterName = "@id_organizator";
                paramIdOrg.Value = concurs.IdOrganizator;
                comm.Parameters.Add(paramIdOrg);

                var result = comm.ExecuteNonQuery();
                Logger.InfoFormat("exiting add with value {0}", result);
            }
        }
        
        public Concurs findOne(int id)
        {
            Logger.InfoFormat("entering findOne with value {0}", id);
            var con = DBUtils.getConnection(props);
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id_participant, id_proba from Concurs where id_concurs = @id_concurs";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_concurs";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var id_participant = dataR.GetInt32(0);
                        var id_proba = dataR.GetInt32(1);
                        var id_organizator = dataR.GetInt32(2);
                        
                        var concurs = new Concurs(id, id_participant, id_proba, id_organizator);
                        
                        Logger.InfoFormat("exiting findOne with value {0}", concurs);
                        return concurs;
                    }
                }
            }
            Logger.InfoFormat("exiting findOne with value {0}", null);
            return null;
        }
        
        public IEnumerable<Concurs> findAll()
        {
            Logger.InfoFormat("entering findAll");
            var con = DBUtils.getConnection(props);
            IList<Concurs> concursList = new List<Concurs>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Concurs";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id_concurs = dataR.GetInt32(0);
                        var id_participant = dataR.GetInt32(1);
                        var id_proba = dataR.GetInt32(2);
                        var id_organizator = dataR.GetInt32(3);
                        
                        var concurs = new Concurs(id_concurs, id_participant, id_proba, id_organizator);
                        concursList.Add(concurs);
                    }
                }
            }
            Logger.InfoFormat("exiting FindAll");
            return concursList;
        }

        public void update(Concurs concurs)
        {
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public Concurs getById(int id)
        {
            throw new NotImplementedException();
        }

        public int id { get; }

        public int size()
        {
            throw new NotImplementedException();
        }

        public List<Concurs> getAll()
        {
            throw new NotImplementedException();
        }
    }
}
// {
        //     Logger.TraceEntry("updating concurs {0}", concurs);
        //     using (IDbConnection con = dbUtils.getConnection())
        //     {
        //         try
        //         {
        //             using (SqlCommand cmd = new SqlCommand("update Concurs set proba = @proba, participant = @participant where id = @id", con))
        //             {
        //                 cmd.Parameters.AddWithValue("@proba", concurs.Proba);
        //                 cmd.Parameters.AddWithValue("@participant", concurs.Participant);
        //                 cmd.Parameters.AddWithValue("@id", concurs.id);
        //
        //                 int result = cmd.ExecuteNonQuery();
        //                 Logger.Trace("updated {0} instances", result);
        //             }
        //         }
        //         catch (SqlException e)
        //         {
        //             Logger.Error(e);
        //             Console.Error.WriteLine("db error " + e);
        //         }
        //     }
        //     Logger.TraceExit();