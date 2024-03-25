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
    public class ProbaDBRepository : IProbaRepository<Proba>
    {
        
        private DBUtils dbUtils;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProbaDBRepository));
        private List<Proba> probe = new List<Proba>();
        IDictionary<string, string> props;
        public ProbaDBRepository(IDictionary<string, string> props)
        {
            Logger.Info("creating ProbaDBRepositroy");
            this.props = props;
        }
        public void add(Proba proba)
        {
            Logger.InfoFormat("saving proba", proba);
            var con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Proba values (@id_proba, @tip_proba, @varsta_min, @varsta_max, @nr_loc, @locatie)";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_proba";
                paramId.Value = proba.IdProba;
                comm.Parameters.Add(paramId);

                var paramTipProba = comm.CreateParameter();
                paramTipProba.ParameterName = "@tip_proba";
                paramTipProba.Value = proba.TipProba;
                comm.Parameters.Add(paramTipProba);

                var paramVarstaMin = comm.CreateParameter();
                paramVarstaMin.ParameterName = "@varsta_min";
                paramVarstaMin.Value = proba.VarstaMin;
                comm.Parameters.Add(paramVarstaMin);

                var paramVarstaMax = comm.CreateParameter();
                paramVarstaMax.ParameterName = "@varsta_max";
                paramVarstaMax.Value = proba.VarstaMax;
                comm.Parameters.Add(paramVarstaMax);

                var paramNrLoc = comm.CreateParameter();
                paramNrLoc.ParameterName = "@nr_loc";
                paramNrLoc.Value = proba.NrLoc;
                comm.Parameters.Add(paramNrLoc);

                var paramLocatie = comm.CreateParameter();
                paramLocatie.ParameterName = "@locatie";
                paramLocatie.Value = proba.Locatie;
                comm.Parameters.Add(paramLocatie);

                var result = comm.ExecuteNonQuery();
                Logger.InfoFormat("exiting add with value {0}", result);
            }
        }

        public void update(Proba proba)
        {
            // Logger.TraceEntry("updating proba {0}", proba);
            // using (IDbConnection con = dbUtils.getConnection())
            // {
            //     try
            //     {
            //         using (SqlCommand cmd = new SqlCommand("update Proba set tip_proba = @tip_proba, varsta_min = @varsta_min, varsta_max = @varsta_max, nr_loc = @nr_loc, locatie = @locatie where id = @id", con))
            //         {
            //             cmd.Parameters.AddWithValue("@tip_proba", proba.TipProba);
            //             cmd.Parameters.AddWithValue("@varsta_min", proba.VarstaMin);
            //             cmd.Parameters.AddWithValue("@varsta_max", proba.VarstaMax);
            //             cmd.Parameters.AddWithValue("@nr_loc", proba.NrLoc);
            //             cmd.Parameters.AddWithValue("@locatie", proba.Locatie);
            //             cmd.Parameters.AddWithValue("@id", proba.IdProba);
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
            // Logger.TraceEntry();
            // List<Proba> probe = new List<Proba>();
            // using (IDbConnection con = dbUtils.getConnection())
            // {
            //     try
            //     {
            //         using (SqlCommand cmd = new SqlCommand("select * from probe", con))
            //         {
            //             using (SqlDataReader reader = cmd.ExecuteReader())
            //             {
            //                 while (reader.Read())
            //                 {
            //                     int id = reader.GetInt32(reader.GetOrdinal("id"));
            //                     string tip_proba = reader.GetString(reader.GetOrdinal("tip_proba"));
            //                     int varsta_min = reader.GetInt32(reader.GetOrdinal("varsta_min"));
            //                     int varsta_max = reader.GetInt32(reader.GetOrdinal("varsta_max"));
            //                     int nr_loc = reader.GetInt32(reader.GetOrdinal("nr_loc"));
            //                     string locatie = reader.GetString(reader.GetOrdinal("locatie"));
            //
            //                     Proba proba = new Proba(id, tip_proba, varsta_min, varsta_max, nr_loc, locatie);
            //                     probe.Add(proba);
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
            return null;
        }
        
        public Proba findOne(int id)
        {
            Logger.InfoFormat("entering FindOne with value {0}", id);
            var con = DBUtils.getConnection(props);
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select tip_proba, varsta_min, varsta_max, nr_loc, locatie from Proba where id_proba = @id_proba";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_proba";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var tip_proba = dataR.GetString(0);
                        var varsta_min = dataR.GetInt32(1);
                        var varsta_max = dataR.GetInt32(2);
                        var nr_loc = dataR.GetInt32(3);
                        var locatie = dataR.GetString(4);
                        
                        var proba = new Proba(id, tip_proba, varsta_min, varsta_max, nr_loc, locatie);
                        
                        Logger.InfoFormat("exiting FindOne with value {0}", proba);
                        return proba;
                    }
                }
            }
            Logger.InfoFormat("exiting FindOne with value {0}", null);
            return null;
        }
        
        public IEnumerable<Proba> findAll()
        {
            Logger.InfoFormat("entering findAll");
            var con = DBUtils.getConnection(props);
            IList<Proba> probas = new List<Proba>();
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Proba";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var tip_proba = dataR.GetString(1);
                        var varsta_min = dataR.GetInt32(2);
                        var varsta_max = dataR.GetInt32(3);
                        var nr_loc = dataR.GetInt32(4);
                        var locatie = dataR.GetString(5);
                        
                        var proba = new Proba(id, tip_proba, varsta_min, varsta_max, nr_loc, locatie);
                        
                        probas.Add(proba);
                    }
                }
            }

            Logger.InfoFormat("exiting FindAll");
            return probas;
        }
    }
}