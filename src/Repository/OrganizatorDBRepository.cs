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
    public class OrganizatorDBRepository : IOrganizatorRepository<Organizator>
    {
        
        private DBUtils dbUtils;
        IDictionary<string, string> props;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OrganizatorDBRepository));

        private List<Organizator> organizators = new List<Organizator>();

        public OrganizatorDBRepository(IDictionary<string, string> props)
        {
            Logger.Info("creating OrganizatorDBRepository ");
            this.props = props;
        }
        
        public Organizator findOneByUsernameAndPassword(string username, string parola)
        {
            Logger.InfoFormat("entering findOneByUsernameAndPassword with values: username={0}", username);
            var con = DBUtils.getConnection(props);
            

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id_organizator, nume, prenume from Organizator where parola = @parola and username = @username";

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@parola";
                paramPassword.Value = parola;
                comm.Parameters.Add(paramPassword);
                
                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = username;
                comm.Parameters.Add(paramUsername);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var id = dataR.GetInt32(0);
                        var nume = dataR.GetString(1);
                        var prenume = dataR.GetString(2);

                        var organizator = new Organizator(id, nume, prenume, parola, username);

                        Logger.InfoFormat("exiting findOneByUsernameAndPassword with value: {0}", organizator);
                        return organizator;
                    }
                }
            }

            Logger.InfoFormat("exiting findOneByUsernameAndPassword with value: null");
            return null;
        }
        
        public void add(Organizator organizator)
        {
            Logger.InfoFormat("saving participant {0}", organizator);
            var con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into Organizator values (@id_organizator, @nume, @prenume, @parola, @username)";

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_organizator";
                paramId.Value = organizator.id;
                comm.Parameters.Add(paramId);

                var paramNume = comm.CreateParameter();
                paramNume.ParameterName = "@nume";
                paramNume.Value = organizator.Nume;
                comm.Parameters.Add(paramNume);

                var paramPrenume = comm.CreateParameter();
                paramPrenume.ParameterName = "@prenume";
                paramPrenume.Value = organizator.Prenume;
                comm.Parameters.Add(paramPrenume);

                var paramParola = comm.CreateParameter();
                paramParola.ParameterName = "@parola";
                paramParola.Value = organizator.Parola;
                comm.Parameters.Add(paramParola);

                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = organizator.Username;
                comm.Parameters.Add(paramUsername);

                var result = comm.ExecuteNonQuery();
                Logger.InfoFormat("exiting add with value {0}", result);
            }
        }
        
        public Organizator findOne(int id)
        {
            Logger.InfoFormat("entering findOne with value {0}", id);
            var con = DBUtils.getConnection(props);
            
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select nume, prenume, parola, username from Organizator where id_organizator = @id_organizator";
                
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_organizator";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        var nume = dataR.GetString(0);
                        var prenume = dataR.GetString(1);
                        var parola = dataR.GetString(2);
                        var username = dataR.GetString(3);
                        
                        var organizator = new Organizator(id, nume, prenume, parola, username);
                        
                        Logger.InfoFormat("exiting findOne with value {0}", organizator);
                        return organizator;
                    }
                }
            }
            Logger.InfoFormat("exiting FindOne with value {0}", null);
            return null;
        }
        
        public IEnumerable<Organizator> findAll()
        {
            Logger.InfoFormat("entering findAll");
            var con = DBUtils.getConnection(props);
            IList<Organizator> organizators = new List<Organizator>();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from Organizator";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        var id_organizator = dataR.GetInt32(0);
                        var nume = dataR.GetString(1);
                        var prenume = dataR.GetString(2);
                        var parola = dataR.GetString(3);
                        var username = dataR.GetString(4);

                        var organizator = new Organizator(id_organizator, nume, prenume, username, parola);
                        organizators.Add(organizator);
                    }
                }
            }
            Logger.InfoFormat("exiting FindAll");
            return organizators;
        }

        public void update(Organizator e)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public Organizator getById(int id)
        {
            foreach (Organizator organizator in organizators)
            {
                if (organizator.Id == id)
                    return organizator;
            }
            return null;
        }

        public int id { get; }
        public int size()
        {
            return organizators.Count();
        }

        public List<Organizator> getAll()
        {
            throw new NotImplementedException();
        }
    }
}