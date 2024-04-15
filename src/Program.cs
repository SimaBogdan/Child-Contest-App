using System;
using System.Collections.Generic;
using System.Configuration;
using src.Model;
using src.Repository;
using System.Linq;
using log4net.Config;
using log4net;
using System.Threading.Tasks;
//using System.Web.Instrumentation;
using System.Windows.Forms;
using src.Service;

namespace src
{
    internal class Program
    {
        private static string getConnectionStringByName(string name)
        {
            string returnValue = null;
            
            var settings = ConfigurationManager.ConnectionStrings[name];
            
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
        
        [STAThread]
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", getConnectionStringByName("concurscopii.db"));
            var participantDbRepository = new ParticipantDBRepository(props);
            var participantService = new ParticipantService(participantDbRepository);
            // Console.WriteLine(participantDbRepository.findOne(1));
            // Console.WriteLine(participantDbRepository.findOneByNumeSiVarsta("Sima", "Bogdan", 15));

            var probaDbRepo = new ProbaDBRepository(props);
            //Console.WriteLine(probaDbRepo.findByVarstaSiTip(6,8,"desen"));
            var probaServ = new ProbaService(probaDbRepo);

            var concursRepo = new ConcursDBRepository(props);
            var concursServ = new ConcursService(concursRepo);
            
            // Console.WriteLine("FindOne: ");
            //
            // Console.WriteLine(participantDbRepository.findOne(1));
            //
            // Console.WriteLine("FindAll: ");
            //
            // foreach (var participant in participantDbRepository.findAll())
            // {
            //     Console.WriteLine(participant);
            // }
            
            

            var organizatorDbRepository = new OrganizatorDBRepository(props);
            var orgServ = new OrganizatorService(organizatorDbRepository);
            
            var service = new Service.Service(participantService, orgServ, probaServ, concursServ);
            
            Application.Run(new LogInForm(service));

            // Console.WriteLine("FindOne: ");
            //
            // Console.WriteLine(organizatorDbRepository.findOne(1));
            // Console.WriteLine("Add: ");

            // var participantAdd = new Participant(2, "Pop", "Ana", 11, "desen");
            // participantDbRepository.add(participantAdd);
            //
            // foreach (var participant in participantDbRepository.findAll())
            // {
            //     Console.WriteLine(participant);
            // }

        }
    }
}